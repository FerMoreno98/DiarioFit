import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-medidas',
  imports: [FormsModule, CommonModule],
  templateUrl: './medidas.html',
  styleUrl: './medidas.css',
})
export class Medidas {

  tabActivo!: string;

  // ── Perímetros (cm) ──────────────────────────────
  cuello!: number;
  brazoDchoRelajado!: number;
  brazoDchoTension!: number;
  brazoIzqRelajado!: number;
  brazoIzqTension!: number;
  pecho!: number;
  hombro!: number;
  cintura!: number;
  cadera!: number;
  abdomen!: number;
  musloDcho!: number;
  musloIzq!: number;
  pantorrillaDcha!: number;
  pantorrillaIzq!: number;

  // ── Pliegues (mm) ───────────────────────────────
  pliegueAbdominal!: number;
  pliegueSuprailiaco!: number;
  pliegueTriicipital!: number;
  pliegueSubescapular!: number;
  pliegueMuslo!: number;
  plieguerPantorrilla!: number;
  sumaPliegues: number = 0;

  calcularSumaPliegues(): void {
    this.sumaPliegues =
      (this.pliegueAbdominal    ?? 0) +
      (this.pliegueSuprailiaco  ?? 0) +
      (this.pliegueTriicipital  ?? 0) +
      (this.pliegueSubescapular ?? 0) +
      (this.pliegueMuslo        ?? 0) +
      (this.plieguerPantorrilla ?? 0);
  }

  guardarPerimetros(): void { }

  guardarPliegues(): void { }
}
