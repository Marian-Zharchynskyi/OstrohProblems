import { useState } from 'react'
import { Link } from 'react-router-dom'
import { useProblems } from '@/features/problems/hooks/use-problems'
import { useAuth } from '@/contexts/auth-context'
import { Badge } from '@/components/ui/badge'
import { Button } from '@/components/ui/button'
import { Plus, MapPin, Calendar, Eye } from 'lucide-react'
import { ProblemStatusConstants } from '@/types'

const getStatusColor = (status: string) => {
  switch (status) {
    case ProblemStatusConstants.New:
      return 'bg-blue-100 text-blue-800'
    case ProblemStatusConstants.InProgress:
      return 'bg-yellow-100 text-yellow-800'
    case ProblemStatusConstants.Completed:
      return 'bg-green-100 text-green-800'
    case ProblemStatusConstants.Rejected:
      return 'bg-red-100 text-red-800'
    default:
      return 'bg-gray-100 text-gray-800'
  }
}

export function MyProblemsPage() {
  const { user } = useAuth()
  const { data: problems, isLoading } = useProblems()
  const [filter, setFilter] = useState<string>('all')

  // Фільтруємо тільки проблеми поточного користувача
  const myProblems = problems?.filter(p => p.createdBy?.id === user?.id) || []

  const filteredProblems = filter === 'all' 
    ? myProblems 
    : myProblems.filter(p => p.status === filter)

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-center">
          <div className="inline-block animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
          <p className="mt-4 text-gray-600">Завантаження...</p>
        </div>
      </div>
    )
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <div className="flex justify-between items-center mb-8">
        <div>
          <h1 className="text-3xl font-bold text-gray-900">Мої проблеми</h1>
          <p className="text-gray-600 mt-2">Перегляд та відстеження ваших звернень</p>
        </div>
        <Link to="/problems/create">
          <Button className="flex items-center gap-2">
            <Plus className="h-4 w-4" />
            Нове звернення
          </Button>
        </Link>
      </div>

      {/* Фільтри */}
      <div className="flex gap-2 mb-6 flex-wrap">
        <Button
          variant={filter === 'all' ? 'default' : 'outline'}
          size="sm"
          onClick={() => setFilter('all')}
        >
          Всі ({myProblems.length})
        </Button>
        <Button
          variant={filter === ProblemStatusConstants.New ? 'default' : 'outline'}
          size="sm"
          onClick={() => setFilter(ProblemStatusConstants.New)}
        >
          Нові ({myProblems.filter(p => p.status === ProblemStatusConstants.New).length})
        </Button>
        <Button
          variant={filter === ProblemStatusConstants.InProgress ? 'default' : 'outline'}
          size="sm"
          onClick={() => setFilter(ProblemStatusConstants.InProgress)}
        >
          В роботі ({myProblems.filter(p => p.status === ProblemStatusConstants.InProgress).length})
        </Button>
        <Button
          variant={filter === ProblemStatusConstants.Completed ? 'default' : 'outline'}
          size="sm"
          onClick={() => setFilter(ProblemStatusConstants.Completed)}
        >
          Виконано ({myProblems.filter(p => p.status === ProblemStatusConstants.Completed).length})
        </Button>
        <Button
          variant={filter === ProblemStatusConstants.Rejected ? 'default' : 'outline'}
          size="sm"
          onClick={() => setFilter(ProblemStatusConstants.Rejected)}
        >
          Відхилено ({myProblems.filter(p => p.status === ProblemStatusConstants.Rejected).length})
        </Button>
      </div>

      {/* Список проблем */}
      {filteredProblems.length === 0 ? (
        <div className="text-center py-12 bg-gray-50 rounded-lg">
          <p className="text-gray-500 text-lg">
            {filter === 'all' 
              ? 'У вас ще немає звернень. Створіть перше!'
              : 'Немає звернень з таким статусом'}
          </p>
          {filter === 'all' && (
            <Link to="/problems/create">
              <Button className="mt-4">
                <Plus className="h-4 w-4 mr-2" />
                Створити звернення
              </Button>
            </Link>
          )}
        </div>
      ) : (
        <div className="grid gap-4">
          {filteredProblems.map((problem) => (
            <div
              key={problem.id}
              className="bg-white border rounded-lg p-6 hover:shadow-md transition-shadow"
            >
              <div className="flex justify-between items-start">
                <div className="flex-1">
                  <div className="flex items-center gap-3 mb-2">
                    <h3 className="text-lg font-semibold text-gray-900">
                      {problem.title}
                    </h3>
                    <Badge className={getStatusColor(problem.status)}>
                      {problem.status}
                    </Badge>
                  </div>
                  <p className="text-gray-600 mb-4 line-clamp-2">
                    {problem.description}
                  </p>
                  <div className="flex items-center gap-4 text-sm text-gray-500">
                    <span className="flex items-center gap-1">
                      <MapPin className="h-4 w-4" />
                      {problem.latitude.toFixed(4)}, {problem.longitude.toFixed(4)}
                    </span>
                    <span className="flex items-center gap-1">
                      <Calendar className="h-4 w-4" />
                      {new Date(problem.createdAt).toLocaleDateString('uk-UA')}
                    </span>
                  </div>
                  
                  {/* Поточний стан від координатора */}
                  {problem.currentState && (
                    <div className="mt-4 p-3 bg-blue-50 rounded-md">
                      <p className="text-sm font-medium text-blue-800">Поточний стан:</p>
                      <p className="text-sm text-blue-700">{problem.currentState}</p>
                    </div>
                  )}

                  {/* Причина відхилення */}
                  {problem.rejectionReason && (
                    <div className="mt-4 p-3 bg-red-50 rounded-md">
                      <p className="text-sm font-medium text-red-800">Причина відхилення:</p>
                      <p className="text-sm text-red-700">{problem.rejectionReason}</p>
                    </div>
                  )}

                  {/* Координатор */}
                  {problem.coordinator && (
                    <div className="mt-4 text-sm text-gray-600">
                      <span className="font-medium">Координатор:</span> {problem.coordinator.firstName} {problem.coordinator.lastName}
                    </div>
                  )}
                </div>
                <Link to={`/problems/${problem.id}`}>
                  <Button variant="outline" size="sm" className="flex items-center gap-2">
                    <Eye className="h-4 w-4" />
                    Детальніше
                  </Button>
                </Link>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  )
}
