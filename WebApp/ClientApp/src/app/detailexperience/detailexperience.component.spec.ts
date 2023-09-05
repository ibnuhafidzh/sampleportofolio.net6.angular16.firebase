import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailexperienceComponent } from './detailexperience.component';

describe('DetailexperienceComponent', () => {
  let component: DetailexperienceComponent;
  let fixture: ComponentFixture<DetailexperienceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DetailexperienceComponent]
    });
    fixture = TestBed.createComponent(DetailexperienceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
