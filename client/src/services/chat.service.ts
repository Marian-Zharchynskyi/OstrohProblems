import axios from 'axios'
import type { ChatMessageRequest, ChatMessageResponse, ExtractedProblemData } from '@/types/chat'

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

  async transcribeAudio(
    audioBlob: Blob,
    token: string | null
  ): Promise<string> {
    const formData = new FormData()
    formData.append('audioFile', audioBlob, 'audio.webm')

    const headers: Record<string, string> = {}

    if (token) {
      headers['Authorization'] = `Bearer ${token}`
    }

    const response = await axios.post<{ transcription: string }>(
      `${API_URL}/chat/transcribe`,
      formData,
      { headers }
    )

    return response.data.transcription
  },

  async sendVoiceMessage(
    audioBlob: Blob,
    token: string | null
  ): Promise<ChatMessageResponse> {
    const formData = new FormData()
    formData.append('audioFile', audioBlob, 'audio.webm')

    const headers: Record<string, string> = {}

    if (token) {
      headers['Authorization'] = `Bearer ${token}`
    }

    const response = await axios.post<ChatMessageResponse>(
      `${API_URL}/chat/voice-message`,
      formData,
      { headers }
    )

    return response.data
  },

  async extractProblemData(
    message: string,
    token: string
  ): Promise<ExtractedProblemData> {
    const response = await axios.post<ExtractedProblemData>(
      `${API_URL}/chat/extract-problem`,
      { message },
      {
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
      }
    )

    return response.data
  },

  async extractProblemDataFromVoice(
    audioBlob: Blob,
    token: string
  ): Promise<ExtractedProblemData> {
    const formData = new FormData()
    formData.append('audioFile', audioBlob, 'audio.webm')

    const response = await axios.post<ExtractedProblemData>(
      `${API_URL}/chat/extract-problem-voice`,
      formData,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )

    return response.data
  },
}
