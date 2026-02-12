import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ServicioCrear {
  http=inject(HttpClient)
  urlbase=environment.GestionUsuarios.urlBase;

  CrearUsuario(nombre:string,Apellidos:string,FechaNacimiento:Date,Email:string,Contrasena:string){
    const body={Nombre:nombre,Apellidos:Apellidos,FechaNacimiento:FechaNacimiento,Email:Email,Contrasena:Contrasena}
    return this.http.post<string>(this.urlbase,body)
  }
}
