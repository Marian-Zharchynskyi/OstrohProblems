import { apiClient } from '@/lib/api-client'
import type { Problem, CreateProblem, PagedResult } from '@/types'

const BASE_URL = '/problems'

export const problemsApi = {
  getAll: async () => {
    const response = await apiClient.get<Problem[]>(`${BASE_URL}/get-all`)
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
    const response = await apiClient.post<Problem>(`${BASE_URL}/create`, data)
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
    const response = await apiClient.postFormData<Problem>(
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
}
