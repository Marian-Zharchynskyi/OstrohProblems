import { useState, useEffect } from 'react'
import type { Problem, CreateProblem } from '@/types'
import { PriorityConstants } from '@/types'
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
import { Info } from 'lucide-react'
import { CATEGORIES } from '@/constants/categories'

const PRIORITIES = [
  { value: PriorityConstants.Low, label: 'Низький' },
  { value: PriorityConstants.Medium, label: 'Середній' },
  { value: PriorityConstants.High, label: 'Високий' },
  { value: PriorityConstants.Critical, label: 'Критичний' },
]

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
  const [formData, setFormData] = useState<CreateProblem>({
    title: '',
    latitude: 0,
    longitude: 0,
    description: '',
    categoryNames: [],
    priority: PriorityConstants.Medium,
  })

  useEffect(() => {
    if (initialData) {
      setFormData({
        title: initialData.title,
        latitude: initialData.latitude,
        longitude: initialData.longitude,
        description: initialData.description,
        categoryNames: initialData.categories || [],
        priority: initialData.priority || PriorityConstants.Medium,
      })
    } else {
      setFormData({
        title: '',
        latitude: 0,
        longitude: 0,
        description: '',
        categoryNames: [],
        priority: PriorityConstants.Medium,
      })
    }
  }, [initialData, open])

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    onSubmit(formData, initialData?.id || undefined)
  }

  const handleCategoryToggle = (categoryName: string) => {
    setFormData((prev) => ({
      ...prev,
      categoryNames: prev.categoryNames.includes(categoryName)
        ? prev.categoryNames.filter((name) => name !== categoryName)
        : [...prev.categoryNames, categoryName],
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
              <div className="flex items-center gap-2">
                <Label>Пріоритет</Label>
                <span
                  title="Пріоритет може бути змінений координатором під час валідації проблеми"
                  className="cursor-help"
                >
                  <Info className="h-4 w-4 text-gray-400" />
                </span>
              </div>
              <select
                value={formData.priority || PriorityConstants.Medium}
                onChange={(e) => setFormData({ ...formData, priority: e.target.value })}
                className="w-full border rounded-md p-2 bg-white"
              >
                {PRIORITIES.map((priority) => (
                  <option key={priority.value} value={priority.value}>
                    {priority.label}
                  </option>
                ))}
              </select>
            </div>

            <div className="space-y-2">
              <Label>Категорії</Label>
              <div className="border rounded-md p-4 space-y-2 max-h-40 overflow-y-auto">
                {CATEGORIES.map((category) => (
                  <div key={category.value} className="flex items-center space-x-2">
                    <input
                      type="checkbox"
                      id={`category-${category.value}`}
                      checked={formData.categoryNames.includes(category.value)}
                      onChange={() => handleCategoryToggle(category.value)}
                      className="h-4 w-4"
                    />
                    <label
                      htmlFor={`category-${category.value}`}
                      className="text-sm cursor-pointer"
                    >
                      {category.label}
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
