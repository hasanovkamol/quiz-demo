import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { QuizResultComponent } from './quiz-result.component';
import { QuizService } from '../../services/quiz.service';

describe('QuizResultComponent', () => {
  let component: QuizResultComponent;
  let fixture: ComponentFixture<QuizResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuizResultComponent, HttpClientTestingModule],
      providers: [QuizService]
    }).compileComponents();

    fixture = TestBed.createComponent(QuizResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create QuizResultComponent', () => {
    expect(component).toBeTruthy();
  });
});
