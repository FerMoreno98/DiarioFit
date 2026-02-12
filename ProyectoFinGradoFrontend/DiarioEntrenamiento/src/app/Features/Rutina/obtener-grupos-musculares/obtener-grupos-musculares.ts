import { Component } from '@angular/core';
import { GruposMuscularesResponse } from './service/GruposMuscularesResponse';
import { ObtenerGruposMuscularesService } from './service/obtener-grupos-musculares';
import { Router } from '@angular/router';
import { DiaRutinaWizardService } from '../Services/dia-rutina-wizard-service';

@Component({
  selector: 'app-obtener-grupos-musculares',
  imports: [],
  templateUrl: './obtener-grupos-musculares.html',
  styleUrl: './obtener-grupos-musculares.css',
})
export class ObtenerGruposMusculares {

  gruposMusculares! :GruposMuscularesResponse[];
  constructor(private service:ObtenerGruposMuscularesService ,private router:Router, private rutinaswizardprueba:DiaRutinaWizardService){}
  ngOnInit(){
    console.log(this.rutinaswizardprueba.getEjercicioUid());
    this.service.ObtenerGruposMusculares().subscribe({
      next:(res)=>{
        console.log(res);
        this.gruposMusculares=res;
      },
      error:(e)=>{
        console.log(e);
      }
    });
  }

  ObtenerSubGrupos(id:number){
    console.log(id);
    this.router.navigate([`/subgruposmusculares/${id}`])
  }

}
