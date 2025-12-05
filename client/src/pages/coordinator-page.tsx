import { useState, useEffect } from 'react'
import { problemsApi } from '@/features/problems/api/problems-api'
import { statusesApi } from '@/features/statuses/api/statuses-api'
import type { Problem, Status } from '@/types'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Textarea } from '@/components/ui/textarea'
import { useAuth } from '@/contexts/auth-context'
import { toast } from '@/lib/toast'

export default function CoordinatorPage() {
  const { user } = useAuth()
  const [problems, setProblems] = useState<Problem[]>([])
  const [statuses, setStatuses] = useState<Status[]>([])
  const [loading, setLoading] = useState(true)
  const [selectedProblem, setSelectedProblem] = useState<Problem | null>(null)
  const [rejectionReason, setRejectionReason] = useState('')
  const [coordinatorComment, setCoordinatorComment] = useState('')

  useEffect(() => {
    loadData()
  }, [])

  const loadData = async () => {
    try {
      const [problemsData, statusesData] = await Promise.all([
        problemsApi.getAll(),
        statusesApi.getAll(),
      ])
      setProblems(problemsData)
      setStatuses(statusesData)
    } catch {
      toast.error('Помилка завантаження даних')
    } finally {
      setLoading(false)
    }
  }

  const handleAssignToMe = async (problemId: string) => {
    if (!user?.id) return
    try {
      await problemsApi.assignCoordinator(problemId, user.id)
      toast.success('Проблему призначено вам')
      loadData()
    } catch {
      toast.error('Помилка призначення')
    }
  }

  const handleReject = async (problemId: string) => {
    if (!rejectionReason.trim()) {
      toast.error('Вкажіть причину відхилення')
      return
    }
    try {
      await problemsApi.reject(problemId, rejectionReason)
      toast.success('Проблему відхилено')
      setRejectionReason('')
      setSelectedProblem(null)
      loadData()
    } catch {
      toast.error('Помилка відхилення')
    }
  }

  const handleSetComment = async (problemId: string) => {
    if (!coordinatorComment.trim()) {
      toast.error('Введіть коментар')
      return
    }
    try {
      await problemsApi.setCoordinatorComment(problemId, coordinatorComment)
      toast.success('Коментар додано')
      setCoordinatorComment('')
      setSelectedProblem(null)
      loadData()
    } catch {
      toast.error('Помилка додавання коментаря')
    }
  }

  const handleStatusChange = async (problemId: string, statusId: string) => {
    try {
      const problem = problems.find((p) => p.id === problemId)
      if (!problem) return

      await problemsApi.update(problemId, {
        title: problem.title,
        latitude: problem.latitude,
        longitude: problem.longitude,
        description: problem.description,
        problemStatusId: statusId,
        problemCategoryIds: problem.categories?.map((c) => c.id!).filter(Boolean) || [],
      })
      toast.success('Статус оновлено')
      loadData()
    } catch {
      toast.error('Помилка оновлення статусу')
    }
  }

  if (loading) {
    return <div className="container mx-auto p-6">Завантаження...</div>
  }

  const newProblems = problems.filter((p) => !p.coordinator)
  const myProblems = problems.filter((p) => p.coordinator?.id === user?.id)

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">Панель координатора</h1>

      <div className="mb-8">
        <h2 className="text-2xl font-semibold mb-4">Нові проблеми ({newProblems.length})</h2>
        <div className="grid gap-4">
          {newProblems.map((problem) => (
            <Card key={problem.id}>
              <CardHeader>
                <CardTitle className="flex justify-between items-center">
                  <span>{problem.title}</span>
                  <span className="inline-flex items-center rounded-full bg-gray-100 px-2 py-1 text-xs font-medium text-gray-800">
                    {problem.problemStatus?.name}
                  </span>
                </CardTitle>
              </CardHeader>
              <CardContent>
                <p className="text-sm text-gray-600 mb-4">{problem.description}</p>
                <p className="text-xs text-gray-500 mb-4">
                  Автор: {problem.createdBy?.email} | Створено: {new Date(problem.createdAt).toLocaleString('uk-UA')}
                </p>
                <div className="flex gap-2">
                  <Button onClick={() => handleAssignToMe(problem.id!)}>Взяти в роботу</Button>
                  <Button
                    variant="destructive"
                    onClick={() => {
                      setSelectedProblem(problem)
                      setRejectionReason('')
                    }}
                  >
                    Відхилити
                  </Button>
                </div>
                {selectedProblem?.id === problem.id && (
                  <div className="mt-4">
                    <Textarea
                      placeholder="Причина відхилення..."
                      value={rejectionReason}
                      onChange={(e) => setRejectionReason(e.target.value)}
                      className="mb-2"
                    />
                    <Button onClick={() => handleReject(problem.id!)}>Підтвердити відхилення</Button>
                  </div>
                )}
              </CardContent>
            </Card>
          ))}
        </div>
      </div>

      <div>
        <h2 className="text-2xl font-semibold mb-4">Мої проблеми ({myProblems.length})</h2>
        <div className="grid gap-4">
          {myProblems.map((problem) => (
            <Card key={problem.id}>
              <CardHeader>
                <CardTitle className="flex justify-between items-center">
                  <span>{problem.title}</span>
                  <span className="inline-flex items-center rounded-full bg-gray-100 px-2 py-1 text-xs font-medium text-gray-800">
                    {problem.problemStatus?.name}
                  </span>
                </CardTitle>
              </CardHeader>
              <CardContent>
                <p className="text-sm text-gray-600 mb-2">{problem.description}</p>
                <p className="text-xs text-gray-500 mb-4">
                  Автор: {problem.createdBy?.email} | Створено: {new Date(problem.createdAt).toLocaleString('uk-UA')}
                </p>
                {problem.coordinatorComment && (
                  <div className="bg-blue-50 p-3 rounded mb-4">
                    <p className="text-sm font-semibold">Коментар координатора:</p>
                    <p className="text-sm">{problem.coordinatorComment}</p>
                  </div>
                )}
                <div className="space-y-2">
                  <div className="flex gap-2">
                    <select
                      className="border rounded px-3 py-2"
                      value={problem.problemStatus?.id || ''}
                      onChange={(e) => handleStatusChange(problem.id!, e.target.value)}
                    >
                      {statuses.map((status) => (
                        <option key={status.id} value={status.id!}>
                          {status.name}
                        </option>
                      ))}
                    </select>
                  </div>
                  <Button
                    onClick={() => {
                      setSelectedProblem(problem)
                      setCoordinatorComment(problem.coordinatorComment || '')
                    }}
                  >
                    Додати/Оновити коментар
                  </Button>
                  {selectedProblem?.id === problem.id && (
                    <div className="mt-2">
                      <Textarea
                        placeholder="Коментар координатора..."
                        value={coordinatorComment}
                        onChange={(e) => setCoordinatorComment(e.target.value)}
                        className="mb-2"
                      />
                      <Button onClick={() => handleSetComment(problem.id!)}>Зберегти коментар</Button>
                    </div>
                  )}
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      </div>
    </div>
  )
}
