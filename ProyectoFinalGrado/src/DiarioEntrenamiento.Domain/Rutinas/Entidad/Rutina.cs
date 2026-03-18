using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas.Errors;
using DiarioEntrenamiento.Domain.Rutinas.Events;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;

namespace DiarioEntrenamiento.Domain.Rutinas.Entidad;

public sealed class Rutina : Entity<Guid>
{
    private Rutina(Guid uid,Guid uidUsuario,string nombre, DateOnly fechaInicio, DateOnly fechaFin):base(uid)
    {
        UidUsuario = uidUsuario;
        Nombre = nombre;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
    }
    private Rutina(Guid uid,Guid uidUsuario,string nombre, DateOnly fechaInicio, DateOnly fechaFin,List<DiaRutina> dias):base(uid)
    {
        UidUsuario = uidUsuario;
        Nombre = nombre;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        _dias=dias;
    }
    public Guid UidUsuario{ get; private set; }
    public string Nombre { get; private set; }
    // ************************************************ CAMBIO PENDIENTE *********************************************************
    // Ideal: Poner el rango de fechas en un VO y validar si las fechas se solapan con otra rutina, de manera que la logica de 
    // negocio se quede en el dominio. De momento se queda como esta, donde se valida esta regla con una query
    // dejar cambio para mas adelante
    public DateOnly FechaInicio { get; private set; }
    public DateOnly FechaFin { get; private set; }
// ************************************************ CAMBIO PENDIENTE *********************************************************
    private readonly List<DiaRutina> _dias = new();
    public IReadOnlyCollection<DiaRutina> Dias => _dias;

    public Result<DiaRutina?> AgregarDia(Guid uidRutina,string nombre, string DiaDeLaSemana)
    {
        DiaRutina dia=DiaRutina.Crear(uidRutina,nombre,DiaDeLaSemana);
        if (Dias.Count() == 7)
        {
            return Result.Failure<DiaRutina>(RutinaErrors.MaximoDiasAlcanzado);
        }
        DiaRutina? esDiaRepe=Dias.FirstOrDefault(x=>x.DiaDeLaSemana==DiaDeLaSemana);
        if (esDiaRepe is not null)
        {
            return Result.Failure<DiaRutina>(RutinaErrors.DiaDuplicado);
        }
        _dias.Add(dia);
        return Result.Success<DiaRutina>(dia);
        // RaiseDomainEvents(new DiaRutinaAgregadoDomainEvent(uidRutina,dia.Id,dia.DiaDeLaSemana));
    }
    public static Rutina Crear(Guid UidUsuario,string nombre,DateOnly fechaInicio, DateOnly FechaFin)
    {
        return new Rutina(Guid.NewGuid(),UidUsuario,nombre,fechaInicio,FechaFin);
    }
    public static Rutina CrearFromDataBase(Guid Uid,Guid UidUsuario,string nombre,DateOnly fechaInicio, DateOnly FechaFin)
    {
        return new Rutina(Uid,UidUsuario,nombre,fechaInicio,FechaFin);
    }
    public static Rutina CrearFromDataBaseWithDias(Guid Uid,Guid UidUsuario,string nombre,DateOnly fechaInicio, DateOnly FechaFin, List<DiaRutina> diasRutina)
    {
        return new Rutina(Uid,UidUsuario,nombre,fechaInicio,FechaFin,diasRutina);      
    }

}