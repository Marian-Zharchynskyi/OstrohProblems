export interface UserImage {
  id: string
  url: string
}

export interface Role {
  id: string
  name: string
}

export interface UserDto {
  id: string
  email: string
  fullName?: string
  image?: UserImage
  role?: Role
}

export interface UpdateUserDto {
  userName?: string
  email: string
}

export interface CreateUserDto {
  email: string
  password: string
  fullName?: string
  roleId: string
}
