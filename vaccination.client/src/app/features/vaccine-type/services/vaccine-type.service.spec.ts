import { TestBed } from '@angular/core/testing';

import { VaccineTypeService } from './vaccine-type.service';

describe('VaccineTypeService', () => {
  let service: VaccineTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VaccineTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
