import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObtenerDiaRutina } from './obtener-dia-rutina';

describe('CrearDiaRutina', () => {
  let component: ObtenerDiaRutina;
  let fixture: ComponentFixture<ObtenerDiaRutina>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ObtenerDiaRutina]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ObtenerDiaRutina);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
