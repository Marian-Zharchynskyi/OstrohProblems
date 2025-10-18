import { useState } from 'react'
import type { Status } from '@/types'
import { DataTable } from '@/components/shared/data-table'
import type { Column } from '@/components/shared/data-table'
import { PageHeader } from '@/components/shared/page-header'
import { DeleteDialog } from '@/components/shared/delete-dialog'
import { StatusForm } from './status-form'
import {
  useStatuses,
  useCreateStatus,
  useUpdateStatus,
  useDeleteStatus,
} from '../hooks/use-statuses'
import { toast } from '@/lib/toast'

export function StatusesList() {
  const [isFormOpen, setIsFormOpen] = useState(false)
  const [isDeleteOpen, setIsDeleteOpen] = useState(false)
  const [selectedStatus, setSelectedStatus] = useState<Status | null>(null)

  const { data: statuses, isLoading } = useStatuses()
  const createMutation = useCreateStatus()
  const updateMutation = useUpdateStatus()
  const deleteMutation = useDeleteStatus()

  const columns: Column<Status>[] = [
    {
      header: 'ID',
      accessor: 'id',
      cell: (value) => String(value).substring(0, 8) + '...',
    },
    {
      header: 'Name',
      accessor: 'name',
    },
  ]

  const handleCreate = () => {
    setSelectedStatus(null)
    setIsFormOpen(true)
  }

  const handleEdit = (status: Status) => {
    setSelectedStatus(status)
    setIsFormOpen(true)
  }

  const handleDelete = (status: Status) => {
    setSelectedStatus(status)
    setIsDeleteOpen(true)
  }

  const handleSubmit = async (data: { name: string }, id?: string) => {
    try {
      if (id) {
        await updateMutation.mutateAsync({ id, data })
        toast.success('Status updated successfully')
      } else {
        await createMutation.mutateAsync(data)
        toast.success('Status created successfully')
      }
      setIsFormOpen(false)
    } catch {
      toast.error('An error occurred')
    }
  }

  const handleConfirmDelete = async () => {
    if (!selectedStatus?.id) return

    try {
      await deleteMutation.mutateAsync(selectedStatus.id)
      toast.success('Status deleted successfully')
      setIsDeleteOpen(false)
    } catch (error) {
      toast.error('An error occurred: ' + error)
    }
  }

  return (
    <div>
      <PageHeader
        title="Statuses"
        description="Manage problem statuses"
        action={{
          label: 'New Status',
          onClick: handleCreate,
        }}
      />

      <DataTable
        data={statuses || []}
        columns={columns}
        onEdit={handleEdit}
        onDelete={handleDelete}
        isLoading={isLoading}
        emptyMessage="No statuses found"
      />

      <StatusForm
        open={isFormOpen}
        onOpenChange={setIsFormOpen}
        onSubmit={handleSubmit}
        initialData={selectedStatus}
        isLoading={createMutation.isPending || updateMutation.isPending}
      />

      <DeleteDialog
        open={isDeleteOpen}
        onOpenChange={setIsDeleteOpen}
        onConfirm={handleConfirmDelete}
        title="Delete Status"
        description="Are you sure you want to delete this status? This action cannot be undone."
        isLoading={deleteMutation.isPending}
      />
    </div>
  )
}
