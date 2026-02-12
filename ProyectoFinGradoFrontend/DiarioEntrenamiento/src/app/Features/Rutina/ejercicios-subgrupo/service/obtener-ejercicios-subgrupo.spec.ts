import { TestBed } from '@angular/core/testing';

import { ObtenerEjerciciosSubgrupoService } from './obtener-ejercicios-subgrupo';

describe('ObtenerEjerciciosSubgrupo', () => {
  let service: ObtenerEjerciciosSubgrupoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObtenerEjerciciosSubgrupoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
