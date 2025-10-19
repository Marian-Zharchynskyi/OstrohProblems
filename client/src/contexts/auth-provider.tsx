import { useState, useEffect, type ReactNode } from 'react'
import { AuthContext } from './auth-context'
import { authService } from '@/services/auth.service'
import { tokenStorage } from '@/lib/token-storage'
import { decodeToken, isTokenExpired } from '@/lib/jwt-utils'
import type { User, JwtTokens } from '@/types/auth'

interface AuthProviderProps {
  children: ReactNode
}

export function AuthProvider({ children }: AuthProviderProps) {
  const [user, setUser] = useState<User | null>(null)
  const [tokens, setTokens] = useState<JwtTokens | null>(null)
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    // Check for existing tokens on mount
    const initAuth = async () => {
      const storedTokens = tokenStorage.getTokens()

      if (storedTokens) {
        // Check if access token is expired
        if (isTokenExpired(storedTokens.accessToken)) {
          // Try to refresh
          try {
            const newTokens = await authService.refreshToken(storedTokens)
            tokenStorage.setTokens(newTokens)
            setTokens(newTokens)

            const decodedUser = decodeToken(newTokens.accessToken)
            setUser(decodedUser)
          } catch {
            // Refresh failed, clear tokens
            tokenStorage.clearTokens()
            setTokens(null)
            setUser(null)
          }
        } else {
          // Token is still valid
          setTokens(storedTokens)
          const decodedUser = decodeToken(storedTokens.accessToken)
          setUser(decodedUser)
        }
      }

      setIsLoading(false)
    }

    initAuth()
  }, [])

  const signIn = async (email: string, password: string) => {
    const newTokens = await authService.signIn({ email, password })
    tokenStorage.setTokens(newTokens)
    setTokens(newTokens)

    const decodedUser = decodeToken(newTokens.accessToken)
    setUser(decodedUser)
  }

  const signUp = async (email: string, password: string, name?: string) => {
    const newTokens = await authService.signUp({ email, password, name })
    tokenStorage.setTokens(newTokens)
    setTokens(newTokens)

    const decodedUser = decodeToken(newTokens.accessToken)
    setUser(decodedUser)
  }

  const signOut = () => {
    tokenStorage.clearTokens()
    setTokens(null)
    setUser(null)
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        tokens,
        isAuthenticated: !!user,
        isLoading,
        signIn,
        signUp,
        signOut,
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}
