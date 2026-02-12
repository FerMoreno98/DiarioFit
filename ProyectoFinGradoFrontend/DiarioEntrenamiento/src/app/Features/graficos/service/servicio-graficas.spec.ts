import { TestBed } from '@angular/core/testing';

import { ServicioGraficas } from './servicio-graficas';

describe('ServicioGraficas', () => {
  let service: ServicioGraficas;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicioGraficas);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
