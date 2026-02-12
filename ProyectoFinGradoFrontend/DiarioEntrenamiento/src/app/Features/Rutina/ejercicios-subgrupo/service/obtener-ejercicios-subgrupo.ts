import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { EjerciciosSubgrupo } from '../ejercicios-subgrupo';
import { EjerciciosSubgrupoResponse } from './EjerciciosSubgrupoResponse';

@Injectable({
  providedIn: 'root',
})
export class ObtenerEjerciciosSubgrupoService {
    http=inject(HttpClient);
    urlbase=environment.GestionRutina.urlBase;

    ObtenerEjerciciosSubGrupo(idSubgrupo:number){
      return this.http.get<EjerciciosSubgrupoResponse[]>(`${this.urlbase}/ejerciciossubgrupo?idsubgrupo=${idSubgrupo}`)
    }
}
