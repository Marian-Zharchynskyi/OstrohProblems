import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Target, Users, Heart, Lightbulb } from 'lucide-react'

const values = [
  {
    title: 'Наша місія',
    description: 'Створити зручну платформу для взаємодії мешканців Острога з місцевою владою для швидкого вирішення міських проблем.',
    icon: Target,
  },
  {
    title: 'Спільнота',
    description: 'Ми віримо в силу спільноти. Кожен мешканець може зробити свій внесок у покращення міста.',
    icon: Users,
  },
  {
    title: 'Відповідальність',
    description: 'Ми прагнемо до прозорості та відповідальності у вирішенні міських проблем.',
    icon: Heart,
  },
  {
    title: 'Інновації',
    description: 'Використовуємо сучасні технології для покращення якості життя у нашому місті.',
    icon: Lightbulb,
  },
]

export function AboutPage() {
  return (
    <div className="space-y-12 max-w-4xl mx-auto">
      {/* Header */}
      <section className="text-center">
        <h1 className="font-heading text-4xl font-bold mb-4">Про нас</h1>
        <p className="text-lg text-muted-foreground">
          Дізнайтеся більше про платформу "Острог разом"
        </p>
      </section>

      {/* Main Content */}
      <section className="prose prose-lg max-w-none">
        <Card>
          <CardHeader>
            <CardTitle className="font-heading text-2xl">Хто ми?</CardTitle>
          </CardHeader>
          <CardContent className="space-y-4 text-muted-foreground">
            <p>
              "Острог разом" — це громадська платформа, створена для покращення
              комунікації між мешканцями міста Острог та місцевою владою.
            </p>
            <p>
              Ми надаємо зручний інструмент для звітування про проблеми в місті,
              відстеження їх вирішення та участі в обговореннях важливих питань.
            </p>
            <p>
              Наша мета — зробити Острог комфортнішим та безпечнішим місцем для
              життя через активну участь громади у вирішенні міських проблем.
            </p>
          </CardContent>
        </Card>
      </section>

      {/* Values Section */}
      <section>
        <h2 className="font-heading text-3xl font-bold text-center mb-8">
          Наші цінності
        </h2>
        <div className="grid gap-6 md:grid-cols-2">
          {values.map((value) => {
            const Icon = value.icon
            return (
              <Card key={value.title}>
                <CardHeader>
                  <div className="flex items-center gap-4 mb-2">
                    <div className="p-3 rounded-lg bg-primary/10">
                      <Icon className="h-6 w-6 text-primary" />
                    </div>
                    <CardTitle className="font-heading">{value.title}</CardTitle>
                  </div>
                  <CardDescription className="text-base">
                    {value.description}
                  </CardDescription>
                </CardHeader>
              </Card>
            )
          })}
        </div>
      </section>

      {/* How it works */}
      <section>
        <Card>
          <CardHeader>
            <CardTitle className="font-heading text-2xl">Як це працює?</CardTitle>
          </CardHeader>
          <CardContent className="space-y-4">
            <div className="space-y-3">
              <div className="flex gap-4">
                <div className="flex-shrink-0 w-8 h-8 rounded-full bg-primary text-primary-foreground flex items-center justify-center font-bold">
                  1
                </div>
                <div>
                  <h3 className="font-heading font-semibold mb-1">Зареєструйтесь</h3>
                  <p className="text-muted-foreground">
                    Створіть акаунт, щоб отримати доступ до всіх функцій платформи.
                  </p>
                </div>
              </div>
              <div className="flex gap-4">
                <div className="flex-shrink-0 w-8 h-8 rounded-full bg-primary text-primary-foreground flex items-center justify-center font-bold">
                  2
                </div>
                <div>
                  <h3 className="font-heading font-semibold mb-1">Повідомте про проблему</h3>
                  <p className="text-muted-foreground">
                    Додайте проблему на карту з описом, фото та локацією.
                  </p>
                </div>
              </div>
              <div className="flex gap-4">
                <div className="flex-shrink-0 w-8 h-8 rounded-full bg-primary text-primary-foreground flex items-center justify-center font-bold">
                  3
                </div>
                <div>
                  <h3 className="font-heading font-semibold mb-1">Відстежуйте прогрес</h3>
                  <p className="text-muted-foreground">
                    Слідкуйте за статусом вирішення проблеми та беріть участь в обговореннях.
                  </p>
                </div>
              </div>
            </div>
          </CardContent>
        </Card>
      </section>
    </div>
  )
}
