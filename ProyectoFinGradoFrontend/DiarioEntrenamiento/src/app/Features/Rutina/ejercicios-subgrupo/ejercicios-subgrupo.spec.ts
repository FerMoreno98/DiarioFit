import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EjerciciosSubgrupo } from './ejercicios-subgrupo';

describe('EjerciciosSubgrupo', () => {
  let component: EjerciciosSubgrupo;
  let fixture: ComponentFixture<EjerciciosSubgrupo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EjerciciosSubgrupo]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EjerciciosSubgrupo);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
