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
    },
    {
      header: 'Бали',
      accessor: 'points',
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
        toast.success('Рейтинг успішно оновлено')
      } else {
        await createMutation.mutateAsync(data)
        toast.success('Рейтинг успішно створено')
      }
      setIsFormOpen(false)
    } catch (error) {
      toast.error('Виникла помилка: ' + error)
    }
  }

  const handleConfirmDelete = async () => {
    if (!selectedRating?.id) return

    try {
      await deleteMutation.mutateAsync(selectedRating.id)
      toast.success('Рейтинг успішно видалено')
      setIsDeleteOpen(false)
    } catch (error) {
      toast.error('Виникла помилка: ' + error)
    }
  }

  return (
    <div>
      <PageHeader
        title="Рейтинги"
        description="Управління рейтингами проблем"
        action={{
          label: 'Новий рейтинг',
          onClick: handleCreate,
        }}
      />

      <DataTable
        data={ratings || []}
        columns={columns}
        onEdit={handleEdit}
        onDelete={handleDelete}
        isLoading={isLoading}
        emptyMessage="Рейтингів не знайдено"
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
        title="Видалити рейтинг"
        description="Ви впевнені, що хочете видалити цей рейтинг? Цю дію неможливо скасувати."
        isLoading={deleteMutation.isPending}
      />
    </div>
  )
}
