export interface UserImage {
  id: string
  filePath: string
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
  roles?: Role[]
}

export interface UpdateUserDto {
  userName?: string
  email: string
}
