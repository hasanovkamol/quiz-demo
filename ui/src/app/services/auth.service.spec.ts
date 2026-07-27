import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { AuthResponse } from '../models/user.model';

describe('AuthService Unit Tests', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    localStorage.clear();
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService]
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should authenticate user via googleLogin and update signals, permissions, and localStorage', () => {
    const mockResponse: AuthResponse = {
      token: 'jwt_mock_token_123',
      refreshToken: 'ref_mock_token_456',
      expiresInSeconds: 300,
      userId: 'user-guid-111',
      email: 'test@quizmaster.local',
      name: 'Test User',
      pictureUrl: '',
      role: 'User',
      permissions: ['quizzes:read', 'attempts:submit']
    };

    service.googleLogin('mock_id_token').subscribe((res) => {
      expect(res.token).toBe('jwt_mock_token_123');
      expect(service.currentUser()?.email).toBe('test@quizmaster.local');
      expect(service.token()).toBe('jwt_mock_token_123');
      expect(service.refreshToken()).toBe('ref_mock_token_456');
      expect(service.hasPermission('quizzes:read')).toBe(true);
      expect(service.hasPermission('admin:stats')).toBe(false);
    });

    const req = httpMock.expectOne('/api/auth/google-login');
    expect(req.request.method).toBe('POST');
    req.flush(mockResponse);
  });

  it('should refresh token via requestTokenRefresh', () => {
    const initialUser = {
      id: 'user-guid-111',
      email: 'test@quizmaster.local',
      name: 'Test User',
      role: 'User',
      permissions: ['quizzes:read']
    };
    service.currentUser.set(initialUser);
    service.refreshToken.set('old_refresh_token');

    const mockRefreshedResponse: AuthResponse = {
      token: 'new_jwt_token_999',
      refreshToken: 'new_refresh_token_999',
      expiresInSeconds: 300,
      userId: 'user-guid-111',
      email: 'test@quizmaster.local',
      name: 'Test User',
      role: 'User',
      permissions: ['quizzes:read', 'attempts:submit']
    };

    service.requestTokenRefresh().subscribe((res) => {
      expect(res?.token).toBe('new_jwt_token_999');
      expect(service.token()).toBe('new_jwt_token_999');
    });

    const req = httpMock.expectOne('/api/auth/refresh');
    expect(req.request.method).toBe('POST');
    req.flush(mockRefreshedResponse);
  });

  it('should clear signals and localStorage on logout', () => {
    service.currentUser.set({ id: '1', email: 'a@b.com', name: 'A', role: 'User', permissions: [] });
    service.token.set('token123');
    service.refreshToken.set('ref123');

    service.logout();

    expect(service.currentUser()).toBeNull();
    expect(service.token()).toBeNull();
    expect(service.refreshToken()).toBeNull();
    expect(localStorage.getItem('quizmaster_jwt_token')).toBeNull();
  });
});
