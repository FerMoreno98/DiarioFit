import { TestBed } from '@angular/core/testing';

import { DiaRutinaWizardService } from './dia-rutina-wizard-service';

describe('DiaRutinaWizardService', () => {
  let service: DiaRutinaWizardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DiaRutinaWizardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
