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

// Status types
export interface Status {
  id: string | null
  name: string
}

export interface CreateStatus {
  name: string
}

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
  problemStatus: Status | null
  user: User | null
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
  problemStatusId: string
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
