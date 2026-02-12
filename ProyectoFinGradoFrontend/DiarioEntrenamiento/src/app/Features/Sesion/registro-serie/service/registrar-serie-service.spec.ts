import { TestBed } from '@angular/core/testing';

import { RegistrarSerieService } from './registrar-serie-service';

describe('RegistrarSerieService', () => {
  let service: RegistrarSerieService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegistrarSerieService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
