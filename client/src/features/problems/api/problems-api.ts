import { apiClient } from '@/lib/api-client'
import type { CreateProblem, CreateProblemResponse, Problem, PagedResult } from '@/types'

const BASE_URL = '/problems'

export const problemsApi = {
  getAll: async () => {
    const response = await apiClient.get<Problem[]>(`${BASE_URL}/get-all`)
    return response.data
  },

  getForMap: async () => {
    const response = await apiClient.get<Problem[]>(`${BASE_URL}/for-map`)
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

  assignCoordinator: async (problemId: string, coordinatorId: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/assign-coordinator/${problemId}`,
      coordinatorId
    )
    return response.data
  },

  validateProblem: async (problemId: string, coordinatorId: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/validate/${problemId}`,
      coordinatorId
    )
    return response.data
  },

  reject: async (problemId: string, rejectionReason: string) => {
    const response = await apiClient.put<Problem>(
      `${BASE_URL}/reject/${problemId}`,
      rejectionReason
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
}
