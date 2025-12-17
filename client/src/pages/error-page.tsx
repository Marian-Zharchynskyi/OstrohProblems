import { Link } from 'react-router-dom'
import { AlertCircle, Home } from 'lucide-react'

export function ErrorPage() {
  return (
    <div className="min-h-[60vh] flex items-center justify-center">
      <div className="text-center space-y-6 max-w-md">
        <div className="flex justify-center">
          <div className="p-6 rounded-full bg-destructive/10">
            <AlertCircle className="h-16 w-16 text-destructive" />
          </div>
        </div>
        
        <div className="space-y-2">
          <h1 className="font-heading text-6xl font-bold text-foreground">404</h1>
          <h2 className="font-heading text-2xl font-semibold">Сторінку не знайдено</h2>
          <p className="text-muted-foreground">
            На жаль, сторінка, яку ви шукаєте, не існує або була переміщена.
          </p>
        </div>

        <div className="flex flex-col sm:flex-row gap-4 justify-center">
          <Link
            to="/"
            className="inline-flex items-center justify-center gap-2 bg-primary text-primary-foreground px-6 py-3 rounded-md hover:bg-primary/90 transition-colors font-medium"
          >
            <Home className="h-4 w-4" />
            Повернутися на головну
          </Link>
        </div>
      </div>
    </div>
  )
}
