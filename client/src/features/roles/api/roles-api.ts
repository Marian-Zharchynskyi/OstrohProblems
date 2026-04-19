import axios from 'axios';
import { apiClient } from '@/lib/api-client';
import type { Role } from '@/types/user';

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5146';
const BASE_URL = '/roles';

export const rolesApi = {
  getAll: async (token?: string) => {
    if (token) {
      const response = await axios.get<Role[]>(`${API_URL}${BASE_URL}/get-all`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      return response.data;
    }
    const response = await apiClient.get<Role[]>(`${BASE_URL}/get-all`);
    return response.data;
  },

  getById: async (id: string, token?: string) => {
    if (token) {
      const response = await axios.get<Role>(`${API_URL}${BASE_URL}/get-by-id/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      return response.data;
    }
    const response = await apiClient.get<Role>(`${BASE_URL}/get-by-id/${id}`);
    return response.data;
  },

  getByName: async (name: string, token?: string) => {
    if (token) {
      const response = await axios.get<Role>(`${API_URL}${BASE_URL}/get-by-name/${encodeURIComponent(name)}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      return response.data;
    }
    const response = await apiClient.get<Role>(`${BASE_URL}/get-by-name/${encodeURIComponent(name)}`);
    return response.data;
  },
};
