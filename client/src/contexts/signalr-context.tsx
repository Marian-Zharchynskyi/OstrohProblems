import { useEffect, useState, type ReactNode } from 'react'
import { signalRService } from '@/services/signalr-service'
import { useAuth } from './auth-context'
import type { Notification } from '@/types'
import { SignalRContext } from './signalr-context'

export function SignalRProvider({ children }: { children: ReactNode }) {
  const { user, tokens } = useAuth()
  const [isConnected, setIsConnected] = useState(false)
  const [notifications, setNotifications] = useState<Notification[]>([])

  useEffect(() => {
    if (user && tokens?.accessToken) {
      const initializeConnections = async () => {
        try {
          // Initialize both hubs
          await signalRService.initializeCommentsHub(tokens.accessToken)
          await signalRService.initializeNotificationsHub(tokens.accessToken)

          // Join user group for notifications
          if (user.id) {
            await signalRService.joinUserGroup(user.id)
          }
          // Subscribe to notifications
          signalRService.onNotificationReceived((notification) => {
            setNotifications((prev) => [notification, ...prev])
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
  }, [user, tokens])

  const joinProblemGroup = async (problemId: string) => {
    await signalRService.joinProblemGroup(problemId)
  }

  const leaveProblemGroup = async (problemId: string) => {
    await signalRService.leaveProblemGroup(problemId)
  }

  // We expose a subscribe method via context value
  const onCommentReceived = signalRService.onCommentReceived.bind(signalRService)

  const markAsRead = (notificationId: string) => {
    setNotifications((prev) =>
      prev.map((n) => (n.id === notificationId ? { ...n, isRead: true } : n))
    )
  }

  const clearNotifications = () => {
    setNotifications([])
  }

  const unreadCount = notifications.filter((n) => !n.isRead).length

  return (
    <SignalRContext.Provider
      value={{
        isConnected,
        joinProblemGroup,
        leaveProblemGroup,
        onCommentReceived,
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
