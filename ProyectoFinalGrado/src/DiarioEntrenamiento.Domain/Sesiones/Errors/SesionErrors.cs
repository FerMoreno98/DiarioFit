using DiarioEntrenamiento.Domain.Abstractions;
using DiarioEntrenamiento.Domain.Sesiones.Entidad;

namespace DiarioEntrenamiento.Domain.Sesiones.Errors;

public static class SesionErrors
{
    public static readonly Error SuenoFueraDeRango= new Error("Sesion.Sueno", "El sueño debe estar entre 1 y 10");
    public static readonly Error MotivacionFueraDeRango= new Error("Sesion.Motivacion", "La motivacion debe estar entre 1 y 10");
    public static readonly Error ERPFueraDeRango= new Error("Sesion.ERP", "El ERP debe estar entre 1 y 10");
    public static readonly Error ValoresFueraDeRango=new Error("Sesion.Error","Algun valor no esta en el rango determinado");
}