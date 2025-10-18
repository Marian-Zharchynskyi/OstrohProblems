import { apiClient } from '@/lib/api-client'
import type { Rating, CreateRating, PagedResult } from '@/types'

const BASE_URL = '/ratings'

export const ratingsApi = {
  getAll: async () => {
    const response = await apiClient.get<Rating[]>(`${BASE_URL}/get-all`)
    return response.data
  },

  getPaged: async (page: number = 1, pageSize: number = 10) => {
    const response = await apiClient.get<PagedResult<Rating>>(
      `${BASE_URL}/paged`,
      { page, pageSize }
    )
    return response.data
  },

  getById: async (id: string) => {
    const response = await apiClient.get<Rating>(`${BASE_URL}/get-by-id/${id}`)
    return response.data
  },

  create: async (data: CreateRating) => {
    const response = await apiClient.post<Rating>(`${BASE_URL}/create`, data)
    return response.data
  },

  update: async (id: string, data: CreateRating) => {
    const response = await apiClient.put<Rating>(`${BASE_URL}/update/${id}`, data)
    return response.data
  },

  delete: async (id: string) => {
    const response = await apiClient.delete<Rating>(`${BASE_URL}/delete/${id}`)
    return response.data
  },
}
