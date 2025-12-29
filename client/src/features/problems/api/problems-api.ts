import { apiClient } from '@/lib/api-client'
import type { CreateProblem, CreateProblemResponse, Problem, ProblemSummary, PagedResult } from '@/types'

const BASE_URL = '/problems'

export const problemsApi = {
  getAll: async () => {
    const response = await apiClient.get<ProblemSummary[]>(`${BASE_URL}/get-all`)
    return response.data
  },

  getForMap: async () => {
    const response = await apiClient.get<ProblemSummary[]>(`${BASE_URL}/for-map`)
    return response.data
  },

  getPaged: async (page: number = 1, pageSize: number = 10) => {
    const response = await apiClient.get<PagedResult<Problem>>(
      `${BASE_URL}/paged`,
      { page, pageSize }
    )
    return response.data
  },

  getById: async (id: string) => {
    const response = await apiClient.get<Problem>(`${BASE_URL}/get-by-id/${id}`)
    return response.data
  },

  create: async (data: CreateProblem) => {
    const response = await apiClient.post<CreateProblemResponse>(`${BASE_URL}/create`, data)
    return response.data
  },

  update: async (id: string, data: CreateProblem) => {
    const response = await apiClient.put<Problem>(`${BASE_URL}/update/${id}`, data)
    return response.data
  },

  delete: async (id: string) => {
    const response = await apiClient.delete<Problem>(`${BASE_URL}/delete/${id}`)
    return response.data
  },

  uploadImages: async (id: string, files: FileList) => {
    const formData = new FormData()
    Array.from(files).forEach((file) => {
      formData.append('imagesFiles', file)
    })
    const response = await apiClient.putFormData<Problem>(
      `${BASE_URL}/upload-images/${id}`,
      formData
    )
    return response.data
  },

  deleteImage: async (problemId: string, imageId: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/delete-image/${problemId}`,
      { problemImageId: imageId }
    )
    return response.data
  },

  assignCoordinator: async (problemId: string, coordinatorId: string, priority?: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/assign-coordinator/${problemId}`,
      { coordinatorId, priority }
    )
    return response.data
  },

  reject: async (problemId: string, coordinatorId: string, rejectionReason: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/reject/${problemId}`,
      { coordinatorId, rejectionReason }
    )
    return response.data
  },

  setCoordinatorComment: async (problemId: string, comment: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/set-coordinator-comment/${problemId}`,
      comment
    )
    return response.data
  },

  updateCurrentState: async (problemId: string, currentState: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/update-current-state/${problemId}`,
      { currentState }
    )
    return response.data
  },
  completeProblem: async (problemId: string, currentState: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/complete/${problemId}`,
      currentState
    )
    return response.data
  },

  getByUser: async (userId: string) => {
    const response = await apiClient.get<Problem[]>(`${BASE_URL}/by-user/${userId}`)
    return response.data
  },

  getByCoordinator: async (coordinatorId: string) => {
    const response = await apiClient.get<Problem[]>(`${BASE_URL}/by-coordinator/${coordinatorId}`)
    return response.data
  },

  restoreProblem: async (problemId: string) => {
    const response = await apiClient.put<Problem>(`${BASE_URL}/restore/${problemId}`)
    return response.data
  },

  getByStatus: async (status: string) => {
    const response = await apiClient.get<Problem[]>(`${BASE_URL}/by-status/${encodeURIComponent(status)}`)
    return response.data
  },

  getByUserFiltered: async (
    userId: string,
    filter: {
      searchTerm?: string
      status?: string
      category?: string
      priority?: string
      sortBy?: string
      sortDescending?: boolean
      dateFilter?: string
    }
  ): Promise<Problem[]> => {
    const params = new URLSearchParams()
    if (filter.searchTerm) params.append('searchTerm', filter.searchTerm)
    if (filter.status) params.append('status', filter.status)
    if (filter.category) params.append('category', filter.category)
    if (filter.priority) params.append('priority', filter.priority)
    if (filter.sortBy) params.append('sortBy', filter.sortBy)
    if (filter.sortDescending !== undefined) params.append('sortDescending', filter.sortDescending.toString())
    if (filter.dateFilter) params.append('dateFilter', filter.dateFilter)

    const queryString = params.toString()
    const url = `${BASE_URL}/by-user-filtered/${userId}${queryString ? `?${queryString}` : ''}`
    const response = await apiClient.get<Problem[]>(url)
    return response.data
  },

  uploadCoordinatorImages: async (id: string, files: FileList) => {
    const formData = new FormData()
    Array.from(files).forEach((file) => {
      formData.append('imagesFiles', file)
    })
    const response = await apiClient.putFormData<Problem>(
      `${BASE_URL}/upload-coordinator-images/${id}`,
      formData
    )
    return response.data
  },

  deleteCoordinatorImage: async (problemId: string, imageId: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/delete-coordinator-image/${problemId}`,
      { coordinatorImageId: imageId }
    )
    return response.data
  },

  updateDescription: async (problemId: string, description: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/update-description/${problemId}`,
      { description }
    )
    return response.data
  },

  updateTitleAndCategories: async (problemId: string, title: string, categoryNames?: string[]) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/update-title-and-categories/${problemId}`,
      { title, categoryNames }
    )
    return response.data
  },

  updateLocation: async (problemId: string, latitude: number, longitude: number) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/update-location/${problemId}`,
      { latitude, longitude }
    )
    return response.data
  },
}
