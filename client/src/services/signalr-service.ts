import * as signalR from '@microsoft/signalr'
import type { Comment, Notification } from '@/types'

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000'

class SignalRService {
  private commentsConnection: signalR.HubConnection | null = null
  private notificationsConnection: signalR.HubConnection | null = null

  // Initialize Comments Hub
  async initializeCommentsHub(token: string) {
    if (this.commentsConnection?.state === signalR.HubConnectionState.Connected) {
      return this.commentsConnection
    }

    this.commentsConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${API_URL}/hubs/comments`, {
        accessTokenFactory: () => token,
      })
      .withAutomaticReconnect()
      .build()

    try {
      await this.commentsConnection.start()
      console.log('Comments Hub connected')
    } catch (error) {
      console.error('Error connecting to Comments Hub:', error)
      throw error
    }

    return this.commentsConnection
  }

  // Initialize Notifications Hub
  async initializeNotificationsHub(token: string) {
    if (this.notificationsConnection?.state === signalR.HubConnectionState.Connected) {
      return this.notificationsConnection
    }

    this.notificationsConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${API_URL}/hubs/notifications`, {
        accessTokenFactory: () => token,
      })
      .withAutomaticReconnect()
      .build()

    try {
      await this.notificationsConnection.start()
      console.log('Notifications Hub connected')
    } catch (error) {
      console.error('Error connecting to Notifications Hub:', error)
      throw error
    }

    return this.notificationsConnection
  }

  // Join problem group to receive comments
  async joinProblemGroup(problemId: string) {
    if (this.commentsConnection?.state === signalR.HubConnectionState.Connected) {
      await this.commentsConnection.invoke('JoinProblemGroup', problemId)
    }
  }

  // Leave problem group
  async leaveProblemGroup(problemId: string) {
    if (this.commentsConnection?.state === signalR.HubConnectionState.Connected) {
      await this.commentsConnection.invoke('LeaveProblemGroup', problemId)
    }
  }

  // Join user group to receive notifications
  async joinUserGroup(userId: string) {
    if (this.notificationsConnection?.state === signalR.HubConnectionState.Connected) {
      await this.notificationsConnection.invoke('JoinUserGroup', userId)
    }
  }

  // Leave user group
  async leaveUserGroup(userId: string) {
    if (this.notificationsConnection?.state === signalR.HubConnectionState.Connected) {
      await this.notificationsConnection.invoke('LeaveUserGroup', userId)
    }
  }

  // Subscribe to comment events
  onCommentReceived(callback: (comment: Comment) => void) {
    if (this.commentsConnection) {
      this.commentsConnection.on('ReceiveComment', callback)
    }
  }

  // Subscribe to notification events
  onNotificationReceived(callback: (notification: Notification) => void) {
    if (this.notificationsConnection) {
      this.notificationsConnection.on('ReceiveNotification', callback)
    }
  }

  // Disconnect from hubs
  async disconnect() {
    if (this.commentsConnection) {
      await this.commentsConnection.stop()
      this.commentsConnection = null
    }
    if (this.notificationsConnection) {
      await this.notificationsConnection.stop()
      this.notificationsConnection = null
    }
  }

  // Get connection states
  getConnectionStates() {
    return {
      comments: this.commentsConnection?.state,
      notifications: this.notificationsConnection?.state,
    }
  }
}

export const signalRService = new SignalRService()
