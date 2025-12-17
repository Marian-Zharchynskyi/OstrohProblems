import { useState, useEffect } from 'react'
import type { Rating, CreateRating } from '@/types'
import { FormField } from '@/components/shared/form-field'
import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'

interface RatingFormProps {
  open: boolean
  onOpenChange: (open: boolean) => void
  onSubmit: (data: CreateRating, id?: string) => void
  initialData?: Rating | null
  isLoading?: boolean
}

export function RatingForm({
  open,
  onOpenChange,
  onSubmit,
  initialData,
  isLoading,
}: RatingFormProps) {
  const [formData, setFormData] = useState<CreateRating>({
    points: 0,
    problemId: '',
  })

  useEffect(() => {
    if (initialData) {
      setFormData({
        points: initialData.points,
        problemId: initialData.problemId,
      })
    } else {
      setFormData({ points: 0, problemId: '' })
    }
  }, [initialData, open])

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    onSubmit(formData, initialData?.id || undefined)
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>
            {initialData ? 'Edit Rating' : 'Create Rating'}
          </DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit}>
          <div className="space-y-4 py-4">
            <FormField
              label="Problem ID"
              name="problemId"
              value={formData.problemId}
              onChange={(value) =>
                setFormData({ ...formData, problemId: value as string })
              }
              required
              placeholder="Enter problem ID"
              disabled={!!initialData}
            />
            <FormField
              label="Points"
              name="points"
              type="number"
              value={formData.points}
              onChange={(value) =>
                setFormData({ ...formData, points: value as number })
              }
              required
              placeholder="Enter rating points"
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
