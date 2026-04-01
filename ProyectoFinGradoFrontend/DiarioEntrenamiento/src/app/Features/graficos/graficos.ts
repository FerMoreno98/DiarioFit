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
import { PlieguesRequest } from '../../shared/Models/PlieguesRequest';

export type ChartOptions2 = {
  series: ApexAxisChartSeries;
  chart: any; //ApexChart;
  dataLabels: ApexDataLabels;
  markers: ApexMarkers;
  title: ApexTitleSubtitle;
  fill: ApexFill;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  tooltip: ApexTooltip;
  stroke: ApexStroke;
  grid: any; //ApexGrid;
  colors: any;
  toolbar: any;
};

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
chartOptions: ChartOptions[] = [];
chartOptionsPliegues: Partial<ChartOptions2>[] = [];
constructor(private servicio:ServicioGraficas){

}
Pliegues!: PlieguesRequest[];
datosGrafico!: Record<string,SerieDatosGraficaResponse>[]
  ngOnInit() : void{
        this.servicio.obtenerDatosGrafica().subscribe({
      next:(res)=>{
        this.datosGrafico=res
        let cont=0
        this.datosGrafico.map(element => {
            this.chartOptions[cont]=this.crearChart(element);
            cont++;
        });
      },
      error:(e)=>{
        console.log(e);
      }
    })
    this.servicio.obtenerPliegues().subscribe({
      next:(res)=>{
        this.Pliegues=res;
        this.chartOptionsPliegues = this.crearChartsSincronizadosPliegues(res);
      },
      error:(e)=>{
        console.log(e);
      }
    })

  }

  onMedidasTabClick(): void {
    setTimeout(() => window.dispatchEvent(new Event('resize')), 50);
  }

  private crearChartsSincronizadosPliegues(datos: PlieguesRequest[]): Partial<ChartOptions2>[] {
    const etiquetas = datos.map((_, i) => `Registro ${i + 1}`);

    const campos: { key: keyof PlieguesRequest; label: string; color: string }[] = [
      { key: 'abdominal',              label: 'Abdominal (mm)',       color: '#008FFB' },
      { key: 'suprailiaco',            label: 'Suprailiaco (mm)',     color: '#00E396' },
      { key: 'tricipital',             label: 'Tricipital (mm)',      color: '#FEB019' },
      { key: 'subescapular',           label: 'Subescapular (mm)',    color: '#FF4560' },
      { key: 'muslo',                  label: 'Muslo (mm)',           color: '#775DD0' },
      { key: 'pantorrilla',            label: 'Pantorrilla (mm)',     color: '#00D9E9' },
      { key: 'porcentajeGrasoEstimado',label: '% Grasa estimado',    color: '#FF66C3' },
    ];

    return campos.map(campo => ({
      series: [{ name: campo.label, data: datos.map(d => (d[campo.key] as number) ?? 0) }],
      chart: {
        id: `pliegue-${String(campo.key)}`,
        group: 'pliegues',
        type: 'area',
        height: 160,
        toolbar: { show: false }
      },
      colors: [campo.color],
      title: { text: campo.label, align: 'left' },
      xaxis: { categories: etiquetas },
      yaxis: { tickAmount: 2, labels: { minWidth: 40 } },
      dataLabels: { enabled: false },
      stroke: { curve: 'smooth', width: 2 },
      fill: { type: 'gradient', gradient: { opacityFrom: 0.55, opacityTo: 0 } },
      tooltip: { shared: true, intersect: false }
    }));
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
