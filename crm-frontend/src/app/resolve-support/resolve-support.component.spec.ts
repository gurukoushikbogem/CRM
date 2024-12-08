import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResolveSupportComponent } from './resolve-support.component';

describe('ResolveSupportComponent', () => {
  let component: ResolveSupportComponent;
  let fixture: ComponentFixture<ResolveSupportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResolveSupportComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ResolveSupportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
