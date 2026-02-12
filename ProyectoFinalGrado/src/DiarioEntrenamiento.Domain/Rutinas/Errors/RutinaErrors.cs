using DiarioEntrenamiento.Domain.Abstractions;

namespace DiarioEntrenamiento.Domain.Rutinas.Errors;

public static class RutinaErrors
{
    public static readonly Error OrdenDuplicado = new Error("Rutina.OrdenDuplicado", "No puedes tener dos ejercicios en la misma posicion");
    public static readonly Error DiaNoEncontrado = new Error("Rutina.DiaNoEncontrado", "Estas intentando crear un ejercicio en un dia que no existe");
    public static readonly Error RutinaActualNoSeleccionada= new Error("Rutina.RutinaActualNoEncontrada","Debes seleccionar una rutina como actual");
    public static readonly Error DiaDuplicado=new Error("Rutina.DiaDuplicado","No puedes poner dos entrenamientos el mismo dia");
    public static readonly Error MaximoDiasAlcanzado=new Error("Rutina.MaximoDiasAlcanzado","No puedes entrenar mas de 7 dias por semana");
    public static readonly Error OrdenRepetido=new Error("Rutina.OrdenRepetido","No puedes tener dos ejercicios con el mismo orden");
    public static readonly Error FechasDuplicadas=new Error("Rutina.FechasDuplicadas","No puedes situar dos mesociclos en la misma Fecha");
    public static readonly Error EjercicioRepetido=new Error("Rutina.EjercicioRepetido","No puedes poner dos ejercicios con los mismos objetivos el mismo dia");
    public static readonly Error FormatoInvalidoReps=new Error("Rutina.FormatoInvalidoReps","El rango de Repeticiones  debe ser 'n' o 'n-n' (ej: 2 o 1-3).");
    public static readonly Error FormatoInvalidoRir=new Error("Rutina.FormatoInvalidoRir","El rango de RIR debe ser 'n' o 'n-n' (ej: 2 o 1-3).");

}