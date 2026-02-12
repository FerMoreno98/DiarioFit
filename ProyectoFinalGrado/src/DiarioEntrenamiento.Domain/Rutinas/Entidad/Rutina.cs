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
    public Guid UidUsuario{ get; private set; }
    public string Nombre { get; private set; }
    public DateOnly FechaInicio { get; private set; }
    public DateOnly FechaFin { get; private set; }

    private readonly List<DiaRutina> _dias = new();
    public IReadOnlyCollection<DiaRutina> Dias => _dias;

    public void AgregarDia(Guid uidRutina,string nombre, string DiaDeLaSemana)
    {
        DiaRutina dia=DiaRutina.Crear(uidRutina,nombre,DiaDeLaSemana);
        _dias.Add(dia);
        RaiseDomainEvents(new DiaRutinaAgregadoDomainEvent(uidRutina,dia.Id,dia.DiaDeLaSemana));
    }
    // public void AgregarDiaFromDataBase(DiaRutina dia)
    // {
    //     _dias.Add(dia);
    // }
    public Result<EjercicioDiaRutina> AgregarEjercicio(DiaRutina dia,Guid UidEjercicioDiaRutina, Guid IdEjercicio, int orden, DatosEjercicio datos)
    {
        // var dia = _dias.SingleOrDefault(d => d.Id == idDia);
        // if (dia == null) return Result.Failure<EjercicioDiaRutina>(RutinaErrors.DiaNoEncontrado);
        // RaiseDomainEvents(new EjercicioAgregadoDomainEvent(this.Id, dia.Id, IdEjercicio, orden));
        // var diaRutina= DiaRutina.CargarDia(dia.Id,dia.Uid_rutina,dia.Nombre,dia.DiaDeLaSemana);
        // _dias.Add(dia);
        EjercicioDiaRutina ejercicio=new EjercicioDiaRutina(UidEjercicioDiaRutina,IdEjercicio,orden,datos);
        return dia.AgregarEjercicioFromDataBase(ejercicio);
    }
    public static Rutina Crear(Guid UidUsuario,string nombre,DateOnly fechaInicio, DateOnly FechaFin)
    {
        return new Rutina(Guid.NewGuid(),UidUsuario,nombre,fechaInicio,FechaFin);
    }
    public static Rutina CrearFromDataBase(Guid Uid,Guid UidUsuario,string nombre,DateOnly fechaInicio, DateOnly FechaFin)
    {
        return new Rutina(Uid,UidUsuario,nombre,fechaInicio,FechaFin);
    }
    public EjercicioDiaRutina CrearNuevoEjercicio(Guid UidEjercicio,int orden,DatosEjercicio datos)
    {
        return EjercicioDiaRutina.Crear(UidEjercicio,orden,datos);
    }

}