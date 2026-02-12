import { Component } from '@angular/core';
import { EjerciciosSubgrupoResponse } from './service/EjerciciosSubgrupoResponse';
import { ObtenerEjerciciosSubgrupoService } from './service/obtener-ejercicios-subgrupo';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-ejercicios-subgrupo',
  imports: [],
  templateUrl: './ejercicios-subgrupo.html',
  styleUrl: './ejercicios-subgrupo.css',
})
export class EjerciciosSubgrupo {

  ejerciciosSubgrupo! : EjerciciosSubgrupoResponse[];
  constructor(private servicio:ObtenerEjerciciosSubgrupoService,private router:Router,private route:ActivatedRoute){}
  idSubGrupo! : string | null;
  idSubGrupoNum! : number|null;
  ngOnInit(){
    this.idSubGrupo=this.route.snapshot.paramMap.get('idSubgrupo');
    this.idSubGrupoNum=Number(this.idSubGrupo)
    this.servicio.ObtenerEjerciciosSubGrupo(this.idSubGrupoNum).subscribe({
      next:(res)=>{
        console.log(res)
        this.ejerciciosSubgrupo=res;
      },
      error:(e)=>{

      }
    })
  }

  AgregarEjercicio(uid:string){
    this.router.navigate([`/completardatosejercicio/${uid}`]);
  }

}
