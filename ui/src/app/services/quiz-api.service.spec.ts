import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { QuizApiService } from './quiz-api.service';
import { Quiz } from '../models/quiz.model';

describe('QuizApiService', () => {
  let service: QuizApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [QuizApiService]
    });

    service = TestBed.inject(QuizApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch quizzes list via GET /api/quizzes', () => {
    const mockQuizzes: Quiz[] = [
      {
        id: 'q1',
        title: 'Angular Test',
        category: 'angular',
        categoryName: 'Angular Framework',
        description: 'Desc',
        iconName: 'code-2',
        difficulty: "O'rta",
        timeLimitSeconds: 300,
        questions: []
      }
    ];

    service.getQuizzes().subscribe(quizzes => {
      expect(quizzes.length).toBe(1);
      expect(quizzes[0].title).toBe('Angular Test');
    });

    const req = httpMock.expectOne('/api/quizzes');
    expect(req.request.method).toBe('GET');
    req.flush(mockQuizzes);
  });

  it('should submit quiz attempt via POST /api/quizattempts', () => {
    const mockAttempt = {
      quizId: 'q1',
      quizTitle: 'Angular Test',
      categoryName: 'Angular Framework',
      userName: 'Test User',
      totalQuestions: 5,
      correctAnswersCount: 4,
      scorePercentage: 80,
      totalTimeSpentSeconds: 100,
      userAnswers: []
    };

    service.submitAttempt(mockAttempt).subscribe(res => {
      expect(res).toBeTruthy();
    });

    const req = httpMock.expectOne('/api/quizattempts');
    expect(req.request.method).toBe('POST');
    req.flush({ ...mockAttempt, id: 'att-123' });
  });
});
