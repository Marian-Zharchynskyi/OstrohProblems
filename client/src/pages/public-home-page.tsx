import { Link } from 'react-router-dom'
import { Card, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { MapPin, MessageSquare, Users, TrendingUp } from 'lucide-react'

const features = [
  {
    title: 'Інтерактивна карта',
    description: 'Переглядайте проблеми на карті міста та додавайте нові',
    icon: MapPin,
    color: 'text-blue-500',
  },
  {
    title: 'Коментарі та обговорення',
    description: 'Обговорюйте проблеми з іншими мешканцями',
    icon: MessageSquare,
    color: 'text-green-500',
  },
  {
    title: 'Спільнота',
    description: 'Приєднуйтесь до активних мешканців міста',
    icon: Users,
    color: 'text-purple-500',
  },
  {
    title: 'Відстеження прогресу',
    description: 'Слідкуйте за вирішенням проблем у реальному часі',
    icon: TrendingUp,
    color: 'text-orange-500',
  },
]

export function PublicHomePage() {
  return (
    <div className="space-y-12">
      {/* Hero Section */}
      <section className="text-center py-12">
        <h1 className="font-heading text-5xl font-bold tracking-tight mb-4">
          Острог разом
        </h1>
        <p className="text-xl text-muted-foreground mb-8 max-w-2xl mx-auto">
          Платформа для звітування та вирішення проблем у нашому місті.
          Разом ми робимо Острог кращим місцем для життя.
        </p>
        <div className="flex gap-4 justify-center">
          <Link
            to="/register"
            className="bg-primary text-primary-foreground px-6 py-3 rounded-md hover:bg-primary/90 transition-colors font-medium"
          >
            Приєднатися
          </Link>
          <Link
            to="/login"
            className="border border-primary text-primary px-6 py-3 rounded-md hover:bg-primary/10 transition-colors font-medium"
          >
            Увійти
          </Link>
        </div>
      </section>

      {/* Features Section */}
      <section>
        <h2 className="font-heading text-3xl font-bold text-center mb-8">
          Можливості платформи
        </h2>
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-4">
          {features.map((feature) => {
            const Icon = feature.icon
            return (
              <Card key={feature.title} className="text-center">
                <CardHeader>
                  <div className="flex justify-center mb-4">
                    <div className={`p-4 rounded-lg bg-muted ${feature.color}`}>
                      <Icon className="h-8 w-8" />
                    </div>
                  </div>
                  <CardTitle className="font-heading">{feature.title}</CardTitle>
                  <CardDescription>{feature.description}</CardDescription>
                </CardHeader>
              </Card>
            )
          })}
        </div>
      </section>

      {/* CTA Section */}
      <section className="bg-muted rounded-lg p-12 text-center">
        <h2 className="font-heading text-3xl font-bold mb-4">
          Готові зробити свій внесок?
        </h2>
        <p className="text-muted-foreground mb-6 max-w-xl mx-auto">
          Зареєструйтесь зараз та почніть повідомляти про проблеми у вашому районі.
          Кожен голос має значення!
        </p>
        <Link
          to="/register"
          className="inline-block bg-primary text-primary-foreground px-6 py-3 rounded-md hover:bg-primary/90 transition-colors font-medium"
        >
          Створити акаунт
        </Link>
      </section>
    </div>
  )
}
