import { useState, useRef, useEffect } from 'react'
import { Bell } from 'lucide-react'
import { useSignalR } from '@/contexts/use-signalr'
import { useNavigate } from 'react-router-dom'
import { cn } from '@/lib/utils'

export function NotificationsBell() {
  const { notifications, unreadCount, markAsRead } = useSignalR()
  const [isOpen, setIsOpen] = useState(false)
  const dropdownRef = useRef<HTMLDivElement>(null)
  const navigate = useNavigate()

  // Close dropdown when clicking outside
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsOpen(false)
      }
    }

    document.addEventListener('mousedown', handleClickOutside)
    return () => document.removeEventListener('mousedown', handleClickOutside)
  }, [])

  const handleNotificationClick = (notificationId: string, problemId?: string) => {
    markAsRead(notificationId)
    setIsOpen(false)
    if (problemId) {
      navigate(`/problems/${problemId}`)
    } else {
      navigate('/my-problems')
    }
  }

  const getNotificationIcon = (type: string) => {
    switch (type) {
      case 'comment':
        return '💬'
      case 'status_change':
        return '🔄'
      case 'rejected':
        return '❌'
      case 'rating':
        return '⭐'
      case 'reply':
        return '↩️'
      default:
        return '🔔'
    }
  }

  const formatTime = (dateString: string) => {
    const date = new Date(dateString)
    const now = new Date()
    const diffInMinutes = Math.floor((now.getTime() - date.getTime()) / (1000 * 60))

    if (diffInMinutes < 1) return 'Щойно'
    if (diffInMinutes < 60) return `${diffInMinutes} хв тому`
    if (diffInMinutes < 1440) return `${Math.floor(diffInMinutes / 60)} год тому`
    return date.toLocaleDateString('uk-UA')
  }

  return (
    <div className="relative" ref={dropdownRef}>
      <button
        onClick={() => setIsOpen(!isOpen)}
        className="relative p-2 text-muted-foreground hover:text-primary transition-colors rounded-lg hover:bg-gray-100"
        aria-label="Нотифікації"
      >
        <Bell className="h-5 w-5" />
        {unreadCount > 0 && (
          <span className="absolute top-1 right-1 flex h-4 w-4 items-center justify-center rounded-full bg-red-500 text-[10px] font-bold text-white">
            {unreadCount > 9 ? '9+' : unreadCount}
          </span>
        )}
      </button>

      {isOpen && (
        <div className="absolute right-0 mt-2 w-80 max-h-96 overflow-y-auto rounded-lg border bg-white shadow-lg z-50">
          <div className="sticky top-0 bg-white border-b px-4 py-3">
            <h3 className="font-semibold text-gray-900">Нотифікації</h3>
          </div>

          {notifications.length === 0 ? (
            <div className="px-4 py-8 text-center text-sm text-gray-500">
              Немає нових нотифікацій
            </div>
          ) : (
            <div className="divide-y">
              {notifications.slice(0, 10).map((notification) => (
                <button
                  key={notification.id}
                  onClick={() => handleNotificationClick(notification.id, notification.problemId)}
                  className={cn(
                    'w-full px-4 py-3 text-left hover:bg-gray-50 transition-colors',
                    !notification.isRead && 'bg-blue-50'
                  )}
                >
                  <div className="flex items-start gap-3">
                    <span className="text-xl">{getNotificationIcon(notification.type)}</span>
                    <div className="flex-1 min-w-0">
                      <p className="text-sm text-gray-900 font-medium line-clamp-2">
                        {notification.message}
                      </p>
                      <p className="text-xs text-gray-500 mt-1 truncate">
                        {notification.problemTitle}
                      </p>
                      <p className="text-xs text-gray-400 mt-1">
                        {formatTime(notification.createdAt)}
                      </p>
                    </div>
                    {!notification.isRead && (
                      <div className="h-2 w-2 rounded-full bg-blue-500 flex-shrink-0 mt-1" />
                    )}
                  </div>
                </button>
              ))}
            </div>
          )}

          {notifications.length > 0 && (
            <div className="border-t px-4 py-2">
              <button
                onClick={() => {
                  setIsOpen(false)
                  navigate('/problems')
                }}
                className="text-sm text-primary hover:underline"
              >
                Переглянути всі
              </button>
            </div>
          )}
        </div>
      )}
    </div>
  )
}
