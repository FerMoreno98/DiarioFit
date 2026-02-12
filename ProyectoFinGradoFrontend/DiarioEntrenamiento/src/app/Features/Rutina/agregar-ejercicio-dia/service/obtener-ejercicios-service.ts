import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { EjerciciosDetalleResponse } from './EjerciciosDetallesResponse';

@Injectable({
  providedIn: 'root',
})
export class ObtenerEjerciciosService {
  http=inject(HttpClient);
  urlBase=environment.GestionRutina.urlBase;

  ObtenerEjercicios(uid:string|null){
  
    return this.http.get<EjerciciosDetalleResponse[]>(`${this.urlBase}/obtenerejerciciosdia?UidDiaRutina=${uid}`);

  }
  ObtenerUidRutina(UidDia:string|null){
    return this.http.get<string>(`${this.urlBase}/obteneruidrutina?UidDia=${UidDia}`);
  }
  EliminarEjercicioDiaRutina(UidEjercicioDiaRutina : string){
    return this.http.delete(`${this.urlBase}/eliminarejerciciodiarutina?UidEjercicioDiaRutina=${UidEjercicioDiaRutina}`);
  }
}
