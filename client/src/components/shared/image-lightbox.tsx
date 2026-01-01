import { useState, useEffect, useCallback } from 'react'
import { X, ChevronLeft, ChevronRight, ZoomIn, ZoomOut } from 'lucide-react'

interface ImageLightboxProps {
  images: { id: string | null; url: string }[]
  initialIndex?: number
  open: boolean
  onClose: () => void
}

export function ImageLightbox({ images, initialIndex = 0, open, onClose }: ImageLightboxProps) {
  const [currentIndex, setCurrentIndex] = useState(initialIndex)
  const [scale, setScale] = useState(1)

  useEffect(() => {
    setCurrentIndex(initialIndex)
    setScale(1)
  }, [initialIndex, open])

  const handlePrev = useCallback(() => {
    setCurrentIndex((prev) => (prev === 0 ? images.length - 1 : prev - 1))
    setScale(1)
  }, [images.length])

  const handleNext = useCallback(() => {
    setCurrentIndex((prev) => (prev === images.length - 1 ? 0 : prev + 1))
    setScale(1)
  }, [images.length])

  const handleZoomIn = () => {
    setScale((prev) => Math.min(prev + 0.5, 3))
  }

  const handleZoomOut = () => {
    setScale((prev) => Math.max(prev - 0.5, 0.5))
  }

  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (!open) return
      if (e.key === 'Escape') onClose()
      if (e.key === 'ArrowLeft') handlePrev()
      if (e.key === 'ArrowRight') handleNext()
    }

    window.addEventListener('keydown', handleKeyDown)
    return () => window.removeEventListener('keydown', handleKeyDown)
  }, [open, onClose, handlePrev, handleNext])

  if (!open || images.length === 0) return null

  return (
    <div className="fixed inset-0 z-[100] flex items-center justify-center bg-black/90">
      <button
        onClick={onClose}
        className="absolute top-4 right-4 p-2 text-white bg-white/10 hover:bg-white/10 rounded-full transition-colors z-10 focus:outline-none focus:ring-0"
        aria-label="Закрити"
      >
        <X className="w-[1.8rem] h-[1.8rem]" />
      </button>

      <div className="absolute top-4 left-1/2 -translate-x-1/2 flex items-center gap-4 z-10">
        <button
          onClick={handleZoomOut}
          className="p-2 text-white bg-white/10 hover:bg-white/10 rounded-full transition-colors focus:outline-none focus:ring-0"
          aria-label="Зменшити"
        >
          <ZoomOut className="w-6 h-6" />
        </button>
        <span className="text-white text-sm">{Math.round(scale * 100)}%</span>
        <button
          onClick={handleZoomIn}
          className="p-2 text-white bg-white/10 hover:bg-white/10 rounded-full transition-colors focus:outline-none focus:ring-0"
          aria-label="Збільшити"
        >
          <ZoomIn className="w-6 h-6" />
        </button>
      </div>

      {images.length > 1 && (
        <>
          <button
            onClick={handlePrev}
            className="absolute left-4 p-2 text-white bg-white/10 hover:bg-white/10 rounded-full transition-colors z-10 focus:outline-none focus:ring-0"
            aria-label="Попереднє"
          >
            <ChevronLeft className="w-[2.4rem] h-[2.4rem]" />
          </button>
          <button
            onClick={handleNext}
            className="absolute right-4 p-2 text-white bg-white/10 hover:bg-white/10 rounded-full transition-colors z-10 focus:outline-none focus:ring-0"
            aria-label="Наступне"
          >
            <ChevronRight className="w-[2.4rem] h-[2.4rem]" />
          </button>
        </>
      )}

      <div className="flex items-center justify-center w-full h-full p-16 overflow-hidden">
        <img
          src={images[currentIndex]?.url}
          alt={`Зображення ${currentIndex + 1}`}
          className="max-w-full max-h-full object-contain transition-transform duration-200"
          style={{ transform: `scale(${scale})` }}
          draggable={false}
        />
      </div>

      {images.length > 1 && (
        <div className="absolute bottom-4 left-1/2 -translate-x-1/2 flex items-center gap-2">
          <span className="text-white text-sm">
            {currentIndex + 1} / {images.length}
          </span>
        </div>
      )}
    </div>
  )
}
