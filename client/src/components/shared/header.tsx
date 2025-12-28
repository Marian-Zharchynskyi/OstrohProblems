import { Link, useLocation } from 'react-router-dom'
import { LogOut, Users, UserCircle, Plus } from 'lucide-react'
import { cn } from '@/lib/utils'
import { useAuth } from '@/contexts/auth-context'
import { NotificationsBell } from '@/components/notifications/notifications-bell'
import { designSystem } from '@/lib/design-system'

// Filtered nav items (Removed Home, About, Contact as requested)
const publicNavItems = [
  { path: '/map', label: 'Карта' },
]

const adminNavItems = [
  { path: '/dashboard', label: 'Головна' },
  { path: '/map', label: 'Карта' },
  { path: '/problems', label: 'Проблеми' },
  { path: '/comments', label: 'Коментарі' },
  { path: '/ratings', label: 'Рейтинги' },
]

const coordinatorNavItems = [
  { path: '/coordinator', label: 'Панель координатора' },
]

const userNavItems = [
  { path: '/map', label: 'Карта' },
]

export const Header = () => {
  const location = useLocation()
  const { isAuthenticated, user, signOut } = useAuth()

  const isAdmin = user?.roles?.includes('Administrator')
  const isCoordinator = user?.roles?.includes('Coordinator')
  const isUser = user?.roles?.includes('User')

  const getNavItems = () => {
    if (!isAuthenticated) return publicNavItems
    if (isAdmin) return adminNavItems
    if (isCoordinator) return coordinatorNavItems
    return userNavItems
  }

  const navItems = getNavItems()
  const logoLink = isAuthenticated && !isUser ? (isAdmin ? '/dashboard' : '/coordinator') : '/'

  return (
    <header className="py-4 bg-transparent">
      <div className="container mx-auto px-4">
        <nav
          className="flex items-center justify-between px-6 py-4 rounded-[30px] shadow-[0_4px_12px_rgba(0,0,0,0.25)]"
          style={{
            backgroundColor: designSystem.colors.header.background,
          }}
        >
          {/* Logo */}
          <Link
            to={logoLink}
            className="text-xl font-heading font-semibold text-black"
          >
            OstrohBetter
          </Link>

          {/* Center Navigation */}
          {navItems.length > 0 && (
            <ul className="hidden md:flex gap-8">
              {navItems.map((item) => (
                <li key={item.path}>
                  <Link
                    to={item.path}
                    className={cn(
                      'text-sm font-heading font-semibold transition-colors hover:text-primary',
                      location.pathname === item.path
                        ? 'text-black'
                        : 'text-black/80'
                    )}
                  >
                    {item.label}
                  </Link>
                </li>
              ))}
            </ul>
          )}

          {/* Right Side Actions */}
          <div className="flex items-center gap-4">
            {isAuthenticated ? (
              <>
                {/* Regular User Actions */}
                {isUser && (
                  <>
                    <Link
                      to="/problems/create"
                      className="hidden sm:flex items-center gap-2 text-sm font-medium bg-primary text-primary-foreground px-4 py-2 rounded-[20px] hover:bg-primary/90 transition-colors"
                    >
                      <Plus className="h-4 w-4" />
                      Подати проблему
                    </Link>

                    <NotificationsBell />
                  </>
                )}

                {!isCoordinator && (
                  <Link
                    to="/profile"
                    className="flex items-center justify-center w-10 h-10 rounded-full hover:bg-black/5 transition-colors"
                    title="Профіль"
                  >
                    <UserCircle className="w-5 h-5 text-black" strokeWidth={2} />
                  </Link>
                )}

                {isAdmin && (
                  <Link
                    to="/admin/users"
                    className="flex items-center justify-center w-10 h-10 rounded-full hover:bg-black/5 transition-colors"
                    title="Користувачі"
                  >
                    <Users className="w-5 h-5 text-black" strokeWidth={2} />
                  </Link>
                )}

                <button
                  onClick={signOut}
                  className="flex items-center justify-center w-10 h-10 rounded-full hover:bg-black/5 transition-colors text-black"
                  title="Вийти"
                >
                  <LogOut className="w-5 h-5" strokeWidth={2} />
                </button>
              </>
            ) : (
              // Unauthenticated Actions
              <div className="flex items-center gap-4">
                <Link
                  to="/login"
                  className="text-sm font-heading font-semibold text-black hover:text-primary transition-colors"
                >
                  Увійти
                </Link>
                <Link
                  to="/register"
                  className="text-sm font-heading font-semibold bg-black text-white px-4 py-2 rounded-[20px] hover:bg-black/80 transition-colors"
                >
                  Реєстрація
                </Link>
              </div>
            )}
          </div>
        </nav>
      </div>
    </header>
  )
}
