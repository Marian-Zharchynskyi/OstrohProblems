export interface ChatMessage {
  id: string
  content: string
  role: 'user' | 'assistant'
  timestamp: Date
  responseType?: ChatResponseType
  problems?: ProblemSummaryChat[]
}

export type ChatResponseType = 'Text' | 'ProblemsList' | 'SingleProblem' | 'Help' | 'Error'

export interface ProblemSummaryChat {
  id: string
  title: string
  description: string
  status: string
  priority: string
  latitude: number
  longitude: number
  categories: string[]
  averageRating: number | null
  commentsCount: number
  createdAt: string
  creatorName: string | null
}

export interface ChatMessageRequest {
  message: string
}

export interface ChatMessageResponse {
  message: string
  responseType: ChatResponseType
  problems: ProblemSummaryChat[] | null
}

export interface ExtractedProblemData {
  title: string
  description: string
  categories: string[]
  priority: string
  streetName: string | null
  latitude: number | null
  longitude: number | null
  aiMessage: string | null
  isComplete: boolean
}
