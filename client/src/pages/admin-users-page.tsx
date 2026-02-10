import { useState, useEffect } from 'react'
import { useAuth } from '@/contexts/auth-context'
import { userService } from '@/services/user.service'
import { rolesApi } from '@/features/roles/api/roles-api'
import type { UserDto, Role, CreateUserDto } from '@/types/user'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { Users, Trash2, Shield, Plus, Pencil } from 'lucide-react'

export function AdminUsersPage() {
  const { tokens, getClerkToken } = useAuth()
  const [users, setUsers] = useState<UserDto[]>([])
  const [currentUserId, setCurrentUserId] = useState<string | null>(null)
  const [roles, setRoles] = useState<Role[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [deletingUserId, setDeletingUserId] = useState<string | null>(null)
  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false)
  const [userToDelete, setUserToDelete] = useState<UserDto | null>(null)

  // Create user dialog state
  const [isCreateDialogOpen, setIsCreateDialogOpen] = useState(false)
  const [isCreating, setIsCreating] = useState(false)
  const [createForm, setCreateForm] = useState<CreateUserDto>({
    email: '',
    password: '',
    name: '',
    surname: '',
    roleId: '',
  })

  // Edit user dialog state
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false)
  const [isEditing, setIsEditing] = useState(false)
  const [editingUser, setEditingUser] = useState<UserDto | null>(null)
  const [editForm, setEditForm] = useState({
    email: '',
    userName: '',
    userSurname: '',
    roleId: '',
  })

  useEffect(() => {
    const loadData = async () => {
      const token = getClerkToken ? await getClerkToken() : tokens?.accessToken
      if (!token) return

      try {
        setIsLoading(true)
        const [currentUser, usersData, rolesData] = await Promise.all([
          userService.getCurrentUser(token),
          userService.getAllUsers(token),
          rolesApi.getAll(token),
        ])
        setCurrentUserId(currentUser.id)
        // Filter out current user from the list
        setUsers(usersData.filter(u => u.id !== currentUser.id))
        setRoles(rolesData)
      } catch (error) {
        console.error('Failed to load data:', error)
      } finally {
        setIsLoading(false)
      }
    }

    loadData()
  }, [tokens?.accessToken, getClerkToken])

  const handleOpenDeleteDialog = (user: UserDto) => {
    setUserToDelete(user)
    setDeleteDialogOpen(true)
  }

  const handleDeleteUser = async () => {
    const token = getClerkToken ? await getClerkToken() : tokens?.accessToken
    if (!token || !userToDelete) return

    try {
      setDeletingUserId(userToDelete.id)
      await userService.deleteUser(userToDelete.id, token)
      // Filter maintains exclusion of current user
      setUsers(users.filter((u) => u.id !== userToDelete.id))
      setDeleteDialogOpen(false)
      setUserToDelete(null)
    } catch (error) {
      console.error('Failed to delete user:', error)
      const errorMessage = error instanceof Error ? error.message : 'Не вдалося видалити користувача'
      alert(errorMessage)
    } finally {
      setDeletingUserId(null)
    }
  }

  const handleCreateUser = async () => {
    const token = getClerkToken ? await getClerkToken() : tokens?.accessToken
    if (!token) return
    if (!createForm.email || !createForm.password || !createForm.roleId) {
      alert('Заповніть всі обов\'язкові поля')
      return
    }

    try {
      setIsCreating(true)
      const newUser = await userService.createUser(createForm, token)
      // Only add if not current user (shouldn't happen, but for safety)
      if (newUser.id !== currentUserId) {
        setUsers([...users, newUser])
      }
      setIsCreateDialogOpen(false)
      setCreateForm({ email: '', password: '', name: '', surname: '', roleId: '' })
    } catch (error) {
      console.error('Failed to create user:', error)
      alert('Не вдалося створити користувача')
    } finally {
      setIsCreating(false)
    }
  }

  const handleOpenEditDialog = (user: UserDto) => {
    setEditingUser(user)
    setEditForm({
      email: user.email,
      userName: user.name || '',
      userSurname: user.surname || '',
      roleId: user.role?.id || '',
    })
    setIsEditDialogOpen(true)
  }

  const handleEditUser = async () => {
    const token = getClerkToken ? await getClerkToken() : tokens?.accessToken
    if (!token || !editingUser) return

    try {
      setIsEditing(true)

      // Update user info
      await userService.updateUser(
        editingUser.id,
        { email: editForm.email, name: editForm.userName, surname: editForm.userSurname },
        token
      )

      // Update role
      const updatedUser = await userService.updateUserRoles(
        editingUser.id,
        editForm.roleId,
        token
      )

      setUsers(users.map((u) => (u.id === editingUser.id ? updatedUser : u)))
      setIsEditDialogOpen(false)
      setEditingUser(null)
    } catch (error) {
      console.error('Failed to update user:', error)
      alert('Не вдалося оновити користувача')
    } finally {
      setIsEditing(false)
    }
  }

  const toggleCreateRole = (roleId: string) => {
    setCreateForm((prev) => ({
      ...prev,
      roleId: prev.roleId === roleId ? '' : roleId,
    }))
  }

  const toggleEditRole = (roleId: string) => {
    setEditForm((prev) => ({
      ...prev,
      roleId: prev.roleId === roleId ? '' : roleId,
    }))
  }

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <p className="text-muted-foreground">Завантаження користувачів...</p>
      </div>
    )
  }

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold tracking-tight flex items-center gap-2">
            <Users className="w-8 h-8" />
            Управління користувачами
          </h1>
          <p className="text-muted-foreground mt-2">
            Керуйте всіма користувачами системи
          </p>
        </div>
        <div className="flex items-center gap-4">
          <span className="text-sm text-muted-foreground">
            Всього користувачів: {users.length}
          </span>
          <Button onClick={() => setIsCreateDialogOpen(true)}>
            <Plus className="w-4 h-4 mr-2" />
            Створити користувача
          </Button>
        </div>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Всі користувачі</CardTitle>
          <CardDescription>
            Перегляд та управління обліковими записами
          </CardDescription>
        </CardHeader>
        <CardContent>
          {users.length === 0 ? (
            <div className="text-center py-8 text-muted-foreground">
              Користувачів не знайдено
            </div>
          ) : (
            <div className="rounded-md border">
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead>Email</TableHead>
                    <TableHead>Ім'я та Прізвище</TableHead>
                    <TableHead>Роль</TableHead>
                    <TableHead>Аватар</TableHead>
                    <TableHead className="text-right">Дії</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {users.map((user) => (
                    <TableRow key={user.id}>
                      <TableCell className="font-medium">
                        {user.email}
                      </TableCell>
                      <TableCell>
                        {user.name && user.surname ? (
                          `${user.name} ${user.surname}`
                        ) : user.name || user.surname ? (
                          `${user.name || ''}${user.surname || ''}`
                        ) : (
                          <span className="text-muted-foreground italic">
                            Не вказано
                          </span>
                        )}
                      </TableCell>
                      <TableCell>
                        <div className="flex gap-1 flex-wrap">
                          {user.role ? (
                            <span
                              className="inline-flex items-center gap-1 px-2 py-1 bg-black/10 text-black rounded text-xs"
                            >
                              <Shield className="w-3 h-3" />
                              {user.role.name === 'Administrator' ? 'Адміністратор' : user.role.name === 'Coordinator' ? 'Координатор' : user.role.name === 'User' ? 'Користувач' : user.role.name}
                            </span>
                          ) : (
                            <span className="text-muted-foreground text-sm">
                              Немає ролі
                            </span>
                          )}
                        </div>
                      </TableCell>
                      <TableCell>
                        {user.image?.url ? (
                          <img
                            src={user.image.url}
                            alt={`${user.name || ''} ${user.surname || ''}`.trim() || user.email}
                            className="w-10 h-10 rounded-full object-cover"
                          />
                        ) : (
                          <div className="w-10 h-10 rounded-full bg-muted flex items-center justify-center">
                            <Users className="w-5 h-5 text-muted-foreground" />
                          </div>
                        )}
                      </TableCell>
                      <TableCell className="text-right">
                        <div className="flex justify-end gap-2">
                          <Button
                            variant="outline"
                            size="icon"
                            className="h-9 w-9 border border-[#D0D5DD] bg-white text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929] disabled:bg-white disabled:text-[#292929]"
                            onClick={() => handleOpenEditDialog(user)}
                            title="Редагувати"
                          >
                            <Pencil className="w-4 h-4 text-[#292929]" />
                          </Button>
                          <Button
                            variant="outline"
                            size="icon"
                            className="h-9 w-9 border border-red-200 bg-white hover:bg-red-50 disabled:bg-white"
                            onClick={() => handleOpenDeleteDialog(user)}
                            disabled={deletingUserId === user.id}
                            title="Видалити"
                          >
                            <Trash2 className="w-4 h-4 text-red-600" />
                          </Button>
                        </div>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </div>
          )}
        </CardContent>
      </Card>

      {/* Create User Dialog */}
      <Dialog open={isCreateDialogOpen} onOpenChange={setIsCreateDialogOpen}>
        <DialogContent className="sm:max-w-[490px]">
          <DialogHeader>
            <DialogTitle>Створити користувача</DialogTitle>
            <DialogDescription>
              Заповніть форму для створення нового користувача
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            <div className="grid gap-2">
              <Label htmlFor="email">Email *</Label>
              <Input
                id="email"
                type="email"
                autoComplete="off"
                placeholder="user@example.com"
                value={createForm.email}
                onChange={(e) =>
                  setCreateForm((prev) => ({ ...prev, email: e.target.value }))
                }
              />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="password">Пароль *</Label>
              <Input
                id="password"
                type="password"
                autoComplete="new-password"
                placeholder="Мінімум 6 символів"
                value={createForm.password}
                onChange={(e) =>
                  setCreateForm((prev) => ({ ...prev, password: e.target.value }))
                }
              />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="name">Ім'я</Label>
              <Input
                id="name"
                placeholder="Іван"
                value={createForm.name}
                onChange={(e) =>
                  setCreateForm((prev) => ({ ...prev, name: e.target.value }))
                }
              />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="surname">Прізвище</Label>
              <Input
                id="surname"
                placeholder="Петренко"
                value={createForm.surname}
                onChange={(e) =>
                  setCreateForm((prev) => ({ ...prev, surname: e.target.value }))
                }
              />
            </div>
            <div className="grid gap-2">
              <Label>Ролі *</Label>
              <div className="flex flex-wrap gap-2">
                {roles.map((role) => (
                  <button
                    key={role.id}
                    type="button"
                    onClick={() => toggleCreateRole(role.id)}
                    className={`inline-flex items-center gap-1 px-3 py-1.5 rounded-md text-sm border transition-colors ${createForm.roleId === role.id
                      ? 'bg-black text-white border-black'
                      : 'border-[#D0D5DD] text-[#292929] bg-transparent hover:bg-[#F5F5F5]'
                      }`}
                  >
                    <Shield className="w-3 h-3" />
                    {role.name === 'Administrator' ? 'Адміністратор' : role.name === 'Coordinator' ? 'Координатор' : role.name === 'User' ? 'Користувач' : role.name}
                  </button>
                ))}
              </div>
            </div>
          </div>
          <DialogFooter>
            <Button
              variant="outline"
              onClick={() => setIsCreateDialogOpen(false)}
              disabled={isCreating}
              className="border border-[#D0D5DD] text-[#292929] bg-transparent hover:bg-[#F5F5F5] hover:text-[#292929] focus:outline-none focus:ring-0 focus-visible:ring-0 focus-visible:ring-offset-0"
            >
              Скасувати
            </Button>
            <Button onClick={handleCreateUser} disabled={isCreating}>
              {isCreating ? 'Створення...' : 'Створити'}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      {/* Edit User Dialog */}
      <Dialog open={isEditDialogOpen} onOpenChange={setIsEditDialogOpen}>
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle>Редагувати користувача</DialogTitle>
            <DialogDescription>
              Змініть дані користувача
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            <div className="grid gap-2">
              <Label htmlFor="edit-email">Email</Label>
              <Input
                id="edit-email"
                type="email"
                value={editForm.email}
                onChange={(e) =>
                  setEditForm((prev) => ({ ...prev, email: e.target.value }))
                }
              />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="edit-userName">Ім'я</Label>
              <Input
                id="edit-userName"
                value={editForm.userName}
                onChange={(e) =>
                  setEditForm((prev) => ({ ...prev, userName: e.target.value }))
                }
              />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="edit-userSurname">Прізвище</Label>
              <Input
                id="edit-userSurname"
                value={editForm.userSurname}
                onChange={(e) =>
                  setEditForm((prev) => ({ ...prev, userSurname: e.target.value }))
                }
              />
            </div>
            <div className="grid gap-2">
              <Label>Ролі</Label>
              <div className="flex flex-wrap gap-2">
                {roles.map((role) => (
                  <button
                    key={role.id}
                    type="button"
                    onClick={() => toggleEditRole(role.id)}
                    className={`inline-flex items-center gap-1 px-3 py-1.5 rounded-md text-sm border transition-colors ${editForm.roleId === role.id
                      ? 'bg-black text-white border-black'
                      : 'border-[#D0D5DD] text-[#292929] bg-transparent hover:bg-[#F5F5F5]'
                      }`}
                  >
                    <Shield className="w-3 h-3" />
                    {role.name === 'Administrator' ? 'Адміністратор' : role.name === 'Coordinator' ? 'Координатор' : role.name === 'User' ? 'Користувач' : role.name}
                  </button>
                ))}
              </div>
            </div>
          </div>
          <DialogFooter>
            <Button
              variant="outline"
              onClick={() => setIsEditDialogOpen(false)}
              disabled={isEditing}
              className="border border-[#D0D5DD] text-[#292929] bg-transparent hover:bg-[#F5F5F5] hover:text-[#292929] focus:outline-none focus:ring-0 focus-visible:ring-0 focus-visible:ring-offset-0"
            >
              Скасувати
            </Button>
            <Button onClick={handleEditUser} disabled={isEditing}>
              {isEditing ? 'Збереження...' : 'Зберегти'}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      {/* Delete Confirmation Dialog */}
      <Dialog open={deleteDialogOpen} onOpenChange={setDeleteDialogOpen}>
        <DialogContent className="sm:max-w-[490px]">
          <DialogHeader>
            <DialogTitle>Підтвердження видалення</DialogTitle>
            <DialogDescription>
              Ви впевнені, що хочете видалити цього користувача?
            </DialogDescription>
          </DialogHeader>
          {userToDelete && (
            <div className="py-4">
              <div className="space-y-2">
                <p className="text-sm">
                  <span className="font-semibold">Email:</span> {userToDelete.email}
                </p>
                {(userToDelete.name || userToDelete.surname) && (
                  <p className="text-sm">
                    <span className="font-semibold">Ім'я:</span> {userToDelete.name} {userToDelete.surname}
                  </p>
                )}
                {userToDelete.role && (
                  <p className="text-sm">
                    <span className="font-semibold">Роль:</span> {userToDelete.role.name === 'Administrator' ? 'Адміністратор' : userToDelete.role.name === 'Coordinator' ? 'Координатор' : userToDelete.role.name === 'User' ? 'Користувач' : userToDelete.role.name}
                  </p>
                )}
              </div>
              <div className="mt-4 p-3 bg-yellow-50 border border-yellow-200 rounded-lg">
                <p className="text-sm text-yellow-800">
                  <strong>Увага:</strong> Всі проблеми, коментарі та рейтинги створені цим користувачем будуть видалені.
                </p>
              </div>
            </div>
          )}
          <DialogFooter>
            <Button
              variant="outline"
              onClick={() => {
                setDeleteDialogOpen(false)
                setUserToDelete(null)
              }}
              disabled={deletingUserId !== null}
              className="border border-[#D0D5DD] text-[#292929] bg-transparent hover:bg-[#F5F5F5] hover:text-[#292929] focus:outline-none focus:ring-0 focus-visible:ring-0 focus-visible:ring-offset-0"
            >
              Скасувати
            </Button>
            <Button
              variant="destructive"
              onClick={handleDeleteUser}
              disabled={deletingUserId !== null}
            >
              {deletingUserId ? 'Видалення...' : 'Так, видалити'}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  )
}
