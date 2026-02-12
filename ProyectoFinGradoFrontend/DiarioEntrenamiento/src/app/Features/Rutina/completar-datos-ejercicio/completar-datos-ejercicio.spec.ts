import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompletarDatosEjercicio } from './completar-datos-ejercicio';

describe('CompletarDatosEjercicio', () => {
  let component: CompletarDatosEjercicio;
  let fixture: ComponentFixture<CompletarDatosEjercicio>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompletarDatosEjercicio]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompletarDatosEjercicio);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
