using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.GruposMusculares.Entidad;

namespace DiarioEntrenamiento.Domain.Ejercicios.Entidad;

public sealed class Ejercicio : Entity<Guid>
{
    private Ejercicio(Guid id,string nombre) : base(id)
    {
        Nombre = nombre;
        // SubGrupoMuscular=idSubgrupo;
    }
    public string Nombre {get;private set;}
    // public int SubGrupoMuscular{get;private set;}

    public static Ejercicio Crear(
        string nombre,
        int subGrupo
    )
    {
        return new Ejercicio(Guid.NewGuid(), nombre);
    }
    public static Ejercicio CrearFromDataBase(Guid uid,string nombre)
    {
        return new Ejercicio(uid,nombre);
    }


}