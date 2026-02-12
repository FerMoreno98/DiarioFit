import { Component } from '@angular/core';
import { SubGruposResponse } from './service/SubGruposResponse';
import { ObtenerSubgruposService } from './service/obtener-subgrupos-service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-obtener-subgrupos-musculares',
  imports: [],
  templateUrl: './obtener-subgrupos-musculares.html',
  styleUrl: './obtener-subgrupos-musculares.css',
})
export class ObtenerSubgruposMusculares {

  Subgrupos! : SubGruposResponse[];
  IdGrupo : string | null=null;
  IdGrupoNum!:number | null;
  constructor(private servicio:ObtenerSubgruposService,private route:ActivatedRoute,private router:Router){}
  ngOnInit(){
    this.IdGrupo=this.route.snapshot.paramMap.get('idGrupo');
    this.IdGrupoNum=Number(this.IdGrupo);
    this.servicio.ObtenerSubGrupos(this.IdGrupoNum).subscribe({
      next:(res)=>{
        this.Subgrupos=res;
      },
      error:(e)=>{
        console.log(e);
      }
    })
  }

  ObtenerEjerciciosDe(id:number){
    this.router.navigate([`/ejerciciossubgrupo/${id}`]);
  }
}
