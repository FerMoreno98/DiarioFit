import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { EjerciciosDetalleResponse } from '../Features/Rutina/agregar-ejercicio-dia/service/EjerciciosDetallesResponse';

@Injectable({
  providedIn: 'root',
})
export class DataService {
// cuando creas un Behaviour subject es un singleton por lo que la instancia quedara durante el resto del tiempo de ejecucion
  private datosEjercicio=new BehaviorSubject<EjerciciosDetalleResponse | null>(null);
  datos$=this.datosEjercicio.asObservable();

  setDatosEjercicio(datos:EjerciciosDetalleResponse){this.datosEjercicio.next(datos);}
  clearDatosEjercicio() {
  this.datosEjercicio.next(null);
}
  
}
