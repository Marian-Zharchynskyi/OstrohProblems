import axios from 'axios'
import type { PagedResult } from '@/types'
import type { UserDto, UpdateUserDto, CreateUserDto } from '@/types/user'

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5146'

export const userService = {
  async getCurrentUser(token: string): Promise<UserDto> {
    const response = await axios.get<UserDto>(`${API_URL}/users/current`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    return response.data
  },

  async getPagedUsers(
    page: number = 1,
    pageSize: number = 10,
    token: string
  ): Promise<PagedResult<UserDto>> {
    const response = await axios.get<PagedResult<UserDto>>(
      `${API_URL}/users/paged`,
      {
        params: { page, pageSize },
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return response.data
  },

  async getAllUsers(token: string): Promise<UserDto[]> {
    const response = await axios.get<UserDto[]>(`${API_URL}/users/get-all`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    return response.data
  },

  async getUserById(userId: string, token: string): Promise<UserDto> {
    const response = await axios.get<UserDto>(
      `${API_URL}/users/get-by-id/${userId}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return response.data
  },

  async updateUser(
    userId: string,
    data: UpdateUserDto,
    token: string
  ): Promise<UserDto> {
    const response = await axios.put<UserDto>(
      `${API_URL}/users/update/${userId}`,
      data,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return response.data
  },

  async uploadUserImage(
    userId: string,
    imageFile: File,
    token: string
  ): Promise<UserDto> {
    const formData = new FormData()
    formData.append('imageFile', imageFile)

    const response = await axios.put<UserDto>(
      `${API_URL}/users/image/${userId}`,
      formData,
      {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'multipart/form-data',
        },
      }
    )
    return response.data
  },

  async deleteUser(userId: string, token: string): Promise<UserDto> {
    const response = await axios.delete<UserDto>(
      `${API_URL}/users/delete/${userId}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return response.data
  },

  async updateUserRoles(
    userId: string,
    roleId: string,
    token: string
  ): Promise<UserDto> {
    const response = await axios.put<UserDto>(
      `${API_URL}/users/update-roles/${userId}`,
      JSON.stringify(roleId),
      {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      }
    )
    return response.data
  },

  async createUser(data: CreateUserDto, token: string): Promise<UserDto> {
    const response = await axios.post<UserDto>(
      `${API_URL}/users/create`,
      data,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return response.data
  },
}
