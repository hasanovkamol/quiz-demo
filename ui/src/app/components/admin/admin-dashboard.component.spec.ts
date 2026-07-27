import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AdminDashboardComponent } from './admin-dashboard.component';
import { QuizApiService } from '../../services/quiz-api.service';
import { QuizService } from '../../services/quiz.service';

describe('AdminDashboardComponent', () => {
  let component: AdminDashboardComponent;
  let fixture: ComponentFixture<AdminDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminDashboardComponent, HttpClientTestingModule],
      providers: [QuizApiService, QuizService]
    }).compileComponents();

    fixture = TestBed.createComponent(AdminDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create AdminDashboardComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should switch active tab', () => {
    component.activeTab.set('ai-generator');
    expect(component.activeTab()).toBe('ai-generator');

    component.activeTab.set('attempts');
    expect(component.activeTab()).toBe('attempts');
  });
});
