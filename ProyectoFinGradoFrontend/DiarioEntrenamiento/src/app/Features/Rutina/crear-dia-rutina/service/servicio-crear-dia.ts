import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { CrearDiaRutinaResponse } from './CrearDiaRutinaResponse';

@Injectable({
  providedIn: 'root',
})
export class ServicioCrearDia {
  http=inject(HttpClient);
  urlBase=environment.GestionRutina.urlBase;

  crearDiaRutina(UidRutina:string|null,NombreDia:string,DiaDeLaSemana:string){
    const body={uidRutina:UidRutina,Nombre:NombreDia,DiaDeLaSemana:DiaDeLaSemana}
    return this.http.post<CrearDiaRutinaResponse[]>(`${this.urlBase}/creardia`,body);
  }
}
