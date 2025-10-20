import type { Problem } from '@/types'
import { MapPin, Calendar, User, Tag } from 'lucide-react'

interface ProblemDetailPopupProps {
  problem: Problem
  onClose: () => void
}

export function ProblemDetailPopup({ problem, onClose }: ProblemDetailPopupProps) {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
      <div className="relative w-full max-w-2xl max-h-[90vh] overflow-y-auto rounded-lg bg-white shadow-xl">
        {/* Header */}
        <div className="sticky top-0 z-10 flex items-center justify-between border-b bg-white p-4">
          <h2 className="text-xl font-semibold text-gray-900">{problem.title}</h2>
          <button
            onClick={onClose}
            className="rounded-lg p-2 text-gray-400 transition-colors hover:bg-gray-100 hover:text-gray-600"
            aria-label="Закрити"
          >
            <svg
              className="h-5 w-5"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M6 18L18 6M6 6l12 12"
              />
            </svg>
          </button>
        </div>

        {/* Content */}
        <div className="p-6 space-y-6">
          {/* Images */}
          {problem.images && problem.images.length > 0 && (
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
          )}

          {/* Description */}
          <div>
            <h3 className="mb-2 text-sm font-medium text-gray-700">Опис проблеми</h3>
            <p className="text-gray-600">{problem.description}</p>
          </div>

          {/* Location */}
          <div className="flex items-start gap-2">
            <MapPin className="h-5 w-5 text-gray-400 mt-0.5" />
            <div>
              <h3 className="text-sm font-medium text-gray-700">Координати</h3>
              <p className="text-sm text-gray-600">
                {problem.latitude.toFixed(6)}, {problem.longitude.toFixed(6)}
              </p>
            </div>
          </div>

          {/* Status */}
          {problem.problemStatus && (
            <div className="flex items-start gap-2">
              <div className="h-5 w-5 mt-0.5">
                <div className="h-3 w-3 rounded-full bg-blue-500" />
              </div>
              <div>
                <h3 className="text-sm font-medium text-gray-700">Статус</h3>
                <p className="text-sm text-gray-600">{problem.problemStatus.name}</p>
              </div>
            </div>
          )}

          {/* Categories */}
          {problem.categories && problem.categories.length > 0 && (
            <div className="flex items-start gap-2">
              <Tag className="h-5 w-5 text-gray-400 mt-0.5" />
              <div className="flex-1">
                <h3 className="text-sm font-medium text-gray-700 mb-2">Категорії</h3>
                <div className="flex flex-wrap gap-2">
                  {problem.categories.map((category) => (
                    <span
                      key={category.id}
                      className="inline-flex items-center rounded-full bg-blue-50 px-3 py-1 text-sm text-blue-700"
                    >
                      {category.name}
                    </span>
                  ))}
                </div>
              </div>
            </div>
          )}

          {/* User */}
          {problem.user && (
            <div className="flex items-start gap-2">
              <User className="h-5 w-5 text-gray-400 mt-0.5" />
              <div>
                <h3 className="text-sm font-medium text-gray-700">Автор</h3>
                <p className="text-sm text-gray-600">
                  {problem.user.firstName} {problem.user.lastName}
                </p>
              </div>
            </div>
          )}

          {/* Dates */}
          <div className="flex items-start gap-2">
            <Calendar className="h-5 w-5 text-gray-400 mt-0.5" />
            <div>
              <h3 className="text-sm font-medium text-gray-700">Дата створення</h3>
              <p className="text-sm text-gray-600">
                {new Date(problem.createdAt).toLocaleDateString('uk-UA', {
                  year: 'numeric',
                  month: 'long',
                  day: 'numeric',
                  hour: '2-digit',
                  minute: '2-digit',
                })}
              </p>
            </div>
          </div>

          {/* Comments */}
          {problem.comments && problem.comments.length > 0 && (
            <div>
              <h3 className="mb-3 text-sm font-medium text-gray-700">
                Коментарі ({problem.comments.length})
              </h3>
              <div className="space-y-3">
                {problem.comments.map((comment) => (
                  <div key={comment.id} className="rounded-lg bg-gray-50 p-3">
                    <div className="mb-1 flex items-center justify-between">
                      <span className="text-sm font-medium text-gray-900">
                        {comment.user?.firstName} {comment.user?.lastName}
                      </span>
                      <span className="text-xs text-gray-500">
                        {new Date(comment.createdAt).toLocaleDateString('uk-UA')}
                      </span>
                    </div>
                    <p className="text-sm text-gray-600">{comment.content}</p>
                  </div>
                ))}
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  )
}
