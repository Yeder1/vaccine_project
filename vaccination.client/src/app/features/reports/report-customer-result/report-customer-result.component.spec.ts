import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportCustomerResultComponent } from './report-customer-result.component';

describe('ReportCustomerResultComponent', () => {
  let component: ReportCustomerResultComponent;
  let fixture: ComponentFixture<ReportCustomerResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReportCustomerResultComponent]
    });
    fixture = TestBed.createComponent(ReportCustomerResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
