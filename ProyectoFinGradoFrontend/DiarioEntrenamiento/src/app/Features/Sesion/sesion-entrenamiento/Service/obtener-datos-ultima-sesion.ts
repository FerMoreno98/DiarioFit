import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { DatosUltimaSesionResponse } from './DatosUltimaSesionResponse';

@Injectable({
  providedIn: 'root',
})
export class ObtenerDatosUltimaSesion {
  http=inject(HttpClient)
  urlbase=environment.GestionSesion.urlBase;

  ObtenerUltimaSesion(UidDia:string |null){

    return this.http.get<Record<string,DatosUltimaSesionResponse[]>>(`${this.urlbase}/datosultimasesion?UidDia=${UidDia}`)

  }
}
