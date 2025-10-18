import { useState, useEffect } from 'react'
import type { Comment, CreateComment } from '@/types'
import { FormField } from '@/components/shared/form-field'
import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'

interface CommentFormProps {
  open: boolean
  onOpenChange: (open: boolean) => void
  onSubmit: (data: CreateComment, id?: string) => void
  initialData?: Comment | null
  isLoading?: boolean
}

export function CommentForm({
  open,
  onOpenChange,
  onSubmit,
  initialData,
  isLoading,
}: CommentFormProps) {
  const [formData, setFormData] = useState<CreateComment>({
    content: '',
    problemId: '',
  })

  useEffect(() => {
    if (initialData) {
      setFormData({
        content: initialData.content,
        problemId: initialData.problemId,
      })
    } else {
      setFormData({ content: '', problemId: '' })
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
            {initialData ? 'Edit Comment' : 'Create Comment'}
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
              label="Content"
              name="content"
              type="textarea"
              value={formData.content}
              onChange={(value) =>
                setFormData({ ...formData, content: value as string })
              }
              required
              placeholder="Enter comment content"
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
