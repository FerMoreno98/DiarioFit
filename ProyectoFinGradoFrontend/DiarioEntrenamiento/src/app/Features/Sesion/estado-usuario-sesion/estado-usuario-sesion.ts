import { Component } from '@angular/core';
import { Iniciarsesionentrenamiento } from './service/iniciarsesionentrenamiento';
import { ActivatedRoute, Router } from '@angular/router';
import { jwtDecode, JwtPayload } from 'jwt-decode';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-estado-usuario-sesion',
  imports: [FormsModule],
  templateUrl: './estado-usuario-sesion.html',
  styleUrl: './estado-usuario-sesion.css',
})
export class EstadoUsuarioSesion {
  constructor(private servicioIniciar:Iniciarsesionentrenamiento,private route:ActivatedRoute,private router:Router){}
  MensajeErrorSueno :string =""
  MensajeErrorERP : string=""
  MensajeErrorMotivacion : string=""
  uidRutina:string|null=null;
  uidDia:string|null=null;
  Sueno!:number | null;
  Motivacion!:number | null;
  ERP!:number |null;

  IniciarEntrenamiento(){

    this.uidDia=this.route.snapshot.paramMap.get('uiddia');
    this.servicioIniciar.iniciarSesionEntrenamiento(this.uidDia,this.Sueno,this.Motivacion,this.ERP).subscribe({
      next:(res)=>{
        this.router.navigate([`/sesionentrenamiento/${this.uidDia}`])
      },
      error:(e)=>{
       this.mapearErrores(e)
      }
    })
    }
    OmitirDatosEstadoEIniciarEntrenamiento(){
      this.uidDia=this.route.snapshot.paramMap.get('uiddia');
      this.servicioIniciar.iniciarSesionEntrenamiento(this.uidDia,null,null,null).subscribe({
        next:(res)=>{
          this.router.navigate([`/sesionentrenamiento/${this.uidDia}`])
        },
        error:(e)=>{
          console.log(e)
        }
      })
    }

  private mapearErrores(e: any): void {
  this.limpiarMensajesError();

  if (!e || !e.error) return;

  if (Array.isArray(e.error)) {
    for (const err of e.error) {
      const code = err.code?.toLowerCase() ?? '';

      if (code.includes('sueno')) {
        this.MensajeErrorSueno = err.name;
      } 
      else if (code.includes('motivacion')) {
        this.MensajeErrorMotivacion = err.name;
      } 
      else if (code.includes('erp')) {
        this.MensajeErrorERP = err.name;
      } 
    }
    return;
  }
    // 2️⃣ Error único
  switch (e.error.code) {
    case 'Sesion.Sueno':
      this.MensajeErrorSueno = e.error.name;
      break;

    case 'Sesion.Motivacion':
      this.MensajeErrorMotivacion = e.error.name;
      break;
    case 'Sesion.ERP':
      this.MensajeErrorERP = e.error.name;
      break;
  }
}
private limpiarMensajesError(): void {
  this.MensajeErrorSueno = '';
  this.MensajeErrorMotivacion = '';
  this.MensajeErrorERP = '';
}
}
