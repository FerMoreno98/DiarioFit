import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Loginlayout } from './loginlayout';

describe('Loginlayout', () => {
  let component: Loginlayout;
  let fixture: ComponentFixture<Loginlayout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Loginlayout]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Loginlayout);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
