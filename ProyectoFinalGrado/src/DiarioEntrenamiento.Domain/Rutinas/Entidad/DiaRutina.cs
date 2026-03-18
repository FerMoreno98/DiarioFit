using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Ejercicios.Entidad;
using DiarioEntrenamiento.Domain.Rutinas.Errors;
using DiarioEntrenamiento.Domain.Rutinas.ValueObjects;

namespace DiarioEntrenamiento.Domain.Rutinas.Entidad;

public sealed class DiaRutina : Entity<Guid>
{
    private DiaRutina(Guid id,Guid uid_rutina, string nombre, string diaDeLaSemana):base(id)
    {
        Uid_rutina = uid_rutina;
        Nombre = nombre;
        DiaDeLaSemana = diaDeLaSemana;
        
    }
    private DiaRutina(Guid id,Guid uid_rutina, string nombre, string diaDeLaSemana,List<EjercicioDiaRutina> ejercicios):base(id)
    {
        Uid_rutina = uid_rutina;
        Nombre = nombre;
        DiaDeLaSemana = diaDeLaSemana;
        _ejercicios=ejercicios;
        
    }

    public Guid Uid_rutina { get; private set; }
    public string Nombre { get; private set; }
    public string DiaDeLaSemana { get; private set; }
    private readonly List<EjercicioDiaRutina> _ejercicios = new();
    //uso IReadOnlyCollection y pongo la list privada para que desde fuera de la entidad solo puedan leer la lista y desde dentro manejarla
    public IReadOnlyCollection<EjercicioDiaRutina> EjerciciosDiaRutinas => _ejercicios;
    public static DiaRutina Crear(Guid idRutina,string nombre,string DiaDeLaSemana)
    {
        DiaRutina dia=new DiaRutina(Guid.NewGuid(),idRutina,nombre,DiaDeLaSemana);
        return dia;
    }
    public static DiaRutina CargarDia(Guid id,Guid UidRutina,string nombre,string DiaDeLaSenaba)
    {

          DiaRutina dia=new DiaRutina(id,UidRutina,nombre,DiaDeLaSenaba);
        return dia;
    }

    public Result AgregarEjercicio(EjercicioDiaRutina ejercicio)
    {
        if (_ejercicios.Any(e => e.Orden == ejercicio.Orden))
            return Result.Failure(RutinaErrors.OrdenDuplicado);
        if(_ejercicios.Any(e=>
        e.Datos.RangoRepsObjetivo==ejercicio.Datos.RangoRepsObjetivo && 
        e.Datos.RangoRIR==ejercicio.Datos.RangoRIR &&
        e.EjercicioUid==ejercicio.EjercicioUid))
        {
            return Result.Failure(RutinaErrors.EjercicioRepetido);
        }    
        _ejercicios.Add(ejercicio);
        return Result.Success();
    }

    public static Result<DiaRutina?> CargarDiaRutinaWithEjercicio(Guid id,Guid UidRutina,string nombre,string DiaDeLaSenaba, List<EjercicioDiaRutina> ejercicios)
    {
        return new DiaRutina(id,UidRutina,nombre,DiaDeLaSenaba,ejercicios);
    }
    public static Result<DiaRutina?> CrearDiaRutinaWithEjercicio(Guid UidRutina,string nombre,string DiaDeLaSenaba, List<EjercicioDiaRutina> ejercicios)
    {
        return new DiaRutina(Guid.NewGuid(),UidRutina,nombre,DiaDeLaSenaba,ejercicios);
    }
    public static Result<DiaRutina?> DuplicarDiaRutinaWithEjercicio(Guid UidRutina,string nombre,string DiaDeLaSenaba, List<EjercicioDiaRutina> ejercicios)
    {

        DiaRutina dia=DiaRutina.Crear(UidRutina,nombre,DiaDeLaSenaba);
        List<EjercicioDiaRutina> ejerciciosDiaRutina=new List<EjercicioDiaRutina>();
        foreach(var ejercicioDia in ejercicios)
        {
            EjercicioDiaRutina ejercicio=EjercicioDiaRutina.Crear(ejercicioDia.EjercicioUid,dia.Id,ejercicioDia.Orden,ejercicioDia.Datos);
            ejerciciosDiaRutina.Add(ejercicio);

        }
        return  new DiaRutina(dia.Id,UidRutina,nombre,DiaDeLaSenaba,ejerciciosDiaRutina);
        
    }


    
}