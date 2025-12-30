import { useState, useMemo, useRef, useEffect, type CSSProperties } from 'react'
import { MapContainer, TileLayer, Marker, Popup, useMap } from 'react-leaflet'
import { Icon } from 'leaflet'
import type { LatLngBoundsExpression, Map as LeafletMap, Marker as LeafletMarker } from 'leaflet'
import { useAuth } from '@/contexts/auth-context'
import { useProblemsByUserFiltered, type UserProblemsFilter } from '../hooks/use-problems'
import { useRealtimeComments } from '@/hooks/use-realtime-comments'
import { problemsApi } from '../api/problems-api'
import type { Problem, Comment } from '@/types'
import { ProblemStatusConstants, PriorityConstants, CategoryConstants } from '@/types'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Badge } from '@/components/ui/badge'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog'
import { toast } from '@/lib/toast'
import { useQueryClient } from '@tanstack/react-query'
import { 
  Search, 
  MapPin, 
  Star, 
  ChevronDown, 
  ChevronUp,
  Image as ImageIcon,
  Loader2,
  X,
  Check,
  Pencil,
  Trash2
} from 'lucide-react'
import 'leaflet/dist/leaflet.css'
import { designSystem } from '@/lib/design-system'
import { CommentForm } from '@/features/comments/components/comment-form'
import { commentsApi } from '@/features/comments/api/comments-api'
import { DeleteDialog } from '@/components/shared/delete-dialog'

const OSTROH_CENTER: [number, number] = [50.3292, 26.5143]

const MAP_BOUNDS: LatLngBoundsExpression = [
  [50.2392, 26.4243],
  [50.4192, 26.6043],
]

const redMarkerIcon = new Icon({
  iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
  shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41],
})

const blueMarkerIcon = new Icon({
  iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-blue.png',
  shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41],
})

import { type CreateComment } from '@/types'

interface CommentsBlockProps {
  comments: Comment[]
  problemId: string | null
  onViewAllComments: () => void
  onCommentUpdate: () => void
}

function CommentsBlock({ comments, problemId, onViewAllComments, onCommentUpdate }: CommentsBlockProps) {
  const { user } = useAuth()
  const realtimeComments = useRealtimeComments(problemId, comments)
  const displayedComments = realtimeComments.slice(0, 3)
  
  const [editingComment, setEditingComment] = useState<Comment | null>(null)
  const [isEditOpen, setIsEditOpen] = useState(false)
  const [commentToDelete, setCommentToDelete] = useState<string | null>(null)
  const [isCreateOpen, setIsCreateOpen] = useState(false)

  const handleCreateComment = async (data: CreateComment) => {
    try {
      await commentsApi.create(data)
      toast.success('Коментар створено')
      setIsCreateOpen(false)
      onCommentUpdate()
    } catch {
      toast.error('Не вдалося створити коментар')
    }
  }

  const handleEditComment = async (data: CreateComment, id?: string) => {
    if (!id) return
    try {
      await commentsApi.update(id, { content: data.content, problemId: data.problemId })
      toast.success('Коментар оновлено')
      setIsEditOpen(false)
      setEditingComment(null)
      onCommentUpdate()
    } catch {
      toast.error('Не вдалося оновити коментар')
    }
  }

  const handleDeleteComment = async () => {
    if (!commentToDelete) return
    try {
      await commentsApi.delete(commentToDelete)
      toast.success('Коментар видалено')
      setCommentToDelete(null)
      onCommentUpdate()
    } catch {
      toast.error('Не вдалося видалити коментар')
    }
  }

  return (
    <div className="flex flex-col">
      <h2 className="text-xl font-bold text-[#1F2732] font-['Mulish'] mb-4">
        Коментарі користувачів
      </h2>
      
      <Card className="bg-white border border-gray-200 rounded-lg flex flex-col h-fit">
        <CardContent className="p-5">
          {realtimeComments.length === 0 ? (
            <p className="text-gray-500 text-sm">Коментарів поки немає</p>
          ) : (
            <div className="flex flex-col">
              {displayedComments.map((comment, index) => {
                const canEdit = user?.id && comment.user?.id && user.id === comment.user.id

                return (
                  <div key={comment.id}>
                    <div className="py-3 px-4 md:px-5">
                      <div className="flex items-center justify-between gap-6 mb-5">
                        <span className="text-sm font-bold text-[#292929]">
                          {comment?.user?.name} {comment?.user?.surname}
                        </span>
                        <div className="flex items-center gap-2">
                          <span className="text-xs font-bold text-[#292929]">
                            {new Date(comment.createdAt).toLocaleTimeString('uk-UA', { hour: '2-digit', minute: '2-digit' })}
                          </span>
                          {canEdit && (
                            <div className="flex items-center gap-1 ml-2">
                              <button 
                                onClick={() => {
                                  setEditingComment(comment)
                                  setIsEditOpen(true)
                                }}
                                className="p-1 hover:bg-gray-100 rounded-full transition-colors"
                                title="Редагувати"
                              >
                                <Pencil className="w-3.5 h-3.5 text-gray-500 hover:text-blue-600" />
                              </button>
                              <button 
                                onClick={() => setCommentToDelete(comment.id)}
                                className="p-1 hover:bg-gray-100 rounded-full transition-colors"
                                title="Видалити"
                              >
                                <Trash2 className="w-3.5 h-3.5 text-gray-500 hover:text-red-600" />
                              </button>
                            </div>
                          )}
                        </div>
                      </div>
                      <p className="text-sm text-gray-700 text-left break-words">{comment.content}</p>
                    </div>
                    {index < displayedComments.length - 1 && (
                      <hr className="border-gray-200 border-b-[1px] mx-4 md:mx-5 my-2" />
                    )}
                  </div>
                )
              })}
              
              {realtimeComments.length > 0 && (
                <>
                  <hr className="border-gray-200 border-b-[1px] mt-3 mx-4 md:mx-5" />
                  <button
                    onClick={onViewAllComments}
                    className="mt-4 text-sm font-semibold text-[#165D9D] hover:underline text-center w-full bg-transparent p-0 focus:outline-none focus-visible:outline-none focus-visible:ring-0 focus-visible:ring-offset-0 active:outline-none border-0"
                    style={{ boxShadow: 'none', borderColor: 'transparent' }}
                  >
                    Дивитися усі коментарі до проблеми
                  </button>
                </>
              )}
            </div>
          )}
        </CardContent>
      </Card>
      
      <Button 
        onClick={() => setIsCreateOpen(true)}
        className="mt-4 bg-[#E42556] hover:bg-[#E42556]/90 text-white font-bold rounded-md self-center px-8 py-2 w-auto min-h-0 h-auto focus-visible:ring-0 focus-visible:ring-offset-0 focus:ring-0 border-0 outline-none"
        style={{ boxShadow: 'none', borderColor: 'transparent' }}
      >
        Написати коментар
      </Button>

      <CommentForm
        open={isCreateOpen}
        onOpenChange={setIsCreateOpen}
        onSubmit={handleCreateComment}
        initialData={null}
      />

      <CommentForm
        open={isEditOpen}
        onOpenChange={setIsEditOpen}
        onSubmit={handleEditComment}
        initialData={editingComment}
      />

      <DeleteDialog
        open={!!commentToDelete}
        onOpenChange={(open) => !open && setCommentToDelete(null)}
        onConfirm={handleDeleteComment}
        title="Видалити коментар"
        description="Ви впевнені, що хочете видалити цей коментар? Цю дію неможливо скасувати."
      />
    </div>
  )
}

interface AllCommentsModalProps {
  open: boolean
  onOpenChange: (open: boolean) => void
  comments: Comment[]
  problemId: string | null
  problemTitle: string
}

function AllCommentsModal({ open, onOpenChange, comments, problemId, problemTitle }: AllCommentsModalProps) {
  const realtimeComments = useRealtimeComments(problemId, comments)

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-3xl max-h-[80vh] overflow-hidden flex flex-col">
        <DialogHeader>
          <DialogTitle className="text-xl font-bold text-[#1F2732] font-['Mulish']">
            Усі коментарі до проблеми
          </DialogTitle>
          <p className="text-sm text-gray-600 mt-2">{problemTitle}</p>
        </DialogHeader>
        
        <div className="flex-1 overflow-y-auto pr-2">
          {realtimeComments.length === 0 ? (
            <p className="text-gray-500 text-sm text-center py-8">Коментарів поки немає</p>
          ) : (
            <div className="space-y-4 px-2 md:px-3">
              {realtimeComments.map((comment, index) => (
                <div key={comment.id}>
                  <div className="py-4 px-3 md:px-4">
                    <div className="flex items-center justify-between gap-6 mb-3">
                      <span className="text-sm font-bold text-gray-900">
                        {comment?.user?.name} {comment?.user?.surname}
                      </span>
                      <span className="text-xs text-gray-500">
                        {new Date(comment.createdAt).toLocaleDateString('uk-UA', { 
                          day: '2-digit', 
                          month: '2-digit', 
                          year: 'numeric',
                          hour: '2-digit', 
                          minute: '2-digit' 
                        })}
                      </span>
                    </div>
                    <p className="text-sm text-gray-700 text-left whitespace-pre-wrap">{comment.content}</p>
                  </div>
                  {index < realtimeComments.length - 1 && (
                    <hr className="border-gray-200 mx-3 md:mx-4" />
                  )}
                </div>
              ))}
            </div>
          )}
        </div>
      </DialogContent>
    </Dialog>
  )
}

interface DescriptionBlockProps {
  problem: Problem
  onUpdate: () => void
}

function DescriptionBlock({ problem, onUpdate }: DescriptionBlockProps) {
  const [expanded, setExpanded] = useState(false)
  const [isEditing, setIsEditing] = useState(false)
  const [editDescription, setEditDescription] = useState(problem.description)
  const [isSaving, setIsSaving] = useState(false)
  const isLongDescription = problem.description.length > 200

  const handleSave = async () => {
    if (!problem.id || !editDescription.trim()) return
    try {
      setIsSaving(true)
      await problemsApi.updateDescription(problem.id, editDescription)
      toast.success('Опис оновлено')
      setIsEditing(false)
      onUpdate()
    } catch {
      toast.error('Не вдалося оновити опис')
    } finally {
      setIsSaving(false)
    }
  }

  const handleCancel = () => {
    setEditDescription(problem.description)
    setIsEditing(false)
  }

  return (
    <Card className="bg-white border border-gray-200 rounded-[10px]">
      <CardHeader className="pb-4 flex flex-row items-center justify-between">
        <CardTitle className="text-lg font-bold text-[#1F2732] font-['Mulish'] flex items-center gap-2">
          Опис
          {!isEditing && (
            <button 
              type="button"
              className="p-0 !bg-transparent border-none shadow-none transition-opacity opacity-100 hover:opacity-80 outline-none focus:outline-none focus:ring-0"
              onClick={() => setIsEditing(true)}
            >
              <img src="/icons/pen.png" alt="Edit" className="w-3.5 h-3.5 cursor-pointer transform -translate-y-[6px]" />
            </button>
          )}
        </CardTitle>
      </CardHeader>
      <CardContent>
        {isEditing ? (
          <div className="space-y-3">
            <Textarea
              value={editDescription}
              onChange={(e) => setEditDescription(e.target.value)}
              className="min-h-[100px] bg-[#F0F1F2] border-none rounded-lg"
              placeholder="Введіть опис проблеми..."
            />
            <div className="flex gap-2 justify-end">
              <Button
                variant="outline"
                size="sm"
                onClick={handleCancel}
                disabled={isSaving}
              >
                <X className="w-4 h-4 mr-1" />
                Скасувати
              </Button>
              <Button
                size="sm"
                onClick={handleSave}
                disabled={isSaving || !editDescription.trim()}
                className="bg-[#E42556] hover:bg-[#E42556]/90"
              >
                <Check className="w-4 h-4 mr-1" />
                {isSaving ? 'Збереження...' : 'Зберегти'}
              </Button>
            </div>
          </div>
        ) : (
          <>
            <p className={`text-gray-600 whitespace-pre-wrap break-words ${!expanded && isLongDescription ? 'line-clamp-3' : ''}`}>
              {problem.description}
            </p>
            
            {isLongDescription && (
              <button
                onClick={() => setExpanded(!expanded)}
                className="mt-2 flex items-center gap-1 text-sm font-semibold text-[#165D9D] hover:underline bg-transparent p-0 border-none shadow-none focus:outline-none focus-visible:outline-none focus-visible:ring-0"
                style={{ backgroundColor: 'transparent' }}
              >
                {expanded ? (
                  <>
                    Згорнути <ChevronUp className="w-4 h-4" />
                  </>
                ) : (
                  <>
                    Читати повністю <ChevronDown className="w-4 h-4" />
                  </>
                )}
              </button>
            )}
          </>
        )}
      </CardContent>
    </Card>
  )
}

interface ProblemDetailsCardProps {
  problem: Problem
  onUpdate: () => void
  onViewAllComments: () => void
}

function ProblemDetailsCard({ problem, onUpdate }: ProblemDetailsCardProps) {
  const [showImages, setShowImages] = useState(false)
  const [showCoordinatorImages, setShowCoordinatorImages] = useState(false)
  const [isEditing, setIsEditing] = useState(false)
  const [editTitle, setEditTitle] = useState(problem.title)
  const [editCategories, setEditCategories] = useState<string[]>(problem.categories || [])
  const [isSaving, setIsSaving] = useState(false)

  const hasUserImages = problem.images && problem.images.length > 0
  const hasCoordinatorImages = problem.coordinatorImages && problem.coordinatorImages.length > 0

  const handleSave = async () => {
    if (!problem.id || !editTitle.trim()) return
    try {
      setIsSaving(true)
      await problemsApi.updateTitleAndCategories(problem.id, editTitle, editCategories.length > 0 ? editCategories : undefined)
      toast.success('Дані оновлено')
      setIsEditing(false)
      onUpdate()
    } catch {
      toast.error('Не вдалося оновити дані')
    } finally {
      setIsSaving(false)
    }
  }

  const handleCancel = () => {
    setEditTitle(problem.title)
    setEditCategories(problem.categories || [])
    setIsEditing(false)
  }

  const toggleCategory = (category: string) => {
    setEditCategories(prev => 
      prev.includes(category) 
        ? prev.filter(c => c !== category)
        : [...prev, category]
    )
  }

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

  const getPriorityColor = (priority: string) => {
    switch (priority) {
      case PriorityConstants.Critical:
        return 'text-red-600'
      case PriorityConstants.High:
        return 'text-orange-600'
      case PriorityConstants.Medium:
        return 'text-yellow-600'
      case PriorityConstants.Low:
        return 'text-green-600'
      default:
        return 'text-gray-600'
    }
  }

  return (
    <Card className="bg-white border border-gray-200 rounded-[10px]">
      <CardHeader className="pb-4">
        <div className="flex items-start justify-between">
          <div className="flex-1">
            {isEditing ? (
              <div className="space-y-3">
                <div>
                  <label className="text-sm text-gray-500 mb-1 block">Назва</label>
                  <Input
                    value={editTitle}
                    onChange={(e) => setEditTitle(e.target.value)}
                    className="bg-[#F0F1F2] border-none rounded-lg"
                    placeholder="Введіть назву..."
                  />
                </div>
                <div>
                  <label className="text-sm text-gray-500 mb-2 block">Категорії</label>
                  <div className="flex flex-wrap gap-2">
                    {Object.values(CategoryConstants).map((cat) => (
                      <button
                        key={cat}
                        type="button"
                        onClick={() => toggleCategory(cat)}
                        className={`px-3 py-1 text-xs rounded-full border transition-colors ${
                          editCategories.includes(cat)
                            ? 'bg-[#E42556] text-white border-[#E42556]'
                            : 'bg-white text-gray-600 border-gray-300 hover:border-[#E42556]'
                        }`}
                      >
                        {cat}
                      </button>
                    ))}
                  </div>
                </div>
                <div className="flex gap-2 justify-end">
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={handleCancel}
                    disabled={isSaving}
                  >
                    <X className="w-4 h-4 mr-1" />
                    Скасувати
                  </Button>
                  <Button
                    size="sm"
                    onClick={handleSave}
                    disabled={isSaving || !editTitle.trim()}
                    className="bg-[#E42556] hover:bg-[#E42556]/90"
                  >
                    <Check className="w-4 h-4 mr-1" />
                    {isSaving ? 'Збереження...' : 'Зберегти'}
                  </Button>
                </div>
              </div>
            ) : (
              <>
                <CardTitle className="text-lg font-bold text-[#1F2732] font-['Mulish'] flex items-center gap-2">
                  {problem.title}
                  <button 
                    type="button"
                    className="p-0 !bg-transparent border-none shadow-none transition-opacity opacity-100 hover:opacity-80 outline-none focus:outline-none focus:ring-0"
                    onClick={() => setIsEditing(true)}
                  >
                    <img src="/icons/pen.png" alt="Edit" className="w-3.5 h-3.5 cursor-pointer transform -translate-y-[6px]" />
                  </button>
                </CardTitle>
                
              </>
            )}
          </div>
          {!isEditing && (
            <Badge className={getStatusColor(problem.status)}>
              {problem.status}
            </Badge>
          )}
        </div>
        
        {!isEditing && (
          <div className="flex items-center gap-1 mt-3">
            {[1, 2, 3, 4, 5].map((star) => (
              <Star
                key={star}
                className={`w-5 h-5 ${star <= 4 ? 'text-yellow-400 fill-yellow-400' : 'text-gray-300'}`}
              />
            ))}
          </div>
        )}
      </CardHeader>
      <CardContent>
        <div className="space-y-3 text-sm">
          <div className="flex justify-between">
            <span className="text-gray-500">Дата створення:</span>
            <span className="font-medium">
              {new Date(problem.createdAt).toLocaleDateString('uk-UA')}
            </span>
          </div>
          <div className="flex justify-between">
            <span className="text-gray-500">Останнє оновлення:</span>
            <span className="font-medium">
              {new Date(problem.updatedAt).toLocaleDateString('uk-UA')}
            </span>
          </div>
          <div className="flex justify-between">
            <span className="text-gray-500">Пріоритетність:</span>
            <span className={`font-medium ${getPriorityColor(problem.priority)}`}>
              {problem.priority}
            </span>
          </div>
          {problem.categories && problem.categories.length > 0 && (
            <div className="flex justify-between">
              <span className="text-gray-500">Категорія:</span>
              <span className="font-medium">{problem.categories.join(', ')}</span>
            </div>
          )}
          {problem.coordinator && (
            <div className="flex justify-between">
              <span className="text-gray-500">Координатор:</span>
              <span className="font-medium">
                {problem.coordinator.name} {problem.coordinator.surname}
              </span>
            </div>
          )}
          {problem.currentState && (
            <div className="mt-4 p-3 bg-blue-50 rounded-md">
              <p className="text-sm font-medium text-blue-800">Поточний стан:</p>
              <p className="text-sm text-blue-700">{problem.currentState}</p>
            </div>
          )}
          {problem.rejectionReason && (
            <div className="mt-4 p-3 bg-red-50 rounded-md">
              <p className="text-sm font-medium text-red-800">Причина відхилення:</p>
              <p className="text-sm text-red-700">{problem.rejectionReason}</p>
            </div>
          )}
        </div>

        <div className="mt-6 space-y-2">
          <div>
            <button
              type="button"
              onClick={() => hasUserImages && setShowImages(!showImages)}
              disabled={!hasUserImages}
              className={`group flex items-center gap-2 text-sm transition-colors focus-visible:outline-none ${
                hasUserImages ? 'hover:text-[#1F2732]' : ''
              }`}
              style={{
                color: hasUserImages
                  ? designSystem.colors.profile.links.text
                  : designSystem.colors.profile.links.disabled,
                backgroundColor: 'transparent',
                border: 'none',
                padding: 0,
                cursor: hasUserImages ? 'pointer' : 'not-allowed',
              }}
              title={!hasUserImages ? 'Немає поданих зображень' : undefined}
            >
              <ImageIcon className="w-4 h-4" />
              <span className="group-hover:underline" style={{ color: 'inherit' }}>
                Відкрити подані зображення {hasUserImages ? `(${problem.images!.length})` : '(0)'}
              </span>
              {hasUserImages && (showImages ? <ChevronUp className="w-4 h-4" /> : <ChevronDown className="w-4 h-4" />)}
            </button>
            {showImages && hasUserImages && (
              <div className="mt-3 grid grid-cols-2 gap-2">
                {problem.images!.map((image) => (
                  <img
                    key={image.id}
                    src={image.url}
                    alt="Зображення проблеми"
                    className="w-full h-32 object-cover rounded-lg cursor-pointer hover:opacity-90"
                    onClick={() => window.open(image.url, '_blank')}
                  />
                ))}
              </div>
            )}
          </div>
          
          <div>
            <button
              type="button"
              onClick={() => hasCoordinatorImages && setShowCoordinatorImages(!showCoordinatorImages)}
              disabled={!hasCoordinatorImages}
              className={`group flex items-center gap-2 text-sm transition-colors focus-visible:outline-none ${
                hasCoordinatorImages ? 'hover:text-[#1F2732]' : ''
              }`}
              style={{
                color: hasCoordinatorImages
                  ? designSystem.colors.profile.links.text
                  : designSystem.colors.profile.links.disabled,
                backgroundColor: 'transparent',
                border: 'none',
                padding: 0,
                cursor: hasCoordinatorImages ? 'pointer' : 'not-allowed',
              }}
              title={!hasCoordinatorImages ? 'Немає зображень від координатора' : undefined}
            >
              <ImageIcon className="w-4 h-4" />
              <span className="group-hover:underline" style={{ color: 'inherit' }}>
                Відкрити зображення від координатора {hasCoordinatorImages ? `(${problem.coordinatorImages!.length})` : '(0)'}
              </span>
              {hasCoordinatorImages && (showCoordinatorImages ? <ChevronUp className="w-4 h-4" /> : <ChevronDown className="w-4 h-4" />)}
            </button>
            {showCoordinatorImages && hasCoordinatorImages && (
              <div className="mt-3 grid grid-cols-2 gap-2">
                {problem.coordinatorImages!.map((image) => (
                  <img
                    key={image.id}
                    src={image.url}
                    alt="Зображення від координатора"
                    className="w-full h-32 object-cover rounded-lg cursor-pointer hover:opacity-90"
                    onClick={() => window.open(image.url, '_blank')}
                  />
                ))}
              </div>
            )}
          </div>
        </div>
      </CardContent>
    </Card>
  )
}

function MapRefUpdater({ onReady }: { onReady: (map: LeafletMap) => void }) {
  const map = useMap()
  
  useEffect(() => {
    onReady(map)
  }, [map, onReady])

  return null
}

function EmptyProblemState() {
  return (
    <div className="flex flex-col items-center justify-center h-full text-center p-8">
      <div className="w-16 h-16 bg-gray-100 rounded-full flex items-center justify-center mb-4">
        <MapPin className="w-8 h-8 text-gray-400" />
      </div>
      <h3 className="text-lg font-semibold text-gray-700 mb-2">Виберіть проблему</h3>
      <p className="text-sm text-gray-500">
        Натисніть на маркер на карті, щоб переглянути деталі проблеми
      </p>
    </div>
  )
}

export function UserProblemsTab() {
  const { user } = useAuth()
  const queryClient = useQueryClient()
  const mapRef = useRef<LeafletMap | null>(null)
  const [selectedProblem, setSelectedProblem] = useState<Problem | null>(null)
  const [showDetails, setShowDetails] = useState(false)
  const [showAllCommentsModal, setShowAllCommentsModal] = useState(false)
  const [statusFilter, setStatusFilter] = useState<string>('all')
  const [searchTerm, setSearchTerm] = useState('')
  const [categoryFilter, setCategoryFilter] = useState<string>('')
  const [priorityFilter, setPriorityFilter] = useState<string>('')
  const [dateFilter, setDateFilter] = useState<string>('')

  const filter: UserProblemsFilter = useMemo(() => ({
    searchTerm: searchTerm || undefined,
    status: statusFilter !== 'all' ? statusFilter : undefined,
    category: categoryFilter || undefined,
    priority: priorityFilter || undefined,
    sortBy: 'createdAt',
    sortDescending: true,
    dateFilter: dateFilter || undefined,
  }), [searchTerm, statusFilter, categoryFilter, priorityFilter, dateFilter])

  const { data: problems, isLoading, refetch } = useProblemsByUserFiltered(user?.id || '', filter)

  const handleProblemUpdate = async () => {
    await refetch()
    queryClient.invalidateQueries({ queryKey: ['problems'] })
    if (selectedProblem?.id) {
      const updatedProblems = await problemsApi.getByUserFiltered(user?.id || '', filter)
      const updated = updatedProblems.find(p => p.id === selectedProblem.id)
      if (updated) {
        setSelectedProblem(updated)
      }
    }
  }

  const handleViewAllComments = () => {
    setShowAllCommentsModal(true)
  }

  const statusTabs = [
    { key: 'all', label: 'Всі' },
    { key: ProblemStatusConstants.New, label: 'Нові' },
    { key: ProblemStatusConstants.InProgress, label: 'У процесі' },
    { key: ProblemStatusConstants.Completed, label: 'Вирішені' },
    { key: ProblemStatusConstants.Rejected, label: 'Відхилені' },
  ]

  const dateFilterOptions = [
    { value: '', label: 'Весь час' },
    { value: 'week', label: 'Цього тижня' },
    { value: 'month', label: 'Цього місяця' },
    { value: 'year', label: 'Цього року' },
  ] as const

  const statusTabColors = designSystem.colors.profile.tabs
type StatusTabStyle = CSSProperties & { '--tab-hover-color'?: string }
  const getStatusTabStyle = (isActive: boolean): StatusTabStyle => ({
    color: isActive ? statusTabColors.text : statusTabColors.inactiveText,
    backgroundColor: 'transparent',
    border: 'none',
    borderRadius: 0,
    '--tab-hover-color': statusTabColors.hoverText,
  })

  const handleMarkerClick = (problem: Problem, marker?: LeafletMarker) => {
    if (mapRef.current) {
      mapRef.current.closePopup()
    }
    marker?.openPopup()
    setSelectedProblem(problem)
  }

  const handleDetailsClick = (problem?: Problem) => {
    if (problem) {
      setSelectedProblem(problem)
    }
    setShowDetails(true)
    mapRef.current?.closePopup()
  }

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <Loader2 className="h-8 w-8 animate-spin text-[#E42556]" />
      </div>
    )
  }

  return (
    <div className="max-w-7xl mx-auto px-6 py-8">
      <div className="border-b border-gray-200 mb-6 w-fit">
        <div className="flex gap-2">
          {statusTabs.map((tab) => (
            <button
              key={tab.key}
              onClick={() => setStatusFilter(tab.key)}
              className="px-4 py-3 text-sm font-medium font-['Mulish'] transition-colors relative pb-4 outline-none focus-visible:outline-none focus-visible:ring-0 hover:text-[var(--tab-hover-color)]"
              style={getStatusTabStyle(statusFilter === tab.key)}
            >
              {tab.label}
              {statusFilter === tab.key && (
                <div
                  className="absolute bottom-0 left-0 right-0 h-0.5"
                  style={{ backgroundColor: statusTabColors.indicator }}
                />
              )}
            </button>
          ))}
        </div>
      </div>

      <div className="flex flex-wrap gap-4 mb-6">
        <div className="relative flex-1 min-w-[200px]">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-[#464646]" />
          <Input
            placeholder="Шукати за назвою..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            className="pl-10 bg-white border border-[#464646] rounded-[10px] text-[#464646] focus-visible:ring-0 focus-visible:border-[2px]"
          />
        </div>
        
        <Select value={categoryFilter} onValueChange={setCategoryFilter}>
          <SelectTrigger className="w-[180px] bg-white border border-[#464646] rounded-[10px] text-[#464646] focus:ring-0 focus:ring-offset-0 focus:border-[2px]">
            <SelectValue placeholder="Категорія" />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="">Всі категорії</SelectItem>
            {Object.values(CategoryConstants).map((cat) => (
              <SelectItem key={cat} value={cat}>{cat}</SelectItem>
            ))}
          </SelectContent>
        </Select>

        <Select value={priorityFilter} onValueChange={setPriorityFilter}>
          <SelectTrigger className="w-[180px] bg-white border border-[#464646] rounded-[10px] text-[#464646] focus:ring-0 focus:ring-offset-0 focus:border-[2px]">
            <SelectValue placeholder="Пріоритетність" />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="">Всі пріоритети</SelectItem>
            {Object.values(PriorityConstants).map((priority) => (
              <SelectItem key={priority} value={priority}>{priority}</SelectItem>
            ))}
          </SelectContent>
        </Select>

        <Select value={dateFilter} onValueChange={setDateFilter}>
          <SelectTrigger className="w-[180px] bg-white border border-[#464646] rounded-[10px] text-[#464646] focus:ring-0 focus:ring-offset-0 focus:border-[2px]">
            <SelectValue
              placeholder="Створено"
              value={dateFilter ? dateFilterOptions.find((option) => option.value === dateFilter)?.label : undefined}
            />
          </SelectTrigger>
          <SelectContent>
            {dateFilterOptions.map((option) => (
              <SelectItem key={option.value} value={option.value}>
                {option.label}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      </div>

      {(!problems || problems.length === 0) ? (
        <Card className="bg-white border border-gray-200 rounded-[10px] h-[500px] flex items-center justify-center">
          <div className="text-center p-8">
            <p className="text-gray-500 text-lg">
              {statusFilter === 'all' 
                ? 'У вас ще немає звернень'
                : 'Немає звернень з таким статусом'}
            </p>
          </div>
        </Card>
      ) : (
        <div className="space-y-6">
          {/* Верхній рядок: Карта зліва + Коментарі справа */}
          <div className="grid grid-cols-1 lg:grid-cols-5 gap-6">
            {/* Карта - займає ~60% ширини (3 з 5 колонок) */}
            <div className="lg:col-span-3 h-[500px] rounded-lg overflow-hidden shadow-lg">
              <MapContainer
                center={OSTROH_CENTER}
                zoom={14}
                style={{ height: '100%', width: '100%' }}
                className="z-0"
                maxBounds={MAP_BOUNDS}
                maxBoundsViscosity={1.0}
                minZoom={12}
              >
                <MapRefUpdater onReady={(map) => (mapRef.current = map)} />
                <TileLayer
                  attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                  url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />

                {problems && problems.length > 0 && problems.map((problem) => (
                  <Marker
                    key={problem.id}
                    position={[problem.latitude, problem.longitude]}
                    icon={selectedProblem?.id === problem.id ? redMarkerIcon : blueMarkerIcon}
                    eventHandlers={{
                      click: (event) => handleMarkerClick(problem, event.target as LeafletMarker),
                    }}
                  >
                    <Popup>
                      <div className="min-w-[200px]">
                        <h3 className="font-semibold text-gray-900 mb-1">{problem.title}</h3>
                        <p className="text-sm text-gray-600 mb-2 line-clamp-2 overflow-hidden text-ellipsis break-words">
                          {problem.description}
                        </p>
                        <button
                          type="button"
                          onClick={() => handleDetailsClick(problem)}
                          className="mt-2 w-1/2 min-w-[100px] rounded-md border border-[#1E40AF] text-[#1E40AF] px-3 py-1.5 text-sm font-semibold bg-transparent hover:bg-[#1E40AF] hover:text-white transition-colors focus:outline-none focus-visible:outline-none focus-visible:ring-0 focus-visible:ring-offset-0 mx-auto block"
                        >
                          Детальніше
                        </button>
                      </div>
                    </Popup>
                  </Marker>
                ))}
              </MapContainer>
            </div>

            {/* Коментарі - займає ~40% ширини (2 з 5 колонок) */}
            <div className="lg:col-span-2 h-[500px]">
              {selectedProblem && showDetails ? (
                <CommentsBlock 
                  comments={selectedProblem.comments || []} 
                  problemId={selectedProblem.id}
                  onViewAllComments={handleViewAllComments}
                  onCommentUpdate={handleProblemUpdate}
                />
              ) : (
                <Card className="bg-white border border-gray-200 rounded-[10px] h-full">
                  <EmptyProblemState />
                </Card>
              )}
            </div>
          </div>

          {/* Нижній рядок: Опис зліва + Деталі справа */}
          {selectedProblem && showDetails && (
            <div className="grid grid-cols-1 lg:grid-cols-5 gap-6">
              {/* Опис - під картою (3 з 5 колонок) */}
              <div className="lg:col-span-2">
                <DescriptionBlock problem={selectedProblem} onUpdate={handleProblemUpdate} />
              </div>
              
              {/* Деталі проблеми - займає більше місця справа (3 з 5 колонок) */}
              <div className="lg:col-span-3">
                <ProblemDetailsCard 
                  problem={selectedProblem} 
                  onUpdate={handleProblemUpdate}
                  onViewAllComments={handleViewAllComments}
                />
              </div>
            </div>
          )}
        </div>
      )}

      {selectedProblem && (
        <AllCommentsModal
          open={showAllCommentsModal}
          onOpenChange={setShowAllCommentsModal}
          comments={selectedProblem.comments || []}
          problemId={selectedProblem.id}
          problemTitle={selectedProblem.title}
        />
      )}
    </div>
  )
}
