import { useState } from 'react'
import type { Comment } from '@/types'
import { DataTable, type Column } from '@/components/shared/data-table'
import { PageHeader } from '@/components/shared/page-header'
import { DeleteDialog } from '@/components/shared/delete-dialog'
import { CommentForm } from './comment-form'
import {
  useComments,
  useCreateComment,
  useUpdateComment,
  useDeleteComment,
} from '../hooks/use-comments'
import { toast } from '@/lib/toast'
import { useAuth } from '@/contexts/auth-context'

export function CommentsList() {
  const { user } = useAuth()
  const [isFormOpen, setIsFormOpen] = useState(false)
  const [isDeleteOpen, setIsDeleteOpen] = useState(false)
  const [selectedComment, setSelectedComment] = useState<Comment | null>(null)

  const { data: comments, isLoading } = useComments()
  const createMutation = useCreateComment()
  const updateMutation = useUpdateComment()
  const deleteMutation = useDeleteComment()
  
  const isAdmin = user?.roles?.includes('Administrator')

  const columns: Column<Comment>[] = [
    {
      header: 'ID',
      accessor: 'id',
    },
    {
      header: 'Зміст',
      accessor: 'content',
      cell: (value) => String(value).substring(0, 50) + (String(value).length > 50 ? '...' : ''),
    },
    {
      header: 'Користувач',
      accessor: (item) => item.user?.email || 'N/A',
    },
    {
      header: 'Створено',
      accessor: 'createdAt',
      cell: (value) => new Date(String(value)).toLocaleDateString('uk-UA'),
    },
  ]

  const handleCreate = () => {
    setSelectedComment(null)
    setIsFormOpen(true)
  }

  const handleEdit = (comment: Comment) => {
    setSelectedComment(comment)
    setIsFormOpen(true)
  }

  const handleDelete = (comment: Comment) => {
    setSelectedComment(comment)
    setIsDeleteOpen(true)
  }

  const handleSubmit = async (data: { content: string; problemId: string }, id?: string) => {
    try {
      if (id) {
        await updateMutation.mutateAsync({ id, data })
        toast.success('Коментар успішно оновлено')
      } else {
        await createMutation.mutateAsync(data)
        toast.success('Коментар успішно створено')
      }
      setIsFormOpen(false)
    } catch (error) {
      toast.error('Виникла помилка: ' + error)
    }
  }

  const handleConfirmDelete = async () => {
    if (!selectedComment?.id) return

    try {
      await deleteMutation.mutateAsync(selectedComment.id)
      toast.success('Коментар успішно видалено')
      setIsDeleteOpen(false)
    } catch (error) {
      toast.error('Виникла помилка: ' + error)
    }
  }

  return (
    <div>
      <PageHeader
        title="Коментарі"
        description="Управління коментарями"
        action={!isAdmin ? {
          label: 'Новий коментар',
          onClick: handleCreate,
        } : undefined}
      />

      <DataTable
        data={comments || []}
        columns={columns}
        onEdit={handleEdit}
        onDelete={handleDelete}
        isLoading={isLoading}
        emptyMessage="Коментарів не знайдено"
      />

      <CommentForm
        open={isFormOpen}
        onOpenChange={setIsFormOpen}
        onSubmit={handleSubmit}
        initialData={selectedComment}
        isLoading={createMutation.isPending || updateMutation.isPending}
      />

      <DeleteDialog
        open={isDeleteOpen}
        onOpenChange={setIsDeleteOpen}
        onConfirm={handleConfirmDelete}
        title="Видалити коментар"
        description="Ви впевнені, що хочете видалити цей коментар? Цю дію неможливо скасувати."
        isLoading={deleteMutation.isPending}
      />
    </div>
  )
}
