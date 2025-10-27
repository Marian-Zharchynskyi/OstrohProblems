import { useState, useEffect } from 'react'
import { useAuth } from '@/contexts/auth-context'
import { userService } from '@/services/user.service'
import type { UserDto, UpdateUserDto } from '@/types/user'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { User, Mail, Shield, Image as ImageIcon } from 'lucide-react'

export function ProfilePage() {
  const { tokens } = useAuth()
  const [userDetails, setUserDetails] = useState<UserDto | null>(null)
  const [isEditing, setIsEditing] = useState(false)
  const [isLoading, setIsLoading] = useState(true)
  const [isSaving, setIsSaving] = useState(false)
  const [formData, setFormData] = useState<UpdateUserDto>({
    email: '',
    userName: '',
  })
  const [selectedImage, setSelectedImage] = useState<File | null>(null)

  useEffect(() => {
    const loadUserDetails = async () => {
      if (!tokens?.accessToken) return

      try {
        setIsLoading(true)
        const data = await userService.getCurrentUser(tokens.accessToken)
        setUserDetails(data)
        setFormData({
          email: data.email,
          userName: data.fullName || '',
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
      setIsEditing(false)
    } catch (error) {
      console.error('Failed to update user:', error)
    } finally {
      setIsSaving(false)
    }
  }

  const handleImageUpload = async () => {
    if (!selectedImage || !userDetails || !tokens?.accessToken) return

    try {
      setIsSaving(true)
      const updated = await userService.uploadUserImage(
        userDetails.id,
        selectedImage,
        tokens.accessToken
      )
      setUserDetails(updated)
      setSelectedImage(null)
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
    <div className="space-y-6 max-w-3xl">
      <div>
        <h1 className="text-3xl font-bold tracking-tight">My Profile</h1>
        <p className="text-muted-foreground mt-2">
          Manage your personal information
        </p>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Profile Information</CardTitle>
          <CardDescription>
            Update your personal details and profile picture
          </CardDescription>
        </CardHeader>
        <CardContent className="space-y-6">
          {/* Profile Image */}
          <div className="flex items-center gap-6">
            <div className="w-24 h-24 rounded-full bg-muted flex items-center justify-center overflow-hidden">
              {userDetails.image?.filePath ? (
                <img
                  src={userDetails.image.filePath}
                  alt="Profile"
                  className="w-full h-full object-cover"
                />
              ) : (
                <User className="w-12 h-12 text-muted-foreground" />
              )}
            </div>
            <div className="space-y-2">
              <Input
                type="file"
                accept="image/*"
                onChange={(e) => setSelectedImage(e.target.files?.[0] || null)}
                disabled={isSaving}
              />
              {selectedImage && (
                <Button
                  onClick={handleImageUpload}
                  disabled={isSaving}
                  size="sm"
                >
                  <ImageIcon className="w-4 h-4 mr-2" />
                  Upload Image
                </Button>
              )}
            </div>
          </div>

          {/* User Details */}
          <div className="space-y-4">
            <div className="space-y-2">
              <Label htmlFor="fullName">
                <User className="w-4 h-4 inline mr-2" />
                Full Name
              </Label>
              {isEditing ? (
                <Input
                  id="fullName"
                  value={formData.userName}
                  onChange={(e) =>
                    setFormData({ ...formData, userName: e.target.value })
                  }
                  disabled={isSaving}
                />
              ) : (
                <p className="text-sm p-2 bg-muted rounded">
                  {userDetails.fullName || 'Not set'}
                </p>
              )}
            </div>

            <div className="space-y-2">
              <Label htmlFor="email">
                <Mail className="w-4 h-4 inline mr-2" />
                Email
              </Label>
              {isEditing ? (
                <Input
                  id="email"
                  type="email"
                  value={formData.email}
                  onChange={(e) =>
                    setFormData({ ...formData, email: e.target.value })
                  }
                  disabled={isSaving}
                />
              ) : (
                <p className="text-sm p-2 bg-muted rounded">
                  {userDetails.email}
                </p>
              )}
            </div>

            <div className="space-y-2">
              <Label>
                <Shield className="w-4 h-4 inline mr-2" />
                Roles
              </Label>
              <div className="flex gap-2 flex-wrap">
                {userDetails.roles && userDetails.roles.length > 0 ? (
                  userDetails.roles.map((role) => (
                    <span
                      key={role.id}
                      className="px-3 py-1 bg-primary/10 text-primary rounded-full text-sm"
                    >
                      {role.name}
                    </span>
                  ))
                ) : (
                  <span className="text-sm text-muted-foreground">
                    No roles assigned
                  </span>
                )}
              </div>
            </div>
          </div>

          {/* Action Buttons */}
          <div className="flex gap-2 pt-4">
            {isEditing ? (
              <>
                <Button onClick={handleSave} disabled={isSaving}>
                  Save Changes
                </Button>
                <Button
                  variant="outline"
                  onClick={() => {
                    setIsEditing(false)
                    setFormData({
                      email: userDetails.email,
                      userName: userDetails.fullName || '',
                    })
                  }}
                  disabled={isSaving}
                >
                  Cancel
                </Button>
              </>
            ) : (
              <Button onClick={() => setIsEditing(true)}>Edit Profile</Button>
            )}
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
