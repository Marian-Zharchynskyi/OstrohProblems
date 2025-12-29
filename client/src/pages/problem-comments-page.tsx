import { useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { useProblem } from '@/features/problems/hooks/use-problems'
import { useRealtimeComments } from '@/hooks/use-realtime-comments'
import { useAuth } from '@/contexts/auth-context'
import { commentsApi } from '@/features/comments/api/comments-api'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { ArrowLeft, MessageSquare, Send, Loader2 } from 'lucide-react'
import { toast } from '@/lib/toast'

export function ProblemCommentsPage() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const { user } = useAuth()
  const { data: problem, isLoading, refetch } = useProblem(id || '')
  
  const realtimeComments = useRealtimeComments(id || null, problem?.comments ?? undefined)
  
  const [newComment, setNewComment] = useState('')
  const [isSubmittingComment, setIsSubmittingComment] = useState(false)

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

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <Loader2 className="h-8 w-8 animate-spin text-[#E42556]" />
      </div>
    )
  }

  if (!problem) {
    return (
      <div className="flex flex-col items-center justify-center h-64 gap-4">
        <p className="text-muted-foreground">Проблему не знайдено</p>
        <Button onClick={() => navigate(-1)}>
          <ArrowLeft className="w-4 h-4 mr-2" />
          Повернутися назад
        </Button>
      </div>
    )
  }

  return (
    <div className="max-w-4xl mx-auto px-6 py-8">
      <div className="flex items-center gap-4 mb-6">
        <Button variant="outline" size="icon" onClick={() => navigate(-1)}>
          <ArrowLeft className="w-4 h-4" />
        </Button>
        <div>
          <h1 className="text-2xl font-bold text-[#1F2732] font-['Mulish']">
            Коментарі до проблеми
          </h1>
          <p className="text-gray-600">{problem.title}</p>
        </div>
      </div>

      <Card className="bg-white border border-gray-200 rounded-[10px]">
        <CardHeader className="pb-4">
          <CardTitle className="text-lg font-bold text-[#1F2732] font-['Mulish'] flex items-center gap-2">
            <MessageSquare className="w-5 h-5" />
            Всі коментарі ({realtimeComments.length})
          </CardTitle>
        </CardHeader>
        <CardContent>
          {user && (
            <div className="flex gap-2 mb-6">
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
                className="bg-[#F0F1F2] border-none rounded-lg"
              />
              <Button 
                onClick={handleSubmitComment} 
                disabled={isSubmittingComment || !newComment.trim()}
                className="bg-[#E42556] hover:bg-[#E42556]/90"
              >
                <Send className="w-4 h-4" />
              </Button>
            </div>
          )}

          {realtimeComments.length === 0 ? (
            <div className="text-center py-12">
              <MessageSquare className="w-12 h-12 text-gray-300 mx-auto mb-4" />
              <p className="text-gray-500 text-lg mb-2">Поки що коментарів немає</p>
              <p className="text-gray-400 text-sm">
                Будьте першим, хто прокоментує цю проблему!
              </p>
            </div>
          ) : (
            <div className="space-y-4">
              {realtimeComments.map((comment) => (
                <div key={comment.id} className="bg-gray-50 rounded-lg p-4">
                  <div className="flex items-center justify-between mb-2">
                    <div className="flex items-center gap-2">
                      <div className="w-8 h-8 bg-[#E42556] rounded-full flex items-center justify-center text-white text-sm font-medium">
                        {comment.user?.name?.[0] || comment.user?.email?.[0] || '?'}
                      </div>
                      <div>
                        <span className="text-sm font-medium text-gray-900">
                          {comment.user?.name} {comment.user?.surname}
                        </span>
                        {comment.user?.email && (
                          <span className="text-xs text-gray-500 ml-2">
                            ({comment.user.email})
                          </span>
                        )}
                      </div>
                    </div>
                    <span className="text-xs text-gray-500">
                      {new Date(comment.createdAt).toLocaleDateString('uk-UA', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                      })}
                    </span>
                  </div>
                  <p className="text-gray-600 whitespace-pre-wrap">{comment.content}</p>
                </div>
              ))}
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  )
}
