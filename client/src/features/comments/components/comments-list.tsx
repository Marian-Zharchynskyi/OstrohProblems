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

export function CommentsList() {
  const [isFormOpen, setIsFormOpen] = useState(false)
  const [isDeleteOpen, setIsDeleteOpen] = useState(false)
  const [selectedComment, setSelectedComment] = useState<Comment | null>(null)

  const { data: comments, isLoading } = useComments()
  const createMutation = useCreateComment()
  const updateMutation = useUpdateComment()
  const deleteMutation = useDeleteComment()

  const columns: Column<Comment>[] = [
    {
      header: 'ID',
      accessor: 'id',
      cell: (value) => String(value).substring(0, 8) + '...',
    },
    {
      header: 'Content',
      accessor: 'content',
      cell: (value) => String(value).substring(0, 50) + (String(value).length > 50 ? '...' : ''),
    },
    {
      header: 'User',
      accessor: (item) => item.user?.email || 'N/A',
    },
    {
      header: 'Created At',
      accessor: 'createdAt',
      cell: (value) => new Date(String(value)).toLocaleDateString(),
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
        toast.success('Comment updated successfully')
      } else {
        await createMutation.mutateAsync(data)
        toast.success('Comment created successfully')
      }
      setIsFormOpen(false)
    } catch (error) {
      toast.error('An error occurred: ' + error)
    }
  }

  const handleConfirmDelete = async () => {
    if (!selectedComment?.id) return

    try {
      await deleteMutation.mutateAsync(selectedComment.id)
      toast.success('Comment deleted successfully')
      setIsDeleteOpen(false)
    } catch (error) {
      toast.error('An error occurred: ' + error)
    }
  }

  return (
    <div>
      <PageHeader
        title="Comments"
        description="Manage problem comments"
        action={{
          label: 'New Comment',
          onClick: handleCreate,
        }}
      />

      <DataTable
        data={comments || []}
        columns={columns}
        onEdit={handleEdit}
        onDelete={handleDelete}
        isLoading={isLoading}
        emptyMessage="No comments found"
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
        title="Delete Comment"
        description="Are you sure you want to delete this comment? This action cannot be undone."
        isLoading={deleteMutation.isPending}
      />
    </div>
  )
}
