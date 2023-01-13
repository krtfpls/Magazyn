import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentsCreateComponent } from './documents-create.component';

describe('DocumentsCreateComponent', () => {
  let component: DocumentsCreateComponent;
  let fixture: ComponentFixture<DocumentsCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentsCreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DocumentsCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
