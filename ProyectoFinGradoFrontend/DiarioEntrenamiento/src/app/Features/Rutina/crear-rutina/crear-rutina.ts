import { Component } from '@angular/core';
import { ServicioCrearRutina } from './service/servicio-crear-rutina';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-crear-rutina',
  imports: [FormsModule],
  templateUrl: './crear-rutina.html',
  styleUrl: './crear-rutina.css',
})
export class CrearRutina {
  constructor(private servicio:ServicioCrearRutina, private router:Router,private route:ActivatedRoute){}
  UidRutina:string|null=null;
  Nombre:string|null="";
  fechaInicio:Date |string| null=new Date;
  fechaFin:Date |string| null =new Date;
  errorFecha="";
  errorNombre="";
  TipoError="";
  modo:string="crear";
  ngOnInit(){
    this.Nombre=this.route.snapshot.paramMap.get('nombrerutina');
    this.UidRutina=this.route.snapshot.paramMap.get('uidrutina');
    const fechaIniciostr=this.route.snapshot.paramMap.get('fechainicio')
    const fechaFinstr=this.route.snapshot.paramMap.get('fechafin');
    if(this.UidRutina && this.Nombre){
      if(!fechaIniciostr && !fechaFinstr){
        this.modo="duplicar"
      }else{
        this.fechaInicio=fechaIniciostr ? new Date(fechaIniciostr).toISOString().slice(0, 10):null;
        this.fechaFin=fechaFinstr ? new Date(fechaFinstr).toISOString().slice(0, 10):null;
        console.log(this.fechaInicio);
        console.log(this.fechaFin);
        this.modo="editar"
      }
    }
  }
  CrearRutina(){
    
    if(this.modo==="duplicar"){
      
      this.servicio.duplicarRutina(this.UidRutina,this.Nombre,this.fechaInicio,this.fechaFin).subscribe({
        next:(res)=>{
          this.router.navigate(['/editarmesociclo'])
        },
        error:(e)=>{
          this.manejarErrores(e);
      }
      })
    }else if(this.modo==="crear"){

    this.servicio.CrearRutina(this.Nombre,this.fechaInicio,this.fechaFin).subscribe({
      next:(res)=>{
        this.router.navigate(['/diarutina',res]);
      },
      error:(e)=>{
        this.manejarErrores(e);
      }
    })
  }else if(this.modo==="editar"){
    this.servicio.editarRutina(this.UidRutina,this.Nombre,this.fechaInicio,this.fechaFin).subscribe({
      next:(res)=>{
        this.router.navigate(['/editarmesociclo'])
      },
      error:(e)=>{
        this.manejarErrores(e);
      }
    })
  }
  }
  manejarErrores(e:any){
        this.errorFecha="";
        this.errorNombre="";
        if(!Array.isArray(e.error)){
        this.TipoError=e.error.code.split('.')[1];
   
        if(e.status==400){
          if(this.TipoError=='FechasDuplicadas'){
            this.errorFecha=e.error.name;
          }
        }
      }
        for(let i=0;i<e.error.length; i++){
        if(e.error[i].code.toLowerCase().includes("fecha")){
          this.errorFecha=e.error[i].name;
        }else if(e.error[i].code.toLowerCase().includes("nombre")){
          this.errorNombre=e.error[i].name;
        }
      }
  }
}
