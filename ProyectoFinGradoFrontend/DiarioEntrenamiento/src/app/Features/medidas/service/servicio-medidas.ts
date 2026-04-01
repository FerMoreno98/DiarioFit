import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { PerimetrosRequest } from '../../../shared/Models/PerimetrosRequest';
import { jwtDecode } from 'jwt-decode';
import { PlieguesRequest } from '../../../shared/Models/PlieguesRequest';

@Injectable({
  providedIn: 'root',
})
export class ServicioMedidas {
  http=inject(HttpClient);
  urlbase=environment.GestionMedidas.urlBase;
  token=localStorage.getItem("JWT");

  insertarPliegues(data:PlieguesRequest){
        let uidUsuario : string | undefined="";
        if(this.token){
            const decoded = jwtDecode(this.token);
            uidUsuario=decoded.sub;
        }
        const body={
          uidUsuario: uidUsuario,
          abdominal: data.abdominal,
          suprailiaco:data.suprailiaco,
          tricipital: data.tricipital,
          subescapular: data.subescapular,
          muslo: data.muslo,
          pantorrilla: data.pantorrilla
        }
        return this.http.post(`${this.urlbase}/registrarpliegues`,body);
  }
  insertarPerimetros(data:PerimetrosRequest){
        let uidUsuario : string | undefined="";
        if(this.token){
            const decoded = jwtDecode(this.token);
            uidUsuario=decoded.sub;
        }
      const body = {
        uidUsuario: uidUsuario,

        cuello: data.cuello ?? null,
        brazoDchoRelajado: data.brazoDchoRelajado ?? null,
        brazoDchoTension: data.brazoDchoTension ?? null,
        brazoIzqRelajado: data.brazoIzqRelajado ?? null,
        brazoIzqTension: data.brazoIzqTension ?? null,

        pecho: data.pecho ?? null,
        hombro: data.hombro ?? null,
        cintura: data.cintura ?? null,
        cadera: data.cadera ?? null,
        abdomen: data.abdomen ?? null,

        musloDcho: data.musloDcho ?? null,
        musloIzq: data.musloIzq ?? null,
        pantorrillaDcha: data.pantorrillaDcha ?? null,
        pantorrillaIzq: data.pantorrillaIzq ?? null
      };

    return this.http.post(`${this.urlbase}/registrarperimetros`,body);
  }
  
}
