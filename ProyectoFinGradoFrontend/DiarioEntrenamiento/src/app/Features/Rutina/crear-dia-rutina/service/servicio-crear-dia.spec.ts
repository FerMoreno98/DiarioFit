import { TestBed } from '@angular/core/testing';

import { ServicioCrearDia } from './servicio-crear-dia';

describe('ServicioCrearDia', () => {
  let service: ServicioCrearDia;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicioCrearDia);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
