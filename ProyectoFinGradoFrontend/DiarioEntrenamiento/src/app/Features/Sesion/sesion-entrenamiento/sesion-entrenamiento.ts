import { Component } from '@angular/core';
import { ObtenerDatosUltimaSesion } from './Service/obtener-datos-ultima-sesion';
import { ActivatedRoute, Router } from '@angular/router';
import { DatosUltimaSesionResponse } from './Service/DatosUltimaSesionResponse';

@Component({
  selector: 'app-sesion-entrenamiento',
  imports: [],
  templateUrl: './sesion-entrenamiento.html',
  styleUrl: './sesion-entrenamiento.css',
})
export class SesionEntrenamiento {

  constructor(private ServicioObtenerDatosUltimaSesion:ObtenerDatosUltimaSesion,private route:ActivatedRoute,private router:Router){}

  DatosUltimaSesion: Record<string,DatosUltimaSesionResponse []>| null=null;
  UidDia : string| null=""

  ngOnInit(){
    this.UidDia=this.route.snapshot.paramMap.get('uiddia');
    this.ServicioObtenerDatosUltimaSesion.ObtenerUltimaSesion(this.UidDia).subscribe({
      next:(res)=>{
        this.DatosUltimaSesion=res
      },
      error:(e)=>{
        console.log(e);
      }
    })
  }

  get clavesUltimaSesion(): string[] {
  return this.DatosUltimaSesion ? Object.keys(this.DatosUltimaSesion) : [];
}
get valoresUltimaSesion(): DatosUltimaSesionResponse[][] {
  return this.DatosUltimaSesion
    ? Object.values(this.DatosUltimaSesion)
    : [];
}

iniciarRegistroSeries(key:string){
  this.router.navigate([`/registrarserie/1/${key}/${this.UidDia}`]);
}

esHoy(fechaSesion: string | Date): boolean {
  const d = typeof fechaSesion === 'string'
    ? new Date(fechaSesion)
    : fechaSesion;

  const hoy = new Date();

  return d.getFullYear() === hoy.getFullYear()
    && d.getMonth() === hoy.getMonth()
    && d.getDate() === hoy.getDate();
}
getUltimaFechaSesion(key: string): Date | null{
  const datos=this.DatosUltimaSesion?.[key];
  if(!datos || datos.length===0) return null
  return datos
    .map(x => new Date(x.fechaSesion))
    .reduce((max, curr) => (curr > max ? curr : max));
}
SesionAcabada(key : string): boolean{
  const fecha=this.getUltimaFechaSesion(key);
  return !!fecha && this.esHoy(fecha);
}

FinalizarEntreno(){
  this.router.navigate(['/home']);
}
}
