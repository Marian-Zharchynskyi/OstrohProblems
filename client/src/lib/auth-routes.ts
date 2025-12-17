import type { User } from '@/types/auth'

export function getDefaultRouteForUser(user: User | null | undefined): string {
  const roles = user?.roles ?? []

  if (roles.includes('Coordinator')) return '/coordinator'
  if (roles.includes('Administrator')) return '/dashboard'
  return '/'
}
