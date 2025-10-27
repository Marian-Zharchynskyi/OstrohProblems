import { ProblemsMap } from '@/features/problems/components/problems-map'
import { useProblems } from '@/features/problems/hooks/use-problems'
import { PageHeader } from '@/components/shared/page-header'
import { Loader2 } from 'lucide-react'

export function MapPage() {
  const { data: problems, isLoading, error } = useProblems()

  if (isLoading) {
    return (
      <div className="flex h-[calc(100vh-200px)] items-center justify-center">
        <Loader2 className="h-8 w-8 animate-spin text-blue-600" />
      </div>
    )
  }

  if (error) {
    return (
      <div className="flex h-[calc(100vh-200px)] items-center justify-center">
        <div className="text-center">
          <p className="text-lg text-red-600">Помилка завантаження проблем</p>
          <p className="text-sm text-gray-500 mt-2">
            {error instanceof Error ? error.message : 'Невідома помилка'}
          </p>
        </div>
      </div>
    )
  }
  
  return (
    <div className="flex flex-1 min-h-0 flex-col">
      <PageHeader
        title="Карта проблем"
        description={`Всього проблем: ${problems?.length || 0}`}
      />
      <div className="mt-6 h-[calc(100vh-200px)]">
        <ProblemsMap problems={problems || []} />
      </div>
    </div>
  )
}
