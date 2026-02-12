import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SesionEntrenamiento } from './sesion-entrenamiento';

describe('SesionEntrenamiento', () => {
  let component: SesionEntrenamiento;
  let fixture: ComponentFixture<SesionEntrenamiento>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SesionEntrenamiento]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SesionEntrenamiento);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
