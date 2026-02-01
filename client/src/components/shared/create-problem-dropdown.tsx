import { useState, useRef, useEffect } from 'react'
import { Link } from 'react-router-dom'
import { Plus, ChevronDown, PenLine, Sparkles } from 'lucide-react'
import { cn } from '@/lib/utils'

export function CreateProblemDropdown() {
  const [isOpen, setIsOpen] = useState(false)
  const dropdownRef = useRef<HTMLDivElement>(null)

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsOpen(false)
      }
    }
    document.addEventListener('mousedown', handleClickOutside)
    return () => document.removeEventListener('mousedown', handleClickOutside)
  }, [])

  return (
    <div className="relative" ref={dropdownRef}>
      <button
        onClick={() => setIsOpen(!isOpen)}
        className="hidden sm:flex items-center gap-2 text-sm font-medium bg-primary text-primary-foreground px-5 py-2.5 rounded-full hover:bg-primary/90 transition-all shadow-sm active:scale-95 outline-none focus:outline-none"
      >
        <Plus className="h-4 w-4" />
        Подати проблему
        <ChevronDown
          className={cn(
            'h-4 w-4 transition-transform duration-200',
            isOpen && 'rotate-180'
          )}
        />
      </button>

      {isOpen && (
        <div className="absolute right-0 top-full mt-2 w-72 bg-white rounded-2xl shadow-[0_10px_40px_-10px_rgba(0,0,0,0.2)] border border-gray-100 p-2 overflow-hidden animate-in fade-in zoom-in-95 duration-200 z-50">
          <Link
            to="/problems/create"
            onClick={() => setIsOpen(false)}
            className="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-gray-50 text-gray-700 transition-colors group"
          >
            <div className="w-10 h-10 rounded-full bg-gray-100 flex items-center justify-center text-gray-500 group-hover:bg-white group-hover:shadow-sm transition-all">
              <PenLine className="w-5 h-5" />
            </div>
            <div>
              <span className="font-medium text-sm block">Вручну</span>
              <span className="text-xs text-gray-500">Заповнити форму самостійно</span>
            </div>
          </Link>

          <Link
            to="/problems/create-ai"
            onClick={() => setIsOpen(false)}
            className="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-violet-50 text-gray-700 transition-colors group"
          >
            <div className="w-10 h-10 rounded-full bg-gradient-to-br from-violet-100 to-purple-100 flex items-center justify-center text-violet-500 group-hover:from-violet-500 group-hover:to-purple-600 group-hover:text-white group-hover:shadow-md transition-all">
              <Sparkles className="w-5 h-5" />
            </div>
            <div>
              <span className="font-medium text-sm block text-violet-700">З AI асистентом</span>
              <span className="text-xs text-gray-500">Опишіть голосом або текстом</span>
            </div>
          </Link>
        </div>
      )}
    </div>
  )
}
