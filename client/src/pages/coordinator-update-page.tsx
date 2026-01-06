import { useState, useRef, useEffect } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { problemsApi } from '@/features/problems/api/problems-api'
import { useProblem } from '@/features/problems/hooks/use-problems'
import { ProblemStatusConstants, PriorityConstants } from '@/types'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Textarea } from '@/components/ui/textarea'
import { Label } from '@/components/ui/label'
import { useAuth } from '@/contexts/auth-context'
import { toast } from '@/lib/toast'
import { useQueryClient } from '@tanstack/react-query'
import { LocationPickerMap } from '@/components/location-picker-map'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { ArrowLeft } from 'lucide-react'

const MAX_COORDINATOR_IMAGES = 6

export default function CoordinatorUpdatePage() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const { user } = useAuth()
  const queryClient = useQueryClient()
  
  const { data: problem, isLoading } = useProblem(id || '')

  const [rejectionReason, setRejectionReason] = useState('')
  const [currentStateInput, setCurrentStateInput] = useState('')
  const [coordinatorFiles, setCoordinatorFiles] = useState<FileList | null>(null)
  const [selectedPriority, setSelectedPriority] = useState<string>('')
  const [newLocation, setNewLocation] = useState<{ lat: number; lng: number } | null>(null)
  const fileInputRef = useRef<HTMLInputElement>(null)

  // Initialize state from problem data
  useEffect(() => {
    if (problem) {
      if (problem.currentState) setCurrentStateInput(problem.currentState)
      if (problem.priority) setSelectedPriority(problem.priority)
    }
  }, [problem])

  if (isLoading || !problem) {
    return <div className="container mx-auto p-6">Завантаження...</div>
  }

  const invalidateQueries = () => {
    queryClient.invalidateQueries({ queryKey: ['problems'] })
  }

  const handleAssignToMe = async () => {
    if (!user?.id || !problem.id) return
    try {
      const priorityToUse = selectedPriority || problem.priority
      await problemsApi.assignCoordinator(problem.id, user.id, priorityToUse)
      toast.success('Проблему взято в роботу')
      invalidateQueries()
      // Stay on page or navigate?
    } catch {
      toast.error('Помилка призначення')
    }
  }

  const handleReject = async () => {
    if (!rejectionReason.trim()) {
      toast.error('Вкажіть причину відхилення')
      return
    }
    if (!user?.id || !problem.id) return
    try {
      await problemsApi.reject(problem.id, user.id, rejectionReason)
      toast.success('Проблему відхилено')
      setRejectionReason('')
      invalidateQueries()
      navigate('/coordinator')
    } catch {
      toast.error('Помилка відхилення')
    }
  }

  const handleUpdateCurrentState = async () => {
    if (!currentStateInput.trim()) {
      toast.error('Введіть поточний стан')
      return
    }
    
    if (!problem.id) return

    const currentImagesCount = problem.coordinatorImages?.length || 0
    const newFilesCount = coordinatorFiles?.length || 0
    if (currentImagesCount + newFilesCount > MAX_COORDINATOR_IMAGES) {
      toast.error(`Максимальна кількість фото: ${MAX_COORDINATOR_IMAGES}. Вже завантажено: ${currentImagesCount}`)
      return
    }
    
    try {
      if (selectedPriority && selectedPriority !== problem.priority) {
        await problemsApi.assignCoordinator(problem.id, user?.id || '', selectedPriority)
      }
      
      await problemsApi.updateCurrentState(problem.id, currentStateInput)
      
      if (coordinatorFiles && coordinatorFiles.length > 0) {
        await problemsApi.uploadCoordinatorImages(problem.id, coordinatorFiles)
      }
      
      toast.success('Поточний стан оновлено')
      setCoordinatorFiles(null)
      if (fileInputRef.current) fileInputRef.current.value = ''
      invalidateQueries()
      navigate('/coordinator')
    } catch {
      toast.error('Помилка оновлення')
    }
  }

  const handleUpdateLocation = async () => {
    if (!newLocation || !problem.id) {
      toast.error('Оберіть нову локацію на карті')
      return
    }
    try {
      await problemsApi.updateLocation(problem.id, newLocation.lat, newLocation.lng)
      toast.success('Адресу оновлено')
      setNewLocation(null)
      invalidateQueries()
    } catch {
      toast.error('Помилка оновлення адреси')
    }
  }

  const handleCompleteProblem = async () => {
    if (!currentStateInput.trim()) {
      toast.error('Опишіть що було зроблено')
      return
    }
    if (!problem.id) return

    const currentImagesCount = problem.coordinatorImages?.length || 0
    const newFilesCount = coordinatorFiles?.length || 0
    if (currentImagesCount + newFilesCount > MAX_COORDINATOR_IMAGES) {
      toast.error(`Максимальна кількість фото: ${MAX_COORDINATOR_IMAGES}. Вже завантажено: ${currentImagesCount}`)
      return
    }
    try {
      await problemsApi.completeProblem(problem.id, currentStateInput)
      
      if (coordinatorFiles && coordinatorFiles.length > 0) {
        await problemsApi.uploadCoordinatorImages(problem.id, coordinatorFiles)
      }
      
      toast.success('Проблему завершено')
      invalidateQueries()
      navigate('/coordinator')
    } catch {
      toast.error('Помилка завершення')
    }
  }

  const handleCoordinatorFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFiles = e.target.files
    const currentImagesCount = problem.coordinatorImages?.length || 0
    const maxNewFiles = MAX_COORDINATOR_IMAGES - currentImagesCount
    
    if (selectedFiles && selectedFiles.length > maxNewFiles) {
      toast.error(`Можна додати ще ${maxNewFiles} фото (максимум ${MAX_COORDINATOR_IMAGES})`)
      e.target.value = ''
      setCoordinatorFiles(null)
      return
    }
    setCoordinatorFiles(selectedFiles)
  }

  const getStatusBadgeClass = (status: string) => {
    switch (status) {
      case ProblemStatusConstants.New: return 'bg-blue-100 text-blue-800'
      case ProblemStatusConstants.InProgress: return 'bg-yellow-100 text-yellow-800'
      case ProblemStatusConstants.Completed: return 'bg-green-100 text-green-800'
      case ProblemStatusConstants.Rejected: return 'bg-red-100 text-red-800'
      default: return 'bg-gray-100 text-gray-800'
    }
  }

  return (
    <div className="container mx-auto p-6 space-y-6">
       <div className="flex items-center gap-4">
        <Button 
          variant="outline" 
          size="icon" 
          onClick={() => navigate('/coordinator')}
          className="h-9 w-9 border border-[#D0D5DD] bg-white text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929]"
        >
          <ArrowLeft className="w-4 h-4 text-[#292929]" />
        </Button>
        <h1 className="text-2xl font-bold">Оновлення стану проблеми</h1>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {/* Left Column: Info & Actions */}
        <div className="space-y-6">
            <Card>
                <CardHeader>
                    <CardTitle className="flex justify-between items-center">
                        <span>{problem.title}</span>
                         <span className={`inline-flex items-center rounded-full px-3 py-1 text-sm font-medium ${getStatusBadgeClass(problem.status)}`}>
                            {problem.status}
                        </span>
                    </CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                     <div className="mb-6">
                        <h3 className="font-semibold mb-1">Опис:</h3>
                        <p className="text-gray-600 whitespace-pre-wrap break-words" style={{ overflowWrap: 'anywhere' }}>
                            {problem.description}
                        </p>
                    </div>
                    <div className="grid grid-cols-2 gap-4 text-sm">
                        <div>
                            <span className="font-semibold">Автор:</span> {problem.createdBy?.email}
                        </div>
                        <div>
                             <span className="font-semibold">Створено:</span> {new Date(problem.createdAt).toLocaleString('uk-UA')}
                        </div>
                    </div>
                </CardContent>
            </Card>

            {/* Actions */}
            <Card>
                <CardHeader>
                    <CardTitle>Дії координатора</CardTitle>
                </CardHeader>
                <CardContent className="space-y-6">
                    {/* Status Management */}
                    {(problem.status === ProblemStatusConstants.New || problem.status === ProblemStatusConstants.InProgress) && (
                        <div className="space-y-4">
                             <div className="space-y-6">
                                <Label className="block text-sm font-medium text-[#1F2732] mb-3">
                                    Пріоритет проблеми
                                </Label>
                                <div className="mb-6">
                                    <Select
                                        value={selectedPriority || problem.priority || ''}
                                        onValueChange={setSelectedPriority}
                                    >
                                        <SelectTrigger className="w-full bg-white border border-gray-300 text-gray-900 focus:ring-offset-0 focus-visible:ring-offset-0">
                                            <SelectValue placeholder="Оберіть пріоритет" />
                                        </SelectTrigger>
                                        <SelectContent className="bg-white border border-gray-200 shadow-lg text-gray-900">
                                            <SelectItem value={PriorityConstants.Low} className="hover:bg-gray-100 cursor-pointer focus:bg-gray-100 focus:text-gray-900">{PriorityConstants.Low}</SelectItem>
                                            <SelectItem value={PriorityConstants.Medium} className="hover:bg-gray-100 cursor-pointer focus:bg-gray-100 focus:text-gray-900">{PriorityConstants.Medium}</SelectItem>
                                            <SelectItem value={PriorityConstants.High} className="hover:bg-gray-100 cursor-pointer focus:bg-gray-100 focus:text-gray-900">{PriorityConstants.High}</SelectItem>
                                            <SelectItem value={PriorityConstants.Critical} className="hover:bg-gray-100 cursor-pointer focus:bg-gray-100 focus:text-gray-900">{PriorityConstants.Critical}</SelectItem>
                                        </SelectContent>
                                    </Select>
                                </div>
                            </div>

                            {/* State / Comments */}
                            <div className="space-y-4">
                                <Label className="block text-sm font-medium text-[#1F2732] mb-3">
                                    Поточний стан / Коментар до виконання
                                </Label>
                                <Textarea
                                    placeholder={"Опишіть поточний стан виконання або причину відхилення (якщо відхиляєте)..."}
                                    value={problem.status === ProblemStatusConstants.New ? rejectionReason : currentStateInput}
                                    onChange={(e) => problem.status === ProblemStatusConstants.New ? setRejectionReason(e.target.value) : setCurrentStateInput(e.target.value)}
                                    className="min-h-[100px] focus-visible:ring-offset-0"
                                />
                            </div>

                             {/* Image Upload for In Progress */}
                             {problem.status === ProblemStatusConstants.InProgress && (
                                <div className="space-y-2">
                                    <Label>Додати фото</Label>
                                    <p className="text-sm text-muted-foreground mb-2">
                                        Завантажено {problem.coordinatorImages?.length || 0} з {MAX_COORDINATOR_IMAGES}
                                    </p>
                                    <input
                                        ref={fileInputRef}
                                        type="file"
                                        multiple
                                        accept="image/*"
                                        onChange={handleCoordinatorFileChange}
                                        className="text-sm w-full"
                                    />
                                </div>
                             )}

                            {/* Buttons */}
                            <div className="flex flex-wrap gap-3 pt-4 border-t">
                                {problem.status === ProblemStatusConstants.New && (
                                    <>
                                        <Button onClick={handleAssignToMe} className="bg-[#E42556] hover:bg-[#D44374] text-white">
                                            Взяти в роботу
                                        </Button>
                                        <Button variant="outline" onClick={handleReject} className="border-red-200 text-red-700 hover:bg-red-50 hover:text-red-800">
                                            Відхилити
                                        </Button>
                                        <Button variant="outline" onClick={() => navigate('/coordinator')} className="border-[#D0D5DD] text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929]">
                                            Скасувати
                                        </Button>
                                    </>
                                )}

                                {problem.status === ProblemStatusConstants.InProgress && (
                                    <>
                                        <Button onClick={handleUpdateCurrentState} className="bg-blue-600 hover:bg-blue-700 text-white">
                                            Оновити стан
                                        </Button>
                                        <Button onClick={handleCompleteProblem} className="bg-green-600 hover:bg-green-700 text-white">
                                            Завершити
                                        </Button>
                                         <Button variant="outline" onClick={() => {
                                            setRejectionReason('Відхилено під час виконання')
                                            handleReject()
                                         }} className="border-red-200 text-red-700 hover:bg-red-50 hover:text-red-800">
                                            Відхилити
                                        </Button>
                                        <Button variant="outline" onClick={() => navigate('/coordinator')} className="border-[#D0D5DD] text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929]">
                                            Скасувати
                                        </Button>
                                    </>
                                )}
                            </div>
                        </div>
                    )}
                </CardContent>
            </Card>
        </div>

        {/* Right Column: Map & Location */}
        <div className="space-y-6">
             <Card>
                <CardHeader>
                    <CardTitle>Місцезнаходження</CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                    <LocationPickerMap
                        latitude={newLocation?.lat || problem.latitude}
                        longitude={newLocation?.lng || problem.longitude}
                        readonly={problem.status !== ProblemStatusConstants.InProgress}
                        height="350px"
                        onLocationChange={(lat, lng) => setNewLocation({ lat, lng })}
                    />
                    {problem.status === ProblemStatusConstants.InProgress && (
                        <div className="flex justify-end pt-2">
                             <Button 
                                onClick={handleUpdateLocation}
                                disabled={!newLocation}
                                variant="outline"
                                className="border-primary text-primary hover:bg-primary/10 hover:text-primary transition-colors"
                             >
                                Зберегти нову адресу
                             </Button>
                        </div>
                    )}
                </CardContent>
            </Card>
        </div>
      </div>
    </div>
  )
}
