import { Navigate } from 'react-router-dom'
import { useAuth } from '@/contexts/auth-context'

interface ProtectedRouteProps {
  children: React.ReactNode
  /**
   * If specified, user must have at least one of these roles
   */
  allowedRoles?: string[]
  /**
   * Where to redirect when user is authenticated but has no required role
   */
  redirectTo?: string
}

export function ProtectedRoute({ children, allowedRoles, redirectTo = '/' }: ProtectedRouteProps) {
  const { isAuthenticated, isLoading, user } = useAuth()

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-center">
          <div className="inline-block animate-spin rounded-full h-12 w-12 border-b-2 border-indigo-600"></div>
          <p className="mt-4 text-gray-600">Завантаження...</p>
        </div>
      </div>
    )
  }

  if (!isAuthenticated) {
    return <Navigate to="/" replace />
  }

  if (allowedRoles && allowedRoles.length > 0) {
    const userRoles = user?.roles || []
    const hasRequiredRole = allowedRoles.some((role) => userRoles.includes(role))

    if (!hasRequiredRole) {
      return <Navigate to={redirectTo} replace />
    }
  }

  return <>{children}</>
}
