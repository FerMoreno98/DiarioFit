import { TestBed } from '@angular/core/testing';

import { CompletarDatosEjercicioService } from './completar-datos-ejercicio-service';

describe('CompletarDatosEjercicioService', () => {
  let service: CompletarDatosEjercicioService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CompletarDatosEjercicioService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
