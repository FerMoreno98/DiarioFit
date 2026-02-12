import { DiaRutinaHomeDto } from "./DiaRutinaHomeDto"
import { EjercicioDiaRutinaHomeDto } from "./EjercicioDiaRutinaHomeDto"

export interface DatosHomePageResponse{
    uidRutina:string,
    nombreRutina:string,
    datosDias:DiaRutinaHomeDto[]
    
}