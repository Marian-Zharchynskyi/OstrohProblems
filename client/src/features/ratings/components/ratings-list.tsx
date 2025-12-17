import { useState } from 'react'
import type { Rating } from '@/types'
import { DataTable, type Column } from '@/components/shared/data-table'
import { PageHeader } from '@/components/shared/page-header'
import { DeleteDialog } from '@/components/shared/delete-dialog'
import { RatingForm } from './rating-form'
import {
  useRatings,
  useCreateRating,
  useUpdateRating,
  useDeleteRating,
} from '../hooks/use-ratings'
import { toast } from '@/lib/toast'

export function RatingsList() {
  const [isFormOpen, setIsFormOpen] = useState(false)
  const [isDeleteOpen, setIsDeleteOpen] = useState(false)
  const [selectedRating, setSelectedRating] = useState<Rating | null>(null)

  const { data: ratings, isLoading } = useRatings()
  const createMutation = useCreateRating()
  const updateMutation = useUpdateRating()
  const deleteMutation = useDeleteRating()

  const columns: Column<Rating>[] = [
    {
      header: 'ID',
      accessor: 'id',
      cell: (value) => String(value).substring(0, 8) + '...',
    },
    {
      header: 'Points',
      accessor: 'points',
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
    setSelectedRating(null)
    setIsFormOpen(true)
  }

  const handleEdit = (rating: Rating) => {
    setSelectedRating(rating)
    setIsFormOpen(true)
  }

  const handleDelete = (rating: Rating) => {
    setSelectedRating(rating)
    setIsDeleteOpen(true)
  }

  const handleSubmit = async (data: { points: number; problemId: string }, id?: string) => {
    try {
      if (id) {
        await updateMutation.mutateAsync({ id, data })
        toast.success('Rating updated successfully')
      } else {
        await createMutation.mutateAsync(data)
        toast.success('Rating created successfully')
      }
      setIsFormOpen(false)
    } catch (error) {
      toast.error('An error occurred: ' + error)
    }
  }

  const handleConfirmDelete = async () => {
    if (!selectedRating?.id) return

    try {
      await deleteMutation.mutateAsync(selectedRating.id)
      toast.success('Rating deleted successfully')
      setIsDeleteOpen(false)
    } catch (error) {
      toast.error('An error occurred: ' + error)
    }
  }

  return (
    <div>
      <PageHeader
        title="Ratings"
        description="Manage problem ratings"
        action={{
          label: 'New Rating',
          onClick: handleCreate,
        }}
      />

      <DataTable
        data={ratings || []}
        columns={columns}
        onEdit={handleEdit}
        onDelete={handleDelete}
        isLoading={isLoading}
        emptyMessage="No ratings found"
      />

      <RatingForm
        open={isFormOpen}
        onOpenChange={setIsFormOpen}
        onSubmit={handleSubmit}
        initialData={selectedRating}
        isLoading={createMutation.isPending || updateMutation.isPending}
      />

      <DeleteDialog
        open={isDeleteOpen}
        onOpenChange={setIsDeleteOpen}
        onConfirm={handleConfirmDelete}
        title="Delete Rating"
        description="Are you sure you want to delete this rating? This action cannot be undone."
        isLoading={deleteMutation.isPending}
      />
    </div>
  )
}
