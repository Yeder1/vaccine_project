import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccineTypeListComponent } from './list.component';

describe('ListComponent', () => {
  let component: VaccineTypeListComponent;
  let fixture: ComponentFixture<VaccineTypeListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VaccineTypeListComponent]
    });
    fixture = TestBed.createComponent(VaccineTypeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
