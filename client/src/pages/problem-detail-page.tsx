import { useState, useEffect } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { useProblem } from '@/features/problems/hooks/use-problems'
import { useRealtimeComments } from '@/hooks/use-realtime-comments'
import { useAuth } from '@/contexts/auth-context'
import { commentsApi } from '@/features/comments/api/comments-api'
import { ratingsApi } from '@/features/ratings/api/ratings-api'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Dialog, DialogContent, DialogFooter, DialogHeader, DialogTitle } from '@/components/ui/dialog'
import { ProblemImagesManager } from '@/components/problem-images-manager'
import { 
  User, 
  Tag, 
  ArrowLeft, 
  MessageSquare, 
  Star,
  Send
} from 'lucide-react'
import { LocationPickerMap } from '@/components/location-picker-map'
import { toast } from '@/lib/toast'

export function ProblemDetailPage() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const { user } = useAuth()
  const { data: problem, isLoading, refetch } = useProblem(id || '')
  
  const realtimeComments = useRealtimeComments(id || null, problem?.comments ?? undefined)
  
  const [newComment, setNewComment] = useState('')
  const [isSubmittingComment, setIsSubmittingComment] = useState(false)
  
  const [newRating, setNewRating] = useState<number>(5)
  const [isSubmittingRating, setIsSubmittingRating] = useState(false)
  const [userRating, setUserRating] = useState<number | null>(null)
  const [hasUserRated, setHasUserRated] = useState(false)
  const [isRatingModalOpen, setIsRatingModalOpen] = useState(false)
  const [averageRating, setAverageRating] = useState<number>(0)

  const handleSubmitComment = async () => {
    if (!id || !newComment.trim()) return
    
    try {
      setIsSubmittingComment(true)
      await commentsApi.create({ content: newComment, problemId: id })
      setNewComment('')
      toast.success('Коментар додано')
      refetch()
    } catch {
      toast.error('Не вдалося додати коментар')
    } finally {
      setIsSubmittingComment(false)
    }
  }

  const fetchUserRating = async () => {
    if (!id) return
    try {
      const rating = await ratingsApi.getUserRatingForProblem(id)
      if (rating) {
        setUserRating(rating.points)
        setHasUserRated(true)
      } else {
        setUserRating(null)
        setHasUserRated(false)
      }
      
      const avgRating = await ratingsApi.getAverageByProblemId(id)
      setAverageRating(avgRating)
    } catch {
      setUserRating(null)
      setHasUserRated(false)
      setAverageRating(0)
    }
  }

  const handleSubmitRating = async () => {
    if (!id) return
    
    try {
      setIsSubmittingRating(true)
      await ratingsApi.create({ points: newRating, problemId: id })
      toast.success('Оцінку додано')
      setIsRatingModalOpen(false)
      await fetchUserRating()
      refetch()
    } catch {
      toast.error('Не вдалося додати оцінку')
    } finally {
      setIsSubmittingRating(false)
    }
  }

  const renderStars = (rating: number) => {
    return [1, 2, 3, 4, 5].map((star) => {
      const fillPercentage = Math.max(0, Math.min(100, (rating - star + 1) * 100))
      
      return (
        <div key={star} className="relative w-6 h-6">
          <Star className="w-6 h-6 text-gray-300 absolute" />
          <div 
            className="overflow-hidden absolute" 
            style={{ width: `${fillPercentage}%` }}
          >
            <Star className="w-6 h-6 text-yellow-500 fill-yellow-500" />
          </div>
        </div>
      )
    })
  }

  useEffect(() => {
    if (id && user) {
      fetchUserRating()
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id, user])

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <p className="text-muted-foreground">Завантаження...</p>
      </div>
    )
  }

  if (!problem) {
    return (
      <div className="flex flex-col items-center justify-center h-64 gap-4">
        <p className="text-muted-foreground">Проблему не знайдено</p>
        <Button onClick={() => navigate('/problems')}>
          <ArrowLeft className="w-4 h-4 mr-2" />
          Повернутися до списку
        </Button>
      </div>
    )
  }


  return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex items-center gap-4">
        <Button 
          variant="outline" 
          size="icon" 
          onClick={() => navigate('/problems')}
          className="h-9 w-9 border border-[#D0D5DD] bg-white text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929]"
        >
          <ArrowLeft className="w-4 h-4 text-[#292929]" />
        </Button>
        <div>
          <h1 className="text-2xl font-bold">{problem.title}</h1>
          <p className="text-muted-foreground">
            Створено: {new Date(problem.createdAt).toLocaleDateString('uk-UA', {
              year: 'numeric',
              month: 'long',
              day: 'numeric',
              hour: '2-digit',
              minute: '2-digit',
            })}
          </p>
        </div>
      </div>

      <div className="grid gap-6 md:grid-cols-3">
        {/* Main Info */}
        <div className="md:col-span-2 space-y-6">
          {/* Images from Author */}
          <ProblemImagesManager
            problemId={problem.id || ''}
            images={problem.images}
            onImagesChange={refetch}
            canEdit={user?.id === problem.createdBy?.id}
            imageType="problem"
            title="Фото від автора"
          />

          {/* Images from Coordinator */}
          <ProblemImagesManager
            problemId={problem.id || ''}
            images={problem.coordinatorImages}
            onImagesChange={refetch}
            canEdit={user?.id === problem.coordinator?.id}
            imageType="coordinator"
            title="Фото від координатора"
          />

          {/* Description */}
          <Card>
            <CardHeader>
              <CardTitle>Опис проблеми</CardTitle>
            </CardHeader>
            <CardContent>
              <p
                className="text-gray-600 whitespace-pre-wrap break-words"
                style={{ overflowWrap: 'anywhere' }}
              >
                {problem.description}
              </p>
            </CardContent>
          </Card>

          {/* Comments */}
          <Card>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <MessageSquare className="w-5 h-5" />
                Коментарі ({realtimeComments.length})
              </CardTitle>
              <CardDescription>Обговорення проблеми</CardDescription>
            </CardHeader>
            <CardContent className="space-y-4">
              {/* Add comment form */}
              {user && (
                <div className="flex gap-2">
                  <Input
                    placeholder="Напишіть коментар..."
                    value={newComment}
                    onChange={(e) => setNewComment(e.target.value)}
                    onKeyDown={(e) => {
                      if (e.key === 'Enter' && !e.shiftKey) {
                        e.preventDefault()
                        handleSubmitComment()
                      }
                    }}
                    className="flex-1"
                  />
                  <Button 
                    onClick={handleSubmitComment} 
                    disabled={isSubmittingComment || !newComment.trim()}
                    variant="outline"
                    size="icon"
                    className="h-10 w-10 border border-[#D0D5DD] bg-white text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929] disabled:bg-white disabled:text-[#292929]"
                  >
                    <Send className="w-5 h-5 text-[#292929]" />
                  </Button>
                </div>
              )}

              {/* Comments list */}
              {realtimeComments.length === 0 ? (
                <p className="text-muted-foreground text-center py-4">
                  Поки немає коментарів
                </p>
              ) : (
                <div className="space-y-3">
                  {realtimeComments.map((comment) => (
                    <div key={comment.id} className="rounded-lg bg-muted p-3">
                      <div className="mb-1 flex items-center justify-between">
                        <span className="text-sm font-medium">
                          {comment.user?.email || 'Анонім'}
                        </span>
                        <span className="text-xs text-muted-foreground">
                          {new Date(comment.createdAt).toLocaleDateString('uk-UA')}
                        </span>
                      </div>
                      <p className="text-sm">{comment.content}</p>
                    </div>
                  ))}
                </div>
              )}
            </CardContent>
          </Card>
        </div>

        {/* Sidebar */}
        <div className="space-y-6">
          {/* Location */}
          <Card>
            <CardHeader>
              <CardTitle>Місце на карті</CardTitle>
            </CardHeader>
            <CardContent>
              <LocationPickerMap
                latitude={problem.latitude}
                longitude={problem.longitude}
                readonly={true}
                height="300px"
              />
            </CardContent>
          </Card>

          {/* Status */}
          <Card>
            <CardHeader>
              <CardTitle>Статус</CardTitle>
            </CardHeader>
            <CardContent>
              <span className="inline-flex items-center rounded-full bg-blue-50 px-3 py-1 text-sm text-blue-700">
                {problem.status || 'Невідомо'}
              </span>
            </CardContent>
          </Card>

          {/* Priority */}
          <Card>
            <CardHeader>
              <CardTitle>Пріоритет</CardTitle>
            </CardHeader>
            <CardContent>
              <span className={`inline-flex items-center rounded-full px-3 py-1 text-sm font-medium ${
                problem.priority === 'Критичний' ? 'bg-red-50 text-red-700' :
                problem.priority === 'Високий' ? 'bg-orange-50 text-orange-700' :
                problem.priority === 'Середній' ? 'bg-yellow-50 text-yellow-700' :
                problem.priority === 'Низький' ? 'bg-green-50 text-green-700' :
                'bg-gray-50 text-gray-700'
              }`}>
                {problem.priority || 'Невідомо'}
              </span>
            </CardContent>
          </Card>

          {/* Current State */}
          {problem.currentState && (
            <Card>
              <CardHeader>
                <CardTitle>Поточний стан</CardTitle>
              </CardHeader>
              <CardContent>
                <p className="text-sm text-muted-foreground">{problem.currentState}</p>
              </CardContent>
            </Card>
          )}

          {/* Categories */}
          {problem.categories && problem.categories.length > 0 && (
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <Tag className="w-5 h-5" />
                  Категорії
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="flex flex-wrap gap-2">
                  {problem.categories.map((category, index) => (
                    <span
                      key={index}
                      className="inline-flex items-center rounded-full bg-primary/10 px-3 py-1 text-sm text-primary"
                    >
                      {category}
                    </span>
                  ))}
                </div>
              </CardContent>
            </Card>
          )}

          {/* Author */}
          {problem.createdBy && (
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <User className="w-5 h-5" />
                  Автор
                </CardTitle>
              </CardHeader>
              <CardContent>
                <p className="text-sm">{problem.createdBy.email}</p>
              </CardContent>
            </Card>
          )}

          {/* Coordinator Comment */}
          {problem.coordinatorComment && (
            <Card>
              <CardHeader>
                <CardTitle>Коментар координатора</CardTitle>
              </CardHeader>
              <CardContent>
                <p className="text-sm text-muted-foreground">{problem.coordinatorComment}</p>
              </CardContent>
            </Card>
          )}

          {/* Rating Display */}
          {user && (
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <Star className="w-5 h-5" />
                  Рейтинг проблеми
                </CardTitle>
              </CardHeader>
              <CardContent className="space-y-4">
                {hasUserRated ? (
                  <div className="space-y-3">
                    <div className="p-3 bg-green-50 rounded-lg">
                      <div className="flex items-center justify-between mb-2">
                        <p className="text-sm font-medium text-green-800">Ваша оцінка</p>
                        <Button 
                          size="sm"
                          variant="outline"
                          onClick={() => setIsRatingModalOpen(true)}
                          className="border border-[#D0D5DD] text-[#292929] bg-white hover:bg-[#F5F5F5] hover:text-[#292929]"
                        >
                          Оновити оцінку
                        </Button>
                      </div>
                      <div className="flex gap-1">
                        {renderStars(userRating || 0)}
                      </div>
                    </div>
                    {averageRating > 0 ? (
                      <div className="p-3 bg-blue-50 rounded-lg">
                        <p className="text-sm font-medium text-blue-800 mb-2">Середня оцінка</p>
                        <div className="flex gap-1">
                          {renderStars(averageRating)}
                        </div>
                      </div>
                    ) : (
                      <div className="p-3 bg-gray-50 rounded-lg">
                        <p className="text-sm font-medium text-gray-600">Середнього рейтингу не має</p>
                      </div>
                    )}
                  </div>
                ) : (
                  <div className="space-y-3">
                    {averageRating === 0 ? (
                      <div className="p-3 bg-blue-50 rounded-lg text-center">
                        <p className="text-sm font-medium text-blue-800 mb-3">Середнього рейтингу не має</p>
                        <Button 
                          onClick={() => setIsRatingModalOpen(true)} 
                          className="w-full bg-[#E42556] hover:bg-[#E42556]/90 text-white font-semibold shadow-md hover:shadow-lg transition-all duration-200"
                        >
                          Оцінити проблему
                        </Button>
                      </div>
                    ) : (
                      <div className="space-y-3">
                        <div className="p-3 bg-blue-50 rounded-lg">
                          <p className="text-sm font-medium text-blue-800 mb-2">Середня оцінка</p>
                          <div className="flex gap-1">
                            {renderStars(averageRating)}
                          </div>
                        </div>
                        <Button 
                          onClick={() => setIsRatingModalOpen(true)} 
                          className="w-full bg-[#E42556] hover:bg-[#E42556]/90 text-white font-semibold shadow-md hover:shadow-lg transition-all duration-200"
                        >
                          Оцінити проблему
                        </Button>
                      </div>
                    )}
                  </div>
                )}
              </CardContent>
            </Card>
          )}
        </div>
      </div>

      {/* Rating Modal */}
      <Dialog open={isRatingModalOpen} onOpenChange={setIsRatingModalOpen}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>{hasUserRated ? 'Оновити оцінку' : 'Оцінити проблему'}</DialogTitle>
          </DialogHeader>
          <div className="space-y-4 py-4">
            <div className="flex justify-center">
              <Input
                type="number"
                min="1"
                max="5"
                step="0.1"
                value={newRating}
                onChange={(e) => {
                  const val = parseFloat(e.target.value)
                  if (!isNaN(val)) {
                    setNewRating(val)
                  }
                }}
                className="w-24 text-center text-lg"
              />
            </div>
            <p className="text-sm text-gray-500 text-center">
              Введіть оцінку від 1 до 5 (можна використовувати дробові числа)
            </p>
          </div>
          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => setIsRatingModalOpen(false)}
              disabled={isSubmittingRating}
              className="border border-[#D0D5DD] text-[#292929] bg-transparent hover:bg-[#F5F5F5] hover:text-[#292929]"
            >
              Скасувати
            </Button>
            <Button 
              onClick={handleSubmitRating} 
              disabled={isSubmittingRating}
              className="bg-[#E42556] hover:bg-[#E42556]/90 text-white"
            >
              {isSubmittingRating ? 'Збереження...' : hasUserRated ? 'Оновити оцінку' : 'Зберегти'}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  )
}
