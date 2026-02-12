import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DiaRutinaWizardService {
  UidDiaRutina! : string | null;

  setEjercicioUid(uid:string|null){
    this.UidDiaRutina=uid;
  }
  getEjercicioUid(){
    return this.UidDiaRutina
  }
  reset(){
    this.UidDiaRutina=null;
  }
  
}
