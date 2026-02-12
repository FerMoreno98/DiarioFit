using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Sesiones.ValueObjects;

namespace DiarioEntrenamiento.Domain.Sesiones.Entidad;

public sealed class Sesion : Entity<Guid>
{
    private Sesion(Guid uid,Guid uidUsuario,Guid uidRutina,Guid uidDia,DateTime fechaSesion,EstadoUsuario estado):base(uid)
    {
        Uid=uid;
        UidUsuario=uidUsuario;
        UidRutina=uidRutina;
        UidDia=uidDia;
        FechaSesion=fechaSesion;
        Estado=estado;
    }

    public Guid Uid{get; private set;}
    public Guid UidUsuario{get;private set;}
    public Guid UidRutina{get; private set;}
    public Guid UidDia{get; private set;}
    public DateTime FechaSesion{get;private set;}
    public EstadoUsuario Estado{get;private set;}
    private readonly List<SerieRealizada>_series=new();
    public IReadOnlyCollection<SerieRealizada> series=>_series;

    public static Sesion Crear(Guid uidUsuario,Guid uidRutina,Guid uidDia,DateTime fechaSesion,EstadoUsuario estado)
    {
       return new Sesion(Guid.NewGuid(),uidUsuario,uidRutina,uidDia,fechaSesion,estado);
    }

}