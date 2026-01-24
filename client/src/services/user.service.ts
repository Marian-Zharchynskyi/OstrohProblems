import axios from 'axios'
import type { PagedResult } from '@/types'
import type { UserDto, UpdateUserDto, CreateUserDto, ChangePasswordDto, SetPasswordDto } from '@/types/user'

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5146'

interface BackendUserDto {
  Id?: string; id?: string;
  Email?: string; email?: string;
  Name?: string; name?: string;
  Surname?: string; surname?: string;
  PhoneNumber?: string; phoneNumber?: string;
  Image?: { Id?: string; id?: string; Url?: string; url?: string };
  image?: { id?: string; url?: string };
  Role?: { Id?: string; id?: string; Name?: string; name?: string };
  role?: { id?: string; name?: string };
  HasPassword?: boolean; hasPassword?: boolean;
}

interface BackendPagedResponse<T> {
  Items?: T[]; items?: T[];
  TotalCount?: number; totalCount?: number;
  PageSize?: number; pageSize?: number;
  CurrentPage?: number; currentPage?: number;
  TotalPages?: number; totalPages?: number;
  HasNextPage?: boolean; hasNextPage?: boolean;
  HasPreviousPage?: boolean; hasPreviousPage?: boolean;
}

// Helper to map PascalCase response to camelCase UserDto
const mapUserToDto = (data: BackendUserDto): UserDto => {
  if (!data) return { id: '', email: '' } as UserDto

  return {
    id: data.Id || data.id || '',
    email: data.Email || data.email || '',
    name: data.Name || data.name,
    surname: data.Surname || data.surname,
    phoneNumber: data.PhoneNumber || data.phoneNumber,
    image: (data.Image || data.image) ? {
      id: data.Image?.Id || data.Image?.id || data.image?.id || '',
      url: data.Image?.Url || data.Image?.url || data.image?.url || ''
    } : undefined,
    role: (data.Role || data.role) ? {
      id: data.Role?.Id || data.Role?.id || data.role?.id || '',
      name: data.Role?.Name || data.Role?.name || data.role?.name || ''
    } : undefined,
    hasPassword: data.HasPassword ?? data.hasPassword ?? true
  }
}

export const userService = {
  async getCurrentUser(token: string): Promise<UserDto> {
    const response = await axios.get<BackendUserDto>(`${API_URL}/users/current`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    return mapUserToDto(response.data)
  },

  async getPagedUsers(
    page: number = 1,
    pageSize: number = 10,
    token: string
  ): Promise<PagedResult<UserDto>> {
    const response = await axios.get<BackendPagedResponse<BackendUserDto>>(
      `${API_URL}/users/paged`,
      {
        params: { page, pageSize },
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )

    return {
      items: (response.data.Items || response.data.items || []).map(mapUserToDto),
      totalCount: response.data.TotalCount || response.data.totalCount || 0,
      page: response.data.CurrentPage || response.data.currentPage || page,
      pageSize: response.data.PageSize || response.data.pageSize || pageSize,
      currentPage: response.data.CurrentPage || response.data.currentPage || page,
      totalPages: response.data.TotalPages || response.data.totalPages || 0,
      hasNextPage: response.data.HasNextPage ?? response.data.hasNextPage ?? false,
      hasPreviousPage: response.data.HasPreviousPage ?? response.data.hasPreviousPage ?? false
    }
  },

  async getAllUsers(token: string): Promise<UserDto[]> {
    const response = await axios.get<BackendUserDto[]>(`${API_URL}/users/get-all`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    return response.data.map(mapUserToDto)
  },

  async getUserById(userId: string, token: string): Promise<UserDto> {
    const response = await axios.get<BackendUserDto>(
      `${API_URL}/users/get-by-id/${userId}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return mapUserToDto(response.data)
  },

  async updateUser(
    userId: string,
    data: UpdateUserDto,
    token: string
  ): Promise<UserDto> {
    const response = await axios.put<BackendUserDto>(
      `${API_URL}/users/update/${userId}`,
      data,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return mapUserToDto(response.data)
  },

  async uploadUserImage(
    userId: string,
    imageFile: File,
    token: string
  ): Promise<UserDto> {
    const formData = new FormData()
    formData.append('imageFile', imageFile)

    const response = await axios.put<BackendUserDto>(
      `${API_URL}/users/image/${userId}`,
      formData,
      {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'multipart/form-data',
        },
      }
    )
    return mapUserToDto(response.data)
  },

  async deleteUser(userId: string, token: string): Promise<UserDto> {
    const response = await axios.delete<BackendUserDto>(
      `${API_URL}/users/delete/${userId}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return mapUserToDto(response.data)
  },

  async deleteUserImage(userId: string, token: string): Promise<UserDto> {
    const response = await axios.delete<BackendUserDto>(
      `${API_URL}/users/image/${userId}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return mapUserToDto(response.data)
  },

  async updateUserRoles(
    userId: string,
    roleId: string,
    token: string
  ): Promise<UserDto> {
    const response = await axios.put<BackendUserDto>(
      `${API_URL}/users/update-roles/${userId}`,
      JSON.stringify(roleId),
      {
        headers: {
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      }
    )
    return mapUserToDto(response.data)
  },

  async createUser(data: CreateUserDto, token: string): Promise<UserDto> {
    const response = await axios.post<BackendUserDto>(
      `${API_URL}/users/create`,
      data,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return mapUserToDto(response.data)
  },

  async changePassword(
    userId: string,
    data: ChangePasswordDto,
    token: string
  ): Promise<UserDto> {
    const response = await axios.put<BackendUserDto>(
      `${API_URL}/users/change-password/${userId}`,
      data,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return mapUserToDto(response.data)
  },

  async setPassword(
    userId: string,
    data: SetPasswordDto,
    token: string
  ): Promise<UserDto> {
    const response = await axios.put<BackendUserDto>(
      `${API_URL}/users/set-password/${userId}`,
      data,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    return mapUserToDto(response.data)
  },
}
