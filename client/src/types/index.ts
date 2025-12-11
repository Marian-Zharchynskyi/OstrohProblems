// Common types
export interface PagedResult<T> {
  items: T[]
  totalCount: number
  page: number
  pageSize: number
}

// Category types
export interface Category {
  id: string | null
  name: string
}

// Problem Status constants
export const ProblemStatusConstants = {
  New: 'Нова',
  InProgress: 'В роботі',
  Completed: 'Виконано',
  Rejected: 'Відхилено',
} as const

export type ProblemStatusType = (typeof ProblemStatusConstants)[keyof typeof ProblemStatusConstants]

// Comment types
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

// Rating types
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

// Problem types
export interface ProblemImage {
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
  createdBy: User | null
  coordinator: User | null
  rejectionReason: string | null
  coordinatorComment: string | null
  currentState: string | null
  comments: Comment[] | null
  images: ProblemImage[] | null
  categories: Category[] | null
  createdAt: string
  updatedAt: string
}

export interface CreateProblem {
  title: string
  latitude: number
  longitude: number
  description: string
  problemCategoryIds: string[]
}

// User types (for reference in other types)
export interface User {
  id: string | null
  email: string
  firstName: string
  lastName: string
  userImage: UserImage | null
}

export interface UserImage {
  id: string | null
  url: string
}

// Notification types
export interface Notification {
  id: string
  type: 'comment' | 'status_change' | 'reply'
  message: string
  problemId: string
  problemTitle: string
  createdAt: string
  isRead: boolean
}
