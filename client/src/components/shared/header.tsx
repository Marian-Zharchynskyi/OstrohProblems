import { Link, useLocation } from 'react-router-dom'
import { LogOut, User, Users, UserCircle } from 'lucide-react'
import { cn } from '@/lib/utils'
import { useAuth } from '@/contexts/auth-context'

const publicNavItems = [
  { path: '/', label: 'Головна' },
  { path: '/about', label: 'Про нас' },
  { path: '/contact', label: 'Контакти' },
]

const protectedNavItems = [
  { path: '/dashboard', label: 'Головна' },
  { path: '/map', label: 'Карта' },
  { path: '/problems', label: 'Проблеми' },
  { path: '/categories', label: 'Категорії' },
  { path: '/statuses', label: 'Статуси' },
  { path: '/comments', label: 'Коментарі' },
  { path: '/ratings', label: 'Рейтинги' },
]

export const Header = () => {
  const location = useLocation()
  const { isAuthenticated, user, signOut } = useAuth()

  const navItems = isAuthenticated ? protectedNavItems : publicNavItems

  return (
    <header className="border-b bg-white">
      <div className="container mx-auto px-4 py-4">
        <nav className="flex items-center justify-between">
          <Link to="/" className="text-2xl font-bold text-primary font-heading">
            Острог разом
          </Link>
          <div className="flex items-center gap-6">
            <ul className="flex gap-6">
              {navItems.map((item) => (
                <li key={item.path}>
                  <Link
                    to={item.path}
                    className={cn(
                      'text-sm font-medium transition-colors hover:text-primary',
                      location.pathname === item.path
                        ? 'text-primary'
                        : 'text-muted-foreground'
                    )}
                  >
                    {item.label}
                  </Link>
                </li>
              ))}
            </ul>
            {isAuthenticated ? (
              <div className="flex items-center gap-4">
                <Link
                  to="/profile"
                  className={cn(
                    'flex items-center gap-2 text-sm font-medium transition-colors hover:text-primary',
                    location.pathname === '/profile'
                      ? 'text-primary'
                      : 'text-muted-foreground'
                  )}
                >
                  <UserCircle className="h-4 w-4" />
                  Профіль
                </Link>
                {user?.roles?.includes('Administrator') && (
                  <Link
                    to="/admin/users"
                    className={cn(
                      'flex items-center gap-2 text-sm font-medium transition-colors hover:text-primary',
                      location.pathname === '/admin/users'
                        ? 'text-primary'
                        : 'text-muted-foreground'
                    )}
                  >
                    <Users className="h-4 w-4" />
                    Користувачі
                  </Link>
                )}
                <div className="flex items-center gap-2 text-sm text-muted-foreground">
                  <User className="h-4 w-4" />
                  <span>{user?.email}</span>
                </div>
                <button
                  onClick={signOut}
                  className="flex items-center gap-2 text-sm font-medium text-muted-foreground hover:text-primary transition-colors"
                >
                  <LogOut className="h-4 w-4" />
                  Вийти
                </button>
              </div>
            ) : (
              <div className="flex items-center gap-4">
                <Link
                  to="/login"
                  className="text-sm font-medium text-muted-foreground hover:text-primary transition-colors"
                >
                  Увійти
                </Link>
                <Link
                  to="/register"
                  className="text-sm font-medium bg-primary text-primary-foreground px-4 py-2 rounded-md hover:bg-primary/90 transition-colors"
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
