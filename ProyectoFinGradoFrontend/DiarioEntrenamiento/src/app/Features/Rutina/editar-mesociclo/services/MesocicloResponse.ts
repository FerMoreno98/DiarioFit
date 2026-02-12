export interface MesociclosResponse{
    uid: string
    uidUsuario:string
    nombre:string,
    fechaInicio:string
    fechaFin:string
    diasEntrenamiento : DiasEntrenamiento []

}

export interface DiasEntrenamiento{
    nombre:string
    diaDeLaSemana:string
}