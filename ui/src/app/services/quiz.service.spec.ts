import { TestBed } from '@angular/core/testing';
import { QuizService } from './quiz.service';
import { QuizApiService } from './quiz-api.service';
import { of } from 'rxjs';
import { Quiz } from '../models/quiz.model';

describe('QuizService Unit Tests', () => {
  let service: QuizService;

  const mockQuizzes: Quiz[] = [
    {
      id: 'q1',
      title: 'Angular Signals Test',
      category: 'angular',
      categoryName: 'Angular Framework',
      description: 'Test Angular',
      iconName: 'code-2',
      difficulty: "O'rta",
      timeLimitSeconds: 300,
      questions: []
    },
    {
      id: 'q2',
      title: 'C# 12 Deep Dive',
      category: 'dotnet',
      categoryName: 'C# & .NET Core',
      description: 'Test .NET',
      iconName: 'cpu',
      difficulty: 'Qiyin',
      timeLimitSeconds: 360,
      questions: []
    }
  ];

  beforeEach(() => {
    const mockApiService = {
      getQuizzes: () => of(mockQuizzes),
      getCategories: () => of([]),
      createCategory: (c: any) => of(c),
      generateAiQuestion: () => of({}),
      addQuestionToQuiz: () => of({}),
      previewMarkdownQuiz: () => of({}),
      importMarkdownQuiz: () => of({}),
      submitAttempt: () => of({})
    };

    TestBed.configureTestingModule({
      providers: [
        QuizService,
        { provide: QuizApiService, useValue: mockApiService }
      ]
    });
    service = TestBed.inject(QuizService);
  });

  it('should be created and load initial quizzes', () => {
    expect(service).toBeTruthy();
    expect(service.quizzes().length).toBe(2);
  });

  it('should filter quizzes by activeCategory computed signal', () => {
    service.activeCategory.set('angular');
    expect(service.filteredQuizzes().length).toBe(1);
    expect(service.filteredQuizzes()[0].category).toBe('angular');

    service.activeCategory.set('all');
    expect(service.filteredQuizzes().length).toBe(2);
  });

  it('should correctly format timer seconds into MM:SS string', () => {
    service.timerSecondsLeft.set(125);
    expect(service.formattedTimer()).toBe('02:05');

    service.timerSecondsLeft.set(45);
    expect(service.formattedTimer()).toBe('00:45');
  });

  it('should start quiz and initialize active quiz state', () => {
    service.setUserName('Test User');
    service.startQuiz('q1');
    expect(service.currentQuiz()?.id).toBe('q1');
    expect(service.currentQuestionIndex()).toBe(0);
  });
});
