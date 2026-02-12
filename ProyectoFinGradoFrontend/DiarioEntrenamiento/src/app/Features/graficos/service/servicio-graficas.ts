import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { jwtDecode } from 'jwt-decode';
import { HttpClient } from '@angular/common/http';
import { SerieDatosGraficaResponse } from './SerieDatosGraficaResponse';

@Injectable({
  providedIn: 'root',
})
export class ServicioGraficas {
  http=inject(HttpClient);
  token=localStorage.getItem('JWT');
  urlbaseSesion=environment.GestionSesion.urlBase;

      obtenerDatosGrafica(){
        let uidUsuario : string | undefined="";
        if(this.token){
            const decoded = jwtDecode(this.token);
            uidUsuario=decoded.sub;
        }
    return this.http.get<Record<string,SerieDatosGraficaResponse>[]>(`${this.urlbaseSesion}/obtener1rmporejercicio?UidUsuario=${uidUsuario}`);
  }
}
