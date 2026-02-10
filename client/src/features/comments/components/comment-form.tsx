import { useState, useEffect } from 'react'
import type { Comment, CreateComment } from '@/types'
import { FormField } from '@/components/shared/form-field'
import { Button } from '@/components/ui/button'
import { Textarea } from '@/components/ui/textarea'
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
  problemId?: string | null
}

export function CommentForm({
  open,
  onOpenChange,
  onSubmit,
  initialData,
  isLoading,
  problemId,
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
      setFormData({ content: '', problemId: problemId || '' })
    }
  }, [initialData, open, problemId])

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    onSubmit(formData, initialData?.id || undefined)
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-xl w-full [&>button]:focus:outline-none [&>button]:focus:ring-0 [&>button]:focus-visible:ring-0 [&>button]:focus-visible:ring-offset-0 [&>button]:border-0">
        <DialogHeader>
          <DialogTitle>
            {initialData ? 'Редагувати коментар' : 'Створити коментар'}
          </DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit}>
          <div className="space-y-4 py-4">
            <FormField
              label=""
              name="content"
              type="textarea"
              value={formData.content}
              onChange={(value) =>
                setFormData({ ...formData, content: value as string })
              }
              placeholder="Введіть текст коментаря"
            >
              <Textarea
                value={formData.content}
                onChange={(e) =>
                  setFormData({ ...formData, content: e.target.value })
                }
                placeholder="Введіть текст коментаря"
                required
                className="min-h-[140px] bg-white border border-[#D0D5DD] rounded-lg text-sm text-[#1F2732] placeholder:text-gray-400 focus-visible:ring-0 focus-visible:border-[#1F2732]"
              />
            </FormField>
          </div>
          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => onOpenChange(false)}
              disabled={isLoading}
              className="border border-[#D0D5DD] text-[#292929] bg-transparent hover:bg-[#F5F5F5] hover:text-[#292929] focus:outline-none focus:ring-0 focus-visible:ring-0 focus-visible:ring-offset-0"
            >
              Скасувати
            </Button>
            <Button
              type="submit"
              disabled={isLoading}
              className="bg-[#E42556] hover:bg-[#E42556]/90 focus:outline-none focus:ring-0 focus-visible:ring-0 focus-visible:ring-offset-0"
            >
              {isLoading
                ? (initialData ? 'Оновлення...' : 'Створення...')
                : (initialData ? 'Оновити' : 'Створити')}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}
