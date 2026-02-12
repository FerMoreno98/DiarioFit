import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import { LoginResponse } from './LoginResponse';

@Injectable({
  providedIn: 'root',
})
export class Service {
  http=inject(HttpClient);
  urlBase=environment.GestionUsuarios.urlBase;

  VerificarUsuario(email:string,contrasena:string){
    
    const body = { Email: email, Contrasena: contrasena };
    return this.http.post<LoginResponse>(`${this.urlBase}/login`,body)
  }
}
