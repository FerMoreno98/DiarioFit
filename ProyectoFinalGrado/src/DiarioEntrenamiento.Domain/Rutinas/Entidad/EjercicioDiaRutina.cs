using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;

namespace DiarioEntrenamiento.Domain.Rutinas.Entidad;

public sealed class EjercicioDiaRutina : Entity<Guid>
{
    internal EjercicioDiaRutina(Guid uid, Guid ejercicioUid, int orden, DatosEjercicio datos) : base(uid)
    {
        EjercicioUid = ejercicioUid;
        Orden = orden;
        Datos = datos;
    }

    public Guid EjercicioUid { get; private set; }
    public int Orden { get; private set; }
    public DatosEjercicio Datos { get; private set; }

    public void ActualizarDatos(DatosEjercicio nuevos) => Datos = nuevos;
    public static EjercicioDiaRutina Crear(Guid UidEjercicio,int orden,DatosEjercicio datos)
    {
        return new EjercicioDiaRutina(Guid.NewGuid(), UidEjercicio,orden,datos);
    }
    public static EjercicioDiaRutina CrearFromDataBase(Guid UidEjercicioDiaRutina,Guid UidEjercicio,int orden,DatosEjercicio datos)
    {
        return new EjercicioDiaRutina(UidEjercicioDiaRutina, UidEjercicio,orden,datos);
    }

}