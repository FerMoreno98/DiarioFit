using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;

namespace DiarioEntrenamiento.Domain.Rutinas.Entidad;

public sealed class EjercicioDiaRutina : Entity<Guid>
{
    internal EjercicioDiaRutina(Guid uid, Guid ejercicioUid,Guid uidDia, int orden, DatosEjercicio datos) : base(uid)
    {
        EjercicioUid = ejercicioUid;
        UidDia=uidDia;
        Orden = orden;
        Datos = datos;
    }

    public Guid EjercicioUid { get; private set; }
    public Guid UidDia{get;private set;}
    public int Orden { get; private set; }
    public DatosEjercicio Datos { get; private set; }

    public void ActualizarDatos(DatosEjercicio nuevos) => Datos = nuevos;
    public static EjercicioDiaRutina Crear(Guid UidEjercicio,Guid UidDia,int orden,DatosEjercicio datos)
    {
        return new EjercicioDiaRutina(Guid.NewGuid(), UidEjercicio,UidDia,orden,datos);
    }
    public static Result<EjercicioDiaRutina?> CrearFromDataBase(Guid UidEjercicioDiaRutina,Guid UidEjercicio,Guid UidDia,int orden,int Series,string RangoReps,string RangoRIR,int TiempoDeDescanso)
    {
        Result<DatosEjercicio> datos=DatosEjercicio.Crear
        (Series,RangoReps,RangoRIR,TiempoDeDescanso);
        if (datos.IsFailure)
        {
            return Result.Failure<EjercicioDiaRutina>(datos.Error);
        }
        return new EjercicioDiaRutina(UidEjercicioDiaRutina, UidEjercicio,UidDia,orden,datos.Value);
    }

}