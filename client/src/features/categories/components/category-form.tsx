import { useState, useEffect } from 'react'
import type { Category } from '@/types'
import { FormField } from '@/components/shared/form-field'
import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'

interface CategoryFormProps {
  open: boolean
  onOpenChange: (open: boolean) => void
  onSubmit: (data: Omit<Category, 'id'> | Category) => void
  initialData?: Category | null
  isLoading?: boolean
}

export function CategoryForm({
  open,
  onOpenChange,
  onSubmit,
  initialData,
  isLoading,
}: CategoryFormProps) {
  const [formData, setFormData] = useState<Omit<Category, 'id'>>({
    name: '',
  })

  useEffect(() => {
    if (initialData) {
      setFormData({
        name: initialData.name,
      })
    } else {
      setFormData({ name: '' })
    }
  }, [initialData, open])

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    if (initialData?.id) {
      onSubmit({ ...formData, id: initialData.id })
    } else {
      onSubmit(formData)
    }
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>
            {initialData ? 'Edit Category' : 'Create Category'}
          </DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit}>
          <div className="space-y-4 py-4">
            <FormField
              label="Name"
              name="name"
              value={formData.name}
              onChange={(value) =>
                setFormData({ ...formData, name: value as string })
              }
              required
              placeholder="Enter category name"
            />
          </div>
          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => onOpenChange(false)}
              disabled={isLoading}
            >
              Cancel
            </Button>
            <Button type="submit" disabled={isLoading}>
              {isLoading ? 'Saving...' : 'Save'}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}
