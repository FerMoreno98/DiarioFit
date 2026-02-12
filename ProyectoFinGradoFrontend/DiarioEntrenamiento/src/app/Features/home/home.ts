
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ServicioHomepage } from './Services/servicio-homepage';
import { DatosHomePageResponse } from './Services/DatosHomePageResponse';
import { Component, ViewChild } from "@angular/core";
import { ChartComponent, NgApexchartsModule } from "ng-apexcharts";

import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart,
  ApexStroke,
  ApexFill
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
  stroke: ApexStroke;
  fill: ApexFill;
};


@Component({
  selector: 'app-home',
  imports: [NgApexchartsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
constructor(private servicioHomePage:ServicioHomepage,private route:ActivatedRoute,private router:Router){}
@ViewChild("chart") chart!: ChartComponent;
public chartOptions!: ChartOptions;
datosHomePage:DatosHomePageResponse | null=null;
uidUsuario:string|undefined="";
datosGrafica!:Record<string,number>

ngOnInit(){
    this.servicioHomePage.ObtenerDatosHomePage().subscribe({
      next:(res)=>{
        this.datosHomePage=res;
      },
      error:(e)=>{
        console.log(e)
      }
    })
    this.servicioHomePage.ObtenerDatosGraficaGruposMusculares().subscribe({
      next:(res)=>{
        this.datosGrafica=res;
        this.chartOptions=this.crearChart(this.datosGrafica);
      },
      error:(e)=>{
        console.log(e);
      }
    })
  }
  private crearChart(datos:Record<string,number>) : ChartOptions{
    return {
      series: Object.values(datos),
      labels:Object.keys(datos),
      chart: {
        type: "polarArea"
      },
      stroke: {
        colors: ["#fff"]
      },
      fill: {
        opacity: 0.8
      },
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: "bottom"
            }
          }
        }
      ]
    };
  }



iniciarEntrenamiento(uidRutina:string | undefined, uidDia:string){

  this.router.navigate([`estadousuario/${uidDia}`])

  }
  verSesion(uidDia:string){
    this.router.navigate([`sesionentrenamiento/${uidDia}`]);
  }
}
