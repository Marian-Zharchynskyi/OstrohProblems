import { jwtDecode } from 'jwt-decode'
import type { User } from '@/types/auth'

interface JwtPayload {
  sub?: string
  email?: string
  name?: string
  role?: string | string[]
  exp?: number
}

export function decodeToken(token: string): User | null {
  try {
    const decoded = jwtDecode<JwtPayload>(token)

    return {
      id: decoded.sub || '',
      email: decoded.email || '',
      name: decoded.name,
      roles: Array.isArray(decoded.role)
        ? decoded.role
        : decoded.role
          ? [decoded.role]
          : [],
    }
  } catch {
    return null
  }
}

export function isTokenExpired(token: string): boolean {
  try {
    const decoded = jwtDecode<JwtPayload>(token)
    if (!decoded.exp) return true

    return decoded.exp * 1000 < Date.now()
  } catch {
    return true
  }
}
