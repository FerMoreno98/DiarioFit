import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class ServicioCrearRutina {
  http=inject(HttpClient)
  urlbase=environment.GestionRutina.urlBase;
  token=localStorage.getItem('JWT');

  CrearRutina(nombre:string|null,fechaIncio:Date|string|null,fechaFin:Date|string|null){
    let uidUsuario : string | undefined="";
    if(this.token){
        const decoded = jwtDecode(this.token);
        uidUsuario=decoded.sub;
    }
    const body={Uid:uidUsuario,Nombre:nombre,FechaInicio:fechaIncio,FechaFin:fechaFin}
    return this.http.post<string>(this.urlbase,body);
  }
  duplicarRutina(uidRutina:string| null,nombre:string|null,fechaIncio:Date|string|null,fechaFin:Date|string|null){
    let uidUsuario : string | undefined="";
    if(this.token){
        const decoded = jwtDecode(this.token);
        uidUsuario=decoded.sub;
    }
    const body={UidUsuario:uidUsuario,UidRutina:uidRutina,Nombre:nombre,FechaInicio:fechaIncio,FechaFin:fechaFin}
    return this.http.post(`${this.urlbase}/duplicarrutina`,body);
  }
  editarRutina(uidRutina:string| null,nombre:string|null,fechaIncio:Date|string|null,fechaFin:Date|string|null){
        let uidUsuario : string | undefined="";
    if(this.token){
        const decoded = jwtDecode(this.token);
        uidUsuario=decoded.sub;
    }
    const body={UidUsuario:uidUsuario,UidRutina:uidRutina,Nombre:nombre,FechaInicio:fechaIncio,FechaFin:fechaFin}
    return this.http.put(`${this.urlbase}/modificarrutina`,body);
  }
}
