import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { useQueryClient } from '@tanstack/react-query'
import type { ProblemSummary, CreateProblem } from '@/types'
import { DataTable, type Column } from '@/components/shared/data-table'
import { PageHeader } from '@/components/shared/page-header'
import { DeleteDialog } from '@/components/shared/delete-dialog'
import { ProblemForm } from './problem-form'
import {
  useProblems,
  useCreateProblem,
  useUpdateProblem,
  useDeleteProblem,
  useProblem,
} from '../hooks/use-problems'
import { useSignalR } from '@/contexts/use-signalr'
import { toast } from '@/lib/toast'

export function ProblemsList() {
  const navigate = useNavigate()
  const queryClient = useQueryClient()
  const { onProblemsUpdated } = useSignalR()
  const [isFormOpen, setIsFormOpen] = useState(false)
  const [isDeleteOpen, setIsDeleteOpen] = useState(false)
  const [selectedProblemId, setSelectedProblemId] = useState<string | null>(null)
  const { data: selectedProblem } = useProblem(selectedProblemId || '')

  // Subscribe to SignalR refresh events for auto-refresh
  useEffect(() => {
    onProblemsUpdated(() => {
      queryClient.invalidateQueries({ queryKey: ['problems'] })
    })
  }, [onProblemsUpdated, queryClient])

  const { data: problems, isLoading } = useProblems()
  const createMutation = useCreateProblem()
  const updateMutation = useUpdateProblem()
  const deleteMutation = useDeleteProblem()

  const columns: Column<ProblemSummary>[] = [
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
      accessor: (item) => item.status || 'N/A',
    },
    {
      header: 'Location',
      accessor: (item) => `${item.latitude.toFixed(4)}, ${item.longitude.toFixed(4)}`,
    },
    {
      header: 'Categories',
      accessor: (item) =>
        item.categories?.join(', ') || 'N/A',
    },
    {
      header: 'Created At',
      accessor: 'createdAt',
      cell: (value) => new Date(String(value)).toLocaleDateString(),
    },
  ]

  const handleCreate = () => {
    setSelectedProblemId(null)
    setIsFormOpen(true)
  }

  const handleEdit = (problem: ProblemSummary) => {
    setSelectedProblemId(problem.id)
    setIsFormOpen(true)
  }

  const handleDelete = (problem: ProblemSummary) => {
    setSelectedProblemId(problem.id)
    setIsDeleteOpen(true)
  }

  const handleSubmit = async (data: CreateProblem, id?: string) => {
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
    if (!selectedProblemId) return

    try {
      await deleteMutation.mutateAsync(selectedProblemId)
      toast.success('Problem deleted successfully')
      setIsDeleteOpen(false)
      setSelectedProblemId(null)
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
        onRowClick={(problem) => problem.id && navigate(`/problems/${problem.id}`)}
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
