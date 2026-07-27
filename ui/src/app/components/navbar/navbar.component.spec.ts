import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NavbarComponent } from './navbar.component';
import { QuizService } from '../../services/quiz.service';
import { AuthService } from '../../services/auth.service';

describe('NavbarComponent Component Tests', () => {
  let component: NavbarComponent;
  let fixture: ComponentFixture<NavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavbarComponent, HttpClientTestingModule],
      providers: [QuizService, AuthService]
    }).compileComponents();

    fixture = TestBed.createComponent(NavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create NavbarComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should render brand logo text QuizMaster PRO', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.font-extrabold')?.textContent).toContain('QuizMaster');
  });

  it('should trigger goHome and reset quiz state on logo click', () => {
    const resetSpy = vi.spyOn(component.quizService, 'resetQuiz');
    component.goHome();
    expect(resetSpy).toHaveBeenCalled();
  });
});
