import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportVaccineResultComponent } from './report-vaccine-result.component';

describe('ReportVaccineResultComponent', () => {
  let component: ReportVaccineResultComponent;
  let fixture: ComponentFixture<ReportVaccineResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReportVaccineResultComponent]
    });
    fixture = TestBed.createComponent(ReportVaccineResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
