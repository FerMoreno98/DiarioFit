import { TestBed } from '@angular/core/testing';

import { ServicioHomepage } from './servicio-homepage';

describe('ServicioHomepage', () => {
  let service: ServicioHomepage;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicioHomepage);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
