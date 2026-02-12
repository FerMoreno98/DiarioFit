using DiarioEntrenamiento.Domain.Abstractions;
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

    public Result<EjercicioDiaRutina> AgregarEjercicio(Guid IdEjercicio,int orden,DatosEjercicio datos)
    {
        if (_ejercicios.Any(e => e.Orden == orden))
            return Result.Failure<EjercicioDiaRutina>(RutinaErrors.OrdenDuplicado);
        EjercicioDiaRutina ejerciciodia=new EjercicioDiaRutina(Guid.NewGuid(), IdEjercicio, orden, datos);
        _ejercicios.Add(ejerciciodia);
        return Result.Success<EjercicioDiaRutina>(ejerciciodia);
    }
        public Result<EjercicioDiaRutina> AgregarEjercicioFromDataBase(EjercicioDiaRutina ejercicio)
    {
        // if (_ejercicios.Any(e => e.Orden == orden))
        //     return Result.Failure<EjercicioDiaRutina>(RutinaErrors.OrdenDuplicado);
        // EjercicioDiaRutina ejerciciodia=new EjercicioDiaRutina(Guid.NewGuid(), IdEjercicio, orden, datos);
        _ejercicios.Add(ejercicio);
        return Result.Success<EjercicioDiaRutina>(ejercicio);
    }




    
}