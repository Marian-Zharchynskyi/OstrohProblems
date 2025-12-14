import { Link } from 'react-router-dom'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { MessageSquare, Star, AlertCircle, Users, MapPin } from 'lucide-react'

const features = [
  {
    title: 'Проблеми',
    description: 'Перегляд та управління проблемами',
    icon: AlertCircle,
    path: '/problems',
    color: 'text-red-500',
  },
  {
    title: 'Коментарі',
    description: 'Управління коментарями до проблем',
    icon: MessageSquare,
    path: '/comments',
    color: 'text-purple-500',
  },
  {
    title: 'Оцінки',
    description: 'Управління оцінками проблем',
    icon: Star,
    path: '/ratings',
    color: 'text-yellow-500',
  },
  {
    title: 'Користувачі',
    description: 'Управління користувачами системи',
    icon: Users,
    path: '/admin/users',
    color: 'text-blue-500',
  },
  {
    title: 'Карта',
    description: 'Перегляд проблем на карті',
    icon: MapPin,
    path: '/map',
    color: 'text-green-500',
  },
]

export function HomePage() {
  return (
    <div className="space-y-8">
      <div>
        <h1 className="text-4xl font-bold tracking-tight">Панель адміністратора</h1>
        <p className="text-muted-foreground mt-2">
          Управління та відстеження проблем громади
        </p>
      </div>

      <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
        {features.map((feature) => {
          const Icon = feature.icon
          return (
            <Link key={feature.path} to={feature.path}>
              <Card className="hover:shadow-lg transition-shadow cursor-pointer h-full">
                <CardHeader>
                  <div className="flex items-center gap-4">
                    <div className={`p-3 rounded-lg bg-muted ${feature.color}`}>
                      <Icon className="h-6 w-6" />
                    </div>
                    <CardTitle>{feature.title}</CardTitle>
                  </div>
                  <CardDescription>{feature.description}</CardDescription>
                </CardHeader>
                <CardContent>
                  <p className="text-sm text-muted-foreground">
                    Натисніть для управління
                  </p>
                </CardContent>
              </Card>
            </Link>
          )
        })}
      </div>
    </div>
  )
}
