import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinationResultManagementComponent } from '../vaccination-result-management.component';

describe('VaccinationResultManagementComponent', () => {
  let component: VaccinationResultManagementComponent;
  let fixture: ComponentFixture<VaccinationResultManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VaccinationResultManagementComponent]
    });
    fixture = TestBed.createComponent(VaccinationResultManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
