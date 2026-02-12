import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { ObtenerDiaRutinaResponse } from './ObtenerDiaRutinaResponse';


@Injectable({
  providedIn: 'root',
})
export class ServicioObtenerDiasRutina {
  http=inject(HttpClient);
  urlBase=environment.GestionRutina.urlBase;

  ObtenerDias(uid:string|null){
    
    return this.http.get<ObtenerDiaRutinaResponse[]>(`${this.urlBase}/obtenerdias?uid=${uid}`);
  }
  
}
