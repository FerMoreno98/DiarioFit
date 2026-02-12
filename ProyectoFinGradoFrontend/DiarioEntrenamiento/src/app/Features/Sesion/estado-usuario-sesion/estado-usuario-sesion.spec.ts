import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EstadoUsuarioSesion } from './estado-usuario-sesion';

describe('EstadoUsuarioSesion', () => {
  let component: EstadoUsuarioSesion;
  let fixture: ComponentFixture<EstadoUsuarioSesion>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EstadoUsuarioSesion]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EstadoUsuarioSesion);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
