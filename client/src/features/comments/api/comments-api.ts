import { apiClient } from '@/lib/api-client'
import type { Comment, CreateComment, PagedResult } from '@/types'

const BASE_URL = '/comments'

export const commentsApi = {
  getAll: async () => {
    const response = await apiClient.get<Comment[]>(`${BASE_URL}/get-all`)
    return response.data
  },

  getPaged: async (page: number = 1, pageSize: number = 10) => {
    const response = await apiClient.get<PagedResult<Comment>>(
      `${BASE_URL}/paged`,
      { page, pageSize }
    )
    return response.data
  },

  getById: async (id: string) => {
    const response = await apiClient.get<Comment>(`${BASE_URL}/get-by-id/${id}`)
    return response.data
  },

  create: async (data: CreateComment) => {
    const response = await apiClient.post<Comment>(`${BASE_URL}/create`, data)
    return response.data
  },

  update: async (id: string, data: CreateComment) => {
    const response = await apiClient.put<Comment>(`${BASE_URL}/update/${id}`, data)
    return response.data
  },

  delete: async (id: string) => {
    const response = await apiClient.delete<Comment>(`${BASE_URL}/delete/${id}`)
    return response.data
  },
}
