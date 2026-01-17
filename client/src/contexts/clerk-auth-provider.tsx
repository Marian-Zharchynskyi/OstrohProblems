import { ClerkProvider, useAuth as useClerkAuth, useUser } from '@clerk/clerk-react'
import { useState, useEffect, type ReactNode } from 'react'
import { AuthContext } from './auth-context'
import type { User } from '@/types/auth'
import { axiosInstance } from '@/lib/axios-instance'
import { apiClient } from '@/lib/api-client'
import { ukUA } from '@/lib/clerk-uk-UA'

interface ClerkAuthProviderProps {
  children: ReactNode
}

const PUBLISHABLE_KEY = import.meta.env.VITE_CLERK_PUBLISHABLE_KEY

if (!PUBLISHABLE_KEY) {
  throw new Error('Missing Clerk Publishable Key')
}

function ClerkAuthWrapper({ children }: ClerkAuthProviderProps) {
  const { isSignedIn, getToken } = useClerkAuth()
  const { user: clerkUser } = useUser()
  const [user, setUser] = useState<User | null>(null)

  // Set up token provider for API client
  useEffect(() => {
    apiClient.setTokenProvider(async () => {
      if (isSignedIn) {
        return await getToken()
      }
      return null
    })
  }, [isSignedIn, getToken])

  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    const syncUser = async () => {
      if (isSignedIn && clerkUser) {
        // Use the session token (not custom template) - this is validated by the API
        const token = await getToken()

        console.log('Clerk user signed in:', clerkUser.id)
        console.log('Token available:', !!token)

        if (token) {
          try {
            const response = await axiosInstance.get('/users/current', {
              headers: {
                Authorization: `Bearer ${token}`
              }
            })

            console.log('User sync successful:', response.data)
            console.log('Role from database:', response.data.role)

            // Get role from database response, not from Clerk metadata
            // The database has the correct role assigned
            // Note: API returns "Name" with capital N, not "name"
            const role = response.data.role?.Name || response.data.role?.name || 'User'
            console.log('Extracted role name:', role)

            const userData: User = {
              id: response.data.id,
              email: clerkUser.primaryEmailAddress?.emailAddress || '',
              name: clerkUser.firstName || undefined,
              roles: [role]
            }

            console.log('Final userData object:', userData)
            console.log('User roles array:', userData.roles)

            setUser(userData)
          } catch (error: unknown) {
            console.error('Failed to sync user:', error)
            // Log more details about the error
            if (error && typeof error === 'object' && 'response' in error) {
              const axiosError = error as { response?: { status?: number; data?: unknown } }
              console.error('Response status:', axiosError.response?.status)
              console.error('Response data:', axiosError.response?.data)
            }
            setUser(null)
          }
        } else {
          console.warn('No token available from Clerk')
          setUser(null)
        }
      } else {
        setUser(null)
      }
      setIsLoading(false)
    }

    syncUser()
  }, [isSignedIn, clerkUser, getToken])

  const signIn = async () => {
    throw new Error('Use Clerk sign-in methods')
  }

  const signUp = async () => {
    throw new Error('Use Clerk sign-up methods')
  }

  const signOut = () => {
    setUser(null)
  }

  // Provide a way to get the Clerk token for API calls
  const getClerkToken = async () => {
    if (isSignedIn) {
      return await getToken()
    }
    return null
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        tokens: null,
        isAuthenticated: !!user && (isSignedIn ?? false),
        isLoading: isLoading || !!(isSignedIn && !user),
        signIn,
        signUp,
        signOut,
        getClerkToken,
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export function ClerkAuthProvider({ children }: ClerkAuthProviderProps) {
  return (
    <ClerkProvider
      publishableKey={PUBLISHABLE_KEY}
      localization={ukUA}
    >
      <ClerkAuthWrapper>{children}</ClerkAuthWrapper>
    </ClerkProvider>
  )
}
