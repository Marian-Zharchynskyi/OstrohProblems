import { ClerkProvider, useAuth as useClerkAuth, useUser } from '@clerk/clerk-react'
import { useState, useEffect, type ReactNode } from 'react'
import { AuthContext } from './auth-context'
import type { User } from '@/types/auth'
import { axiosInstance } from '@/lib/axios-instance'
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
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    const syncUser = async () => {
      if (isSignedIn && clerkUser) {
        // Use the custom template to get the token with public_metadata
        const token = await getToken({ template: 'ostroh-problems-token' })
        
        if (token) {
          try {
            const response = await axiosInstance.get('/users/current', {
              headers: {
                Authorization: `Bearer ${token}`
              }
            })
            
            const role = (clerkUser.publicMetadata?.role as string) || 'User'
            
            const userData: User = {
              id: response.data.id,
              email: clerkUser.primaryEmailAddress?.emailAddress || '',
              name: clerkUser.firstName || undefined,
              roles: [role]
            }
            
            setUser(userData)
          } catch (error) {
            console.error('Failed to sync user:', error)
            setUser(null)
          }
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

  return (
    <AuthContext.Provider
      value={{
        user,
        tokens: null,
        isAuthenticated: !!user && (isSignedIn ?? false),
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
