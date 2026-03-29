import { TestBed } from '@angular/core/testing';

import { ServicioMedidas } from './servicio-medidas';

describe('ServicioMedidas', () => {
  let service: ServicioMedidas;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicioMedidas);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
