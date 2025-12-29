import { useState, useMemo } from 'react'
import { useNavigate } from 'react-router-dom'
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet'
import { Icon } from 'leaflet'
import type { LatLngBoundsExpression } from 'leaflet'
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
import { toast } from '@/lib/toast'
import { useQueryClient } from '@tanstack/react-query'
import { 
  Search, 
  MapPin, 
  Star, 
  ChevronDown, 
  ChevronUp,
  Pencil,
  MessageSquare,
  Image as ImageIcon,
  Loader2,
  X,
  Check
} from 'lucide-react'
import 'leaflet/dist/leaflet.css'

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

interface CommentsBlockProps {
  comments: Comment[]
  problemId: string | null
}

function CommentsBlock({ comments, problemId }: CommentsBlockProps) {
  const [showAll, setShowAll] = useState(false)
  const realtimeComments = useRealtimeComments(problemId, comments)
  const displayedComments = showAll ? realtimeComments : realtimeComments.slice(0, 2)

  return (
    <Card className="bg-white border border-gray-200 rounded-[10px]">
      <CardHeader className="pb-4">
        <CardTitle className="text-lg font-bold text-[#1F2732] font-['Mulish'] flex items-center gap-2">
          <MessageSquare className="w-5 h-5" />
          Коментарі користувачів
        </CardTitle>
      </CardHeader>
      <CardContent>
        {realtimeComments.length === 0 ? (
          <p className="text-gray-500 text-sm">Коментарів поки немає</p>
        ) : (
          <div className="space-y-3">
            {displayedComments.map((comment) => (
              <div key={comment.id} className="bg-gray-50 rounded-lg p-3">
                <div className="flex items-center justify-between mb-1">
                  <span className="text-sm font-medium text-gray-900">
                    {comment.user?.name} {comment.user?.surname}
                  </span>
                  <span className="text-xs text-gray-500">
                    {new Date(comment.createdAt).toLocaleDateString('uk-UA')}
                  </span>
                </div>
                <p className="text-sm text-gray-600">{comment.content}</p>
              </div>
            ))}
          </div>
        )}
        
        {realtimeComments.length > 2 && (
          <button
            onClick={() => setShowAll(!showAll)}
            className="mt-4 flex items-center gap-1 text-sm text-[#E42556] hover:underline"
          >
            {showAll ? (
              <>
                Згорнути <ChevronUp className="w-4 h-4" />
              </>
            ) : (
              <>
                Дивитися усі коментарі ({realtimeComments.length}) <ChevronDown className="w-4 h-4" />
              </>
            )}
          </button>
        )}
        
        <Button className="mt-4 w-full bg-[#E42556] hover:bg-[#E42556]/90 text-white">
          Написати коментар
        </Button>
      </CardContent>
    </Card>
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
              className="p-1 hover:bg-gray-100 rounded transition-colors"
              onClick={() => setIsEditing(true)}
            >
              <Pencil className="w-4 h-4 text-[#596872]" />
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
                className="mt-2 flex items-center gap-1 text-sm text-[#E42556] hover:underline"
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

function ProblemDetailsCard({ problem, onUpdate, onViewAllComments }: ProblemDetailsCardProps) {
  const navigate = useNavigate()
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
                    className="p-1 hover:bg-gray-100 rounded transition-colors"
                    onClick={() => setIsEditing(true)}
                  >
                    <Pencil className="w-4 h-4 text-[#596872]" />
                  </button>
                </CardTitle>
                <div className="flex items-center gap-2 mt-2 text-sm text-gray-600">
                  <MapPin className="w-4 h-4" />
                  <span>Координати: {problem.latitude.toFixed(4)}, {problem.longitude.toFixed(4)}</span>
                </div>
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
              onClick={() => hasUserImages && setShowImages(!showImages)}
              disabled={!hasUserImages}
              className={`flex items-center gap-2 text-sm ${
                hasUserImages 
                  ? 'text-[#E42556] hover:underline cursor-pointer' 
                  : 'text-gray-400 cursor-not-allowed'
              }`}
              title={!hasUserImages ? 'Немає поданих зображень' : undefined}
            >
              <ImageIcon className="w-4 h-4" />
              Відкрити подані зображення {hasUserImages ? `(${problem.images!.length})` : '(0)'}
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
              onClick={() => hasCoordinatorImages && setShowCoordinatorImages(!showCoordinatorImages)}
              disabled={!hasCoordinatorImages}
              className={`flex items-center gap-2 text-sm ${
                hasCoordinatorImages 
                  ? 'text-[#E42556] hover:underline cursor-pointer' 
                  : 'text-gray-400 cursor-not-allowed'
              }`}
              title={!hasCoordinatorImages ? 'Немає зображень від координатора' : undefined}
            >
              <ImageIcon className="w-4 h-4" />
              Відкрити зображення від координатора {hasCoordinatorImages ? `(${problem.coordinatorImages!.length})` : '(0)'}
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

        <div className="mt-6 pt-4 border-t space-y-2">
          <button
            onClick={onViewAllComments}
            className="text-sm text-[#E42556] hover:underline"
          >
            Дивитися усі коментарі до проблеми
          </button>
          <div>
            <button
              onClick={() => navigate(`/problems/${problem.id}`)}
              className="text-sm text-[#E42556] hover:underline"
            >
              Перейти на сторінку проблеми
            </button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
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
  const navigate = useNavigate()
  const queryClient = useQueryClient()
  const [selectedProblem, setSelectedProblem] = useState<Problem | null>(null)
  const [showDetails, setShowDetails] = useState(false)
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
    if (selectedProblem?.id) {
      navigate(`/problems/${selectedProblem.id}/comments`)
    }
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

  const handleMarkerClick = (problem: Problem) => {
    setSelectedProblem(problem)
  }

  const handleDetailsClick = () => {
    setShowDetails(true)
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
      <div className="border-b border-gray-200 mb-6">
        <div className="flex gap-2">
          {statusTabs.map((tab) => (
            <button
              key={tab.key}
              onClick={() => setStatusFilter(tab.key)}
              className="px-4 py-3 text-sm font-medium font-['Mulish'] text-[#464646] hover:text-[#1F2732] transition-colors relative pb-4 outline-none focus:outline-none focus:ring-0"
            >
              {tab.label}
              {statusFilter === tab.key && (
                <div className="absolute bottom-0 left-0 right-0 h-0.5 bg-[#E42556]" />
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
            className="pl-10 bg-white border border-[#464646] rounded-[10px] text-[#464646]"
          />
        </div>
        
        <Select value={categoryFilter} onValueChange={setCategoryFilter}>
          <SelectTrigger className="w-[180px] bg-white border border-[#464646] rounded-[10px] text-[#464646]">
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
          <SelectTrigger className="w-[180px] bg-white border border-[#464646] rounded-[10px] text-[#464646]">
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
          <SelectTrigger className="w-[180px] bg-white border border-[#464646] rounded-[10px] text-[#464646]">
            <SelectValue
              placeholder="Створено"
              value={dateFilterOptions.find((option) => option.value === dateFilter)?.label}
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
                      click: () => handleMarkerClick(problem),
                    }}
                  >
                    <Popup>
                      <div className="min-w-[200px]">
                        <h3 className="font-semibold text-gray-900 mb-1">{problem.title}</h3>
                        <p className="text-sm text-gray-600 mb-2 line-clamp-2">
                          {problem.description}
                        </p>
                        <Badge className={`mb-2 ${
                          problem.status === ProblemStatusConstants.New ? 'bg-blue-100 text-blue-800' :
                          problem.status === ProblemStatusConstants.InProgress ? 'bg-yellow-100 text-yellow-800' :
                          problem.status === ProblemStatusConstants.Completed ? 'bg-green-100 text-green-800' :
                          'bg-red-100 text-red-800'
                        }`}>
                          {problem.status}
                        </Badge>
                        <button
                          onClick={handleDetailsClick}
                          className="mt-2 w-full rounded-md bg-[#E42556] px-3 py-1.5 text-sm font-medium text-white hover:bg-[#E42556]/90 transition-colors"
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
    </div>
  )
}
