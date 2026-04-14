export interface ChatMessage {
  id: string
  content: string
  role: 'user' | 'assistant'
  timestamp: Date
  responseType?: ChatResponseType
  problems?: ProblemSummaryChat[]
  suggestedFilter?: DashboardFilter
}

export type ChatResponseType = 'Text' | 'ProblemsList' | 'SingleProblem' | 'Help' | 'Error' | 'DashboardUpdate'

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

// Admin Dashboard types
export interface DashboardStatistics {
  totalProblems: number
  newProblems: number
  inProgressProblems: number
  completedProblems: number
  rejectedProblems: number
  totalUsers: number
  totalComments: number
  averageRating: number
  criticalProblems: number
  highPriorityProblems: number
  problemsThisMonth: number
  problemsLastMonth: number
  resolutionRate: number
  avgResolutionTimeDays: number
  categoryStats: CategoryStat[]
  statusStats: StatusStat[]
  priorityStats: PriorityStat[]
  monthlyTrends: MonthlyTrend[]
  recentProblems: ProblemSummaryChat[]
  topRatedProblems: ProblemSummaryChat[]
}

export interface CategoryStat {
  category: string
  count: number
  percentage: number
}

export interface StatusStat {
  status: string
  count: number
  percentage: number
}

export interface PriorityStat {
  priority: string
  count: number
  percentage: number
}

export interface MonthlyTrend {
  month: string
  created: number
  resolved: number
}

export interface AdminChatResponse {
  message: string
  responseType: ChatResponseType
  suggestedFilter: DashboardFilter | null
  problems: ProblemSummaryChat[] | null
}

export interface DashboardFilter {
  status: string | null
  category: string | null
  priority: string | null
  dateRange: string | null
  sortBy: string | null
}
