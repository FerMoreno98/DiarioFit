import { TestBed } from '@angular/core/testing';

import { ServicioCrearRutina } from './servicio-crear-rutina';

describe('ServicioCrearRutina', () => {
  let service: ServicioCrearRutina;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicioCrearRutina);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
