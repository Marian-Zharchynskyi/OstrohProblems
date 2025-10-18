import { useState } from 'react'
import type { Problem } from '@/types'
import { DataTable, type Column } from '@/components/shared/data-table'
import { PageHeader } from '@/components/shared/page-header'
import { DeleteDialog } from '@/components/shared/delete-dialog'
import { ProblemForm } from './problem-form'
import {
  useProblems,
  useCreateProblem,
  useUpdateProblem,
  useDeleteProblem,
} from '../hooks/use-problems'
import { toast } from '@/lib/toast'

export function ProblemsList() {
  const [isFormOpen, setIsFormOpen] = useState(false)
  const [isDeleteOpen, setIsDeleteOpen] = useState(false)
  const [selectedProblem, setSelectedProblem] = useState<Problem | null>(null)

  const { data: problems, isLoading } = useProblems()
  const createMutation = useCreateProblem()
  const updateMutation = useUpdateProblem()
  const deleteMutation = useDeleteProblem()

  const columns: Column<Problem>[] = [
    {
      header: 'ID',
      accessor: 'id',
      cell: (value) => String(value).substring(0, 8) + '...',
    },
    {
      header: 'Title',
      accessor: 'title',
    },
    {
      header: 'Status',
      accessor: (item) => item.problemStatus?.name || 'N/A',
    },
    {
      header: 'Location',
      accessor: (item) => `${item.latitude.toFixed(4)}, ${item.longitude.toFixed(4)}`,
    },
    {
      header: 'Categories',
      accessor: (item) =>
        item.categories?.map((c) => c.name).join(', ') || 'N/A',
    },
    {
      header: 'Created At',
      accessor: 'createdAt',
      cell: (value) => new Date(String(value)).toLocaleDateString(),
    },
  ]

  const handleCreate = () => {
    setSelectedProblem(null)
    setIsFormOpen(true)
  }

  const handleEdit = (problem: Problem) => {
    setSelectedProblem(problem)
    setIsFormOpen(true)
  }

  const handleDelete = (problem: Problem) => {
    setSelectedProblem(problem)
    setIsDeleteOpen(true)
  }

  const handleSubmit = async (
    data: {
      title: string
      latitude: number
      longitude: number
      description: string
      problemStatusId: string
      problemCategoryIds: string[]
    },
    id?: string
  ) => {
    try {
      if (id) {
        await updateMutation.mutateAsync({ id, data })
        toast.success('Problem updated successfully')
      } else {
        await createMutation.mutateAsync(data)
        toast.success('Problem created successfully')
      }
      setIsFormOpen(false)
    } catch (error) {
      toast.error('An error occurred: ' + error)
    }
  }

  const handleConfirmDelete = async () => {
    if (!selectedProblem?.id) return

    try {
      await deleteMutation.mutateAsync(selectedProblem.id)
      toast.success('Problem deleted successfully')
      setIsDeleteOpen(false)
    } catch (error) {
      toast.error('An error occurred: ' + error)
    }
  }

  return (
    <div>
      <PageHeader
        title="Problems"
        description="Manage problems reported in the system"
        action={{
          label: 'New Problem',
          onClick: handleCreate,
        }}
      />

      <DataTable
        data={problems || []}
        columns={columns}
        onEdit={handleEdit}
        onDelete={handleDelete}
        isLoading={isLoading}
        emptyMessage="No problems found"
      />

      <ProblemForm
        open={isFormOpen}
        onOpenChange={setIsFormOpen}
        onSubmit={handleSubmit}
        initialData={selectedProblem}
        isLoading={createMutation.isPending || updateMutation.isPending}
      />

      <DeleteDialog
        open={isDeleteOpen}
        onOpenChange={setIsDeleteOpen}
        onConfirm={handleConfirmDelete}
        title="Delete Problem"
        description="Are you sure you want to delete this problem? This action cannot be undone."
        isLoading={deleteMutation.isPending}
      />
    </div>
  )
}
