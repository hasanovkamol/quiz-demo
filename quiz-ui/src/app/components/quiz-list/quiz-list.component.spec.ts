import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { QuizListComponent } from './quiz-list.component';
import { QuizService } from '../../services/quiz.service';

describe('QuizListComponent', () => {
  let component: QuizListComponent;
  let fixture: ComponentFixture<QuizListComponent>;
  let quizService: QuizService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuizListComponent, HttpClientTestingModule],
      providers: [QuizService]
    }).compileComponents();

    fixture = TestBed.createComponent(QuizListComponent);
    component = fixture.componentInstance;
    quizService = TestBed.inject(QuizService);
    fixture.detectChanges();
  });

  it('should create QuizListComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should filter categories when pill is selected in QuizService', () => {
    quizService.selectCategory('angular');
    expect(quizService.activeCategory()).toBe('angular');
  });

  it('should filter search term when typed', () => {
    component.searchTerm.set('Angular');
    expect(component.searchTerm()).toBe('Angular');
  });
});
