import { TestBed } from '@angular/core/testing';

import { ObtenerDatosUltimaSesion } from './obtener-datos-ultima-sesion';

describe('ObtenerDatosUltimaSesion', () => {
  let service: ObtenerDatosUltimaSesion;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObtenerDatosUltimaSesion);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
