import { useState, useEffect } from 'react'
import type { Problem, CreateProblem } from '@/types'
import { FormField } from '@/components/shared/form-field'
import { Button } from '@/components/ui/button'
import { Label } from '@/components/ui/label'
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import { useStatuses } from '@/features/statuses/hooks/use-statuses'
import { useCategories } from '@/features/categories/hooks/use-categories'

interface ProblemFormProps {
  open: boolean
  onOpenChange: (open: boolean) => void
  onSubmit: (data: CreateProblem, id?: string) => void
  initialData?: Problem | null
  isLoading?: boolean
}

export function ProblemForm({
  open,
  onOpenChange,
  onSubmit,
  initialData,
  isLoading,
}: ProblemFormProps) {
  const { data: statuses } = useStatuses()
  const { data: categories } = useCategories()

  const [formData, setFormData] = useState<CreateProblem>({
    title: '',
    latitude: 0,
    longitude: 0,
    description: '',
    problemStatusId: '',
    problemCategoryIds: [],
  })

  useEffect(() => {
    if (initialData) {
      setFormData({
        title: initialData.title,
        latitude: initialData.latitude,
        longitude: initialData.longitude,
        description: initialData.description,
        problemStatusId: initialData.problemStatus?.id || '',
        problemCategoryIds: initialData.categories?.map((c) => c.id || '') || [],
      })
    } else {
      setFormData({
        title: '',
        latitude: 0,
        longitude: 0,
        description: '',
        problemStatusId: '',
        problemCategoryIds: [],
      })
    }
  }, [initialData, open])

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    onSubmit(formData, initialData?.id || undefined)
  }

  const handleCategoryToggle = (categoryId: string) => {
    setFormData((prev) => ({
      ...prev,
      problemCategoryIds: prev.problemCategoryIds.includes(categoryId)
        ? prev.problemCategoryIds.filter((id) => id !== categoryId)
        : [...prev.problemCategoryIds, categoryId],
    }))
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-2xl max-h-[90vh] overflow-y-auto">
        <DialogHeader>
          <DialogTitle>
            {initialData ? 'Edit Problem' : 'Create Problem'}
          </DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit}>
          <div className="space-y-4 py-4">
            <FormField
              label="Title"
              name="title"
              value={formData.title}
              onChange={(value) =>
                setFormData({ ...formData, title: value as string })
              }
              required
              placeholder="Enter problem title"
            />

            <FormField
              label="Description"
              name="description"
              type="textarea"
              value={formData.description}
              onChange={(value) =>
                setFormData({ ...formData, description: value as string })
              }
              required
              placeholder="Enter problem description"
            />

            <div className="grid grid-cols-2 gap-4">
              <FormField
                label="Latitude"
                name="latitude"
                type="number"
                value={formData.latitude}
                onChange={(value) =>
                  setFormData({ ...formData, latitude: value as number })
                }
                required
                placeholder="Enter latitude"
              />

              <FormField
                label="Longitude"
                name="longitude"
                type="number"
                value={formData.longitude}
                onChange={(value) =>
                  setFormData({ ...formData, longitude: value as number })
                }
                required
                placeholder="Enter longitude"
              />
            </div>

            <div className="space-y-2">
              <Label htmlFor="status">
                Status <span className="text-destructive ml-1">*</span>
              </Label>
              <select
                id="status"
                className="flex h-10 w-full rounded-md border border-input bg-background px-3 py-2 text-sm ring-offset-background focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2"
                value={formData.problemStatusId}
                onChange={(e) =>
                  setFormData({ ...formData, problemStatusId: e.target.value })
                }
                required
              >
                <option value="">Select a status</option>
                {statuses?.map((status) => (
                  <option key={status.id} value={status.id || ''}>
                    {status.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="space-y-2">
              <Label>Categories</Label>
              <div className="border rounded-md p-4 space-y-2 max-h-40 overflow-y-auto">
                {categories?.map((category) => (
                  <div key={category.id} className="flex items-center space-x-2">
                    <input
                      type="checkbox"
                      id={`category-${category.id}`}
                      checked={formData.problemCategoryIds.includes(category.id || '')}
                      onChange={() => handleCategoryToggle(category.id || '')}
                      className="h-4 w-4"
                    />
                    <label
                      htmlFor={`category-${category.id}`}
                      className="text-sm cursor-pointer"
                    >
                      {category.name}
                    </label>
                  </div>
                ))}
              </div>
            </div>
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
