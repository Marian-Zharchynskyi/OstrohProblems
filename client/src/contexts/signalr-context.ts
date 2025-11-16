import { createContext } from 'react'
import type { Comment, Notification } from '@/types'

export interface SignalRContextType {
  isConnected: boolean
  joinProblemGroup: (problemId: string) => Promise<void>
  leaveProblemGroup: (problemId: string) => Promise<void>
  onCommentReceived: (callback: (comment: Comment) => void) => void
  notifications: Notification[]
  unreadCount: number
  markAsRead: (notificationId: string) => void
  clearNotifications: () => void
}

export const SignalRContext = createContext<SignalRContextType | undefined>(
  undefined
)
