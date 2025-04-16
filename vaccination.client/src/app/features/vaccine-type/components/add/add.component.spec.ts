import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccineTypeAddComponent } from './add.component';

describe('VaccineTypeAddComponent', () => {
  let component: VaccineTypeAddComponent;
  let fixture: ComponentFixture<VaccineTypeAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VaccineTypeAddComponent]
    });
    fixture = TestBed.createComponent(VaccineTypeAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
