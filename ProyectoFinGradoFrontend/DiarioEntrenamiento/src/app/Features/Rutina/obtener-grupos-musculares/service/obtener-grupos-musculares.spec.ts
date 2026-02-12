import { TestBed } from '@angular/core/testing';

import { ObtenerGruposMuscularesService } from './obtener-grupos-musculares';

describe('ObtenerGruposMusculares', () => {
  let service: ObtenerGruposMuscularesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObtenerGruposMuscularesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
