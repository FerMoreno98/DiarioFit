import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RegistrarSerieService } from './service/registrar-serie-service';
import { RegistrarSerieResponse } from './service/RegistrarSerieResponse';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-registro-serie',
  imports: [FormsModule],
  templateUrl: './registro-serie.html',
  styleUrl: './registro-serie.css',
})
export class RegistroSerie {
  constructor(private route:ActivatedRoute, private service: RegistrarSerieService,private router:Router){}
  MensajeErrorPeso:string="";
  MensajeErrorRir:string="";
  MensajeErrorReps:string="";
  serie : string | null="0";
  ejercicio:string | null="";
  UidDia: string | null="";
  Peso!: number;
  Repeticiones!:number;
  Rir : string="";
  result! :RegistrarSerieResponse;

  isSaving = false;
saveOk = false;
saveError = false;

  ngOnInit(){
    this.serie=this.route.snapshot.paramMap.get('serie')
    this.ejercicio=this.route.snapshot.paramMap.get('ejercicio')
    this.UidDia=this.route.snapshot.paramMap.get('uiddia');

  }

  RegistroSerie(){
    this.isSaving = true;
    this.saveOk = false;
    this.saveError = false;
    this.service.RegistrarSerie(this.UidDia,this.ejercicio,this.Peso,this.Repeticiones,this.Rir,Number(this.serie)).subscribe({
      next:(res)=>{
        this.result=res
        this.saveOk = true;         
        this.isSaving = false;
        this.resetearCamposSerie();
        setTimeout(() => {
        if(this.result.ejercicioTerminado){
          this.router.navigate([`/sesionentrenamiento/${this.UidDia}`]);
          
        }else{
          const serieNumero=Number(this.serie)+1;
          this.serie=serieNumero.toString();
          this.router.navigate([`/registrarserie/${this.serie}/${this.ejercicio}/${this.UidDia}`])
        }
      },200);
      },
      error:(e)=>{
        this.mapearErrores(e);
        this.isSaving = false;
        this.saveError = true;       // dispara "shake"
        setTimeout(() => (this.saveError = false), 500);
      }
    })
  }
  omitirSerie(){
    this.isSaving = true;
    this.saveOk = false;
    this.saveError = false;
    this.limpiarMensajesError();
    this.service.RegistrarSerie(this.UidDia,this.ejercicio,null,null,null,Number(this.serie)).subscribe({
      next:(res)=>{
        this.result=res
        this.saveOk = true;
        this.isSaving = false;
        this.resetearCamposSerie();
        setTimeout(() => {
        if(this.result.ejercicioTerminado){
          this.router.navigate([`/sesionentrenamiento/${this.UidDia}`]);
          
        }else{
          const serieNumero=Number(this.serie)+1;
          this.serie=serieNumero.toString();
          this.router.navigate([`/registrarserie/${this.serie}/${this.ejercicio}/${this.UidDia}`])
        }
      },200);
      },
      error:(e)=>{
        this.isSaving = false;
        this.saveError = true;
        setTimeout(() => (this.saveError = false), 500);
        console.log(e);
      }
    })
  }

  private mapearErrores(e: any): void {
  this.limpiarMensajesError();

  if (!e || !e.error) return;

  // 1️⃣ Errores múltiples (array)
  if (Array.isArray(e.error)) {
    for (const err of e.error) {
      const code = err.code?.toLowerCase() ?? '';

      if (code.includes('peso')) {
        this.MensajeErrorPeso = err.name;
      } 
      else if (code.includes('repeticiones')) {
        this.MensajeErrorReps = err.name;
      } 
      else if (code.includes('rir')) {
        this.MensajeErrorRir = err.name;
      } 
    }
    return;
  }
    // 2️⃣ Error único
  switch (e.error.code) {
    case 'Serie.FormatoInvalidoRir':
      this.MensajeErrorRir=e.error.name;
      break;
  }
}
private limpiarMensajesError(){
  this.MensajeErrorPeso=""
  this.MensajeErrorReps=""
  this.MensajeErrorRir=""
}

private resetearCamposSerie(): void {
  this.Peso = 0;
  this.Repeticiones = 0;
  this.Rir = '';
}

}
