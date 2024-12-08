import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpputunitiesComponent } from './opputunities.component';

describe('OpputunitiesComponent', () => {
  let component: OpputunitiesComponent;
  let fixture: ComponentFixture<OpputunitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OpputunitiesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OpputunitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
