import { Component } from '@angular/core';
import {  ActivatedRoute, Route, Router } from '@angular/router';
import { ObtenerEjerciciosService } from './service/obtener-ejercicios-service';
import { EjerciciosDetalleResponse } from './service/EjerciciosDetallesResponse';
import { DiaRutinaWizardService } from '../Services/dia-rutina-wizard-service';
import { ServicioObtenerDiasRutina } from '../Services/ObtenerDiasRutinaService/servicio-obtener-dia-rutina';
import { ObtenerDiaRutinaResponse } from '../Services/ObtenerDiasRutinaService/ObtenerDiaRutinaResponse';
import { DataService } from '../../../shared/data-service';

@Component({
  selector: 'app-agregar-ejercicio-dia',
  standalone:true,
  imports: [],
  templateUrl: './agregar-ejercicio-dia.html',
  styleUrl: './agregar-ejercicio-dia.css',
})
export class AgregarEjercicioDia {
  UidDiaRutina!:string|null;
  Nombre!:string |null;
  DiaDeLaSemana!:string|null;
  UidRutina!:string | null;
  DiaRutina! : ObtenerDiaRutinaResponse | undefined;
  constructor(
    private router:Router,
    private route:ActivatedRoute,
    private service:ObtenerEjerciciosService,
    private diaRutinaWizard:DiaRutinaWizardService,
    private servicioObtenerDias:ServicioObtenerDiasRutina,
    private dataService:DataService
  ){}
  datosEjercicios:EjerciciosDetalleResponse[]=[];
  ngOnInit(){
    this.UidDiaRutina=this.route.snapshot.paramMap.get('uid');
    this.Nombre=this.route.snapshot.paramMap.get('nombre');
    this.DiaDeLaSemana=this.route.snapshot.paramMap.get('dia');
    this.diaRutinaWizard.setEjercicioUid(this.UidDiaRutina);
    this.service.ObtenerEjercicios(this.UidDiaRutina).subscribe({
      next:(res)=>{
        console.log(res);
        this.datosEjercicios=res;
      },
      error:(e)=>{
        console.log(e);
      }
    })
    this.ObtenerDatosDiaRutina();
    this.dataService.clearDatosEjercicio();
  }


  ElegirEjercicio(){
    this.router.navigate(['/gruposmusculares']);
  }

  VolverALosDias(){
    this.service.ObtenerUidRutina(this.UidDiaRutina).subscribe({
      next:(res)=>{
        console.log(res);
        this.router.navigate([`/diarutina/${res}`]);
      },
      error:(e)=>{
        console.log(e);
      }
    })
  }

  ObtenerDatosDiaRutina(){
    this.service.ObtenerUidRutina(this.UidDiaRutina).subscribe({
      
      next:(res)=>{
        this.UidRutina=res;
        this.servicioObtenerDias.ObtenerDias(this.UidRutina).subscribe({
          next:(res)=>{
           this.DiaRutina= res.find(item=>item.id===this.UidDiaRutina)
          },
          error:(e)=>{
            console.log(e);
          }
        })
      },
      error:(e)=>{
        console.log(e);
      }
    })
  }

  EditarEjercicio(datos:EjerciciosDetalleResponse){
    console.log(datos);
    this.dataService.setDatosEjercicio(datos);
    this.router.navigate(['/completardatosejercicio',datos.uidEjercicios])
  }
  EliminarEjercicio(uidEjercicioDiaRutina:string){
    console.log(uidEjercicioDiaRutina)
    this.service.EliminarEjercicioDiaRutina(uidEjercicioDiaRutina).subscribe({
      next:(res)=>{
        this.datosEjercicios=this.datosEjercicios.filter(e=>e.uidEjercicioDiaRutina!==uidEjercicioDiaRutina)
      },
      error:(e)=>{
        console.log(e);
      }
    })
  }

}
