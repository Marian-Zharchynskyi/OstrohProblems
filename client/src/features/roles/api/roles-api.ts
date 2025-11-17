import { apiClient } from '@/lib/api-client'
import type { Role } from '@/types/user'

const BASE_URL = '/roles'

export const rolesApi = {
  getAll: async () => {
    const response = await apiClient.get<Role[]>(`${BASE_URL}/get-all`)
    return response.data
  },

  getById: async (id: string) => {
    const response = await apiClient.get<Role>(`${BASE_URL}/get-by-id/${id}`)
    return response.data
  },

  getByName: async (name: string) => {
    const response = await apiClient.get<Role>(`${BASE_URL}/get-by-name/${encodeURIComponent(name)}`)
    return response.data
  },
}
