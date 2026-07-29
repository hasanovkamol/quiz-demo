import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { QuizPlayComponent } from './quiz-play.component';
import { QuizService } from '../../services/quiz.service';

describe('QuizPlayComponent Anti-Cheating & Play Tests', () => {
  let component: QuizPlayComponent;
  let fixture: ComponentFixture<QuizPlayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuizPlayComponent, HttpClientTestingModule],
      providers: [QuizService]
    }).compileComponents();

    fixture = TestBed.createComponent(QuizPlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create QuizPlayComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should return correct option letter A, B, C, D', () => {
    expect(component.getOptionLetter(0)).toBe('A');
    expect(component.getOptionLetter(1)).toBe('B');
    expect(component.getOptionLetter(2)).toBe('C');
  });

  it('should toggle showConfirmExit signal', () => {
    component.showConfirmExit.set(true);
    expect(component.showConfirmExit()).toBe(true);

    component.showConfirmExit.set(false);
    expect(component.showConfirmExit()).toBe(false);
  });

  it('should trigger violation toast on copy event', () => {
    const copyEvent = new Event('copy') as unknown as ClipboardEvent;
    const preventSpy = vi.spyOn(copyEvent, 'preventDefault');

    component.onCopyCutAttempt(copyEvent);

    expect(preventSpy).toHaveBeenCalled();
    expect(component.showViolationToast()).toBe(true);
    expect(component.violationMessage()).toContain('nusxalash');
  });
});
