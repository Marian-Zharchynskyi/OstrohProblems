import axios from 'axios'
import type { SignInDto, SignUpDto, JwtTokens } from '@/types/auth'

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5146'

export const authService = {
  async signIn(credentials: SignInDto): Promise<JwtTokens> {
    const response = await axios.post<JwtTokens>(
      `${API_URL}/account/signin`,
      credentials
    )
    return response.data
  },

  async signUp(data: SignUpDto): Promise<JwtTokens> {
    const response = await axios.post<JwtTokens>(
      `${API_URL}/account/signup`,
      data
    )
    return response.data
  },

  async refreshToken(tokens: JwtTokens): Promise<JwtTokens> {
    const response = await axios.post<JwtTokens>(
      `${API_URL}/account/refresh-token`,
      tokens
    )
    return response.data
  },
}
