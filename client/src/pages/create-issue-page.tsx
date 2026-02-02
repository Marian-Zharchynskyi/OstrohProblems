import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import type { CreateProblem } from '@/types'
import { CATEGORIES } from '@/constants/categories'
import {
  useCreateProblem,
  useUploadProblemImages,
} from '@/features/problems/hooks/use-problems'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Label } from '@/components/ui/label'
import { LocationPickerMap } from '@/components/location-picker-map'
import { toast } from '@/lib/toast'
import { Camera, ChevronDown, Plus, RotateCcw, Crosshair } from 'lucide-react'

const MAX_IMAGES_COUNT = 4 // Changed to 4 as per UI requirement "Grid of 4 slots"
const PRIORITIES = ['Низький', 'Середній', 'Високий', 'Критичний']

export function CreateIssuePage() {
  const navigate = useNavigate()
  const createMutation = useCreateProblem()
  const uploadImagesMutation = useUploadProblemImages()

  const [formData, setFormData] = useState<CreateProblem>({
    title: '',
    latitude: 0,
    longitude: 0,
    description: '',
    categoryNames: [],
    priority: '',
  })

  const [files, setFiles] = useState<FileList | null>(null)
  const [imagePreviews, setImagePreviews] = useState<string[]>([])
  const [descriptionError, setDescriptionError] = useState('')
  const [streetName, setStreetName] = useState('')
  const [mapKey, setMapKey] = useState(0)

  // Helper for single category selection
  const selectedCategory = formData.categoryNames[0] || ''

  const handleLocationChange = (lat: number, lng: number, street: string) => {
    setFormData({ ...formData, latitude: lat, longitude: lng })
    setStreetName(street)
  }

  const handleCategoryChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const val = e.target.value
    setFormData({
      ...formData,
      categoryNames: val ? [val] : [],
    })
  }

  const handlePriorityChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setFormData({ ...formData, priority: e.target.value })
  }

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFiles = e.target.files
    if (selectedFiles && selectedFiles.length > MAX_IMAGES_COUNT) {
      toast.error(`Максимальна кількість фото: ${MAX_IMAGES_COUNT}`)
      e.target.value = ''
      setFiles(null)
      setImagePreviews([])
      return
    }
    setFiles(selectedFiles)

    // Generate image previews
    if (selectedFiles) {
      const previews: string[] = []
      Array.from(selectedFiles).forEach((file) => {
        const reader = new FileReader()
        reader.onloadend = () => {
          previews.push(reader.result as string)
          if (previews.length === selectedFiles.length) {
            setImagePreviews([...previews])
          }
        }
        reader.readAsDataURL(file)
      })
    }
  }

  const handleResetLocation = () => {
    setFormData({ ...formData, latitude: 0, longitude: 0 })
    setStreetName('')
    setMapKey((prev) => prev + 1) // Force re-render to reset map view
  }

  const handleCenterLocation = () => {
    // Re-centers the map (conceptually). If we have a location, maybe center on it?
    // Since we don't have map ref, forcing re-render with current coords (if any) or default
    // works to "Center" the view.
    setMapKey((prev) => prev + 1)
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setDescriptionError('')

    if (files && files.length > MAX_IMAGES_COUNT) {
      toast.error(`Максимальна кількість фото: ${MAX_IMAGES_COUNT}`)
      return
    }

    if (formData.description.trim().length < 30) {
      setDescriptionError("Введіть щонайменше 30 символів")
      return
    }

    if (formData.latitude === 0 || formData.longitude === 0) {
      toast.error('Будь ласка, виберіть місце на карті')
      return
    }

    try {
      const createdProblem = await createMutation.mutateAsync(formData)

      if (files && files.length > 0 && createdProblem.id) {
        await uploadImagesMutation.mutateAsync({ id: createdProblem.id, files })
      }

      toast.success('Проблему створено успішно')
      navigate('/map')
    } catch (error) {
      toast.error('Сталася помилка під час створення проблеми')
      console.error(error)
    }
  }

  const isSubmitting = createMutation.isPending || uploadImagesMutation.isPending

  // Input styles
  const inputBaseClasses = "w-full bg-[#F5F6F7] border-none rounded-[10px] px-4 text-sm focus:ring-0 transition-all outline-none placeholder:text-gray-400"

  return (
    <div className="w-full max-w-[1600px] mx-auto py-8 px-4 md:px-8 font-sans">
      {/* Page Title */}
      <h1 className="text-[#1F2732] text-3xl font-bold mb-8 font-heading">
        Створення проблеми
      </h1>

      <form onSubmit={handleSubmit} className="space-y-8">
        {/* Card 1: Details */}
        <Card className="bg-white border text-card-foreground shadow-sm rounded-xl overflow-hidden">
          <CardHeader className="border-b border-gray-100 bg-white pb-4">
            <CardTitle className="font-heading font-semibold text-lg text-[#1F2732]">
              Надайте подробиці
            </CardTitle>
          </CardHeader>
          <CardContent className="p-6 space-y-6">
            {/* Title Input */}
            <div className="flex flex-col gap-4">
              <Label className="text-[#1F2732] font-medium ml-1">
                Назва <span className="text-destructive">*</span>
              </Label>
              <input
                type="text"
                value={formData.title}
                onChange={(e) => setFormData({ ...formData, title: e.target.value })}
                required
                placeholder="Наприклад : велика яма біля парку Т. Шевченка"
                className={`${inputBaseClasses} h-12`}
              />
            </div>

            {/* Grid for Category & Priority */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              {/* Category */}
              <div className="flex flex-col gap-4 relative">
                <Label className="text-[#1F2732] font-medium ml-1">
                  Категорія <span className="text-destructive">*</span>
                </Label>
                <div className="relative">
                  <select
                    value={selectedCategory}
                    onChange={handleCategoryChange}
                    className={`${inputBaseClasses} h-12 appearance-none cursor-pointer`}
                  >
                    <option value="" disabled hidden>Виберіть категорію</option>
                    {CATEGORIES.map((cat) => (
                      <option key={cat.value} value={cat.value}>{cat.label}</option>
                    ))}
                  </select>
                  <ChevronDown className="absolute right-4 top-1/2 -translate-y-1/2 text-gray-500 h-5 w-5 pointer-events-none" />
                </div>
              </div>

              {/* Priority */}
              <div className="flex flex-col gap-4 relative">
                <Label className="text-[#1F2732] font-medium ml-1">
                  Пріоритет <span className="text-destructive">*</span>
                </Label>
                <div className="relative">
                  <select
                    value={formData.priority || ''}
                    onChange={handlePriorityChange}
                    className={`${inputBaseClasses} h-12 appearance-none cursor-pointer`}
                  >
                    <option value="" disabled hidden>Виберіть пріоритет</option>
                    {PRIORITIES.map((p) => (
                      <option key={p} value={p}>{p}</option>
                    ))}
                  </select>
                  <ChevronDown className="absolute right-4 top-1/2 -translate-y-1/2 text-gray-500 h-5 w-5 pointer-events-none" />
                </div>
              </div>
            </div>

            {/* Description */}
            <div className="flex flex-col gap-4">
              <Label className="text-[#1F2732] font-medium ml-1">
                Опис <span className="text-destructive">*</span>
              </Label>
              <textarea
                value={formData.description}
                onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                placeholder="Опишіть вашу проблему максимально детально та точно"
                className={`${inputBaseClasses} min-h-[140px] py-3 resize-y`}
              />
              <div className="flex justify-between items-start">
                <p className="text-xs text-red-500 ml-1">
                  {descriptionError || (formData.description.length < 30 ? "Введіть щонайменше 30 символів" : "")}
                </p>
              </div>
            </div>
          </CardContent>
        </Card>

        {/* Card 2: Media */}
        <Card className="bg-white border text-card-foreground shadow-sm rounded-xl overflow-hidden">
          <CardHeader className="border-b border-gray-100 bg-white pb-4">
            <CardTitle className="font-heading font-semibold text-lg text-[#1F2732]">
              Фото
            </CardTitle>
            <p className="text-sm text-gray-500 font-sans">
              Для кращого відображення проблеми ви можете прикріпити фото
            </p>
          </CardHeader>
          <CardContent className="p-6">
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              {/* Add Button Slot */}
              <div
                onClick={() => document.getElementById('problem-images')?.click()}
                className="aspect-[2/1] bg-[#F5F6F7] rounded-[10px] flex flex-col items-center justify-center cursor-pointer hover:bg-gray-200 transition-colors group"
              >
                <span className="text-sm text-[#1F2732] border-b border-black pb-0.5 mb-1 font-medium">Додати фото</span>
                <input
                  id="problem-images"
                  type="file"
                  multiple
                  accept="image/*"
                  className="hidden"
                  onChange={handleFileChange}
                />
              </div>

              {/* Placeholder Slots / Preview */}
              {/* If files selected, show count or previews? User requested "Slots 2-4 (Empty): ... center camera icon with small plus". */}
              {/* I'll show placeholders primarily, maybe indicate if files are selected via text below or toast. The prompt says "Slots 2-4 (Empty)". I'll strictly follow the design requested. */}

              {[...Array(3)].map((_, i) => (
                <div key={i} className="aspect-[2/1] bg-[#F5F6F7] rounded-[10px] flex items-center justify-center relative text-gray-400 overflow-hidden">
                  {imagePreviews[i] ? (
                    <img
                      src={imagePreviews[i]}
                      alt={`Preview ${i + 1}`}
                      className="w-full h-full object-cover"
                    />
                  ) : (
                    <div className="relative">
                      <Camera className="w-8 h-8" />
                      <Plus className="w-3 h-3 absolute -top-1 -right-1 text-gray-400" />
                    </div>
                  )}
                </div>
              ))}
            </div>
            {files && files.length > 0 && (
              <p className="text-sm text-muted-foreground mt-2">
                Обрано файлів: {files.length} / {MAX_IMAGES_COUNT}
              </p>
            )}
          </CardContent>
        </Card>

        {/* Card 3: Location */}
        <Card className="bg-white border text-card-foreground shadow-sm rounded-xl overflow-hidden">
          <CardHeader className="border-b border-gray-100 bg-white pb-4">
            <div className="flex items-center gap-1">
              <CardTitle className="font-heading font-semibold text-lg text-[#1F2732]">
                Місце на карті
              </CardTitle>
              <span className="text-destructive text-lg">*</span>
            </div>
            <p className="text-sm text-gray-500 font-sans">
              Виберіть наближене місце на карті Острога, де наявна ваша проблема
            </p>
          </CardHeader>
          <CardContent className="p-6">
            <div className="flex flex-col md:flex-row gap-6">
              {/* Map View */}
              <div className="flex-1 relative rounded-lg overflow-hidden border border-gray-200">
                <LocationPickerMap
                  key={mapKey}
                  latitude={formData.latitude}
                  longitude={formData.longitude}
                  onLocationChange={handleLocationChange}
                  height="345px"
                />
              </div>

              {/* Controls */}
              <div className="flex flex-row md:flex-col gap-3 justify-center md:min-w-[120px]">
                <Button
                  type="button"
                  onClick={handleResetLocation}
                  className="bg-[#1F2732] hover:bg-[#2d3845] text-white rounded-lg flex items-center justify-center gap-2"
                >
                  <RotateCcw className="w-4 h-4" />
                  Скинути
                </Button>
                <Button
                  type="button"
                  onClick={handleCenterLocation}
                  className="bg-[#1F2732] hover:bg-[#2d3845] text-white rounded-lg flex items-center justify-center gap-2"
                >
                  <Crosshair className="w-4 h-4" />
                  Центрувати
                </Button>
              </div>
            </div>

            <div className="mt-4 text-[#1F2732] font-semibold">
              Поточна вулиця : <span className="text-gray-500 font-medium ml-1">{streetName || 'Не вибрано'}</span>
            </div>

            {/* Footer Button inside Location Card or Page Footer? User says "In the bottom right corner of the location card (or whole form)". */}
            {/* I will put it bottom right of the location card/form area. */}
            <div className="flex justify-end mt-6">
              <Button
                type="submit"
                disabled={isSubmitting}
                className="bg-[#E42556]/80 hover:bg-[#E42556] text-white font-extrabold font-sans text-lg px-8 py-6 rounded-[20px] shadow-lg transition-all transform hover:scale-105"
              >
                {isSubmitting ? 'Створення...' : 'Створити'}
              </Button>
            </div>
          </CardContent>
        </Card>
      </form>
    </div>
  )
}
