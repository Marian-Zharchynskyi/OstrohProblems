import { apiClient } from '@/lib/api-client'
import type { Status, CreateStatus } from '@/types'

const BASE_URL = '/problem-statuses'

export const statusesApi = {
  getAll: async () => {
    const response = await apiClient.get<Status[]>(`${BASE_URL}/get-all`)
    return response.data
  },

  getById: async (id: string) => {
    const response = await apiClient.get<Status>(`${BASE_URL}/get-by-id/${id}`)
    return response.data
  },

  create: async (data: CreateStatus) => {
    const response = await apiClient.post<Status>(`${BASE_URL}/create`, data)
    return response.data
  },

  update: async (id: string, data: CreateStatus) => {
    const response = await apiClient.put<Status>(`${BASE_URL}/update`, { id, ...data })
    return response.data
  },

  delete: async (id: string) => {
    const response = await apiClient.delete<Status>(`${BASE_URL}/delete/${id}`)
    return response.data
  },
}
