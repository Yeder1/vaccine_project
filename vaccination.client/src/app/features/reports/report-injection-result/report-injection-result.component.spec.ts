import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportInjectionResultComponent } from './report-injection-result.component';

describe('ReportInjectionResultComponent', () => {
  let component: ReportInjectionResultComponent;
  let fixture: ComponentFixture<ReportInjectionResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReportInjectionResultComponent]
    });
    fixture = TestBed.createComponent(ReportInjectionResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
