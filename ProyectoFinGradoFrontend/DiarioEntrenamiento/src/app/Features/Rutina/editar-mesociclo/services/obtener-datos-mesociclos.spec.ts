import { TestBed } from '@angular/core/testing';

import { ObtenerDatosMesociclos } from './obtener-datos-mesociclos';

describe('ObtenerDatosMesociclos', () => {
  let service: ObtenerDatosMesociclos;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObtenerDatosMesociclos);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
