import { Component, ViewChild } from '@angular/core';
import { ServicioGraficas } from './service/servicio-graficas';
import { SerieDatosGraficaResponse } from './service/SerieDatosGraficaResponse';
import { ChartComponentGeneric } from "../../shared/chart-component/chart-component";
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexDataLabels,
  ApexFill,
  ApexGrid,
  ApexMarkers,
  ApexStroke,
  ApexTooltip,
  ApexXAxis,
  ApexTitleSubtitle,
  ApexYAxis,
  NgApexchartsModule
} from "ng-apexcharts";
import { PlieguesRequest } from '../../shared/Models/PlieguesRequest';
import { PerimetrosRequest } from '../../shared/Models/PerimetrosRequest';

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
chartOptionsPerimetros: Partial<ChartOptions2>[] = [];
constructor(private servicio:ServicioGraficas){

}
Pliegues!: PlieguesRequest[];
Perimetros!: PerimetrosRequest[];
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
    this.servicio.obtenerPerimetros().subscribe({
      next:(res)=>{
        this.Perimetros=res;
        this.chartOptionsPerimetros = this.crearChartsSincronizadosPerimetros(res);
      },
      error:(e)=>{
        console.log(e);
      }
    })

  }

  onFuerzaTabClick(): void {
    setTimeout(() => window.dispatchEvent(new Event('resize')), 50);
  }

  onMedidasTabClick(): void {
    setTimeout(() => window.dispatchEvent(new Event('resize')), 50);
  }

  onPerimetrosTabClick(): void {
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
        type: 'line',
        height: 350,
        zoom: { enabled: false },
        toolbar: { show: false }
      },
      colors: [campo.color],
      title: { text: campo.label, align: 'left' },
      xaxis: { categories: etiquetas },
      yaxis: { tickAmount: 2, labels: { minWidth: 40, style: { fontSize: '13px' } } },
      dataLabels: { enabled: false },
      stroke: { curve: 'straight', width: 2 },
      grid: {
        row: {
          colors: ['rgba(255,255,255,0.03)', 'transparent'],
          opacity: 0.5
        }
      },
      tooltip: { shared: true, intersect: false }
    }));
  }

  private crearChartsSincronizadosPerimetros(datos: PerimetrosRequest[]): Partial<ChartOptions2>[] {
    const etiquetas = datos.map((_, i) => `Registro ${i + 1}`);

    const campos: { key: keyof PerimetrosRequest; label: string; color: string }[] = [
      { key: 'cuello', label: 'Cuello (cm)', color: '#2E93FA' },
      { key: 'brazoDchoRelajado', label: 'Brazo dcho relajado (cm)', color: '#66DA26' },
      { key: 'brazoDchoTension', label: 'Brazo dcho tensión (cm)', color: '#546E7A' },
      { key: 'brazoIzqRelajado', label: 'Brazo izq relajado (cm)', color: '#E91E63' },
      { key: 'brazoIzqTension', label: 'Brazo izq tensión (cm)', color: '#FF9800' },
      { key: 'pecho', label: 'Pecho (cm)', color: '#1B998B' },
      { key: 'hombro', label: 'Hombro (cm)', color: '#A259FF' },
      { key: 'cintura', label: 'Cintura (cm)', color: '#F9C80E' },
      { key: 'cadera', label: 'Cadera (cm)', color: '#FF4560' },
      { key: 'abdomen', label: 'Abdomen (cm)', color: '#00B8D9' },
      { key: 'musloDcho', label: 'Muslo dcho (cm)', color: '#4CAF50' },
      { key: 'musloIzq', label: 'Muslo izq (cm)', color: '#C0CA33' },
      { key: 'pantorrillaDcha', label: 'Pantorrilla dcha (cm)', color: '#775DD0' },
      { key: 'pantorrillaIzq', label: 'Pantorrilla izq (cm)', color: '#FF66C3' },
    ];

    return campos.map(campo => ({
      series: [{ name: campo.label, data: datos.map(d => (d[campo.key] as number) ?? 0) }],
      chart: {
        id: `perimetro-${String(campo.key)}`,
        group: 'perimetros',
        type: 'line',
        height: 350,
        zoom: { enabled: false },
        toolbar: { show: false }
      },
      colors: [campo.color],
      title: { text: campo.label, align: 'left' },
      xaxis: { categories: etiquetas },
      yaxis: { tickAmount: 2, labels: { minWidth: 40, style: { fontSize: '13px' } } },
      dataLabels: { enabled: false },
      stroke: { curve: 'straight', width: 2 },
      grid: {
        row: {
          colors: ['rgba(255,255,255,0.03)', 'transparent'],
          opacity: 0.5
        }
      },
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
