import { Component, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexTitleSubtitle,
  NgApexchartsModule
} from "ng-apexcharts";
import { SerieDatosGraficaResponse } from '../../Features/graficos/service/SerieDatosGraficaResponse';

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
  selector: 'app-chart-component',
  standalone:true,
  imports: [NgApexchartsModule],
  templateUrl: './chart-component.html',
  styleUrl: './chart-component.css',
})
export class ChartComponentGeneric implements OnChanges {
  @ViewChild("chart") chart!: ChartComponent;
  @Input({ required: true }) datos!: Record<string, SerieDatosGraficaResponse>;
  public chartOptions: ChartOptions = this.crearChart({});
  ngOnChanges(changes: SimpleChanges): void {
    if (changes["datos"] || changes["titulo"] || changes["serieNombre"] || changes["tipo"] || changes["alto"]) {
      this.chartOptions = this.crearChart(this.datos ?? {});
    }
  }
  private crearChart(datos: Record<string,SerieDatosGraficaResponse>): ChartOptions {

    return {
      series: [
        {
          name: "Object.values(datos).map(v=>v.ejercicio)[0]",
          data: Object.values(datos).map(v=>v.rmCalculado)
        }
      ],
      chart: {
        height: 350,
        type: "line",
        zoom: { enabled: false }
      },
      dataLabels: { enabled: false },
      stroke: { curve: "straight" },
      title: {
        text: `Ejercicio`,
        align: "left"
      },
      grid: {
        row: {
          colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
          opacity: 0.5
        }
      },
      xaxis: {
        categories: Object.keys(datos)
      }
    };
  }


}
