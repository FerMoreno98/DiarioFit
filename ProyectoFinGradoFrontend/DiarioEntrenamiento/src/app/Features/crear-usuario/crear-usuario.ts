import { Component } from '@angular/core';
import { ServicioCrear } from './service/servicio-crear';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-crear-usuario',
  imports: [FormsModule],
  templateUrl: './crear-usuario.html',
  styleUrl: './crear-usuario.css',
})
export class CrearUsuario {

  constructor(private servicio:ServicioCrear,private router:Router){}
  Nombre:string="";
  Apellidos:string="";
  FechaNacimiento:Date=new Date;
  Email:string="";
  Contrasena:string="";
  MensajeErrorEmail:string | null ="";
  MensajeErrorContrasena:string | null ="";
  MensajeErrorFecha:string | null ="";
  MensajeErrorNombre:string | null="";
  TipoError:string="";

  registrar(){
    this.servicio.CrearUsuario(this.Nombre,this.Apellidos,this.FechaNacimiento,this.Email,this.Contrasena).subscribe({
      next:(res)=>{
        if(res !==null){
          this.router.navigate(['/auth/login'])
        }
      },
      error:(e)=>{
        this.MensajeErrorEmail=""
        this.MensajeErrorContrasena=""
        this.MensajeErrorFecha=""
        if(!Array.isArray(e.error)){
        this.TipoError=e.error.code.split('.')[0];
   
        if(e.status==400){
          if(this.TipoError=='Email'){
            this.MensajeErrorEmail=e.error.name;
          }else if( this.TipoError=='Contrasena'){
            this.MensajeErrorContrasena=e.error.name
          }
        }
      }else{
        for(let i=0;i<e.error.length;i++){
          console.log(e.error[i])
          if(e.error[i].code.toLowerCase().includes("email")){
            this.MensajeErrorEmail=e.error[i].name;
          }else if(e.error[i].code.toLowerCase().includes("contrasena")){
            this.MensajeErrorContrasena=e.error[i].name;
        }else if(e.error[i].code.toLowerCase().includes("fechanacimiento")){
            this.MensajeErrorFecha=e.error[i].name;
        }else if(e.error[i].code.toLowerCase().includes("nombre")){
            this.MensajeErrorNombre=e.error[i].name;
        }
      }
      }

        
      }
    })
  }

}
