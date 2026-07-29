export interface User {
  id: string;
  email: string;
  name: string;
  pictureUrl?: string;
  role: string;
  permissions?: string[];
}

export interface AuthResponse {
  token: string;
  refreshToken: string;
  expiresInSeconds: number;
  userId: string;
  email: string;
  name: string;
  pictureUrl?: string;
  role: string;
  permissions: string[];
}
