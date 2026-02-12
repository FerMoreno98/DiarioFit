import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { SubGruposResponse } from './SubGruposResponse';

@Injectable({
  providedIn: 'root',
})
export class ObtenerSubgruposService {
  http=inject(HttpClient);
  urlbase=environment.GestionRutina.urlBase;

  ObtenerSubGrupos(idGrupo: number){
    return this.http.get<SubGruposResponse[]>(`${this.urlbase}/subgruposmusculares?idGrupo=${idGrupo}`);

  }
  
}
