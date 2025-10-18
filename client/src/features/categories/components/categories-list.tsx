import { useState } from 'react'
import type { Category } from '@/types'
import { DataTable, type Column } from '@/components/shared/data-table'
import { PageHeader } from '@/components/shared/page-header'
import { DeleteDialog } from '@/components/shared/delete-dialog'
import { CategoryForm } from './category-form'
import {
  useCategories,
  useCreateCategory,
  useUpdateCategory,
  useDeleteCategory,
} from '../hooks/use-categories'
import { toast } from '@/lib/toast'

export function CategoriesList() {
  const [isFormOpen, setIsFormOpen] = useState(false)
  const [isDeleteOpen, setIsDeleteOpen] = useState(false)
  const [selectedCategory, setSelectedCategory] = useState<Category | null>(null)

  const { data: categories, isLoading } = useCategories()
  const createMutation = useCreateCategory()
  const updateMutation = useUpdateCategory()
  const deleteMutation = useDeleteCategory()

  const columns: Column<Category>[] = [
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
    setSelectedCategory(null)
    setIsFormOpen(true)
  }

  const handleEdit = (category: Category) => {
    setSelectedCategory(category)
    setIsFormOpen(true)
  }

  const handleDelete = (category: Category) => {
    setSelectedCategory(category)
    setIsDeleteOpen(true)
  }

  const handleSubmit = async (data: Omit<Category, 'id'> | Category) => {
    try {
      if ('id' in data && data.id) {
        await updateMutation.mutateAsync(data as Category)
        toast.success('Category updated successfully')
      } else {
        await createMutation.mutateAsync(data)
        toast.success('Category created successfully')
      }
      setIsFormOpen(false)
    } catch {
      toast.error('An error occurred')
    }
  }

  const handleConfirmDelete = async () => {
    if (!selectedCategory?.id) return

    try {
      await deleteMutation.mutateAsync(selectedCategory.id)
      toast.success('Category deleted successfully')
      setIsDeleteOpen(false)
    } catch {
      toast.error('An error occurred')
    }
  }

  return (
    <div>
      <PageHeader
        title="Categories"
        description="Manage problem categories"
        action={{
          label: 'New Category',
          onClick: handleCreate,
        }}
      />

      <DataTable
        data={categories || []}
        columns={columns}
        onEdit={handleEdit}
        onDelete={handleDelete}
        isLoading={isLoading}
        emptyMessage="No categories found"
      />

      <CategoryForm
        open={isFormOpen}
        onOpenChange={setIsFormOpen}
        onSubmit={handleSubmit}
        initialData={selectedCategory}
        isLoading={createMutation.isPending || updateMutation.isPending}
      />

      <DeleteDialog
        open={isDeleteOpen}
        onOpenChange={setIsDeleteOpen}
        onConfirm={handleConfirmDelete}
        title="Delete Category"
        description="Are you sure you want to delete this category? This action cannot be undone."
        isLoading={deleteMutation.isPending}
      />
    </div>
  )
}
