import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { jwtDecode } from 'jwt-decode';
import { DatosHomePageResponse } from './DatosHomePageResponse';
import { SerieDatosGraficaResponse } from '../../graficos/service/SerieDatosGraficaResponse';

@Injectable({
  providedIn: 'root',
})
export class ServicioHomepage {
  http=inject(HttpClient)
  urlbaseRutina=environment.GestionRutina.urlBase;

  token=localStorage.getItem('JWT');
  ObtenerDatosHomePage(){
    let uidUsuario : string | undefined="";
    if(this.token){
        const decoded = jwtDecode(this.token);
        uidUsuario=decoded.sub;
    }
    return this.http.get<DatosHomePageResponse>(`${this.urlbaseRutina}/obtenerdatoshomepage?UidUsuario=${uidUsuario}`);
  }

  ObtenerDatosGraficaGruposMusculares(){
    let uidUsuario : string | undefined="";
    if(this.token){
        const decoded = jwtDecode(this.token);
        uidUsuario=decoded.sub;
    }
    return this.http.get<Record<string,number>>(`${this.urlbaseRutina}/obtenerdatosgraficagruposmusculares?UidUsuario=${uidUsuario}`);
  }

}
