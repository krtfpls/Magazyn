import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentCreateHeaderComponent } from './document-create-header.component';

describe('DocumentCreateHeaderComponent', () => {
  let component: DocumentCreateHeaderComponent;
  let fixture: ComponentFixture<DocumentCreateHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentCreateHeaderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DocumentCreateHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
