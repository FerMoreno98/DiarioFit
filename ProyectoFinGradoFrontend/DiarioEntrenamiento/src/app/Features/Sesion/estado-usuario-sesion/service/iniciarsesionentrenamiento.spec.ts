import { TestBed } from '@angular/core/testing';

import { Iniciarsesionentrenamiento } from './iniciarsesionentrenamiento';

describe('Iniciarsesionentrenamiento', () => {
  let service: Iniciarsesionentrenamiento;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Iniciarsesionentrenamiento);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
