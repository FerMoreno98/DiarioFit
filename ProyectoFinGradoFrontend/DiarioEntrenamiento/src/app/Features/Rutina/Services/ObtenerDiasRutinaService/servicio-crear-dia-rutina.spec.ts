import { TestBed } from '@angular/core/testing';

import { ServicioObtenerDiasRutina } from './servicio-obtener-dia-rutina';

describe('ServicioCrearDiaRutina', () => {
  let service: ServicioObtenerDiasRutina;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicioObtenerDiasRutina);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
