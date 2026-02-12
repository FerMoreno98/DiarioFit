import { Component } from '@angular/core';
import { ServicioObtenerDiasRutina } from '../Services/ObtenerDiasRutinaService/servicio-obtener-dia-rutina';
import { ObtenerDiaRutinaResponse } from '../Services/ObtenerDiasRutinaService/ObtenerDiaRutinaResponse';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { DiaRutinaWizardService } from '../Services/dia-rutina-wizard-service';

@Component({
  selector: 'app-obtener-dia-rutina',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './obtener-dia-rutina.html',
  styleUrl: './obtener-dia-rutina.css',
})
export class ObtenerDiaRutina {
  uidRutina!: string | null;
  
  constructor(private route: ActivatedRoute, private servicio:ServicioObtenerDiasRutina,private router:Router,private uidDiaService: DiaRutinaWizardService) {}

  dias:ObtenerDiaRutinaResponse[]=[];

  ngOnInit() {
    this.uidRutina = this.route.snapshot.paramMap.get('uid');
    
    this.servicio.ObtenerDias(this.uidRutina).subscribe({
      next:(res)=>{
        this.dias=res;
        console.log(this.dias)
      },
      error:(e)=>{

      }
    })

 

    // o si quieres reaccionar a cambios:
    // this.route.paramMap.subscribe(params => {
    //   this.uidRutina = params.get('uid');
    // });
  }
     AniadirDia() {
      this.router.navigate(['/creardia',this.uidRutina]);
    }
    AgregarEjercicio(uid:string | null){
      console.log(uid);
      this.uidDiaService.setEjercicioUid(uid);
      this.router.navigate(['/agregarejercicio',uid]);
    }

    Finalizar(){
      this.router.navigate(['/home']);
    }
}

