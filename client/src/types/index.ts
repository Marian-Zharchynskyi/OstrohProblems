export interface PagedResult<T> {
  items: T[]
  totalCount: number
  page: number
  pageSize: number
}


export const ProblemStatusConstants = {
  New: 'Нова',
  InProgress: 'В роботі',
  Completed: 'Виконано',
  Rejected: 'Відхилено',
} as const

export type ProblemStatusType = (typeof ProblemStatusConstants)[keyof typeof ProblemStatusConstants]

export const PriorityConstants = {
  Low: 'Низький',
  Medium: 'Середній',
  High: 'Високий',
  Critical: 'Критичний',
} as const

export type PriorityType = (typeof PriorityConstants)[keyof typeof PriorityConstants]

export interface Comment {
  id: string | null
  content: string
  problemId: string
  user: User | null
  createdAt: string
  updatedAt: string
}

export interface CreateComment {
  content: string
  problemId: string
}

export interface Rating {
  id: string | null
  points: number
  problemId: string
  user: User | null
  createdAt: string
}

export interface CreateRating {
  points: number
  problemId: string
}

export interface ProblemImage {
  id: string | null
  url: string
}

export interface CoordinatorImage {
  id: string | null
  url: string
}

export interface Problem {
  id: string | null
  title: string
  latitude: number
  longitude: number
  description: string
  status: string
  priority: string
  createdBy: User | null
  coordinator: User | null
  rejectionReason: string | null
  coordinatorComment: string | null
  currentState: string | null
  comments: Comment[] | null
  images: ProblemImage[] | null
  coordinatorImages: CoordinatorImage[] | null
  categories: string[] | null
  createdAt: string
  updatedAt: string
}

export interface CreateProblem {
  title: string
  latitude: number
  longitude: number
  description: string
  categoryNames: string[]
  priority?: string
}

export interface CreateProblemResponse {
  id: string | null
  title: string
  latitude: number
  longitude: number
  description: string
  categoryNames: string[]
}

export interface User {
  id: string | null
  email: string
  name: string
  surname: string
  userImage: UserImage | null
}

export interface UserImage {
  id: string | null
  url: string
}

export interface Notification {
  id: string
  type: 'comment' | 'status_change' | 'reply'
  message: string
  problemId: string
  problemTitle: string
  createdAt: string
  isRead: boolean
}
