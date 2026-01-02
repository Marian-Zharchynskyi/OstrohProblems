import { type ReactNode } from 'react'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { Button } from '@/components/ui/button'
import { Pencil, Trash2 } from 'lucide-react'

export interface Column<T> {
  header: string
  accessor: keyof T | ((item: T) => ReactNode)
  cell?: (value: unknown, item: T) => ReactNode
}

interface DataTableProps<T> {
  data: T[]
  columns: Column<T>[]
  onEdit?: (item: T) => void
  onDelete?: (item: T) => void
  onRowClick?: (item: T) => void
  isLoading?: boolean
  emptyMessage?: string
}

export function DataTable<T extends { id: string | null }>({
  data,
  columns,
  onEdit,
  onDelete,
  onRowClick,
  isLoading,
  emptyMessage = 'No data available',
}: DataTableProps<T>) {
  if (isLoading) {
    return (
      <div className="flex items-center justify-center p-8">
        <div className="text-muted-foreground">Завантаження...</div>
      </div>
    )
  }

  if (!data || data.length === 0) {
    return (
      <div className="flex items-center justify-center p-8">
        <div className="text-muted-foreground">{emptyMessage}</div>
      </div>
    )
  }

  const getCellValue = (item: T, column: Column<T>) => {
    if (typeof column.accessor === 'function') {
      return column.accessor(item)
    }
    return item[column.accessor]
  }

  return (
    <div className="rounded-md border">
      <Table>
        <TableHeader>
          <TableRow>
            {columns.map((column, index) => (
              <TableHead key={index}>{column.header}</TableHead>
            ))}
            {(onEdit || onDelete) && <TableHead className="text-right">Дії</TableHead>}
          </TableRow>
        </TableHeader>
        <TableBody>
          {data.map((item) => (
            <TableRow 
              key={item.id || Math.random()}
              onClick={() => onRowClick?.(item)}
              className={onRowClick ? 'cursor-pointer hover:bg-muted/50' : ''}
            >
              {columns.map((column, colIndex) => {
                const value = getCellValue(item, column)
                return (
                  <TableCell key={colIndex}>
                    {column.cell ? column.cell(value, item) : String(value ?? '')}
                  </TableCell>
                )
              })}
              {(onEdit || onDelete) && (
                <TableCell className="text-right">
                  <div className="flex justify-end gap-2">
                    {onEdit && (
                      <Button
                        variant="outline"
                        size="icon"
                        className="h-9 w-9 border border-[#D0D5DD] bg-white text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929] disabled:bg-white disabled:text-[#292929]"
                        onClick={(e) => {
                          e.stopPropagation()
                          onEdit(item)
                        }}
                        title="Редагувати"
                      >
                        <Pencil className="h-4 w-4 text-[#292929]" />
                      </Button>
                    )}
                    {onDelete && (
                      <Button
                        variant="destructive"
                        size="icon"
                        className="h-9 w-9 bg-[#DC2626] hover:bg-[#B91C1C] text-white disabled:bg-[#DC2626]/80 disabled:text-white"
                        onClick={(e) => {
                          e.stopPropagation()
                          onDelete(item)
                        }}
                        title="Видалити"
                      >
                        <Trash2 className="h-4 w-4 text-white" />
                      </Button>
                    )}
                  </div>
                </TableCell>
              )}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  )
}
