import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import type { CreateProblem } from '@/types'
import { useCategories } from '@/features/categories/hooks/use-categories'
import {
  useCreateProblem,
  useUploadProblemImages,
} from '@/features/problems/hooks/use-problems'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Label } from '@/components/ui/label'
import { FormField } from '@/components/shared/form-field'
import { toast } from '@/lib/toast'

const MAX_IMAGES_COUNT = 6

export function CreateIssuePage() {
  const navigate = useNavigate()
  const { data: categories } = useCategories()
  const createMutation = useCreateProblem()
  const uploadImagesMutation = useUploadProblemImages()

  const [formData, setFormData] = useState<CreateProblem>({
    title: '',
    latitude: 0,
    longitude: 0,
    description: '',
    problemCategoryIds: [],
  })

  const [files, setFiles] = useState<FileList | null>(null)
  const [descriptionError, setDescriptionError] = useState('')

  const handleCategoryToggle = (categoryId: string) => {
    setFormData((prev) => ({
      ...prev,
      problemCategoryIds: prev.problemCategoryIds.includes(categoryId)
        ? prev.problemCategoryIds.filter((id) => id !== categoryId)
        : [...prev.problemCategoryIds, categoryId],
    }))
  }

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFiles = e.target.files
    if (selectedFiles && selectedFiles.length > MAX_IMAGES_COUNT) {
      toast.error(`Максимальна кількість фото: ${MAX_IMAGES_COUNT}`)
      e.target.value = ''
      setFiles(null)
      return
    }
    setFiles(selectedFiles)
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

  return (
    <form onSubmit={handleSubmit} className="space-y-8">
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle className="font-heading text-2xl">
            Створити проблему
          </CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <FormField
            label="Вкажіть назву"
            name="title"
            value={formData.title}
            onChange={(value) =>
              setFormData({ ...formData, title: value as string })
            }
            required
            placeholder="Введіть тут"
          />

          <div className="space-y-2">
            <Label>Категорії</Label>
            <div className="border rounded-md p-3 space-y-1 max-h-40 overflow-y-auto bg-background">
              {categories?.map((category) => (
                <div key={category.id} className="flex items-center space-x-2">
                  <input
                    type="checkbox"
                    id={`category-${category.id}`}
                    checked={formData.problemCategoryIds.includes(category.id || '')}
                    onChange={() => handleCategoryToggle(category.id || '')}
                    className="h-4 w-4"
                  />
                  <label
                    htmlFor={`category-${category.id}`}
                    className="text-sm cursor-pointer"
                  >
                    {category.name}
                  </label>
                </div>
              ))}
            </div>
          </div>

          <div className="grid gap-4 md:grid-cols-2">
            <FormField
              label="Широта (latitude)"
              name="latitude"
              type="number"
              value={formData.latitude}
              onChange={(value) =>
                setFormData({ ...formData, latitude: value as number })
              }
              required
              placeholder="Введіть широту"
            />
            <FormField
              label="Довгота (longitude)"
              name="longitude"
              type="number"
              value={formData.longitude}
              onChange={(value) =>
                setFormData({ ...formData, longitude: value as number })
              }
              required
              placeholder="Введіть довготу"
            />
          </div>
        </CardContent>
      </Card>

      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle className="font-heading text-xl">Опис у подробицях</CardTitle>
        </CardHeader>
        <CardContent className="space-y-2">
          <Label htmlFor="description">
            Опис <span className="text-destructive ml-1">*</span>
          </Label>
          <textarea
            id="description"
            className="min-h-[140px] w-full rounded-md border border-input bg-background px-3 py-2 text-sm ring-offset-background focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2"
            value={formData.description}
            onChange={(e) =>
              setFormData({ ...formData, description: e.target.value })
            }
            placeholder="Опишіть вашу проблему максимально детально"
          />
          <p className="text-xs text-muted-foreground">
            Введіть щонайменше 30 символів
          </p>
          {descriptionError && (
            <p className="text-xs text-destructive">{descriptionError}</p>
          )}
        </CardContent>
      </Card>

      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle className="font-heading text-xl">Фото</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <p className="text-sm text-muted-foreground">
            Для кращого відображення проблеми ви можете прикріпити до {MAX_IMAGES_COUNT} фото.
          </p>
          <div className="flex flex-wrap gap-4 items-center">
            <Button
              type="button"
              variant="outline"
              onClick={() =>
                document.getElementById('problem-images')?.click()
              }
            >
              Додати фото
            </Button>
            <input
              id="problem-images"
              type="file"
              multiple
              className="hidden"
              onChange={handleFileChange}
            />
            {files && files.length > 0 && (
              <p className="text-sm text-muted-foreground">
                Обрано файлів: {files.length} / {MAX_IMAGES_COUNT}
              </p>
            )}
          </div>
        </CardContent>
      </Card>

      <div className="flex justify-end">
        <Button type="submit" size="lg" disabled={isSubmitting}>
          {isSubmitting ? 'Створення...' : 'Створити'}
        </Button>
      </div>
    </form>
  )
}
