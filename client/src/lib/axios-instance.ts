import axios from 'axios'
import { tokenStorage } from './token-storage'
import { authService } from '@/services/auth.service'
import { isTokenExpired } from './jwt-utils'

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000'

export const axiosInstance = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
})

// Request interceptor to add auth token
axiosInstance.interceptors.request.use(
  async (config) => {
    const tokens = tokenStorage.getTokens()

    if (tokens) {
      // Check if token is expired
      if (isTokenExpired(tokens.accessToken)) {
        try {
          // Try to refresh the token
          const newTokens = await authService.refreshToken(tokens)
          tokenStorage.setTokens(newTokens)
          config.headers.Authorization = `Bearer ${newTokens.accessToken}`
        } catch {
          // Refresh failed, clear tokens
          tokenStorage.clearTokens()
          window.location.href = '/login'
          return Promise.reject(new Error('Session expired'))
        }
      } else {
        config.headers.Authorization = `Bearer ${tokens.accessToken}`
      }
    }

    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

// Response interceptor to handle 401 errors
axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config

    // If 401 and we haven't retried yet
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true

      const tokens = tokenStorage.getTokens()
      if (tokens) {
        try {
          const newTokens = await authService.refreshToken(tokens)
          tokenStorage.setTokens(newTokens)

          // Retry the original request with new token
          originalRequest.headers.Authorization = `Bearer ${newTokens.accessToken}`
          return axiosInstance(originalRequest)
        } catch {
          // Refresh failed, redirect to login
          tokenStorage.clearTokens()
          window.location.href = '/login'
        }
      }
    }

    return Promise.reject(error)
  }
)
