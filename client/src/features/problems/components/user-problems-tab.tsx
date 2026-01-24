import { useState, useMemo, useRef, useEffect, useCallback, memo, type CSSProperties } from 'react'
import { MapContainer, TileLayer, Marker, Popup, useMap } from 'react-leaflet'
import { Icon } from 'leaflet'
import type { LatLngBoundsExpression, Map as LeafletMap } from 'leaflet'
import { useAuth } from '@/contexts/auth-context'
import { useProblemsByUserFiltered, type UserProblemsFilter } from '../hooks/use-problems'
import { useRealtimeComments } from '@/hooks/use-realtime-comments'
import { problemsApi } from '../api/problems-api'
import { ratingsApi } from '@/features/ratings/api/ratings-api'
import type { Problem, Comment } from '@/types'
import { ProblemStatusConstants, PriorityConstants, CategoryConstants } from '@/types'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Badge } from '@/components/ui/badge'
import { Card, CardContent } from '@/components/ui/card'
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from '@/components/ui/dialog'
import { toast } from '@/lib/toast'
import { useQueryClient } from '@tanstack/react-query'
import { 
  Search, 
  MapPin, 
  Star, 
  ChevronDown, 
  ChevronUp,
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
import { ImageLightbox } from '@/components/shared/image-lightbox'

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

const CommentsBlock = memo(function CommentsBlock({ comments, problemId, onViewAllComments, onCommentUpdate }: CommentsBlockProps) {
  const { user } = useAuth()
  const realtimeComments = useRealtimeComments(problemId, comments)
  const sortedComments = useMemo(() => {
    return [...realtimeComments].sort((a, b) => 
      new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
    )
  }, [realtimeComments])
  const displayedComments = sortedComments.slice(0, 3)
  
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
        Останні коментарі
      </h2>
      
      <Card className="bg-white border border-gray-200 rounded-lg flex flex-col h-fit">
        <CardContent className="p-5">
          {sortedComments.length === 0 ? (
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
                            <div className="flex items-center gap-2 ml-2">
                              <button 
                                type="button"
                                onClick={() => {
                                  setEditingComment(comment)
                                  setIsEditOpen(true)
                                }}
                                className="p-0 bg-transparent border-none shadow-none transition-opacity hover:opacity-80 outline-none focus:outline-none focus:ring-0"
                                title="Редагувати"
                              >
                                <Pencil className="w-3.5 h-3.5 text-gray-500 hover:text-blue-600" />
                              </button>
                              <button 
                                type="button"
                                onClick={() => setCommentToDelete(comment.id)}
                                className="p-0 bg-transparent border-none shadow-none transition-opacity hover:opacity-80 outline-none focus:outline-none focus:ring-0"
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
              
              {sortedComments.length > 0 && (
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
        problemId={problemId}
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
})

interface AllCommentsModalProps {
  open: boolean
  onOpenChange: (open: boolean) => void
  comments: Comment[]
  problemId: string | null
  problemTitle: string
}

const AllCommentsModal = memo(function AllCommentsModal({ open, onOpenChange, comments, problemId, problemTitle }: AllCommentsModalProps) {
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
})

interface DescriptionBlockProps {
  problem: Problem
  onUpdate: () => void
}

const DescriptionBlock = memo(function DescriptionBlock({ problem, onUpdate }: DescriptionBlockProps) {
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
    <div className="flex flex-col h-full">
      <h2 className="text-xl font-bold text-[#1F2732] font-['Mulish'] mb-4 flex items-center gap-2">
        Опис
        {!isEditing && (
          <button 
            type="button"
            className="p-0 !bg-transparent border-none shadow-none transition-opacity opacity-100 hover:opacity-80 outline-none focus:outline-none focus:ring-0"
            onClick={() => setIsEditing(true)}
          >
            <img src="/icons/pen.png" alt="Edit" className="w-3.5 h-3.5 cursor-pointer" />
          </button>
        )}
      </h2>
      <Card className="bg-white border border-gray-200 rounded-[10px] mt-0">
        <CardContent className="p-5 md:p-6">
          {isEditing ? (
            <div className="space-y-3">
              <Textarea
                value={editDescription}
                onChange={(e) => setEditDescription(e.target.value)}
                className="min-h-[120px] bg-[#F0F1F2] border-none rounded-lg"
                placeholder="Введіть опис проблеми..."
              />
              <div className="flex gap-2 justify-end">
                <Button
                  variant="outline"
                  size="sm"
                  onClick={handleCancel}
                  disabled={isSaving}
                  className="border border-[#D0D5DD] text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929]"
                >
                  <X className="w-4 h-4 mr-1 text-[#292929]" />
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
    </div>
  )
})

interface ProblemDetailsCardProps {
  problem: Problem
  onUpdate: () => void
  onViewAllComments: () => void
}

const ProblemDetailsCard = memo(function ProblemDetailsCard({ problem, onUpdate }: ProblemDetailsCardProps) {
  const { user } = useAuth()
  const [isEditing, setIsEditing] = useState(false)
  const [editTitle, setEditTitle] = useState(problem.title)
  const [editCategories, setEditCategories] = useState<string[]>(problem.categories || [])
  const [isSaving, setIsSaving] = useState(false)
  const [averageRating, setAverageRating] = useState<number>(0)
  const [userRating, setUserRating] = useState<number | null>(null)
  const [newRating, setNewRating] = useState<number>(5)
  const [isRatingModalOpen, setIsRatingModalOpen] = useState(false)
  const [isSubmittingRating, setIsSubmittingRating] = useState(false)
  const [showUserRating, setShowUserRating] = useState(false)
  const [lightboxOpen, setLightboxOpen] = useState(false)
  const [lightboxImages, setLightboxImages] = useState<{ id: string | null; url: string }[]>([])
  const [lightboxInitialIndex, setLightboxInitialIndex] = useState(0)

  const hasUserImages = problem.images && problem.images.length > 0
  const hasCoordinatorImages = problem.coordinatorImages && problem.coordinatorImages.length > 0
  const hasRejectionReason = Boolean(problem.rejectionReason && problem.rejectionReason.trim())
  const [address, setAddress] = useState<string>('Завантаження...')

  useEffect(() => {
    const fetchAddress = async () => {
      try {
        const response = await fetch(
          `https://nominatim.openstreetmap.org/reverse?format=json&lat=${problem.latitude}&lon=${problem.longitude}&zoom=18&addressdetails=1`
        )
        const data = await response.json()
        const addr = data.address || {}
        const streetName = addr.road || addr.pedestrian || addr.footway || 
                          addr.street || addr.suburb || addr.neighbourhood || 
                          addr.city || 'Невідома вулиця'
        setAddress(streetName)
      } catch {
        setAddress('Не вдалося визначити адресу')
      }
    }
    
    fetchAddress()
  }, [problem.latitude, problem.longitude])

  useEffect(() => {
    const fetchRatings = async () => {
      if (!problem.id) return
      try {
        const avgRating = await ratingsApi.getAverageByProblemId(problem.id)
        setAverageRating(avgRating)

        if (user) {
          const userRatingData = await ratingsApi.getUserRatingForProblem(problem.id)
          if (userRatingData) {
            setUserRating(userRatingData.points)
            setNewRating(userRatingData.points)
          } else {
            setUserRating(null)
            setNewRating(5)
          }
        } else {
          setUserRating(null)
          setNewRating(5)
        }
      } catch {
        setAverageRating(0)
        setUserRating(null)
        setNewRating(5)
      }
    }

    fetchRatings()
  }, [problem.id, user])

  const handleSubmitRating = async () => {
    if (!problem.id) return

    try {
      setIsSubmittingRating(true)
      await ratingsApi.create({ points: newRating, problemId: problem.id })
      toast.success('Оцінку додано')
      setIsRatingModalOpen(false)

      const avgRating = await ratingsApi.getAverageByProblemId(problem.id)
      setAverageRating(avgRating)

      if (user) {
        const userRatingData = await ratingsApi.getUserRatingForProblem(problem.id)
        if (userRatingData) {
          setUserRating(userRatingData.points)
          setNewRating(userRatingData.points)
        }
      }
    } catch {
      toast.error('Не вдалося додати оцінку')
    } finally {
      setIsSubmittingRating(false)
    }
  }

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

  const openUserImagesLightbox = () => {
    if (hasUserImages) {
      setLightboxImages(problem.images!)
      setLightboxInitialIndex(0)
      setLightboxOpen(true)
    }
  }

  const openCoordinatorImagesLightbox = () => {
    if (hasCoordinatorImages) {
      setLightboxImages(problem.coordinatorImages!)
      setLightboxInitialIndex(0)
      setLightboxOpen(true)
    }
  }

  const getStatusColor = (status: string) => {
    switch (status) {
      case ProblemStatusConstants.New:
        return 'bg-blue-100 text-blue-800 hover:bg-blue-100'
      case ProblemStatusConstants.InProgress:
        return 'bg-yellow-100 text-yellow-800 hover:bg-yellow-100'
      case ProblemStatusConstants.Completed:
        return 'bg-green-100 text-green-800 hover:bg-green-100'
      case ProblemStatusConstants.Rejected:
        return 'bg-red-100 text-red-800 hover:bg-red-100'
      default:
        return 'bg-gray-100 text-gray-800 hover:bg-gray-100'
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

  const renderStars = (rating: number) => {
    return [1, 2, 3, 4, 5].map((star) => {
      const fillPercentage = Math.max(0, Math.min(100, (rating - star + 1) * 100))

      return (
        <div key={star} className="relative w-5 h-5">
          <Star className="w-5 h-5 text-[#D2D2D2] absolute" />
          <div
            className="overflow-hidden absolute"
            style={{ width: `${fillPercentage}%` }}
          >
            <Star className="w-5 h-5 text-[#FFA900] fill-[#FFA900]" />
          </div>
        </div>
      )
    })
  }

  const hasUserRating = Boolean(user && userRating !== null)
  const displayRating = showUserRating ? (userRating ?? 0) : averageRating

  return (
    <div className="flex flex-col">
      <h2 className="text-xl font-bold text-[#1F2732] font-['Mulish'] mb-4 flex items-center gap-2">
        Характеристики
        {!isEditing && (
          <button 
            type="button"
            className="p-0 !bg-transparent border-none shadow-none transition-opacity opacity-100 hover:opacity-80 outline-none focus:outline-none focus:ring-0"
            onClick={() => setIsEditing(true)}
          >
            <img src="/icons/pen.png" alt="Edit" className="w-3.5 h-3.5 cursor-pointer" />
          </button>
        )}
      </h2>

      <Card className="bg-white border border-gray-200 rounded-lg">
        <CardContent className="p-5 md:p-6">
          {isEditing ? (
            <div className="space-y-4">
              <div>
                <label className="text-sm font-bold text-gray-700 mb-1 block">Назва</label>
                <Input
                  value={editTitle}
                  onChange={(e) => setEditTitle(e.target.value)}
                  className="bg-[#F0F1F2] border-none rounded-lg"
                  placeholder="Введіть назву..."
                />
              </div>
              <div>
                <label className="text-sm font-bold text-gray-700 mb-2 block">Категорії</label>
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
                  className="border border-[#D0D5DD] text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929]"
                >
                  <X className="w-4 h-4 mr-1 text-[#292929]" />
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
              <div className="flex items-start justify-between mb-4">
                <div className="flex-1">
                  <h3 className="text-lg font-bold text-[#1F2732] font-['Mulish'] mb-1">
                    {problem.title}
                  </h3>
                  <p className="text-sm text-gray-600">
                    <span className="font-bold">Адреса :</span> {address}
                  </p>
                </div>
                <div className="flex flex-col items-end">
                  {!showUserRating && averageRating === 0 && (
                    <span className="text-xs text-gray-500 mb-1">поки не оцінено</span>
                  )}
                  <div className="flex flex-col items-end group">
                    <div
                      className="flex items-center gap-0.5 cursor-pointer"
                      onClick={() => setIsRatingModalOpen(true)}
                    >
                      {renderStars(displayRating)}
                    </div>
                    <div className="mt-2 flex items-center justify-center gap-2">
                      <button
                        type="button"
                        onClick={() => setShowUserRating(!showUserRating)}
                        className="p-0 bg-transparent border-none shadow-none transition-opacity hover:opacity-80 outline-none focus:outline-none focus:ring-0"
                        title="Змінити відображення рейтингу"
                      >
                        <img
                          src="/icons/change.png"
                          alt="Change rating view"
                          className="w-5 h-5"
                        />
                      </button>
                      <span className="text-xs font-bold text-[#E42556]">
                        {showUserRating ? 'Ваший рейтинг' : 'Середній рейтинг'}
                      </span>
                    </div>
                  </div>
                </div>
              </div>

              <div className="grid grid-cols-2 gap-x-8 gap-y-3 text-sm mb-4 my-8">
                <div className="flex justify-between my-1">
                  <span className="font-bold text-gray-700">Дата створення</span>
                  <span className="text-gray-600">
                    {new Date(problem.createdAt).toLocaleDateString('uk-UA')}
                  </span>
                </div>
                <div className="flex justify-between my-1">
                  <span className="font-bold text-gray-700">Останнє оновлення</span>
                  <span className="text-gray-600">
                    {new Date(problem.updatedAt).toLocaleDateString('uk-UA')}
                  </span>
                </div>
                <div className="flex justify-between my-1">
                  <span className="font-bold text-gray-700">Пріоритетність</span>
                  <span className={`font-medium ${getPriorityColor(problem.priority)}`}>
                    {problem.priority}
                  </span>
                </div>
                <div className="flex justify-between items-start my-1">
                  <span className="font-bold text-gray-700">Категорія</span>
                  <div className="flex flex-col items-end">
                    {problem.categories && problem.categories.length > 0 
                      ? problem.categories.map((cat, index) => (
                          <span key={index} className="text-gray-600">
                            {cat}
                          </span>
                        ))
                      : <span className="text-gray-600">Не вказано</span>}
                  </div>
                </div>
                {hasRejectionReason ? (
                  <div className="col-span-2 my-1">
                    <div className="p-3 bg-red-50 rounded-lg">
                      <span className="font-bold text-red-800 block mb-1">
                        Причина відхилення :
                      </span>
                      <span className="text-red-700 font-semibold text-left">
                        {problem.rejectionReason}
                      </span>
                    </div>
                  </div>
                ) : problem.currentState ? (
                  <div className="col-span-2 my-1">
                    <div className="p-3 bg-sky-50 rounded-lg">
                      <span className="font-bold text-[#464646] block mb-1">
                        Поточний стан :
                      </span>
                      <span className="text-[#464646] font-semibold text-left">
                        {problem.currentState}
                      </span>
                    </div>
                  </div>
                ) : null}
              </div>

              <div className="flex items-end justify-between">
                <div className="flex flex-col gap-1 my-4">
                  <button
                    type="button"
                    onClick={openUserImagesLightbox}
                    disabled={!hasUserImages}
                    className="text-sm text-left focus:outline-none focus:ring-0 bg-transparent p-0 border-none shadow-none"
                    style={{
                      color: hasUserImages ? '#193CB8' : '#9CA3AF',
                      cursor: hasUserImages ? 'pointer' : 'not-allowed',
                    }}
                  >
                    Відкрити подані зображення ({problem.images?.length || 0})
                  </button>
                  <button
                    type="button"
                    onClick={openCoordinatorImagesLightbox}
                    disabled={!hasCoordinatorImages}
                    className="text-sm text-left focus:outline-none focus:ring-0 bg-transparent p-0 border-none shadow-none"
                    style={{
                      color: hasCoordinatorImages ? '#193CB8' : '#9CA3AF',
                      cursor: hasCoordinatorImages ? 'pointer' : 'not-allowed',
                    }}
                  >
                    Відкрити зображення від координатора ({problem.coordinatorImages?.length || 0})
                  </button>
                </div>
                <Badge className={`${getStatusColor(problem.status)} px-4 py-1 rounded-full`}>
                  {problem.status}
                </Badge>
              </div>
            </>
          )}
        </CardContent>
      </Card>

      <ImageLightbox
        images={lightboxImages}
        initialIndex={lightboxInitialIndex}
        open={lightboxOpen}
        onClose={() => setLightboxOpen(false)}
      />

      <Dialog open={isRatingModalOpen} onOpenChange={setIsRatingModalOpen}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>{hasUserRating ? 'Оновити оцінку' : 'Оцінити проблему'}</DialogTitle>
          </DialogHeader>
          <div className="space-y-4 py-4">
            <div className="flex justify-center pt-4">
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
            <p className="text-sm text-gray-500 text-center mb-2">
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
              {isSubmittingRating ? 'Збереження...' : hasUserRating ? 'Оновити оцінку' : 'Зберегти'}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  )
})

function MapRefUpdater({ onReady }: { onReady: (map: LeafletMap) => void }) {
  const map = useMap()
  
  useEffect(() => {
    onReady(map)
  }, [map, onReady])

  return null
}

const EmptyProblemState = memo(function EmptyProblemState() {
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
})

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

  const handleProblemUpdate = useCallback(async () => {
    await refetch()
    queryClient.invalidateQueries({ queryKey: ['problems'] })
    if (selectedProblem?.id) {
      try {
        const updated = await problemsApi.getById(selectedProblem.id)
        setSelectedProblem(updated)
      } catch {
        toast.error('Не вдалося оновити дані проблеми')
      }
    }
  }, [refetch, queryClient, selectedProblem?.id])

  const handleViewAllComments = useCallback(() => {
    setShowAllCommentsModal(true)
  }, [])

  const statusTabs = useMemo(() => [
    { key: 'all', label: 'Всі' },
    { key: ProblemStatusConstants.New, label: 'Нові' },
    { key: ProblemStatusConstants.InProgress, label: 'У процесі' },
    { key: ProblemStatusConstants.Completed, label: 'Вирішені' },
    { key: ProblemStatusConstants.Rejected, label: 'Відхилені' },
  ], [])

  const dateFilterOptions = useMemo(() => [
    { value: '', label: 'Весь час' },
    { value: 'week', label: 'Цього тижня' },
    { value: 'month', label: 'Цього місяця' },
    { value: 'year', label: 'Цього року' },
  ] as const, [])

  const statusTabColors = designSystem.colors.profile.tabs
type StatusTabStyle = CSSProperties & { '--tab-hover-color'?: string }
  const getStatusTabStyle = useCallback((isActive: boolean): StatusTabStyle => ({
    color: isActive ? statusTabColors.text : statusTabColors.inactiveText,
    backgroundColor: 'transparent',
    border: 'none',
    borderRadius: 0,
    '--tab-hover-color': statusTabColors.hoverText,
  }), [statusTabColors])

  const handleMarkerClick = useCallback(async (problemId: string | null) => {
    if (!problemId) return
    try {
      const fullProblem = await problemsApi.getById(problemId)
      setSelectedProblem(fullProblem)
      setShowDetails(true)
    } catch {
      toast.error('Не вдалося завантажити дані проблеми')
    }
  }, [])

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
          <div className="grid grid-cols-1 lg:grid-cols-5 gap-6 my-10">
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
                      click: () => handleMarkerClick(problem.id),
                    }}
                  >
                    <Popup>
                      <div className="text-sm font-semibold">
                        {problem.title}
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
