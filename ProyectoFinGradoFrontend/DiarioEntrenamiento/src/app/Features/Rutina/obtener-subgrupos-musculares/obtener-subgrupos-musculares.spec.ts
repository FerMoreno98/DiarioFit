import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObtenerSubgruposMusculares } from './obtener-subgrupos-musculares';

describe('ObtenerSubgruposMusculares', () => {
  let component: ObtenerSubgruposMusculares;
  let fixture: ComponentFixture<ObtenerSubgruposMusculares>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ObtenerSubgruposMusculares]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ObtenerSubgruposMusculares);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
