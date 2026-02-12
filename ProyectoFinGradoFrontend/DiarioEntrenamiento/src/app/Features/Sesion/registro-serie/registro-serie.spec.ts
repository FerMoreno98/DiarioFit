import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroSerie } from './registro-serie';

describe('RegistroSerie', () => {
  let component: RegistroSerie;
  let fixture: ComponentFixture<RegistroSerie>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistroSerie]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistroSerie);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
