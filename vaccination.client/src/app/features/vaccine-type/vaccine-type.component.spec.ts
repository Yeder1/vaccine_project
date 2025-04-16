import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccineTypeComponent } from './vaccine-type.component';

describe('VaccineTypeComponent', () => {
  let component: VaccineTypeComponent;
  let fixture: ComponentFixture<VaccineTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VaccineTypeComponent]
    });
    fixture = TestBed.createComponent(VaccineTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
