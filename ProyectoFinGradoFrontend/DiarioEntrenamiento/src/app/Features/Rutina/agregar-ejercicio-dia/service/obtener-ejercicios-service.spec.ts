import { TestBed } from '@angular/core/testing';

import { ObtenerEjerciciosService } from './obtener-ejercicios-service';

describe('ObtenerEjerciciosService', () => {
  let service: ObtenerEjerciciosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObtenerEjerciciosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
