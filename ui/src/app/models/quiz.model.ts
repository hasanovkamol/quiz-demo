export type QuizCategory = 'angular' | 'dotnet' | 'webdev' | 'custom';
export type Difficulty = 'Oson' | 'O\'rta' | 'Qiyin';

export interface QuestionOption {
  id: string;
  text: string;
}

export interface Question {
  id: string;
  text: string;
  codeSnippet?: string;
  options: QuestionOption[];
  correctOptionId: string;
  explanation: string;
}

export interface Quiz {
  id: string;
  title: string;
  category: QuizCategory;
  categoryName: string;
  description: string;
  iconName: string;
  difficulty: Difficulty;
  timeLimitSeconds: number;
  questions: Question[];
  isCustom?: boolean;
}

export interface UserAnswer {
  questionId: string;
  selectedOptionId: string | null;
  isCorrect: boolean;
  timeSpentSeconds: number;
}

export interface QuizResult {
  id: string;
  quizId: string;
  quizTitle: string;
  categoryName: string;
  totalQuestions: number;
  correctAnswersCount: number;
  scorePercentage: number;
  totalTimeSpentSeconds: number;
  completedAt: string;
  userAnswers: UserAnswer[];
}

export interface QuizAttempt {
  id: string;
  quizId: string;
  quizTitle: string;
  categoryName: string;
  userName: string;
  totalQuestions: number;
  correctAnswersCount: number;
  scorePercentage: number;
  totalTimeSpentSeconds: number;
  completedAt: string;
  userAnswers: UserAnswer[];
}
