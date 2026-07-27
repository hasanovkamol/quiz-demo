import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { ResultShareComponent } from './result-share.component';
import { QuizApiService } from '../../services/quiz-api.service';

describe('ResultShareComponent', () => {
  let component: ResultShareComponent;
  let fixture: ComponentFixture<ResultShareComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResultShareComponent, HttpClientTestingModule],
      providers: [QuizApiService, provideRouter([])]
    }).compileComponents();

    fixture = TestBed.createComponent(ResultShareComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create ResultShareComponent', () => {
    expect(component).toBeTruthy();
  });
});
