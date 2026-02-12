import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import { MesociclosResponse } from './MesocicloResponse';

@Injectable({
  providedIn: 'root',
})
export class ObtenerDatosMesociclos {
  urlbase=environment.GestionRutina.urlBase;
  http=inject(HttpClient);
  token=localStorage.getItem("JWT");

  ObtenerMesociclos(){
    let UidUsuario : string | undefined;
    if(this.token){
      const decoded=jwtDecode<JwtPayload>(this.token);
      UidUsuario=decoded.sub
    }
   return this.http.get<MesociclosResponse[]>(`${this.urlbase}/obtenerdatoseditarmesocilo?UidUsuario=${UidUsuario}`);
  }
  EliminarMesociclo(UidRutina:string|null){
    return this.http.delete(`${this.urlbase}/eliminarrutina?UidRutina=${UidRutina}`)
  }
}
