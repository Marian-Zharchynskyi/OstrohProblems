import type { JwtTokens } from '@/types/auth'

const ACCESS_TOKEN_KEY = 'access_token'
const REFRESH_TOKEN_KEY = 'refresh_token'
const REMEMBER_ME_KEY = 'remember_me'

export const tokenStorage = {
  getTokens(): JwtTokens | null {
    const rememberMe = localStorage.getItem(REMEMBER_ME_KEY) === 'true'
    const storage = rememberMe ? localStorage : sessionStorage
    
    const accessToken = storage.getItem(ACCESS_TOKEN_KEY)
    const refreshToken = storage.getItem(REFRESH_TOKEN_KEY)

    if (!accessToken || !refreshToken) {
      return null
    }

    return { accessToken, refreshToken }
  },

  setTokens(tokens: JwtTokens, rememberMe: boolean = false): void {
    const storage = rememberMe ? localStorage : sessionStorage
    
    storage.setItem(ACCESS_TOKEN_KEY, tokens.accessToken)
    storage.setItem(REFRESH_TOKEN_KEY, tokens.refreshToken)
    
    if (rememberMe) {
      localStorage.setItem(REMEMBER_ME_KEY, 'true')
    } else {
      localStorage.removeItem(REMEMBER_ME_KEY)
    }
  },

  clearTokens(): void {
    sessionStorage.removeItem(ACCESS_TOKEN_KEY)
    sessionStorage.removeItem(REFRESH_TOKEN_KEY)
    localStorage.removeItem(ACCESS_TOKEN_KEY)
    localStorage.removeItem(REFRESH_TOKEN_KEY)
    localStorage.removeItem(REMEMBER_ME_KEY)
  },

  getAccessToken(): string | null {
    const rememberMe = localStorage.getItem(REMEMBER_ME_KEY) === 'true'
    const storage = rememberMe ? localStorage : sessionStorage
    return storage.getItem(ACCESS_TOKEN_KEY)
  },
}
