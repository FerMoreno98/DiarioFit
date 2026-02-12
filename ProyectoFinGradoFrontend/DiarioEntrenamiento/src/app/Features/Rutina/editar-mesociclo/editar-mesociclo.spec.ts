import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarMesociclo } from './editar-mesociclo';

describe('EditarMesociclo', () => {
  let component: EditarMesociclo;
  let fixture: ComponentFixture<EditarMesociclo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditarMesociclo]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditarMesociclo);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
