import { apiClient } from '@/lib/api-client'
import type { Category, PagedResult } from '@/types'

const BASE_URL = '/problem-categories'

export const categoriesApi = {
  getAll: async () => {
    const response = await apiClient.get<Category[]>(`${BASE_URL}/get-all`)
    return response.data
  },

  getPaged: async (page: number = 1, pageSize: number = 10) => {
    const response = await apiClient.get<PagedResult<Category>>(
      `${BASE_URL}/paged`,
      { page, pageSize }
    )
    return response.data
  },

  getById: async (id: string) => {
    const response = await apiClient.get<Category>(`${BASE_URL}/get-by-id/${id}`)
    return response.data
  },

  create: async (data: Omit<Category, 'id'>) => {
    const response = await apiClient.post<Category>(`${BASE_URL}/create`, data)
    return response.data
  },

  update: async (data: Category) => {
    const response = await apiClient.put<Category>(`${BASE_URL}/update`, data)
    return response.data
  },

  delete: async (id: string) => {
    const response = await apiClient.delete<Category>(`${BASE_URL}/delete/${id}`)
    return response.data
  },
}
