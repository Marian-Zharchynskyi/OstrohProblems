import type { JwtTokens } from '@/types/auth'

const ACCESS_TOKEN_KEY = 'access_token'
const REFRESH_TOKEN_KEY = 'refresh_token'

export const tokenStorage = {
  getTokens(): JwtTokens | null {
    const accessToken = sessionStorage.getItem(ACCESS_TOKEN_KEY)
    const refreshToken = sessionStorage.getItem(REFRESH_TOKEN_KEY)

    if (!accessToken || !refreshToken) {
      return null
    }

    return { accessToken, refreshToken }
  },

  setTokens(tokens: JwtTokens): void {
    sessionStorage.setItem(ACCESS_TOKEN_KEY, tokens.accessToken)
    sessionStorage.setItem(REFRESH_TOKEN_KEY, tokens.refreshToken)
  },

  clearTokens(): void {
    sessionStorage.removeItem(ACCESS_TOKEN_KEY)
    sessionStorage.removeItem(REFRESH_TOKEN_KEY)
  },

  getAccessToken(): string | null {
    return sessionStorage.getItem(ACCESS_TOKEN_KEY)
  },
}
