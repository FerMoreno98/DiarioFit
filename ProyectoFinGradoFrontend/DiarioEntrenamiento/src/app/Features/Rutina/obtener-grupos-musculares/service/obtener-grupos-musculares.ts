import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { GruposMuscularesResponse } from './GruposMuscularesResponse';

@Injectable({
  providedIn: 'root',
})
export class ObtenerGruposMuscularesService {
  http=inject(HttpClient);
  urlBase=environment.GestionRutina.urlBase;

  ObtenerGruposMusculares(){
    return this.http.get<GruposMuscularesResponse []>(`${this.urlBase}/obtenergruposmusculares`);
  }
}
