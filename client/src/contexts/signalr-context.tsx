import { useEffect, useState, useCallback, useRef, type ReactNode } from 'react'
import { signalRService } from '@/services/signalr-service'
import { useAuth } from './auth-context'
import type { Notification, Comment } from '@/types'
import { SignalRContext } from './signalr-context'

export function SignalRProvider({ children }: { children: ReactNode }) {
  const { user, tokens } = useAuth()
  const [isConnected, setIsConnected] = useState(false)
  const [notifications, setNotifications] = useState<Notification[]>([])
  const problemsUpdatedCallbackRef = useRef<(() => void) | null>(null)

  const isCoordinatorOrAdmin = user?.roles?.some(
    (role) => role === 'Coordinator' || role === 'Administrator'
  )

  useEffect(() => {
    if (user && tokens?.accessToken) {
      const initializeConnections = async () => {
        try {
          // Initialize both hubs
          await signalRService.initializeCommentsHub(tokens.accessToken)
          await signalRService.initializeNotificationsHub(tokens.accessToken)

          // Join user group for notifications
          if (!isCoordinatorOrAdmin && user.id) {
            await signalRService.joinUserGroup(user.id)
          }
          // Subscribe to notifications (only for regular users)
          signalRService.onNotificationReceived((notification) => {
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
  }, [user, tokens, isCoordinatorOrAdmin])

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

  const clearNotifications = useCallback(() => {
    setNotifications([])
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
        clearNotifications,
      }}
    >
      {children}
    </SignalRContext.Provider>
  )
}
