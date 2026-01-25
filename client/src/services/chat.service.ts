import axios from 'axios'
import type { ChatMessageRequest, ChatMessageResponse } from '@/types/chat'

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5146'

export const chatService = {
  async sendMessage(
    request: ChatMessageRequest,
    token: string | null
  ): Promise<ChatMessageResponse> {
    const headers: Record<string, string> = {
      'Content-Type': 'application/json',
    }

    if (token) {
      headers['Authorization'] = `Bearer ${token}`
    }

    const response = await axios.post<ChatMessageResponse>(
      `${API_URL}/chat/message`,
      request,
      { headers }
    )

    return response.data
  },
}
