import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { jwtDecode, JwtPayload } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class Iniciarsesionentrenamiento {
  
  urlbase=environment.GestionSesion.urlBase;
  http=inject(HttpClient);
  uidUsuario: string|undefined;

  iniciarSesionEntrenamiento(uidDia:string|null,sueno:number|null,motivacion:number|null,eRP:number|null){
    const token=localStorage.getItem('JWT');
    
    if(token){
      const decode=jwtDecode(token)
      this.uidUsuario=decode.sub;
    }
    const body={
      UidUsuario:this.uidUsuario,
      UidDia:uidDia,
      Sueno:sueno,
      Motivacion:motivacion,
      ERP:eRP
    }
    return this.http.post(`${this.urlbase}`,body);

  }
}
