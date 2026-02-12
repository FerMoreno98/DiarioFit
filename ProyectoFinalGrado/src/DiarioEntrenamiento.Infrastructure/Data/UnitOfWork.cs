using System.Data;
using DiarioEntrenamiento.Application.Abstractions.Data;


namespace DiarioEntrenamiento.Infrastructure.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private IDbConnection _connection;
    private IDbTransaction _transaction;
    private bool _completed;
    private bool TieneTransaccionActiva;

    public UnitOfWork(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IDbConnection Connection => _connection ?? throw new InvalidOperationException("UoW no iniciada, Llama a BeginAsync()");

    public IDbTransaction Transaction => _transaction;

    bool IUnitOfWork.TieneTransaccionActiva => _transaction is not null;

    public async Task BeginAsync(CancellationToken cancellationToken = default)
    {
        if (_connection is not null) return;
        _connection = await _connectionFactory.CrearConexion(cancellationToken);
        _transaction = _connection.BeginTransaction();
        _completed = false;

    }

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) throw new InvalidOperationException("No hay transaccion activa");
        _transaction.Commit();
        _completed = true;
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (!_completed && _transaction is not null)
                _transaction.Rollback();
        }
        catch { /* swallow */ }
        finally
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            _transaction = null;
            _connection = null;
        }
        await Task.CompletedTask;
    }
    

    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            _transaction.Rollback();
            _completed = true;
        }
        return Task.CompletedTask;
    }
}