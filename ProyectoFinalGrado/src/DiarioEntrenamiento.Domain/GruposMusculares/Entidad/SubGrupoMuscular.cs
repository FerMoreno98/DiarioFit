using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.GruposMusculares.Entidad;

public sealed class SubGrupoMuscular:Entity<int>
{
    internal SubGrupoMuscular(int id,int idGrupoMuscular,string nombreSubgrupo):base(id)
    {
        IdGrupoMuscular=idGrupoMuscular;
        NombreSubgrupo=nombreSubgrupo;

    }
    public int IdGrupoMuscular{get;}
    public string NombreSubgrupo{get;}


}