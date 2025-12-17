export interface SignInDto {
  email: string
  password: string
}

export interface SignUpDto {
  email: string
  password: string
  name?: string
}

export interface JwtTokens {
  accessToken: string
  refreshToken: string
}

export interface User {
  id: string
  email: string
  name?: string
  roles?: string[]
}

export interface AuthState {
  user: User | null
  tokens: JwtTokens | null
  isAuthenticated: boolean
  isLoading: boolean
}
