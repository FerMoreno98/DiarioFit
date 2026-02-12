import { TestBed } from '@angular/core/testing';

import { ServicioCrear } from './servicio-crear';

describe('ServicioCrear', () => {
  let service: ServicioCrear;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicioCrear);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
