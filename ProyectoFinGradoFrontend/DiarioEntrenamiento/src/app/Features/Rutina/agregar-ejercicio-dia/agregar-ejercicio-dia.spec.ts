import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgregarEjercicioDia } from './agregar-ejercicio-dia';

describe('AgregarEjercicioDia', () => {
  let component: AgregarEjercicioDia;
  let fixture: ComponentFixture<AgregarEjercicioDia>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AgregarEjercicioDia]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgregarEjercicioDia);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
