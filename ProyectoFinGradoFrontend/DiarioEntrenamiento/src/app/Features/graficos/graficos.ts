import { Component, ViewChild } from '@angular/core';
import { ServicioGraficas } from './service/servicio-graficas';
import { SerieDatosGraficaResponse } from './service/SerieDatosGraficaResponse';
import { ChartComponentGeneric } from "../../shared/chart-component/chart-component";
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexTitleSubtitle,
  NgApexchartsModule
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  dataLabels: ApexDataLabels;
  grid: ApexGrid;
  stroke: ApexStroke;
  title: ApexTitleSubtitle;
};
@Component({
  selector: 'app-graficos',
  imports: [NgApexchartsModule],
  templateUrl: './graficos.html',
  styleUrl: './graficos.css',
})
export class Graficos {
@ViewChild("chart") chart!: ChartComponent;
chartOptions:ChartOptions[]=[];
constructor(private servicio:ServicioGraficas){

}
datosGrafico!: Record<string,SerieDatosGraficaResponse>[]
  ngOnInit() : void{
        this.servicio.obtenerDatosGrafica().subscribe({
      next:(res)=>{
        this.datosGrafico=res
        console.log(this.datosGrafico);
        let cont=0
        this.datosGrafico.map(element => {
            this.chartOptions[cont]=this.crearChart(element)
            console.log(this.chartOptions);
            cont++;
        });
      },
      error:(e)=>{
        console.log(e);
      }
    })

  }

    private crearChart(datos: Record<string,SerieDatosGraficaResponse>): ChartOptions {
  
      return {
        series: [
          {
            name: Object.values(datos).map(v=>v.ejercicio)[0],
            data: Object.values(datos).map(v=>v.rmCalculado)
          }
        ],
        chart: {
          height: 350,
          type: "line",
          zoom: { 
            enabled: false 
          }
        },
        dataLabels: { enabled: false },
        stroke: { curve: "straight" },
        title: {
          text: `Nivel de fuerza en: ${Object.values(datos).map(v=>v.ejercicio)[0]}`,
          align: "left"
        },
        grid: {
          row: {
            colors: ["rgba(255,255,255,0.03)", "transparent"], // takes an array which will be repeated on columns
            opacity: 0.5
          }
        },
        xaxis: {
          categories: Object.keys(datos).map(fecha => {
            const d = new Date(fecha);
            return d.toLocaleDateString('es-ES');
          })
        }
      };
    }
}
