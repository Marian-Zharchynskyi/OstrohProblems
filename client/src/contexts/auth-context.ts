import { createContext, useContext } from 'react'
import type { User, JwtTokens } from '@/types/auth'

export interface AuthContextType {
  user: User | null
  tokens: JwtTokens | null
  isAuthenticated: boolean
  isLoading: boolean
  signIn: (email: string, password: string) => Promise<void>
  signUp: (email: string, password: string, name?: string) => Promise<void>
  signOut: () => void
}

export const AuthContext = createContext<AuthContextType | undefined>(
  undefined
)

export function useAuth() {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider')
  }
  return context
}
