import { useState, useRef, useEffect } from 'react'
import { Bell } from 'lucide-react'
import { useSignalR } from '@/contexts/use-signalr'
import { useNavigate } from 'react-router-dom'
import { cn } from '@/lib/utils'

export function NotificationsBell() {
  const { notifications, unreadCount, markAsRead, markAllAsRead, clearNotifications } = useSignalR()
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
      case 'completed':
        return '✅'
      case 'state_updated':
        return '📝'
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
        className="relative flex items-center justify-center w-10 h-10 p-0 bg-transparent rounded-full hover:bg-black/5 transition-colors text-black/70 hover:text-black border-none outline-none focus:outline-none focus-visible:ring-0"
        aria-label="Нотифікації"
      >
        <Bell className="h-6 w-6" strokeWidth={2} />
        {unreadCount > 0 && (
          <span className="absolute top-2 right-2 flex h-2.5 w-2.5 items-center justify-center rounded-full bg-red-500 ring-2 ring-white">
            <span className="sr-only">{unreadCount} unread</span>
          </span>
        )}
      </button>

      {isOpen && (
        <div className="absolute right-0 mt-2 w-80 max-h-96 overflow-y-auto rounded-lg border border-gray-200 bg-white shadow-lg z-50">
          <div className="sticky top-0 bg-white border-b border-gray-200 px-4 py-3 z-10">
            <h3 className="font-semibold text-gray-900">Нотифікації</h3>
          </div>

          {notifications.length === 0 ? (
            <div className="px-4 py-8 text-center text-sm text-gray-500">
              Немає нових нотифікацій
            </div>
          ) : (
            <div className="divide-y divide-gray-100">
              {notifications.slice(0, 10).map((notification) => (
                <button
                  key={notification.id}
                  onClick={() => handleNotificationClick(notification.id, notification.problemId)}
                  className={cn(
                    'w-full px-4 py-3 text-left transition-colors duration-150 border-none outline-none focus:outline-none',
                    !notification.isRead
                      ? 'bg-blue-50 hover:bg-blue-100'
                      : 'bg-white hover:bg-gray-50'
                  )}
                >
                  <div className="flex items-start gap-3">
                    <span className="text-xl flex-shrink-0">{getNotificationIcon(notification.type)}</span>
                    <div className="flex-1 min-w-0">
                      <p className="text-sm text-gray-900 font-medium break-words whitespace-normal">
                        {notification.message}
                      </p>
                      <p className="text-xs text-gray-500 mt-1" title={notification.problemTitle}>
                        {notification.problemTitle && notification.problemTitle.length > 50
                          ? `${notification.problemTitle.substring(0, 50)}...`
                          : notification.problemTitle}
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
            <div className="border-t border-gray-100 p-2 bg-gray-50/50 space-y-2">
              <button
                onClick={markAllAsRead}
                className="w-full flex items-center justify-center gap-2 py-2 text-sm font-medium text-[#1F2732] bg-white border border-[#D0D5DD] rounded-md hover:bg-[#F5F5F5] transition-all focus:outline-none focus:ring-0 active:scale-[0.98]"
              >
                Позначити всі як прочитані
              </button>
              <button
                onClick={() => {
                  clearNotifications()
                  setIsOpen(false)
                }}
                className="w-full flex items-center justify-center gap-2 py-2 text-sm font-medium text-[#E42556] bg-white border border-[#E42556] rounded-md hover:bg-[#E42556]/10 transition-all focus:outline-none focus:ring-0 active:scale-[0.98]"
              >
                Очистити всі нотифікації
              </button>
            </div>
          )}
        </div>
      )}
    </div>
  )
}
