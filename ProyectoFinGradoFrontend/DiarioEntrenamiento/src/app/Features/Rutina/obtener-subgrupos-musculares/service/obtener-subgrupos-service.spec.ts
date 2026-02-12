import { TestBed } from '@angular/core/testing';

import { ObtenerSubgruposService } from './obtener-subgrupos-service';

describe('ObtenerSubgruposService', () => {
  let service: ObtenerSubgruposService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObtenerSubgruposService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
