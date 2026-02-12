import { EjercicioDiaRutinaHomeDto } from "./EjercicioDiaRutinaHomeDto";

export interface DiaRutinaHomeDto{
    uidDia:string,
    nombreDiaRutina:string,
    diaDeLaSemana:string,
    datosEjercicios:EjercicioDiaRutinaHomeDto[]
    rutinaHecha:boolean
}