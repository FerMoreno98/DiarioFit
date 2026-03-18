import { Component } from '@angular/core';
import { DiaRutinaWizardService } from '../Services/dia-rutina-wizard-service';
import { CompletarDatosEjercicioService } from './service/completar-datos-ejercicio-service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { DataService } from '../../../shared/data-service';
import { EjerciciosDetalleResponse } from '../agregar-ejercicio-dia/service/EjerciciosDetallesResponse';

@Component({
  selector: 'app-completar-datos-ejercicio',
  imports: [FormsModule],
  templateUrl: './completar-datos-ejercicio.html',
  styleUrl: './completar-datos-ejercicio.css',
})
export class CompletarDatosEjercicio {

  constructor(
    private UidDiaRutinaService:DiaRutinaWizardService,
    private servicio:CompletarDatosEjercicioService,
    private route:ActivatedRoute,
    private router:Router,
    private dataService:DataService
  ){}
  uidDia!:string|null;
  UidEjercicio!:string|null;
  DatosEjercicio:EjerciciosDetalleResponse|null=null;
  ngOnInit(){
     this.uidDia=this.UidDiaRutinaService.getEjercicioUid();
     this.UidEjercicio=this.route.snapshot.paramMap.get('uidejercicio');
    this.dataService.datos$.subscribe(data=>{
      this.DatosEjercicio=data;
    })
    if(this.DatosEjercicio){
      this.Series=Number(this.DatosEjercicio.series)
      this.RangoReps=this.DatosEjercicio.objetivoReps
      this.RangoRIR=this.DatosEjercicio.objetivoRIR
      this.TiempoDeDescanso=this.DatosEjercicio.tiempoDescanso
      this.Orden=this.DatosEjercicio.orden;
    }
    console.log(this.DatosEjercicio);

  }
  MensajeErrorOrdenRepetido: string=""
  MensajeErrorSeries :string=""
  MensajeErrorRangoReps :string=""
  MensajeErrorRangoRir :string=""
  MensajeErrorDescanso :string=""
  MensajeErrorEjercicioRepetido : string="";
  Orden!:number;
  Series!:number;
  RangoReps:string="";
  RangoRIR:string="";
  TiempoDeDescanso!:number;
AgregarEjercicio(form: NgForm) {
  const esEditar = !!this.DatosEjercicio;

  const request$ = esEditar
    ? this.servicio.ModificarDatosEjercicio(
        this.DatosEjercicio!.uidEjercicioDiaRutina,
        this.uidDia,
        this.UidEjercicio,
        this.Orden,
        this.Series,
        this.RangoReps,
        this.RangoRIR,
        this.TiempoDeDescanso
      )
    : this.servicio.CompletarDatosEjercicio(
        this.uidDia,
        this.UidEjercicio,
        this.Orden,
        this.Series,
        this.RangoReps,
        this.RangoRIR,
        this.TiempoDeDescanso
      );

  request$.subscribe({
    next: () => {
      this.limpiarMensajesError();
      this.dataService.clearDatosEjercicio();
      this.DatosEjercicio = null;
      form.resetForm({
        Series: null,
        RangoReps: '',
        RangoRIR: '',
        TiempoDeDescanso: null,
        orden: null,
      });

      this.router.navigate([`/agregarejercicio/${this.uidDia}`]);
    },
    error: (e) => this.mapearErrores(e)
  });
}

private limpiarMensajesError(): void {
  this.MensajeErrorSeries = '';
  this.MensajeErrorRangoReps = '';
  this.MensajeErrorRangoRir = '';
  this.MensajeErrorOrdenRepetido = '';
  this.MensajeErrorDescanso = '';
  this.MensajeErrorEjercicioRepetido = '';
}

private mapearErrores(e: any): void {
  this.limpiarMensajesError();
console.log(e)
  if (!e || !e.error) return;
  console.log("aqui si")

  // 1️⃣ Errores múltiples (array)
  if (Array.isArray(e.error)) {
    for (const err of e.error) {
      const code = err.code?.toLowerCase() ?? '';

      if (code.includes('series')) {
        this.MensajeErrorSeries = err.name;
      } 
      else if (code.includes('rangoreps')) {
        this.MensajeErrorRangoReps = err.name;
      } 
      else if (code.includes('rangorir')) {
        this.MensajeErrorRangoRir = err.name;
      } 
      else if (code.includes('tiempodedescanso')) {
        this.MensajeErrorDescanso = err.name;
      } 
      else if (code.includes('orden')) {
        this.MensajeErrorOrdenRepetido = err.name;
      }
    }
    return;
  }
console.log(e.error.code)
  // 2️⃣ Error único
  switch (e.error.code) {
    case 'Rutina.OrdenDuplicado':
      this.MensajeErrorOrdenRepetido = e.error.name;
      break;

    case 'Rutina.EjercicioRepetido':
      this.MensajeErrorEjercicioRepetido = e.error.name;
      break;
    case 'Rutina.FormatoInvalidoReps':
      this.MensajeErrorRangoReps=e.error.name;
      break;
    case 'Rutina.FormatoInvalidoRir':
      this.MensajeErrorRangoRir=e.error.name;
      break;
  }
}


}
