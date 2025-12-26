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
  name?: string
  surname?: string
  phoneNumber?: string
  image?: UserImage
  role?: Role
}

export interface UpdateUserDto {
  email: string
  name?: string
  surname?: string
  phoneNumber?: string
}

export interface CreateUserDto {
  email: string
  password: string
  name?: string
  surname?: string
  roleId: string
}

export interface ChangePasswordDto {
  currentPassword: string
  newPassword: string
}
