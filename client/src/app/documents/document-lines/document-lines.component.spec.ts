import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentLinesComponent } from './document-lines.component';

describe('DocumentLinesComponent', () => {
  let component: DocumentLinesComponent;
  let fixture: ComponentFixture<DocumentLinesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentLinesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DocumentLinesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
