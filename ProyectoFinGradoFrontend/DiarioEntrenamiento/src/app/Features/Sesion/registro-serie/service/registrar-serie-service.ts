import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { RegistrarSerieResponse } from './RegistrarSerieResponse';

@Injectable({
  providedIn: 'root',
})
export class RegistrarSerieService {
  urlbase=environment.GestionSesion.urlBase;
  http=inject(HttpClient);

  RegistrarSerie(UidDia:string | null,Ejercicio:string | null,Peso : number| null,Repeticiones:number | null,Rir:string | null,Serie:number | null){
    const body=
    {
      UidDia:UidDia,
      Ejercicio:Ejercicio,
      Peso:Peso,
      Repeticiones:Repeticiones,
      Rir:Rir,
      Serie:Serie
    }
    return this.http.post<RegistrarSerieResponse>(`${this.urlbase}/registrarserie`,body);
  }
}
