import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { useAuth } from '@/contexts/auth-context'
import { userService } from '@/services/user.service'
import type { UserDto, UpdateUserDto } from '@/types/user'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { User, FileText } from 'lucide-react'

export function ProfilePage() {
  const { tokens, signOut } = useAuth()
  const navigate = useNavigate()
  const [userDetails, setUserDetails] = useState<UserDto | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [isSaving, setIsSaving] = useState(false)
  const [activeTab, setActiveTab] = useState<'profile' | 'problems'>('profile')
  const [formData, setFormData] = useState<UpdateUserDto>({
    email: '',
    name: '',
    surname: '',
    phoneNumber: '',
  })
  const [passwordData, setPasswordData] = useState({
    currentPassword: '',
    newPassword: '',
  })
  const [deleteConfirmation, setDeleteConfirmation] = useState(false)
  const [passwordError, setPasswordError] = useState('')
  const [passwordSuccess, setPasswordSuccess] = useState('')
  const [isBasicInfoEditing, setIsBasicInfoEditing] = useState(false)
  const [isSecurityEditing, setIsSecurityEditing] = useState(false)

  useEffect(() => {
    const loadUserDetails = async () => {
      if (!tokens?.accessToken) return

      try {
        setIsLoading(true)
        const data = await userService.getCurrentUser(tokens.accessToken)
        setUserDetails(data)
        setFormData({
          email: data.email,
          name: data.name || '',
          surname: data.surname || '',
          phoneNumber: data.phoneNumber || '',
        })
      } catch (error) {
        console.error('Failed to load user details:', error)
      } finally {
        setIsLoading(false)
      }
    }

    loadUserDetails()
  }, [tokens?.accessToken])

  const handleSave = async () => {
    if (!userDetails || !tokens?.accessToken) return

    try {
      setIsSaving(true)
      const updated = await userService.updateUser(
        userDetails.id,
        formData,
        tokens.accessToken
      )
      setUserDetails(updated)
      setIsBasicInfoEditing(false)
    } catch (error) {
      console.error('Failed to update user:', error)
    } finally {
      setIsSaving(false)
    }
  }

  const handlePasswordChange = async () => {
    if (!userDetails || !tokens?.accessToken) return
    if (!passwordData.currentPassword || !passwordData.newPassword) {
      setPasswordError('Заповніть обидва поля паролю')
      return
    }
    if (passwordData.newPassword.length < 8) {
      setPasswordError('Новий пароль повинен містити мінімум 8 символів')
      return
    }

    try {
      setIsSaving(true)
      setPasswordError('')
      setPasswordSuccess('')
      await userService.changePassword(
        userDetails.id,
        {
          currentPassword: passwordData.currentPassword,
          newPassword: passwordData.newPassword,
        },
        tokens.accessToken
      )
      setPasswordSuccess('Пароль успішно змінено')
      setPasswordData({ currentPassword: '', newPassword: '' })
      setIsSecurityEditing(false)
    } catch (error) {
      setPasswordError('Не вдалося змінити пароль. Перевірте поточний пароль.')
      console.error('Failed to change password:', error)
    } finally {
      setIsSaving(false)
    }
  }

  const handleDeleteAccount = async () => {
    if (!deleteConfirmation || !userDetails || !tokens?.accessToken) return

    try {
      setIsSaving(true)
      await userService.deleteUser(userDetails.id, tokens.accessToken)
      signOut()
      navigate('/')
    } catch (error) {
      console.error('Failed to delete account:', error)
    } finally {
      setIsSaving(false)
    }
  }
  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0]
    if (!file || !userDetails || !tokens?.accessToken) return
    
    try {
      setIsSaving(true)
      const updated = await userService.uploadUserImage(
        userDetails.id,
        file,
        tokens.accessToken
      )
      setUserDetails(updated)
      e.target.value = ''
    } catch (error) {
      console.error('Failed to upload image:', error)
    } finally {
      setIsSaving(false)
    }
  }

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <p className="text-muted-foreground">Loading profile...</p>
      </div>
    )
  }

  if (!userDetails) {
    return (
      <div className="flex items-center justify-center h-64">
        <p className="text-muted-foreground">Failed to load profile</p>
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-white">
      {/* Верхня панель та Навігація */}
      <div className="bg-[#F7F9FA] py-6">
        <div className="max-w-6xl mx-auto px-6">
          <h1 className="text-3xl font-semibold text-[#1F2732] font-['Sora'] mb-6">
            Налаштування профілю
          </h1>
          
          {/* Вкладки навігації */}
          <div className="flex justify-center gap-8">
            <button
              onClick={() => setActiveTab('profile')}
              className={`flex items-center gap-3 pb-3 border-b-4 transition-colors ${
                activeTab === 'profile'
                  ? 'border-[#E42556] text-[#1F2732]'
                  : 'border-transparent text-gray-500 hover:text-gray-700'
              }`}
            >
              <User className="w-6 h-6" />
              <span className="font-medium">Профіль</span>
            </button>
            <button
              onClick={() => setActiveTab('problems')}
              className={`flex items-center gap-3 pb-3 border-b-4 transition-colors ${
                activeTab === 'problems'
                  ? 'border-[#E42556] text-[#1F2732]'
                  : 'border-transparent text-gray-500 hover:text-gray-700'
              }`}
            >
              <FileText className="w-6 h-6" />
              <span className="font-medium">Подані проблеми</span>
            </button>
          </div>
        </div>
      </div>

      {activeTab === 'profile' && (
        <div className="max-w-6xl mx-auto px-6 py-8">
          {/* Секція Аватара */}
          <div className="bg-white p-6 mb-6 rounded-[10px]">
            <div className="flex flex-col md:flex-row items-center justify-center gap-8 text-center md:text-left">
              <div className="relative">
                <div className="w-24 h-24 rounded-full bg-gray-200 flex items-center justify-center overflow-hidden">
                  {userDetails.image?.url ? (
                    <img
                      src={userDetails.image.url}
                      alt="Profile"
                      className="w-full h-full object-cover"
                    />
                  ) : (
                    <User className="w-12 h-12 text-gray-500" />
                  )}
                </div>
                <label className="absolute -top-1 -right-1 cursor-pointer hover:opacity-80">
                  <img src="/icons/pen.png" alt="Edit" className="w-4 h-4" />
                  <input
                    type="file"
                    accept="image/*"
                    onChange={handleImageUpload}
                    className="hidden"
                    disabled={isSaving}
                  />
                </label>
              </div>
              <div className="flex-1 md:max-w-2xl">
                <h2 className="text-xl font-bold text-[#1F2732] font-['Mulish'] mb-2 md:mb-3">
                  Редагування профілю
                </h2>
                <p className="text-[#464646] font-['Mulish']">
                  Змініть деталі вашого профілю за вашими уподобаннями
                </p>
              </div>
            </div>
          </div>

          {/* Картка 1: Основна інформація */}
          <Card className="bg-white border border-gray-200 mb-6 rounded-[10px]">
            <CardHeader className="flex flex-row items-center justify-between pb-8">
              <CardTitle className="text-lg font-bold text-[#1F2732] font-['Mulish']">
                Основна інформація
              </CardTitle>
              <button
                type="button"
                onClick={() => {
                   if (isBasicInfoEditing && userDetails) {
                      setFormData({
                        email: userDetails.email,
                        name: userDetails.name || '',
                        surname: userDetails.surname || '',
                        phoneNumber: userDetails.phoneNumber || '',
                      })
                   }
                   setIsBasicInfoEditing(!isBasicInfoEditing)
                }}
                className={`p-0 bg-transparent border-none shadow-none transition-opacity ${isBasicInfoEditing ? 'opacity-100 brightness-75' : 'opacity-100 hover:opacity-80'}`}
              >
                <img src="/icons/pen.png" alt="Edit" className="w-5 h-5 cursor-pointer" />
              </button>
            </CardHeader>
            <CardContent>
              <div className="grid grid-cols-2 gap-x-4 gap-y-8 mb-8">
                <div>
                  <Label htmlFor="name" className="block text-sm font-medium text-gray-700 mb-3">
                    Ім'я <span className="text-[#E42556]">*</span>
                  </Label>
                  <Input
                    id="name"
                    value={formData.name || ''}
                    onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                    className="bg-[#F0F1F2] border-none rounded-lg placeholder-[#8C8C8C] placeholder:font-['Mulish'] placeholder:font-medium"
                    disabled={!isBasicInfoEditing || isSaving}
                    placeholder="Введіть тут"
                  />
                </div>
                <div>
                  <Label htmlFor="surname" className="block text-sm font-medium text-gray-700 mb-3">
                    Прізвище
                  </Label>
                  <Input
                    id="surname"
                    value={formData.surname || ''}
                    onChange={(e) => setFormData({ ...formData, surname: e.target.value })}
                    className="bg-[#F0F1F2] border-none rounded-lg placeholder-[#8C8C8C] placeholder:font-['Mulish'] placeholder:font-medium"
                    disabled={!isBasicInfoEditing || isSaving}
                    placeholder="Введіть тут"
                  />
                </div>
                <div>
                  <Label htmlFor="email" className="block text-sm font-medium text-gray-700 mb-3">
                    Електронна пошта <span className="text-[#E42556]">*</span>
                  </Label>
                  <Input
                    id="email"
                    type="email"
                    value={formData.email}
                    onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                    className="bg-[#F0F1F2] border-none rounded-lg placeholder-[#8C8C8C] placeholder:font-['Mulish'] placeholder:font-medium"
                    disabled={!isBasicInfoEditing || isSaving}
                    placeholder="Введіть тут"
                  />
                </div>
                <div>
                  <Label htmlFor="phone" className="block text-sm font-medium text-gray-700 mb-3">
                    Номер телефону
                  </Label>
                  <Input
                    id="phone"
                    value={formData.phoneNumber || ''}
                    onChange={(e) => setFormData({ ...formData, phoneNumber: e.target.value })}
                    placeholder="Введіть тут"
                    className="bg-[#F0F1F2] border-none rounded-lg placeholder-[#8C8C8C] placeholder:font-['Mulish'] placeholder:font-medium"
                    disabled={!isBasicInfoEditing || isSaving}
                  />
                </div>
              </div>
              
              <div className="flex justify-between items-center mt-6">
                <p className="text-sm font-extrabold text-[#1F2732]">
                  <span className="text-[#E42556]">*</span> - Обов'язкове поле для заповнення
                </p>
                {isBasicInfoEditing && (
                  <Button
                    onClick={handleSave}
                    disabled={isSaving}
                    className="bg-[#E42556] hover:bg-[#E42556]/90 text-white rounded-lg px-6"
                  >
                    {isSaving ? 'Збереження...' : 'Зберегти'}
                  </Button>
                )}
              </div>
            </CardContent>
          </Card>

          {/* Картка 2: Безпека профілю */}
          <Card className="bg-white border border-gray-200 mb-6 rounded-[10px]">
            <CardHeader className="flex flex-row items-center justify-between pb-8">
              <CardTitle className="text-lg font-bold text-[#1F2732] font-['Mulish']">
                Безпека профілю
              </CardTitle>
              <button
                type="button"
                onClick={() => {
                  if (isSecurityEditing) {
                    setPasswordData({ currentPassword: '', newPassword: '' })
                    setPasswordError('')
                    setPasswordSuccess('')
                  }
                  setIsSecurityEditing(!isSecurityEditing)
                }}
                className={`p-0 bg-transparent border-none shadow-none transition-opacity ${isSecurityEditing ? 'opacity-100 brightness-75' : 'opacity-100 hover:opacity-80'}`}
              >
                <img src="/icons/pen.png" alt="Edit" className="w-5 h-5 cursor-pointer" />
              </button>
            </CardHeader>
            <CardContent>
              <div className="grid grid-cols-2 gap-x-4 gap-y-8 mb-8">
                <div>
                  <Label htmlFor="currentPassword" className="block text-sm font-medium text-gray-700 mb-3">
                    Поточний пароль <span className="text-[#E42556]">*</span>
                  </Label>
                  <Input
                    id="currentPassword"
                    type="password"
                    autoComplete="new-password"
                    value={passwordData.currentPassword}
                    onChange={(e) => setPasswordData({ ...passwordData, currentPassword: e.target.value })}
                    className="bg-[#F0F1F2] border-none rounded-lg placeholder-[#8C8C8C] placeholder:font-['Mulish'] placeholder:font-medium"
                    disabled={!isSecurityEditing || isSaving}
                    placeholder="Введіть тут"
                  />
                </div>
                <div>
                  <Label htmlFor="newPassword" className="block text-sm font-medium text-gray-700 mb-3">
                    Новий пароль <span className="text-[#E42556]">*</span>
                  </Label>
                  <Input
                    id="newPassword"
                    type="password"
                    autoComplete="new-password"
                    value={passwordData.newPassword}
                    onChange={(e) => setPasswordData({ ...passwordData, newPassword: e.target.value })}
                    className="bg-[#F0F1F2] border-none rounded-lg placeholder-[#8C8C8C] placeholder:font-['Mulish'] placeholder:font-medium"
                    disabled={!isSecurityEditing || isSaving}
                    placeholder="Введіть тут"
                  />
                </div>
              </div>
              
              {passwordError && (
                <div className="mb-4 p-3 bg-red-50 border border-red-200 text-red-700 rounded-lg text-sm">
                  {passwordError}
                </div>
              )}
              {passwordSuccess && (
                <div className="mb-4 p-3 bg-green-50 border border-green-200 text-green-700 rounded-lg text-sm">
                  {passwordSuccess}
                </div>
              )}
              
              <div className="flex justify-between items-center mt-6">
                <p className="text-sm font-extrabold text-[#1F2732] font-['Mulish']">
                  Введіть існуючий пароль, щоб створити новий
                </p>
                {isSecurityEditing && (
                  <Button
                    onClick={handlePasswordChange}
                    disabled={isSaving}
                    className="bg-[#E42556] hover:bg-[#E42556]/90 text-white rounded-lg px-6"
                  >
                    {isSaving ? 'Збереження...' : 'Зберегти'}
                  </Button>
                )}
              </div>
            </CardContent>
          </Card>

          {/* Картка 3: Видалити профіль (Danger Zone) */}
          <Card className="bg-white border border-gray-200 rounded-[10px]">
            <CardHeader className="pb-8">
              <CardTitle className="text-lg font-bold text-[#1F2732] font-['Mulish']">
                Видалити профіль
              </CardTitle>
            </CardHeader>
            <CardContent>
              <p className="text-[#1F2732] font-['Mulish'] font-bold mb-8">
                Після того, як Ви видалите ваш профіль, всі створені Вами проблеми будуть видалені, також Ви не зможете створювати нові проблеми
              </p>
              
              <div className="flex justify-between items-center">
                <div className="flex items-center gap-3">
                  <input
                    type="checkbox"
                    id="deleteConfirmation"
                    checked={deleteConfirmation}
                    onChange={(e) => setDeleteConfirmation(e.target.checked)}
                    className="w-4 h-4 text-[#E42556] border-gray-300 rounded focus:ring-[#E42556]"
                  />
                  <Label htmlFor="deleteConfirmation" className="text-sm font-medium text-gray-700">
                    Я підтверджую, що хочу видалити свій профіль
                  </Label>
                </div>
                
                <Button
                  onClick={handleDeleteAccount}
                  disabled={!deleteConfirmation || isSaving}
                  className="bg-red-600 hover:bg-red-700 text-white rounded-lg px-6 font-['Mulish'] font-extrabold"
                >
                  {isSaving ? 'Видалення...' : 'Видалити'}
                </Button>
              </div>
            </CardContent>
          </Card>
        </div>
      )}

      {activeTab === 'problems' && (
        <div className="max-w-6xl mx-auto px-6 py-8">
          <div className="text-center py-12">
            <FileText className="w-16 h-16 text-gray-400 mx-auto mb-4" />
            <h3 className="text-lg font-semibold text-gray-600 mb-2">Подані проблеми</h3>
            <p className="text-gray-500">Тут будуть відображатися ваші подані проблеми</p>
          </div>
        </div>
      )}
    </div>
  )
}
