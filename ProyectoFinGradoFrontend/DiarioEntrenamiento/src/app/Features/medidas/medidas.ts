import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ServicioMedidas } from './service/servicio-medidas';
import { inject } from '@angular/core';

@Component({
  selector: 'app-medidas',
  imports: [FormsModule, CommonModule],
  templateUrl: './medidas.html',
  styleUrl: './medidas.css',
})
export class Medidas {

  servicioMedidas = inject(ServicioMedidas);

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
  porcentajeGrasoEstimado: number=0;

  // ── Errores perímetros ───────────────────────────
  errores: { [campo: string]: string } = {};
  TipoError = '';

  calcularPorcentajeGrasoEstimado(): void {
    this.sumaPliegues =
      (this.pliegueAbdominal    ?? 0) +
      (this.pliegueSuprailiaco  ?? 0) +
      (this.pliegueTriicipital  ?? 0) +
      (this.pliegueSubescapular ?? 0) +
      (this.pliegueMuslo        ?? 0) +
      (this.plieguerPantorrilla ?? 0);
    this.porcentajeGrasoEstimado = 0.1051 * this.sumaPliegues + 2.585;
  }

  guardarPerimetros(): void {
    this.servicioMedidas.insertarPerimetros({
      cuello: this.cuello ?? null,
      brazoDchoRelajado: this.brazoDchoRelajado ?? null,
      brazoDchoTension: this.brazoDchoTension ?? null,
      brazoIzqRelajado: this.brazoIzqRelajado ?? null,
      brazoIzqTension: this.brazoIzqTension ?? null,
      pecho: this.pecho ?? null,
      hombro: this.hombro ?? null,
      cintura: this.cintura ?? null,
      cadera: this.cadera ?? null,
      abdomen: this.abdomen ?? null,
      musloDcho: this.musloDcho ?? null,
      musloIzq: this.musloIzq ?? null,
      pantorrillaDcha: this.pantorrillaDcha ?? null,
      pantorrillaIzq: this.pantorrillaIzq ?? null,
    }).subscribe({
      next: () => {
        this.cuello = null!;
        this.brazoDchoRelajado = null!;
        this.brazoDchoTension = null!;
        this.brazoIzqRelajado = null!;
        this.brazoIzqTension = null!;
        this.pecho = null!;
        this.hombro = null!;
        this.cintura = null!;
        this.cadera = null!;
        this.abdomen = null!;
        this.musloDcho = null!;
        this.musloIzq = null!;
        this.pantorrillaDcha = null!;
        this.pantorrillaIzq = null!;
        this.errores = {};
        this.TipoError = '';
      },
      error: (e) => {
        this.manejarErroresPerimetros(e);
      }
    });
  }

  manejarErroresPerimetros(e: any): void {
    this.errores = {};
    this.TipoError = '';

    if (!Array.isArray(e.error)) {
      this.TipoError = e.error.code.split('.')[1];
      if (e.status === 400) {
        const campo = this.TipoError.toLowerCase();
        this.errores[campo] = e.error.name;
      }
      return;
    }

    for (let i = 0; i < e.error.length; i++) {
      const campo = e.error[i].code.split('.').pop()?.toLowerCase() ?? '';
      this.errores[campo] = e.error[i].name;
    }
  }

  guardarPliegues(): void {
    this.servicioMedidas.insertarPliegues({
      uidUsuario: null,
      abdominal: this.pliegueAbdominal ?? null,
      suprailiaco: this.pliegueSuprailiaco ?? null,
      tricipital: this.pliegueTriicipital ?? null,
      subescapular: this.pliegueSubescapular ?? null,
      muslo: this.pliegueMuslo ?? null,
      pantorrilla: this.plieguerPantorrilla ?? null,
      porcentajeGrasoEstimado: null
    }).subscribe({
      next: () => {
        this.pliegueAbdominal = null!;
        this.pliegueSuprailiaco = null!;
        this.pliegueTriicipital = null!;
        this.pliegueSubescapular = null!;
        this.pliegueMuslo = null!;
        this.plieguerPantorrilla = null!;
        this.sumaPliegues = 0;
        this.errores = {};
        this.TipoError = '';
        this.porcentajeGrasoEstimado=0;
      },
      error: (e) => {
        this.manejarErroresPerimetros(e);
      }
    });
  }
}
