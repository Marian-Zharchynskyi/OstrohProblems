import { useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { useProblem } from '@/features/problems/hooks/use-problems'
import { useRealtimeComments } from '@/hooks/use-realtime-comments'
import { useAuth } from '@/contexts/auth-context'
import { commentsApi } from '@/features/comments/api/comments-api'
import { ratingsApi } from '@/features/ratings/api/ratings-api'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { 
  MapPin, 
  User, 
  Tag, 
  ArrowLeft, 
  MessageSquare, 
  Star,
  Send
} from 'lucide-react'
import { toast } from '@/lib/toast'

export function ProblemDetailPage() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const { user } = useAuth()
  const { data: problem, isLoading, refetch } = useProblem(id || '')
  
  const realtimeComments = useRealtimeComments(id || null, problem?.comments || [])
  
  const [newComment, setNewComment] = useState('')
  const [isSubmittingComment, setIsSubmittingComment] = useState(false)
  
  const [newRating, setNewRating] = useState<number>(5)
  const [isSubmittingRating, setIsSubmittingRating] = useState(false)

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

  const handleSubmitRating = async () => {
    if (!id) return
    
    try {
      setIsSubmittingRating(true)
      await ratingsApi.create({ points: newRating, problemId: id })
      toast.success('Оцінку додано')
      refetch()
    } catch {
      toast.error('Не вдалося додати оцінку')
    } finally {
      setIsSubmittingRating(false)
    }
  }

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
        <Button variant="outline" size="icon" onClick={() => navigate('/problems')}>
          <ArrowLeft className="w-4 h-4" />
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
          {/* Images */}
          {problem.images && problem.images.length > 0 && (
            <Card>
              <CardHeader>
                <CardTitle>Зображення</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="grid grid-cols-2 gap-4">
                  {problem.images.map((image) => (
                    <img
                      key={image.id}
                      src={image.url}
                      alt={problem.title}
                      className="h-48 w-full rounded-lg object-cover"
                    />
                  ))}
                </div>
              </CardContent>
            </Card>
          )}

          {/* Description */}
          <Card>
            <CardHeader>
              <CardTitle>Опис проблеми</CardTitle>
            </CardHeader>
            <CardContent>
              <p className="text-gray-600 whitespace-pre-wrap">{problem.description}</p>
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
                  />
                  <Button 
                    onClick={handleSubmitComment} 
                    disabled={isSubmittingComment || !newComment.trim()}
                  >
                    <Send className="w-4 h-4" />
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
          {/* Status */}
          <Card>
            <CardHeader>
              <CardTitle>Статус</CardTitle>
            </CardHeader>
            <CardContent>
              <span className="inline-flex items-center rounded-full bg-blue-50 px-3 py-1 text-sm text-blue-700">
                {problem.problemStatus?.name || 'Невідомо'}
              </span>
            </CardContent>
          </Card>

          {/* Location */}
          <Card>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <MapPin className="w-5 h-5" />
                Координати
              </CardTitle>
            </CardHeader>
            <CardContent>
              <p className="text-sm text-muted-foreground">
                {problem.latitude.toFixed(6)}, {problem.longitude.toFixed(6)}
              </p>
            </CardContent>
          </Card>

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
                  {problem.categories.map((category) => (
                    <span
                      key={category.id}
                      className="inline-flex items-center rounded-full bg-primary/10 px-3 py-1 text-sm text-primary"
                    >
                      {category.name}
                    </span>
                  ))}
                </div>
              </CardContent>
            </Card>
          )}

          {/* Author */}
          {problem.user && (
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <User className="w-5 h-5" />
                  Автор
                </CardTitle>
              </CardHeader>
              <CardContent>
                <p className="text-sm">{problem.user.email}</p>
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

          {/* Add Rating */}
          {user && (
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <Star className="w-5 h-5" />
                  Оцінити проблему
                </CardTitle>
              </CardHeader>
              <CardContent className="space-y-4">
                <div className="space-y-2">
                  <Label>Оцінка (1-5)</Label>
                  <div className="flex gap-1">
                    {[1, 2, 3, 4, 5].map((value) => (
                      <button
                        key={value}
                        type="button"
                        onClick={() => setNewRating(value)}
                        className={`p-1 rounded ${
                          value <= newRating
                            ? 'text-yellow-500'
                            : 'text-gray-300'
                        }`}
                      >
                        <Star className="w-6 h-6 fill-current" />
                      </button>
                    ))}
                  </div>
                </div>
                <Button 
                  onClick={handleSubmitRating} 
                  disabled={isSubmittingRating}
                  className="w-full"
                >
                  {isSubmittingRating ? 'Збереження...' : 'Оцінити'}
                </Button>
              </CardContent>
            </Card>
          )}
        </div>
      </div>
    </div>
  )
}
