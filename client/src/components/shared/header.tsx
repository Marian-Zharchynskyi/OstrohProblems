import { useState, useRef, useEffect } from 'react'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { LogOut, Plus, ChevronDown, User as UserIcon, FileText } from 'lucide-react'
import { cn } from '@/lib/utils'
import { useAuth } from '@/contexts/auth-context'
import { NotificationsBell } from '@/components/notifications/notifications-bell'
import { designSystem } from '@/lib/design-system'
import { useQuery } from '@tanstack/react-query'
import { userService } from '@/services/user.service'
import { tokenStorage } from '@/lib/token-storage'

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
  const navigate = useNavigate()
  const { isAuthenticated, user, signOut } = useAuth()
  const [isDropdownOpen, setIsDropdownOpen] = useState(false)
  const dropdownRef = useRef<HTMLDivElement>(null)

  // Fetch full user details to get avatar
  const { data: userProfile } = useQuery({
    queryKey: ['currentUser', user?.id],
    queryFn: async () => {
      const token = tokenStorage.getAccessToken()
      if (!token) return null
      return userService.getCurrentUser(token)
    },
    enabled: !!isAuthenticated && !!user?.id
  })

  // Close dropdown when clicking outside
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsDropdownOpen(false)
      }
    }
    document.addEventListener('mousedown', handleClickOutside)
    return () => document.removeEventListener('mousedown', handleClickOutside)
  }, [])

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

  const userRoleLabel = isAdmin ? 'Адміністратор' : isCoordinator ? 'Координатор' : 'Користувач'
  
  const getDisplayName = () => {
    const name = userProfile?.name
      ? `${userProfile.name} ${userProfile.surname || ''}`
      : user?.name || user?.email || 'Користувач'
    return String(name).trim()
  }

  const userDisplayName = getDisplayName()

  const userAvatarUrl = userProfile?.image?.url

  return (
    <header className="py-4 bg-transparent relative z-40">
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

                {/* Profile Dropdown */}
                <div className="relative" ref={dropdownRef}>
                  <button
                    onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                    className="flex items-center gap-2 pl-2 pr-1 py-1 rounded-full hover:bg-black/5 transition-all text-left outline-none focus:outline-none bg-transparent border-none"
                  >
                    <div className="w-10 h-10 rounded-full overflow-hidden border border-gray-200 bg-gray-100 flex items-center justify-center">
                      {userAvatarUrl ? (
                        <img src={userAvatarUrl} alt={userDisplayName} className="w-full h-full object-cover" />
                      ) : (
                        <div className="text-gray-500 font-bold text-lg">
                          {userDisplayName.charAt(0).toUpperCase()}
                        </div>
                      )}
                    </div>
                    <ChevronDown className={cn("w-4 h-4 text-gray-500 transition-transform duration-200", isDropdownOpen && "rotate-180")} />
                  </button>

                  {/* Dropdown Menu */}
                  {isDropdownOpen && (
                    <div className="absolute right-0 top-full mt-2 w-72 bg-white rounded-2xl shadow-[0_10px_40px_-10px_rgba(0,0,0,0.2)] border border-gray-100 p-2 overflow-hidden animate-in fade-in zoom-in-95 duration-200">
                      {/* User Info Header */}
                      <div className="px-4 py-3 border-b border-gray-100 mb-2">
                        <p className="font-bold text-gray-900 truncate">{userDisplayName}</p>
                        <p className="text-xs font-medium text-gray-500 uppercase tracking-wide mt-1">{userRoleLabel}</p>
                      </div>

                      {/* Menu Items */}
                      <div className="space-y-1">
                        {!isCoordinator && !isAdmin && (
                          <Link
                            to="/profile"
                            onClick={() => setIsDropdownOpen(false)}
                            className="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-gray-50 text-gray-700 transition-colors group"
                          >
                            <div className="w-8 h-8 rounded-full bg-gray-100 flex items-center justify-center text-gray-500 group-hover:bg-white group-hover:shadow-sm transition-all">
                              <UserIcon className="w-4 h-4" />
                            </div>
                            <span className="font-medium text-sm">Мій профіль</span>
                          </Link>
                        )}

                        {isUser && (
                          <Link
                            to="/problems/my"
                            onClick={() => setIsDropdownOpen(false)}
                            className="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-gray-50 text-gray-700 transition-colors group"
                          >
                            <div className="w-8 h-8 rounded-full bg-gray-100 flex items-center justify-center text-gray-500 group-hover:bg-white group-hover:shadow-sm transition-all">
                              <FileText className="w-4 h-4" />
                            </div>
                            <span className="font-medium text-sm">Подані проблеми</span>
                          </Link>
                        )}

                        <button
                          onClick={() => {
                            setIsDropdownOpen(false)
                            signOut(navigate)
                          }}
                          className="w-full flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-red-50 text-red-600 transition-colors group mt-2 outline-none focus:outline-none bg-transparent border-none"
                        >
                          <div className="w-8 h-8 rounded-full bg-red-100 flex items-center justify-center text-red-500 group-hover:bg-white group-hover:shadow-sm transition-all">
                            <LogOut className="w-4 h-4" />
                          </div>
                          <span className="font-medium text-sm">Вийти з акаунту</span>
                        </button>
                      </div>
                    </div>
                  )}
                </div>
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
