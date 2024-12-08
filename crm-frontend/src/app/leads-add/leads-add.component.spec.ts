import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeadsAddComponent } from './leads-add.component';

describe('LeadsAddComponent', () => {
  let component: LeadsAddComponent;
  let fixture: ComponentFixture<LeadsAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeadsAddComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LeadsAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
