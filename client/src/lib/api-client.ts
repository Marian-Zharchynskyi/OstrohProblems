import axios, { type AxiosInstance } from 'axios';
import { tokenStorage } from '@/lib/token-storage';

const API_BASE_URL = import.meta.env.VITE_API_URL || import.meta.env.VITE_API_BASE_URL || 'http://localhost:5146';

class ApiClient {
  private client: AxiosInstance;

  private getToken: (() => Promise<string | null>) | null = null;

  constructor() {
    this.client = axios.create({
      baseURL: API_BASE_URL,
      headers: {
        'Content-Type': 'application/json',
      },
      // Increase timeout just in case
      timeout: 30000,
    });

    // Request interceptor for adding auth token
    this.client.interceptors.request.use(
      async (config) => {
        // Try to use the configured token provider (Clerk)
        if (this.getToken) {
          const token = await this.getToken();
          if (token) {
            config.headers.Authorization = `Bearer ${token}`;
          }
        }
        // Fallback to old token storage (if needed, or remove)
        else {
          const token = tokenStorage.getAccessToken();
          if (token) {
            config.headers.Authorization = `Bearer ${token}`;
          }
        }
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );

    // Response interceptor for handling errors
    this.client.interceptors.response.use(
      (response) => response,
      (error) => {
        // Log error for debugging
        if (error.response?.status === 401) {
          console.warn('Unauthorized access (401)', error.config?.url);
          // Do NOT automatically redirect here, let the React components handle it
          // window.location.href = '/login'
        }
        return Promise.reject(error);
      }
    );
  }

  // Method to set the token provider function
  setTokenProvider(getToken: () => Promise<string | null>) {
    this.getToken = getToken;
  }

  get<T>(url: string, params?: Record<string, unknown>) {
    return this.client.get<T>(url, { params });
  }

  post<T>(url: string, data?: unknown) {
    return this.client.post<T>(url, data);
  }

  put<T>(url: string, data?: unknown) {
    return this.client.put<T>(url, data);
  }

  delete<T>(url: string) {
    return this.client.delete<T>(url);
  }

  postFormData<T>(url: string, formData: FormData) {
    return this.client.post<T>(url, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
  }

  putFormData<T>(url: string, formData: FormData) {
    return this.client.put<T>(url, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
  }
}

export const apiClient = new ApiClient();
