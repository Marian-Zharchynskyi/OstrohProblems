import { useState, useRef } from 'react';
import type { ProblemImage, CoordinatorImage } from '@/types';
import { problemsApi } from '@/features/problems/api/problems-api';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Upload, Trash2, Image as ImageIcon, X } from 'lucide-react';
import { toast } from '@/lib/toast';
import { ImageLightbox } from '@/components/shared/image-lightbox';

type ImageType = 'problem' | 'coordinator';

interface ProblemImagesManagerProps {
  problemId: string;
  images: ProblemImage[] | CoordinatorImage[] | null;
  onImagesChange: () => void;
  maxImages?: number;
  canEdit?: boolean;
  imageType?: ImageType;
  title?: string;
}

export function ProblemImagesManager({ problemId, images, onImagesChange, maxImages = 6, canEdit = true, imageType = 'problem', title }: ProblemImagesManagerProps) {
  const [isUploading, setIsUploading] = useState(false);
  const [isDeleting, setIsDeleting] = useState<string | null>(null);
  const [selectedFiles, setSelectedFiles] = useState<File[]>([]);
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [lightboxOpen, setLightboxOpen] = useState(false);
  const [lightboxInitialIndex, setLightboxInitialIndex] = useState(0);

  const currentImagesCount = images?.length || 0;
  const canAddMore = currentImagesCount + selectedFiles.length < maxImages;

  const displayTitle = title || (imageType === 'coordinator' ? 'Фото координатора' : 'Зображення');

  if ((!images || images.length === 0) && selectedFiles.length === 0 && !canEdit) {
    return null;
  }

  const handleFileSelect = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = Array.from(e.target.files || []);
    const remainingSlots = maxImages - currentImagesCount - selectedFiles.length;

    if (files.length > remainingSlots) {
      toast.error(`Можна додати ще максимум ${remainingSlots} зображень`);
      return;
    }

    setSelectedFiles((prev) => [...prev, ...files]);
    if (fileInputRef.current) {
      fileInputRef.current.value = '';
    }
  };

  const handleRemoveSelectedFile = (index: number) => {
    setSelectedFiles((prev) => prev.filter((_, i) => i !== index));
  };

  const handleUpload = async () => {
    if (selectedFiles.length === 0) return;

    try {
      setIsUploading(true);
      const fileList = new DataTransfer();
      selectedFiles.forEach((file) => fileList.items.add(file));

      if (imageType === 'coordinator') {
        await problemsApi.uploadCoordinatorImages(problemId, fileList.files);
      } else {
        await problemsApi.uploadImages(problemId, fileList.files);
      }
      setSelectedFiles([]);
      toast.success('Зображення завантажено');
      onImagesChange();
    } catch {
      toast.error('Не вдалося завантажити зображення');
    } finally {
      setIsUploading(false);
    }
  };

  const handleDeleteImage = async (imageId: string) => {
    try {
      setIsDeleting(imageId);
      if (imageType === 'coordinator') {
        await problemsApi.deleteCoordinatorImage(problemId, imageId);
      } else {
        await problemsApi.deleteImage(problemId, imageId);
      }
      toast.success('Зображення видалено');
      onImagesChange();
    } catch {
      toast.error('Не вдалося видалити зображення');
    } finally {
      setIsDeleting(null);
    }
  };

  return (
    <Card>
      <CardHeader>
        <CardTitle className="flex items-center gap-2">
          <ImageIcon className="w-5 h-5" />
          {displayTitle} ({currentImagesCount}/{maxImages})
        </CardTitle>
        <CardDescription>Максимум {maxImages} зображень</CardDescription>
      </CardHeader>
      <CardContent className="space-y-4">
        {/* Existing Images */}
        {images && images.length > 0 && (
          <div className="grid grid-cols-2 md:grid-cols-3 gap-4">
            {images.map((image, index) => (
              <div key={image.id} className="relative group">
                <img
                  src={image.url}
                  alt="Problem"
                  className="h-32 w-full rounded-lg object-cover cursor-pointer hover:opacity-90 transition-opacity"
                  onClick={() => {
                    setLightboxInitialIndex(index);
                    setLightboxOpen(true);
                  }}
                />
                {canEdit && (
                  <button
                    onClick={(e) => {
                      e.stopPropagation();
                      if (image.id) handleDeleteImage(image.id);
                    }}
                    disabled={isDeleting === image.id}
                    className="absolute top-2 right-2 p-1.5 bg-red-500 text-white rounded-full opacity-0 group-hover:opacity-100 transition-opacity disabled:opacity-50">
                    {isDeleting === image.id ? (
                      <span className="w-4 h-4 block animate-spin border-2 border-white border-t-transparent rounded-full" />
                    ) : (
                      <Trash2 className="w-4 h-4" />
                    )}
                  </button>
                )}
              </div>
            ))}
          </div>
        )}

        {/* Selected Files Preview */}
        {selectedFiles.length > 0 && (
          <div className="space-y-2">
            <p className="text-sm font-medium">Обрані файли:</p>
            <div className="grid grid-cols-2 md:grid-cols-3 gap-4">
              {selectedFiles.map((file, index) => (
                <div key={index} className="relative group">
                  <img src={URL.createObjectURL(file)} alt={file.name} className="h-32 w-full rounded-lg object-cover border-2 border-dashed border-primary" />
                  <button onClick={() => handleRemoveSelectedFile(index)} className="absolute top-2 right-2 p-1.5 bg-gray-500 text-white rounded-full">
                    <X className="w-4 h-4" />
                  </button>
                </div>
              ))}
            </div>
          </div>
        )}

        {/* Upload Controls */}
        {canEdit && (
          <div className="flex flex-wrap gap-2">
            <input ref={fileInputRef} type="file" accept="image/*" multiple onChange={handleFileSelect} className="hidden" disabled={!canAddMore || isUploading} />
            <Button
              variant="outline"
              onClick={() => fileInputRef.current?.click()}
              disabled={!canAddMore || isUploading}
              className="border border-[#D0D5DD] bg-white text-[#292929] hover:bg-[#F5F5F5] hover:text-[#292929]">
              <Upload className="w-4 h-4 mr-2" />
              Обрати файли
            </Button>
            {selectedFiles.length > 0 && (
              <>
                <Button onClick={handleUpload} disabled={isUploading}>
                  {isUploading ? 'Завантаження...' : `Завантажити (${selectedFiles.length})`}
                </Button>
                <Button
                  variant="outline"
                  onClick={() => setSelectedFiles([])}
                  disabled={isUploading}
                  className="border border-[#E42556] text-[#E42556] bg-transparent hover:bg-[#E42556]/10 hover:text-[#E42556]">
                  Скасувати
                </Button>
              </>
            )}
          </div>
        )}

        {/* Empty State */}
        {(!images || images.length === 0) && selectedFiles.length === 0 && (
          <div className="text-center py-8 text-muted-foreground">
            <ImageIcon className="w-12 h-12 mx-auto mb-2 opacity-50" />
            <p>Немає зображень</p>
            {canEdit && <p className="text-sm">Натисніть "Обрати файли" щоб додати</p>}
          </div>
        )}
      </CardContent>

      <ImageLightbox images={images || []} initialIndex={lightboxInitialIndex} open={lightboxOpen} onClose={() => setLightboxOpen(false)} />
    </Card>
  );
}
