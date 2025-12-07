import { useState, useEffect } from 'react'
import { problemsApi } from '@/features/problems/api/problems-api'
import type { Problem } from '@/types'
import { ProblemStatusConstants } from '@/types'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Textarea } from '@/components/ui/textarea'
import { useAuth } from '@/contexts/auth-context'
import { toast } from '@/lib/toast'

const statusOptions = [
  ProblemStatusConstants.New,
  ProblemStatusConstants.InProgress,
  ProblemStatusConstants.Completed,
  ProblemStatusConstants.Rejected,
  ProblemStatusConstants.NeedsClarification,
]

export default function CoordinatorPage() {
  const { user } = useAuth()
  const [problems, setProblems] = useState<Problem[]>([])
  const [loading, setLoading] = useState(true)
  const [rejectionReason, setRejectionReason] = useState('')
  const [currentStateInput, setCurrentStateInput] = useState('')
  const [activeTab, setActiveTab] = useState<'new' | 'my'>('new')
  const [detailProblem, setDetailProblem] = useState<Problem | null>(null)
  const [actionMode, setActionMode] = useState<'reject' | 'currentState' | 'complete' | null>(null)

  useEffect(() => {
    loadData()
  }, [])

  const loadData = async () => {
    try {
      const problemsData = await problemsApi.getAll()
      setProblems(problemsData)
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
      setDetailProblem(null)
      loadData()
    } catch {
      toast.error('Помилка призначення')
    }
  }

  const handleStartProblem = async (problemId: string) => {
    try {
      await problemsApi.startProblem(problemId, currentStateInput || undefined)
      toast.success('Роботу над проблемою розпочато')
      setCurrentStateInput('')
      setDetailProblem(null)
      loadData()
    } catch {
      toast.error('Помилка')
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
      setDetailProblem(null)
      setActionMode(null)
      loadData()
    } catch {
      toast.error('Помилка відхилення')
    }
  }

  const handleUpdateCurrentState = async (problemId: string) => {
    if (!currentStateInput.trim()) {
      toast.error('Введіть поточний стан')
      return
    }
    try {
      await problemsApi.updateCurrentState(problemId, currentStateInput)
      toast.success('Поточний стан оновлено')
      setCurrentStateInput('')
      setActionMode(null)
      loadData()
    } catch {
      toast.error('Помилка оновлення')
    }
  }

  const handleStatusChange = async (problemId: string, status: string) => {
    try {
      await problemsApi.updateStatus(problemId, status)
      toast.success('Статус оновлено')
      loadData()
    } catch {
      toast.error('Помилка оновлення статусу')
    }
  }

  const handleCompleteProblem = async (problemId: string) => {
    if (!currentStateInput.trim()) {
      toast.error('Опишіть що було зроблено')
      return
    }
    try {
      await problemsApi.completeProblem(problemId, currentStateInput)
      toast.success('Проблему завершено')
      setCurrentStateInput('')
      setActionMode(null)
      loadData()
    } catch {
      toast.error('Помилка завершення')
    }
  }

  if (loading) {
    return <div className="container mx-auto p-6">Завантаження...</div>
  }

  const newProblems = problems.filter((p) => !p.coordinator && p.status === ProblemStatusConstants.New)
  const myProblems = problems.filter((p) => p.coordinator?.id === user?.id)

  // Detail view for a specific problem
  if (detailProblem) {
    const isMyProblem = detailProblem.coordinator?.id === user?.id
    return (
      <div className="container mx-auto p-6">
        <Button variant="outline" onClick={() => setDetailProblem(null)} className="mb-4">
          ← Назад
        </Button>
        <Card>
          <CardHeader>
            <CardTitle className="flex justify-between items-center">
              <span>{detailProblem.title}</span>
              <span className={`inline-flex items-center rounded-full px-3 py-1 text-sm font-medium ${
                detailProblem.status === ProblemStatusConstants.New ? 'bg-blue-100 text-blue-800' :
                detailProblem.status === ProblemStatusConstants.InProgress ? 'bg-yellow-100 text-yellow-800' :
                detailProblem.status === ProblemStatusConstants.Completed ? 'bg-green-100 text-green-800' :
                detailProblem.status === ProblemStatusConstants.Rejected ? 'bg-red-100 text-red-800' :
                'bg-gray-100 text-gray-800'
              }`}>
                {detailProblem.status}
              </span>
            </CardTitle>
          </CardHeader>
          <CardContent className="space-y-4">
            <div>
              <h3 className="font-semibold mb-1">Опис:</h3>
              <p className="text-gray-600">{detailProblem.description}</p>
            </div>
            <div className="grid grid-cols-2 gap-4 text-sm">
              <div>
                <span className="font-semibold">Автор:</span> {detailProblem.createdBy?.email}
              </div>
              <div>
                <span className="font-semibold">Створено:</span> {new Date(detailProblem.createdAt).toLocaleString('uk-UA')}
              </div>
              <div>
                <span className="font-semibold">Координати:</span> {detailProblem.latitude}, {detailProblem.longitude}
              </div>
            </div>

            {detailProblem.currentState && (
              <div className="bg-blue-50 p-4 rounded">
                <h3 className="font-semibold mb-1">Поточний стан:</h3>
                <p>{detailProblem.currentState}</p>
              </div>
            )}

            {detailProblem.rejectionReason && (
              <div className="bg-red-50 p-4 rounded">
                <h3 className="font-semibold mb-1">Причина відхилення:</h3>
                <p>{detailProblem.rejectionReason}</p>
              </div>
            )}

            {!isMyProblem && detailProblem.status === ProblemStatusConstants.New && (
              <div className="space-y-4 pt-4 border-t">
                <div className="flex gap-2">
                  <Button onClick={() => handleAssignToMe(detailProblem.id!)}>
                    Взяти в роботу
                  </Button>
                  <Button variant="destructive" onClick={() => setActionMode('reject')}>
                    Відхилити
                  </Button>
                </div>
                {actionMode === 'reject' && (
                  <div className="space-y-2">
                    <Textarea
                      placeholder="Причина відхилення..."
                      value={rejectionReason}
                      onChange={(e) => setRejectionReason(e.target.value)}
                    />
                    <div className="flex gap-2">
                      <Button onClick={() => handleReject(detailProblem.id!)}>Підтвердити відхилення</Button>
                      <Button variant="outline" onClick={() => setActionMode(null)}>Скасувати</Button>
                    </div>
                  </div>
                )}
              </div>
            )}

            {isMyProblem && detailProblem.status !== ProblemStatusConstants.Completed && detailProblem.status !== ProblemStatusConstants.Rejected && (
              <div className="space-y-4 pt-4 border-t">
                <div className="flex gap-2 flex-wrap">
                  {detailProblem.status === ProblemStatusConstants.New && (
                    <Button onClick={() => handleStartProblem(detailProblem.id!)}>
                      Почати виконання
                    </Button>
                  )}
                  <Button variant="outline" onClick={() => setActionMode('currentState')}>
                    Оновити поточний стан
                  </Button>
                  <Button className="bg-green-600 hover:bg-green-700" onClick={() => setActionMode('complete')}>
                    Завершити
                  </Button>
                  <Button variant="destructive" onClick={() => setActionMode('reject')}>
                    Відхилити
                  </Button>
                </div>

                <div className="flex gap-2 items-center">
                  <span className="font-semibold">Змінити статус:</span>
                  <select
                    className="border rounded px-3 py-2"
                    value={detailProblem.status}
                    onChange={(e) => handleStatusChange(detailProblem.id!, e.target.value)}
                  >
                    {statusOptions.map((status) => (
                      <option key={status} value={status}>
                        {status}
                      </option>
                    ))}
                  </select>
                </div>

                {actionMode === 'currentState' && (
                  <div className="space-y-2">
                    <Textarea
                      placeholder="Опишіть поточний стан виконання..."
                      value={currentStateInput}
                      onChange={(e) => setCurrentStateInput(e.target.value)}
                    />
                    <div className="flex gap-2">
                      <Button onClick={() => handleUpdateCurrentState(detailProblem.id!)}>Зберегти</Button>
                      <Button variant="outline" onClick={() => setActionMode(null)}>Скасувати</Button>
                    </div>
                  </div>
                )}

                {actionMode === 'complete' && (
                  <div className="space-y-2">
                    <Textarea
                      placeholder="Опишіть що було зроблено для вирішення проблеми..."
                      value={currentStateInput}
                      onChange={(e) => setCurrentStateInput(e.target.value)}
                    />
                    <div className="flex gap-2">
                      <Button className="bg-green-600 hover:bg-green-700" onClick={() => handleCompleteProblem(detailProblem.id!)}>
                        Завершити проблему
                      </Button>
                      <Button variant="outline" onClick={() => setActionMode(null)}>Скасувати</Button>
                    </div>
                  </div>
                )}

                {actionMode === 'reject' && (
                  <div className="space-y-2">
                    <Textarea
                      placeholder="Причина відхилення..."
                      value={rejectionReason}
                      onChange={(e) => setRejectionReason(e.target.value)}
                    />
                    <div className="flex gap-2">
                      <Button variant="destructive" onClick={() => handleReject(detailProblem.id!)}>Підтвердити відхилення</Button>
                      <Button variant="outline" onClick={() => setActionMode(null)}>Скасувати</Button>
                    </div>
                  </div>
                )}
              </div>
            )}
          </CardContent>
        </Card>
      </div>
    )
  }

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">Панель координатора</h1>

      <div className="flex gap-2 mb-6">
        <Button
          variant={activeTab === 'new' ? 'default' : 'outline'}
          onClick={() => setActiveTab('new')}
        >
          Нові проблеми ({newProblems.length})
        </Button>
        <Button
          variant={activeTab === 'my' ? 'default' : 'outline'}
          onClick={() => setActiveTab('my')}
        >
          Мої проблеми ({myProblems.length})
        </Button>
      </div>

      {activeTab === 'new' && (
        <div className="grid gap-4">
          {newProblems.length === 0 ? (
            <p className="text-gray-500">Немає нових проблем</p>
          ) : (
            newProblems.map((problem) => (
              <Card key={problem.id} className="cursor-pointer hover:shadow-md transition-shadow" onClick={() => setDetailProblem(problem)}>
                <CardHeader>
                  <CardTitle className="flex justify-between items-center">
                    <span>{problem.title}</span>
                    <span className="inline-flex items-center rounded-full bg-blue-100 px-2 py-1 text-xs font-medium text-blue-800">
                      {problem.status}
                    </span>
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <p className="text-sm text-gray-600 mb-2 line-clamp-2">{problem.description}</p>
                  <p className="text-xs text-gray-500">
                    Автор: {problem.createdBy?.email} | {new Date(problem.createdAt).toLocaleDateString('uk-UA')}
                  </p>
                </CardContent>
              </Card>
            ))
          )}
        </div>
      )}

      {activeTab === 'my' && (
        <div className="grid gap-4">
          {myProblems.length === 0 ? (
            <p className="text-gray-500">У вас немає взятих проблем</p>
          ) : (
            myProblems.map((problem) => (
              <Card key={problem.id} className="cursor-pointer hover:shadow-md transition-shadow" onClick={() => setDetailProblem(problem)}>
                <CardHeader>
                  <CardTitle className="flex justify-between items-center">
                    <span>{problem.title}</span>
                    <span className={`inline-flex items-center rounded-full px-2 py-1 text-xs font-medium ${
                      problem.status === ProblemStatusConstants.InProgress ? 'bg-yellow-100 text-yellow-800' :
                      problem.status === ProblemStatusConstants.Completed ? 'bg-green-100 text-green-800' :
                      problem.status === ProblemStatusConstants.Rejected ? 'bg-red-100 text-red-800' :
                      'bg-gray-100 text-gray-800'
                    }`}>
                      {problem.status}
                    </span>
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <p className="text-sm text-gray-600 mb-2 line-clamp-2">{problem.description}</p>
                  {problem.currentState && (
                    <p className="text-sm text-blue-600 mb-2 line-clamp-1">
                      <span className="font-semibold">Стан:</span> {problem.currentState}
                    </p>
                  )}
                  <p className="text-xs text-gray-500">
                    Автор: {problem.createdBy?.email} | {new Date(problem.createdAt).toLocaleDateString('uk-UA')}
                  </p>
                </CardContent>
              </Card>
            ))
          )}
        </div>
      )}
    </div>
  )
}
