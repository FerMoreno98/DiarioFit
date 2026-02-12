import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CompletarDatosEjercicioService {
http=inject(HttpClient)
urlbase=environment.GestionRutina.urlBase;

CompletarDatosEjercicio
(
  uidDiaRutina:string|null,
  UidEjercicio:string|null,
  orden:number,
  series:number,
  RangoReps:string,
  RangoRIR:string,
  tiempoDescanso:number
)
{
  const body=
  {
    UidEjercicio:UidEjercicio,
    UidDiaRutina:uidDiaRutina,
    orden:orden,
    Series:series,
    RangoReps:RangoReps,
    RangoRIR:RangoRIR,
    TiempoDeDescanso:tiempoDescanso
  }
return this.http.post(`${this.urlbase}/completardatosejerciciodia`,body);
}
ModificarDatosEjercicio
(
  UidEjercicioDiaRutina:string | null,
  uidDiaRutina:string|null,
  UidEjercicio:string|null,
  orden:number,
  series:number,
  RangoReps:string,
  RangoRIR:string,
  tiempoDescanso:number
)
{
  const body=
  {
    UidEjercicioDiaRutina:UidEjercicioDiaRutina,
    UidEjercicio:UidEjercicio,
    UidDiaRutina:uidDiaRutina,
    orden:orden,
    Series:series,
    RangoReps:RangoReps,
    RangoRIR:RangoRIR,
    TiempoDeDescanso:tiempoDescanso
  }
  return this.http.put(`${this.urlbase}/modificardatosejerciciodiarutina`,body);
}
  
  
}
