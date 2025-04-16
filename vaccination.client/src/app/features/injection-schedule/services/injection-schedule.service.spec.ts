import { TestBed } from '@angular/core/testing';

import { InjectionScheduleService } from './injection-schedule.service';

describe('InjectionScheduleService', () => {
  let service: InjectionScheduleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InjectionScheduleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
