import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ServicioCrearDia } from './service/servicio-crear-dia';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-crear-dia-rutina',
  imports: [FormsModule],
  templateUrl: './crear-dia-rutina.html',
  styleUrl: './crear-dia-rutina.css',
})
export class CrearDiaRutina {
constructor(private servicio:ServicioCrearDia, private router:Router,private route:ActivatedRoute){}
  NombreDia:string="";
  DiaDeLaSemana:string="";
  uidRutina!:string|null;
  ErrorDia :string="";

   ngOnInit(){
    this.uidRutina=this.route.snapshot.paramMap.get('uid');

   }

  CrearDia(){
    this.servicio.crearDiaRutina(this.uidRutina,this.NombreDia,this.DiaDeLaSemana).subscribe({
      next:(res)=>{
        console.log(res);
        this.router.navigate([`/diarutina/${res[0].uid_rutina}`]);
      },
      error:(e)=>{
        if(e.error.code="Rutina.DiaDuplicado"){
          this.ErrorDia=e.error.name;
        }

      }
    })
  }
}
