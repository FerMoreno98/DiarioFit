import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrearDiaRutina } from './crear-dia-rutina';

describe('CrearDiaRutina', () => {
  let component: CrearDiaRutina;
  let fixture: ComponentFixture<CrearDiaRutina>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CrearDiaRutina]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrearDiaRutina);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
