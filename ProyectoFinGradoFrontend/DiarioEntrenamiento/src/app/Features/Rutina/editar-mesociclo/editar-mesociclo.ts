import { Component } from '@angular/core';
import { ObtenerDatosMesociclos } from './services/obtener-datos-mesociclos';
import { MesociclosResponse } from './services/MesocicloResponse';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialog } from '@angular/material/dialog';
import { ModalConfirmacion } from '../../../shared/modal-confirmacion/modal-confirmacion';
import { ModalError } from '../../../shared/modal-error/modal-error';

@Component({
  selector: 'app-editar-mesociclo',
  imports: [FormsModule,MatDialogModule],
  templateUrl: './editar-mesociclo.html',
  styleUrl: './editar-mesociclo.css',
})
export class EditarMesociclo {

  constructor(
    private servicioObtenerDatosMesociclo:ObtenerDatosMesociclos,
    private router:ActivatedRoute,
    private route:Router,
    private dialog:MatDialog
  ){}

  datosmesociclos: MesociclosResponse[] = []
  hoy: Date=new Date()

  ngOnInit(){
    this.obtenerDatosMesociclos()
  }

  obtenerDatosMesociclos(){
    this.servicioObtenerDatosMesociclo.ObtenerMesociclos().subscribe({
      next:(res)=>{
        console.log(res)
      this.datosmesociclos = res.map(m => ({
        ...m,
        fechaInicio: this.toDateInput(m.fechaInicio),
        fechaFin: this.toDateInput(m.fechaFin),
      }));
      },
      error:(e)=>{
        console.log(e);
      }
    })
  }
private toDateInput(value: string | Date): string {
  const d = value instanceof Date ? value : new Date(value);

  const y = d.getFullYear();
  const m = String(d.getMonth() + 1).padStart(2, '0');
  const day = String(d.getDate()).padStart(2, '0');

  return `${y}-${m}-${day}`;
}
private parseLocalDate(ymd: string): Date {
  const [y, m, d] = ymd.split('-').map(Number);
  return new Date(y, m - 1, d); // local, sin UTC
}
esMesocicloActivo(m: MesociclosResponse): boolean {
  const hoy = this.getHoy();

  const fechaInicio = new Date(m.fechaInicio);
  const fechaFin = new Date(m.fechaFin);

  fechaInicio.setHours(0,0,0,0);
  fechaFin.setHours(0,0,0,0);

  return hoy >= fechaInicio && hoy <= fechaFin;
}

private getHoy(): Date {
  const hoy = new Date();
  hoy.setHours(0, 0, 0, 0);
  return hoy;
}

  VerDias(UidRutina:string){
    this.route.navigate(["/diarutina",UidRutina])
  }

    openConfirm(UidRutina:string|null) {
    const ref = this.dialog.open(ModalConfirmacion, {
      width: '420px',
      panelClass:'custom-dialog',
       backdropClass: 'custom-dialog-backdrop',
      disableClose: true, // opcional: evita cerrar clicando fuera o con ESC
      data: {
        title: 'Confirmación',
        message: '¿Seguro que quieres borrar este mesociclo, no podrás volver a revisar sus datos?',
        okText: 'Sí, borrar',
        cancelText: 'No',
      },
    });
        ref.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.servicioObtenerDatosMesociclo.EliminarMesociclo(UidRutina).subscribe({
          next:(res)=>{
            this.datosmesociclos=this.datosmesociclos.filter(e=>e.uid!==UidRutina);
          },
          error:(e)=>{
            if(e.error==="23503: update or delete on table \"DiaRutina\" violates foreign key constraint \"Fk_DiaRutina_Sesion\" on table \"RegistroDatosSesion\"\r\n\r\nDETAIL: Detail redacted as it may contain sensitive data. Specify 'Include Error Detail' in the connection string to include this information."){
              this.dialog.open(ModalError, {
                data: {
                  title: 'Error',
                  message: 'No puedes eliminar mesociclos en los que tengas sesiones realizadas.',
                  cancelText: 'Entendido',
                },
                panelClass: 'custom-dialog',
              });
            }else{
              this.dialog.open(ModalError, {
                data: {
                  title: 'Error',
                  message: 'Ocurrió un error inesperado.',
                  cancelText: 'Entendido',
                },
                panelClass: 'custom-dialog',
              });
            }
          }
        })
      }
    });
  }
duplicarRutina(uidRutina:string,nombrerutina:string){
  this.route.navigate(['nuevomesociclo',uidRutina,nombrerutina]);
}
crearMesociclo(){
  this.route.navigate(['nuevomesociclo']);
}

editarMesociclo(uidRutina:string,Nombre:string,FechaInicio:string,FechaFin:string)
{
  this.route.navigate(['nuevomesociclo',uidRutina,Nombre,FechaInicio,FechaFin])
}
}
