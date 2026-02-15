import { useEffect, useState, useCallback, useRef, type ReactNode } from 'react'
import { signalRService } from '@/services/signalr-service'
import { useAuth } from './auth-context'
import type { Notification, Comment } from '@/types'
import { SignalRContext } from './signalr-context'

const NOTIFICATIONS_STORAGE_KEY = 'ostroh_notifications'

// Helper functions for localStorage
const loadNotificationsFromStorage = (): Notification[] => {
  try {
    const stored = localStorage.getItem(NOTIFICATIONS_STORAGE_KEY)
    return stored ? JSON.parse(stored) : []
  } catch (error) {
    console.error('Failed to load notifications from localStorage:', error)
    return []
  }
}

const saveNotificationsToStorage = (notifications: Notification[]) => {
  try {
    localStorage.setItem(NOTIFICATIONS_STORAGE_KEY, JSON.stringify(notifications))
  } catch (error) {
    console.error('Failed to save notifications to localStorage:', error)
  }
}

export function SignalRProvider({ children }: { children: ReactNode }) {
  const { user, getClerkToken } = useAuth()
  const [isConnected, setIsConnected] = useState(false)
  const [notifications, setNotifications] = useState<Notification[]>(() => loadNotificationsFromStorage())
  const problemsUpdatedCallbackRef = useRef<(() => void) | null>(null)

  const isCoordinatorOrAdmin = user?.roles?.some(
    (role) => role === 'Coordinator' || role === 'Administrator'
  )

  // Save notifications to localStorage whenever they change
  useEffect(() => {
    saveNotificationsToStorage(notifications)
  }, [notifications])

  useEffect(() => {
    if (user && getClerkToken) {
      const initializeConnections = async () => {
        try {
          // Get Clerk token
          const token = await getClerkToken()
          if (!token) {
            console.error('No Clerk token available')
            return
          }

          console.log('Initializing SignalR connections...')

          // Initialize both hubs
          await signalRService.initializeCommentsHub(token)
          await signalRService.initializeNotificationsHub(token)

          // Join user group for notifications
          if (user.id) {
            console.log('Joining user group:', user.id)
            await signalRService.joinUserGroup(user.id)
          }

          // Subscribe to notifications (only for regular users)
          signalRService.onNotificationReceived((notification) => {
            console.log('Notification received:', notification)
            if (!isCoordinatorOrAdmin) {
              setNotifications((prev) => [notification, ...prev])
            }
          })

          // Subscribe to refresh events (for Coordinator/Admin auto-refresh)
          signalRService.onRefreshProblemList(() => {
            if (problemsUpdatedCallbackRef.current) {
              problemsUpdatedCallbackRef.current()
            }
          })

          setIsConnected(true)
          console.log('SignalR connections established successfully')
        } catch (error) {
          console.error('Failed to initialize SignalR:', error)
          setIsConnected(false)
        }
      }

      initializeConnections()

      return () => {
        signalRService.disconnect()
        setIsConnected(false)
      }
    }
  }, [user, getClerkToken, isCoordinatorOrAdmin])

  const joinProblemGroup = useCallback(async (problemId: string) => {
    await signalRService.joinProblemGroup(problemId)
  }, [])

  const leaveProblemGroup = useCallback(async (problemId: string) => {
    await signalRService.leaveProblemGroup(problemId)
  }, [])

  // We expose a subscribe method via context value
  const onCommentReceived = useCallback(
    (callback: (comment: Comment) => void) => {
      signalRService.onCommentReceived(callback)
    },
    []
  )

  const onProblemsUpdated = useCallback(
    (callback: () => void) => {
      problemsUpdatedCallbackRef.current = callback
    },
    []
  )

  const markAsRead = useCallback((notificationId: string) => {
    setNotifications((prev) =>
      prev.map((n) => (n.id === notificationId ? { ...n, isRead: true } : n))
    )
  }, [])
  const markAllAsRead = useCallback(() => {
    setNotifications((prev) =>
      prev.map((n) => ({ ...n, isRead: true }))
    )
  }, [])

  const clearNotifications = useCallback(() => {
    setNotifications([])
    localStorage.removeItem(NOTIFICATIONS_STORAGE_KEY)
  }, [])

  const unreadCount = notifications.filter((n) => !n.isRead).length

  return (
    <SignalRContext.Provider
      value={{
        isConnected,
        joinProblemGroup,
        leaveProblemGroup,
        onCommentReceived,
        onProblemsUpdated,
        notifications,
        unreadCount,
        markAsRead,
        markAllAsRead,
        clearNotifications,
      }}
    >
      {children}
    </SignalRContext.Provider>
  )
}
