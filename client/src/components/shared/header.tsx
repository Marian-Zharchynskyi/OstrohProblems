import { Link, useLocation } from 'react-router-dom'
import { LogOut, UserCircle, Plus } from 'lucide-react'
import { cn } from '@/lib/utils'
import { useAuth } from '@/contexts/auth-context'
import { NotificationsBell } from '@/components/notifications/notifications-bell'
import { designSystem } from '@/lib/design-system'

const publicNavItems = [
  { path: '/map', label: 'Всі проблеми' },
]

const adminNavItems = [
  { path: '/dashboard', label: 'Головна' },
  { path: '/problems', label: 'Проблеми' },
  { path: '/comments', label: 'Коментарі' },
  { path: '/ratings', label: 'Рейтинги' },
  { path: '/admin/users', label: 'Користувачі' },
]

const coordinatorNavItems = [
  { path: '/coordinator', label: 'Панель координатора' },
]

const userNavItems = [
  { path: '/map', label: 'Всі проблеми' },
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
            Острог Разом
          </Link>

          {/* Center Navigation */}
          {navItems.length > 0 && (
            <ul className="hidden md:flex items-center gap-2">
              {navItems.map((item) => {
                const isActive = location.pathname === item.path
                return (
                  <li key={item.path}>
                    <Link
                      to={item.path}
                      className={cn(
                        'text-sm font-heading font-semibold px-5 py-2.5 rounded-full transition-all duration-200',
                        isActive
                          ? 'bg-black text-white shadow-md transform scale-105'
                          : 'text-black/70 hover:text-black hover:bg-black/5'
                      )}
                    >
                      {item.label}
                    </Link>
                  </li>
                )
              })}
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
                      className="hidden sm:flex items-center gap-2 text-sm font-medium bg-primary text-primary-foreground px-5 py-2.5 rounded-full hover:bg-primary/90 transition-all shadow-sm active:scale-95"
                    >
                      <Plus className="h-4 w-4" />
                      Подати проблему
                    </Link>

                    <NotificationsBell />
                  </>
                )}

                {!isCoordinator && !isAdmin && (
                  <Link
                    to="/profile"
                    className="flex items-center justify-center w-10 h-10 rounded-full hover:bg-black/5 transition-colors text-black/70 hover:text-black"
                    title="Профіль"
                  >
                    <UserCircle className="w-6 h-6" strokeWidth={2} />
                  </Link>
                )}

                <button
                  onClick={signOut}
                  className="group flex items-center justify-center w-10 h-10 p-0 bg-transparent rounded-full hover:bg-red-50 transition-all duration-200"
                  title="Вийти"
                >
                  <LogOut className="w-5 h-5 text-black/70 group-hover:text-red-600 transition-colors" strokeWidth={2} />
                </button>
              </>
            ) : (
              // Unauthenticated Actions
              <div className="flex items-center gap-2">
                <Link
                  to="/login"
                  className="text-sm font-heading font-semibold text-black hover:bg-black/5 px-5 py-2.5 rounded-full transition-all"
                >
                  Увійти
                </Link>
                <Link
                  to="/register"
                  className="text-sm font-heading font-semibold bg-black text-white px-5 py-2.5 rounded-full hover:bg-black/80 transition-all shadow-md hover:shadow-lg active:scale-95"
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
