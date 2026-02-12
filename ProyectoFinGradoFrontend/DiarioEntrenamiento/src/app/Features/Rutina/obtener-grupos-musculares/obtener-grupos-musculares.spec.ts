import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObtenerGruposMusculares } from './obtener-grupos-musculares';

describe('ObtenerGruposMusculares', () => {
  let component: ObtenerGruposMusculares;
  let fixture: ComponentFixture<ObtenerGruposMusculares>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ObtenerGruposMusculares]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ObtenerGruposMusculares);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
