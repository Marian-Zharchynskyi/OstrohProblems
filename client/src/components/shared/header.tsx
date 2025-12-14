import { Link, useLocation } from 'react-router-dom'
import { LogOut, User, Users, UserCircle, Plus } from 'lucide-react'
import { cn } from '@/lib/utils'
import { useAuth } from '@/contexts/auth-context'
import { NotificationsBell } from '@/components/notifications/notifications-bell'

const publicNavItems = [
  { path: '/', label: 'Головна' },
  { path: '/map', label: 'Карта' },
  { path: '/about', label: 'Про нас' },
  { path: '/contact', label: 'Контакти' },
]

// Меню для адміністратора - повний доступ
const adminNavItems = [
  { path: '/dashboard', label: 'Головна' },
  { path: '/map', label: 'Карта' },
  { path: '/problems', label: 'Проблеми' },
  { path: '/categories', label: 'Категорії' },
  { path: '/comments', label: 'Коментарі' },
  { path: '/ratings', label: 'Рейтинги' },
]

// Меню для координатора
const coordinatorNavItems = [
  { path: '/coordinator', label: 'Панель координатора' },
]

// Меню для звичайного користувача
const userNavItems = [
  { path: '/', label: 'Головна' },
  { path: '/map', label: 'Карта' },
  { path: '/about', label: 'Про нас' },
  { path: '/contact', label: 'Контакти' },
]

export const Header = () => {
  const location = useLocation()
  const { isAuthenticated, user, signOut } = useAuth()

  const isAdmin = user?.roles?.includes('Administrator')
  const isCoordinator = user?.roles?.includes('Coordinator')
  const isUser = user?.roles?.includes('User')

  // Визначаємо меню залежно від ролі
  const getNavItems = () => {
    if (!isAuthenticated) return publicNavItems
    if (isAdmin) return adminNavItems
    if (isCoordinator) return coordinatorNavItems
    return userNavItems
  }

  const navItems = getNavItems()

  // Визначаємо куди веде логотип
  const logoLink = isAuthenticated && !isUser ? (isAdmin ? '/dashboard' : '/coordinator') : '/'

  return (
    <header className="border-b bg-white">
      <div className="container mx-auto px-4 py-4">
        <nav className="flex items-center justify-between">
          <Link to={logoLink} className="text-2xl font-bold text-primary font-heading">
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
                {/* Кнопка "Почати" для користувача */}
                {isUser && (
                  <Link
                    to="/problems/create"
                    className="flex items-center gap-2 text-sm font-medium bg-primary text-primary-foreground px-4 py-2 rounded-md hover:bg-primary/90 transition-colors"
                  >
                    <Plus className="h-4 w-4" />
                    Почати
                  </Link>
                )}
                
                {/* Мої проблеми для користувача */}
                {isUser && (
                  <Link
                    to="/my-problems"
                    className={cn(
                      'text-sm font-medium transition-colors hover:text-primary',
                      location.pathname === '/my-problems'
                        ? 'text-primary'
                        : 'text-muted-foreground'
                    )}
                  >
                    Мої проблеми
                  </Link>
                )}

                <NotificationsBell />
                {!isCoordinator && (
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
                )}
                {isAdmin && (
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
