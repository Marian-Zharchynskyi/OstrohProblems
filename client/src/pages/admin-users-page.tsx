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
  const { tokens } = useAuth()
  const [users, setUsers] = useState<UserDto[]>([])
  const [roles, setRoles] = useState<Role[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [deletingUserId, setDeletingUserId] = useState<string | null>(null)

  // Create user dialog state
  const [isCreateDialogOpen, setIsCreateDialogOpen] = useState(false)
  const [isCreating, setIsCreating] = useState(false)
  const [createForm, setCreateForm] = useState<CreateUserDto>({
    email: '',
    password: '',
    fullName: '',
    roleId: '',
  })

  // Edit user dialog state
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false)
  const [isEditing, setIsEditing] = useState(false)
  const [editingUser, setEditingUser] = useState<UserDto | null>(null)
  const [editForm, setEditForm] = useState({
    email: '',
    userName: '',
    roleId: '',
  })

  useEffect(() => {
    const loadData = async () => {
      if (!tokens?.accessToken) return

      try {
        setIsLoading(true)
        const [usersData, rolesData] = await Promise.all([
          userService.getAllUsers(tokens.accessToken),
          rolesApi.getAll(),
        ])
        setUsers(usersData)
        setRoles(rolesData)
      } catch (error) {
        console.error('Failed to load data:', error)
      } finally {
        setIsLoading(false)
      }
    }

    loadData()
  }, [tokens?.accessToken])

  const handleDeleteUser = async (userId: string) => {
    if (!tokens?.accessToken) return
    if (!confirm('Ви впевнені, що хочете видалити цього користувача?')) return

    try {
      setDeletingUserId(userId)
      await userService.deleteUser(userId, tokens.accessToken)
      setUsers(users.filter((u) => u.id !== userId))
    } catch (error) {
      console.error('Failed to delete user:', error)
      alert('Не вдалося видалити користувача')
    } finally {
      setDeletingUserId(null)
    }
  }

  const handleCreateUser = async () => {
    if (!tokens?.accessToken) return
    if (!createForm.email || !createForm.password || !createForm.roleId) {
      alert('Заповніть всі обов\'язкові поля')
      return
    }

    try {
      setIsCreating(true)
      const newUser = await userService.createUser(createForm, tokens.accessToken)
      setUsers([...users, newUser])
      setIsCreateDialogOpen(false)
      setCreateForm({ email: '', password: '', fullName: '', roleId: '' })
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
      userName: user.fullName || '',
      roleId: user.roles && user.roles.length > 0 ? user.roles[0].id : '',
    })
    setIsEditDialogOpen(true)
  }

  const handleEditUser = async () => {
    if (!tokens?.accessToken || !editingUser) return

    try {
      setIsEditing(true)

      // Update user info
      await userService.updateUser(
        editingUser.id,
        { email: editForm.email, userName: editForm.userName },
        tokens.accessToken
      )

      // Update role
      const updatedUser = await userService.updateUserRoles(
        editingUser.id,
        editForm.roleId,
        tokens.accessToken
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
                    <TableHead>Повне ім'я</TableHead>
                    <TableHead>Ролі</TableHead>
                    <TableHead>Зображення</TableHead>
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
                        {user.fullName || (
                          <span className="text-muted-foreground italic">
                            Не вказано
                          </span>
                        )}
                      </TableCell>
                      <TableCell>
                        <div className="flex gap-1 flex-wrap">
                          {user.roles && user.roles.length > 0 ? (
                            user.roles.map((role) => (
                              <span
                                key={role.id}
                                className="inline-flex items-center gap-1 px-2 py-1 bg-primary/10 text-primary rounded text-xs"
                              >
                                <Shield className="w-3 h-3" />
                                {role.name}
                              </span>
                            ))
                          ) : (
                            <span className="text-muted-foreground text-sm">
                              Немає ролей
                            </span>
                          )}
                        </div>
                      </TableCell>
                      <TableCell>
                        {user.image?.filePath ? (
                          <img
                            src={user.image.filePath}
                            alt={user.fullName || user.email}
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
                            size="sm"
                            onClick={() => handleOpenEditDialog(user)}
                          >
                            <Pencil className="w-4 h-4 mr-1" />
                            Редагувати
                          </Button>
                          <Button
                            variant="destructive"
                            size="sm"
                            onClick={() => handleDeleteUser(user.id)}
                            disabled={deletingUserId === user.id}
                          >
                            <Trash2 className="w-4 h-4 mr-1" />
                            {deletingUserId === user.id ? 'Видалення...' : 'Видалити'}
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
        <DialogContent className="sm:max-w-[425px]">
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
                placeholder="Мінімум 6 символів"
                value={createForm.password}
                onChange={(e) =>
                  setCreateForm((prev) => ({ ...prev, password: e.target.value }))
                }
              />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="fullName">Повне ім'я</Label>
              <Input
                id="fullName"
                placeholder="Іван Петренко"
                value={createForm.fullName}
                onChange={(e) =>
                  setCreateForm((prev) => ({ ...prev, fullName: e.target.value }))
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
                    className={`inline-flex items-center gap-1 px-3 py-1.5 rounded-md text-sm border transition-colors ${
                      createForm.roleId === role.id
                        ? 'bg-primary text-primary-foreground border-primary'
                        : 'bg-background hover:bg-muted border-input'
                    }`}
                  >
                    <Shield className="w-3 h-3" />
                    {role.name}
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
              <Label htmlFor="edit-userName">Повне ім'я</Label>
              <Input
                id="edit-userName"
                value={editForm.userName}
                onChange={(e) =>
                  setEditForm((prev) => ({ ...prev, userName: e.target.value }))
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
                    className={`inline-flex items-center gap-1 px-3 py-1.5 rounded-md text-sm border transition-colors ${
                      editForm.roleId === role.id
                        ? 'bg-primary text-primary-foreground border-primary'
                        : 'bg-background hover:bg-muted border-input'
                    }`}
                  >
                    <Shield className="w-3 h-3" />
                    {role.name}
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
            >
              Скасувати
            </Button>
            <Button onClick={handleEditUser} disabled={isEditing}>
              {isEditing ? 'Збереження...' : 'Зберегти'}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  )
}
