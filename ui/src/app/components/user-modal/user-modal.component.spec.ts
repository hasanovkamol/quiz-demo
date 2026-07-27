import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UserModalComponent } from './user-modal.component';
import { QuizService } from '../../services/quiz.service';
import { AuthService } from '../../services/auth.service';

describe('UserModalComponent', () => {
  let component: UserModalComponent;
  let fixture: ComponentFixture<UserModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserModalComponent, HttpClientTestingModule],
      providers: [QuizService, AuthService]
    }).compileComponents();

    fixture = TestBed.createComponent(UserModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create UserModalComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should confirm valid user name and update QuizService', () => {
    const quizService = TestBed.inject(QuizService);
    const spy = vi.spyOn(quizService, 'setUserName');

    component.userNameInput = 'Ali Valiyev';
    component.confirmName();

    expect(spy).toHaveBeenCalledWith('Ali Valiyev');
  });
});
