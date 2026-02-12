using DiarioEntrenamiento.Domain.Abstractions;


namespace DiarioEntrenamiento.Domain.GruposMusculares.Entidad;

public sealed class GrupoMuscular : Entity<int>
{
    private readonly List<SubGrupoMuscular>_SubGruposMusculares=new();

    private GrupoMuscular(int IdNumerico,string nombre):base(IdNumerico)
    {
        Nombre = nombre;
    }

    public string Nombre{get; private set;}
    public IReadOnlyCollection<SubGrupoMuscular> SubGrupos => _SubGruposMusculares.AsReadOnly();

    public static GrupoMuscular CrearFromDataBase(int id,string nombre)
    {
        return new GrupoMuscular(id,nombre);
    }
    public void CargarSubGrupos(List<SubGrupoMuscular> subGrupos)
    {
        if (subGrupos is null)
            throw new ArgumentNullException(nameof(subGrupos));

        _SubGruposMusculares.Clear();
        _SubGruposMusculares.AddRange(subGrupos);
    }
    public  SubGrupoMuscular CrearSubGrupoFromDataBase(int id,int idGrupo,string Nombre)
    {
        return new SubGrupoMuscular(id,idGrupo,Nombre);
    }
}