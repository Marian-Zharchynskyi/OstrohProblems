import { ClerkProvider, useAuth as useClerkAuth, useUser, useClerk } from '@clerk/clerk-react'
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
  const { isSignedIn, getToken, isLoaded } = useClerkAuth()
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

        if (token) {
          try {
            const response = await axiosInstance.get('/users/current', {
              headers: {
                Authorization: `Bearer ${token}`
              }
            })

            // Get role from database response, not from Clerk metadata
            // The database has the correct role assigned
            // Note: API returns "Name" with capital N, not "name"
            const role = response.data.role?.Name || response.data.role?.name || 'User'

            const userData: User = {
              id: response.data.id,
              email: clerkUser.primaryEmailAddress?.emailAddress || '',
              name: clerkUser.firstName || undefined,
              roles: [role]
            }

            setUser(userData)
          } catch (error: unknown) {
            console.error('Failed to sync user:', error)
            setUser(null)
          }
        } else {
          setUser(null)
        }
      } else {
        setUser(null)
      }
      setIsLoading(false)
    }

    syncUser()
  }, [isSignedIn, clerkUser, getToken])

  const { signOut: clerkSignOut } = useClerk()

  const signIn = async () => {
    throw new Error('Use Clerk sign-in methods')
  }

  const signUp = async () => {
    throw new Error('Use Clerk sign-up methods')
  }

  const signOut = async (navigate?: (path: string) => void) => {
    await clerkSignOut()
    setUser(null)
    if (navigate) {
      navigate('/map')
    }
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
        // CRITICAL: Include Clerk's isLoaded to prevent redirects before auth state is ready
        isLoading: !isLoaded || isLoading || !!(isSignedIn && !user),
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
      signInUrl="/login"
      signUpUrl="/register"
    >
      <ClerkAuthWrapper>{children}</ClerkAuthWrapper>
    </ClerkProvider>
  )
}
