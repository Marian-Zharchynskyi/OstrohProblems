import { useState, useRef } from 'react'
import { useNavigate } from 'react-router-dom'
import { problemsApi } from '@/features/problems/api/problems-api'
import { useProblemsByCoordinator, useProblemsByStatus } from '@/features/problems/hooks/use-problems'
import type { Problem } from '@/types'
import { ProblemStatusConstants } from '@/types'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Textarea } from '@/components/ui/textarea'
import { useAuth } from '@/contexts/auth-context'
import { toast } from '@/lib/toast'
import { useQueryClient } from '@tanstack/react-query'

const MAX_COORDINATOR_IMAGES = 6

type TabType = 'new' | 'my' | 'completed' | 'rejected'

const tabConfig: { key: TabType; label: string; status?: string }[] = [
  { key: 'new', label: 'Нові', status: ProblemStatusConstants.New },
  { key: 'my', label: 'Мої проблеми' },
  { key: 'completed', label: 'Виконано', status: ProblemStatusConstants.Completed },
  { key: 'rejected', label: 'Відхилені', status: ProblemStatusConstants.Rejected },
]

export default function CoordinatorPage() {
  const { user } = useAuth()
  const queryClient = useQueryClient()
  const navigate = useNavigate()
  
  const [activeTab, setActiveTab] = useState<TabType>('new')
  
  // Get current status for API filtering
  const currentTabConfig = tabConfig.find(t => t.key === activeTab)
  const currentStatus = currentTabConfig?.status || ''
  
  // Use hooks for fetching problems with API filtering
  const { data: statusProblems = [], isLoading: loadingStatus } = useProblemsByStatus(currentStatus)
  const { data: myProblems = [], isLoading: loadingMy } = useProblemsByCoordinator(user?.id || '')
  
  const [rejectionReason, setRejectionReason] = useState('')
  const [currentStateInput, setCurrentStateInput] = useState('')
  const [detailProblem, setDetailProblem] = useState<Problem | null>(null)
  const [actionMode, setActionMode] = useState<'reject' | 'currentState' | 'complete' | null>(null)
  const [coordinatorFiles, setCoordinatorFiles] = useState<FileList | null>(null)
  const fileInputRef = useRef<HTMLInputElement>(null)

  const loading = loadingStatus || loadingMy

  const invalidateQueries = () => {
    queryClient.invalidateQueries({ queryKey: ['problems'] })
  }

  const handleValidate = async (problemId: string) => {
    if (!user?.id) return
    try {
      await problemsApi.validateProblem(problemId, user.id)
      toast.success('Проблему провалідовано та призначено вам')
      setDetailProblem(null)
      invalidateQueries()
    } catch {
      toast.error('Помилка валідації')
    }
  }

  const handleAssignToMe = async (problemId: string) => {
    if (!user?.id) return
    try {
      await problemsApi.assignCoordinator(problemId, user.id)
      toast.success('Проблему взято в роботу')
      setDetailProblem(null)
      invalidateQueries()
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
      setDetailProblem(null)
      setActionMode(null)
      invalidateQueries()
    } catch {
      toast.error('Помилка відхилення')
    }
  }

  const handleUpdateCurrentState = async (problemId: string) => {
    if (!currentStateInput.trim()) {
      toast.error('Введіть поточний стан')
      return
    }
    
    const currentImagesCount = detailProblem?.coordinatorImages?.length || 0
    const newFilesCount = coordinatorFiles?.length || 0
    if (currentImagesCount + newFilesCount > MAX_COORDINATOR_IMAGES) {
      toast.error(`Максимальна кількість фото: ${MAX_COORDINATOR_IMAGES}. Вже завантажено: ${currentImagesCount}`)
      return
    }
    
    try {
      await problemsApi.updateCurrentState(problemId, currentStateInput)
      
      if (coordinatorFiles && coordinatorFiles.length > 0) {
        await problemsApi.uploadCoordinatorImages(problemId, coordinatorFiles)
      }
      
      toast.success('Поточний стан оновлено')
      setCurrentStateInput('')
      setCoordinatorFiles(null)
      setActionMode(null)
      invalidateQueries()
    } catch {
      toast.error('Помилка оновлення')
    }
  }

  const handleRestoreProblem = async (problemId: string) => {
    try {
      await problemsApi.restoreProblem(problemId)
      toast.success('Проблему повернено')
      setDetailProblem(null)
      invalidateQueries()
    } catch {
      toast.error('Помилка повернення проблеми')
    }
  }

  const handleCompleteProblem = async (problemId: string) => {
    if (!currentStateInput.trim()) {
      toast.error('Опишіть що було зроблено')
      return
    }

    const currentImagesCount = detailProblem?.coordinatorImages?.length || 0
    const newFilesCount = coordinatorFiles?.length || 0
    if (currentImagesCount + newFilesCount > MAX_COORDINATOR_IMAGES) {
      toast.error(`Максимальна кількість фото: ${MAX_COORDINATOR_IMAGES}. Вже завантажено: ${currentImagesCount}`)
      return
    }
    try {
      await problemsApi.completeProblem(problemId, currentStateInput)
      
      if (coordinatorFiles && coordinatorFiles.length > 0) {
        await problemsApi.uploadCoordinatorImages(problemId, coordinatorFiles)
      }
      
      toast.success('Проблему завершено')
      setCurrentStateInput('')
      setCoordinatorFiles(null)
      setActionMode(null)
      invalidateQueries()
    } catch {
      toast.error('Помилка завершення')
    }
  }

  const handleCoordinatorFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFiles = e.target.files
    const currentImagesCount = detailProblem?.coordinatorImages?.length || 0
    const maxNewFiles = MAX_COORDINATOR_IMAGES - currentImagesCount
    
    if (selectedFiles && selectedFiles.length > maxNewFiles) {
      toast.error(`Можна додати ще ${maxNewFiles} фото (максимум ${MAX_COORDINATOR_IMAGES})`)
      e.target.value = ''
      setCoordinatorFiles(null)
      return
    }
    setCoordinatorFiles(selectedFiles)
  }

  const resetActionMode = () => {
    setActionMode(null)
    setCoordinatorFiles(null)
    setCurrentStateInput('')
    setRejectionReason('')
    if (fileInputRef.current) fileInputRef.current.value = ''
  }

  if (loading) {
    return <div className="container mx-auto p-6">Завантаження...</div>
  }

  // For 'new' tab, filter only problems without coordinator
  const newProblems = activeTab === 'new' 
    ? statusProblems.filter((p) => !p.coordinator)
    : []

  // Get problems to display based on active tab
  const getDisplayProblems = () => {
    // For 'my' tab, show only problems with status 'В роботі' assigned to this coordinator
    if (activeTab === 'my') return myProblems.filter((p) => p.status === ProblemStatusConstants.InProgress)
    if (activeTab === 'new') return newProblems
    // For 'completed' and 'rejected' tabs, show only problems assigned to this coordinator
    if (activeTab === 'completed' || activeTab === 'rejected') {
      return statusProblems.filter((p) => p.coordinator?.id === user?.id)
    }
    return statusProblems
  }

  const displayProblems = getDisplayProblems()

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
                  <Button onClick={() => handleValidate(detailProblem.id!)}>
                    Провалідувати
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

            {isMyProblem && detailProblem.status === ProblemStatusConstants.Validated && (
              <div className="space-y-4 pt-4 border-t">
                <div className="bg-cyan-50 p-4 rounded">
                  <p className="text-sm text-cyan-700">
                    Проблема провалідована. Ви можете взяти її в роботу або відхилити.
                  </p>
                </div>
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
                  {actionMode !== 'reject' && actionMode !== 'complete' && actionMode !== 'currentState' && (
                    <>
                      <Button variant="outline" onClick={() => setActionMode('currentState')}>
                        Оновити поточний стан
                      </Button>
                      <Button className="bg-green-600 hover:bg-green-700" onClick={() => setActionMode('complete')}>
                        Завершити
                      </Button>
                      <Button variant="destructive" onClick={() => setActionMode('reject')}>
                        Відхилити
                      </Button>
                    </>
                  )}
                </div>

                {actionMode === 'currentState' && (
                  <div className="space-y-3">
                    <Textarea
                      placeholder="Опишіть поточний стан виконання..."
                      value={currentStateInput}
                      onChange={(e) => setCurrentStateInput(e.target.value)}
                    />
                    <div className="space-y-2">
                      <p className="text-sm text-muted-foreground">
                        Додати фото (завантажено {detailProblem.coordinatorImages?.length || 0} з {MAX_COORDINATOR_IMAGES}, можна ще {MAX_COORDINATOR_IMAGES - (detailProblem.coordinatorImages?.length || 0)})
                      </p>
                      <input
                        ref={fileInputRef}
                        type="file"
                        multiple
                        accept="image/*"
                        onChange={handleCoordinatorFileChange}
                        className="text-sm"
                      />
                      {coordinatorFiles && coordinatorFiles.length > 0 && (
                        <p className="text-sm text-muted-foreground">
                          Обрано файлів: {coordinatorFiles.length}
                        </p>
                      )}
                    </div>
                    <div className="flex gap-2">
                      <Button onClick={() => handleUpdateCurrentState(detailProblem.id!)}>Зберегти</Button>
                      <Button variant="outline" onClick={resetActionMode}>Скасувати</Button>
                    </div>
                  </div>
                )}

                {actionMode === 'complete' && (
                  <div className="space-y-3">
                    <Textarea
                      placeholder="Опишіть що було зроблено для вирішення проблеми..."
                      value={currentStateInput}
                      onChange={(e) => setCurrentStateInput(e.target.value)}
                    />
                    <div className="space-y-2">
                      <p className="text-sm text-muted-foreground">
                        Додати фото (завантажено {detailProblem.coordinatorImages?.length || 0} з {MAX_COORDINATOR_IMAGES}, можна ще {MAX_COORDINATOR_IMAGES - (detailProblem.coordinatorImages?.length || 0)})
                      </p>
                      <input
                        ref={fileInputRef}
                        type="file"
                        multiple
                        accept="image/*"
                        onChange={handleCoordinatorFileChange}
                        className="text-sm"
                      />
                      {coordinatorFiles && coordinatorFiles.length > 0 && (
                        <p className="text-sm text-muted-foreground">
                          Обрано файлів: {coordinatorFiles.length}
                        </p>
                      )}
                    </div>
                    <div className="flex gap-2">
                      <Button className="bg-green-600 hover:bg-green-700" onClick={() => handleCompleteProblem(detailProblem.id!)}>
                        Завершити проблему
                      </Button>
                      <Button variant="outline" onClick={resetActionMode}>Скасувати</Button>
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

            {/* Restore button for rejected problems */}
            {detailProblem.status === ProblemStatusConstants.Rejected && (
              <div className="space-y-4 pt-4 border-t">
                <div className="bg-red-50 p-4 rounded">
                  <p className="text-sm text-red-700">
                    Ця проблема була відхилена. Ви можете повернути її до статусу "Нова".
                  </p>
                </div>
                <Button onClick={() => handleRestoreProblem(detailProblem.id!)}>
                  Повернути проблему
                </Button>
              </div>
            )}
          </CardContent>
        </Card>
      </div>
    )
  }

  const getStatusBadgeClass = (status: string) => {
    switch (status) {
      case ProblemStatusConstants.New: return 'bg-blue-100 text-blue-800'
      case ProblemStatusConstants.Validated: return 'bg-cyan-100 text-cyan-800'
      case ProblemStatusConstants.InProgress: return 'bg-yellow-100 text-yellow-800'
      case ProblemStatusConstants.Completed: return 'bg-green-100 text-green-800'
      case ProblemStatusConstants.Rejected: return 'bg-red-100 text-red-800'
      default: return 'bg-gray-100 text-gray-800'
    }
  }

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">Панель координатора</h1>

      <div className="flex gap-2 mb-6 flex-wrap">
        {tabConfig.map((tab) => (
          <Button
            key={tab.key}
            variant={activeTab === tab.key ? 'default' : 'outline'}
            onClick={() => setActiveTab(tab.key)}
            size="sm"
          >
            {tab.label} {tab.key === 'my' ? `(${myProblems.length})` : tab.key === 'new' ? `(${newProblems.length})` : ''}
          </Button>
        ))}
      </div>

      <div className="grid gap-4">
        {displayProblems.length === 0 ? (
          <p className="text-gray-500">Немає проблем у цій категорії</p>
        ) : (
          displayProblems.map((problem) => (
            <Card
              key={problem.id}
              className="cursor-pointer hover:shadow-md transition-shadow"
              onClick={() => setDetailProblem(problem)}
            >
              <CardHeader>
                <CardTitle className="flex justify-between items-center">
                  <span>{problem.title}</span>
                  <span className={`inline-flex items-center rounded-full px-2 py-1 text-xs font-medium ${getStatusBadgeClass(problem.status)}`}>
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
                {problem.rejectionReason && (
                  <p className="text-sm text-red-600 mb-2 line-clamp-1">
                    <span className="font-semibold">Причина:</span> {problem.rejectionReason}
                  </p>
                )}
                <p className="text-xs text-gray-500">
                  Автор: {problem.createdBy?.email} | {new Date(problem.createdAt).toLocaleDateString('uk-UA')}
                </p>

                {activeTab === 'my' && (
                  <div className="mt-3 flex gap-2" onClick={(e) => e.stopPropagation()}>
                    <Button
                      variant="outline"
                      size="sm"
                      disabled={!problem.id}
                      onClick={() => problem.id && navigate(`/problems/${problem.id}`)}
                    >
                      Детальніше
                    </Button>
                    <Button
                      size="sm"
                      onClick={() => {
                        setDetailProblem(problem)
                        setActionMode('currentState')
                        setCurrentStateInput(problem.currentState || '')
                      }}
                    >
                      Оновити
                    </Button>
                  </div>
                )}
              </CardContent>
            </Card>
          ))
        )}
      </div>
    </div>
  )
}
