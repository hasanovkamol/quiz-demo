export type QuizCategory = string;
export type Difficulty = 'Oson' | 'O\'rta' | 'Qiyin';

export interface CategoryItem {
  id: string;
  name: string;
  iconName?: string;
  description?: string;
}

export interface AiSingleQuestionRequest {
  topic: string;
  category?: string;
  difficulty: Difficulty;
  apiKey?: string;
}


export interface QuestionOption {
  id: string;
  questionId?: string;
  text: string;
}

export interface Question {
  id: string;
  quizId?: string;
  text: string;
  codeSnippet?: string;
  correctOptionId: string;
  explanation: string;
  options: QuestionOption[];
  isCodeQuestion?: boolean;
  initialCodeTemplate?: string;
  expectedOutput?: string;
}

export interface Quiz {
  id: string;
  title: string;
  category: string;
  categoryName: string;
  description: string;
  iconName?: string;
  difficulty: Difficulty;
  timeLimitSeconds: number;
  isCustom?: boolean;
  questionsCount?: number;
  questions: Question[];
}

export interface UserAnswer {
  questionId: string;
  selectedOptionId: string;
  isCorrect?: boolean;
  timeSpentSeconds?: number;
  isCodeAnswer?: boolean;
  submittedCode?: string;
}

export interface QuizAttempt {
  id?: string;
  quizId: string;
  quizTitle: string;
  categoryName: string;
  userName: string;
  totalQuestions: number;
  correctAnswersCount: number;
  scorePercentage: number;
  totalTimeSpentSeconds: number;
  completedAt?: string;
  cheatingWarningsCount?: number;
  cheatingDetected?: boolean;
  userAnswers: UserAnswer[];
}

export interface QuizResult {
  id?: string;
  quizId: string;
  quizTitle: string;
  categoryName: string;
  userName?: string;
  totalQuestions: number;
  correctAnswersCount: number;
  scorePercentage: number;
  totalTimeSpentSeconds: number;
  userAnswers: UserAnswer[];
  completedAt: string;
}
