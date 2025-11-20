import { useState } from 'react'
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
  const [activeGoal, setActiveGoal] = useState<string | null>(null)
  const [activeAdvice, setActiveAdvice] = useState<string | null>(null)

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

      {/* Goals & Tasks (from old OurGoals) */}
      <section className="grid gap-8 md:grid-cols-[minmax(0,1fr)_minmax(0,2fr)] items-start">
        <div className="space-y-2">
          <h2 className="font-heading text-4xl font-bold text-muted-foreground">Цілі</h2>
          <h2 className="font-heading text-4xl font-bold text-muted-foreground">та</h2>
          <h2 className="font-heading text-4xl font-bold text-muted-foreground">завдання</h2>
        </div>

        <div className="space-y-3">
          <button
            type="button"
            onClick={() => setActiveGoal(activeGoal === '0' ? null : '0')}
            className="flex w-full items-center justify-between rounded-md border bg-background px-4 py-3 text-left text-sm font-medium hover:bg-muted"
          >
            <span>1. Визначення проблем</span>
            <span className="text-lg">{activeGoal === '0' ? '−' : '+'}</span>
          </button>
          {activeGoal === '0' && (
            <div className="rounded-md border border-t-0 bg-muted px-4 py-3 text-sm text-muted-foreground">
              Ми допомагаємо корисно вирішувати глибоко заховані проблеми в Острозі —
              від стану доріг до питань екології та соціальних конфліктів.
            </div>
          )}

          <button
            type="button"
            onClick={() => setActiveGoal(activeGoal === '1' ? null : '1')}
            className="flex w-full items-center justify-between rounded-md border bg-background px-4 py-3 text-left text-sm font-medium hover:bg-muted"
          >
            <span>2. Підтримка локальних ініціатив</span>
            <span className="text-lg">{activeGoal === '1' ? '−' : '+'}</span>
          </button>
          {activeGoal === '1' && (
            <div className="rounded-md border border-t-0 bg-muted px-4 py-3 text-sm text-muted-foreground">
              Ми підтримуємо локальні проєкти мешканців, що спрямовані на розвиток
              міста та покращення якості життя громади.
            </div>
          )}

          <button
            type="button"
            onClick={() => setActiveGoal(activeGoal === '2' ? null : '2')}
            className="flex w-full items-center justify-between rounded-md border bg-background px-4 py-3 text-left text-sm font-medium hover:bg-muted"
          >
            <span>3. Співпраця з владою</span>
            <span className="text-lg">{activeGoal === '2' ? '−' : '+'}</span>
          </button>
          {activeGoal === '2' && (
            <div className="rounded-md border border-t-0 bg-muted px-4 py-3 text-sm text-muted-foreground">
              Працюємо разом з місцевою владою для прозорої та ефективної комунікації
              щодо міських проблем.
            </div>
          )}

          <button
            type="button"
            onClick={() => setActiveGoal(activeGoal === '3' ? null : '3')}
            className="flex w-full items-center justify-between rounded-md border bg-background px-4 py-3 text-left text-sm font-medium hover:bg-muted"
          >
            <span>4. Підвищення обізнаності</span>
            <span className="text-lg">{activeGoal === '3' ? '−' : '+'}</span>
          </button>
          {activeGoal === '3' && (
            <div className="rounded-md border border-t-0 bg-muted px-4 py-3 text-sm text-muted-foreground">
              Поширюємо інформацію про важливі ініціативи та проблеми, щоб залучити
              якомога більше мешканців до спільної роботи.
            </div>
          )}
        </div>
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

      {/* Tips for reporting problems (from old ProblemAdvices) */}
      <section className="space-y-4">
        <div>
          <h2 className="font-heading text-2xl font-bold text-slate-900">
            Поради щодо подання проблем
          </h2>
          <p className="text-sm text-muted-foreground">
            Дізнайтесь, які деталі важливо вказати для ефективної комунікації та як
            чітко сформувати проблему, щоб її швидше вирішували.
          </p>
        </div>

        <div className="space-y-3">
          <button
            type="button"
            onClick={() => setActiveAdvice(activeAdvice === '0' ? null : '0')}
            className="flex w-full items-center justify-between rounded-md border bg-background px-4 py-3 text-left text-sm font-medium hover:bg-muted"
          >
            <span>Глибокий аналіз проблем</span>
            <span className="text-lg">{activeAdvice === '0' ? '∨' : '∧'}</span>
          </button>
          {activeAdvice === '0' && (
            <div className="rounded-md border border-t-0 bg-muted px-4 py-3 text-sm text-muted-foreground space-y-3">
              <p>
                Дізнайтесь, які деталі важливо врахувати для ефективного вирішення
                проблем, щоб покращити якість міського простору.
              </p>
              <button className="inline-flex items-center rounded-md bg-primary px-3 py-1 text-xs font-medium text-primary-foreground hover:bg-primary/90">
                Більше
              </button>
            </div>
          )}

          <button
            type="button"
            onClick={() => setActiveAdvice(activeAdvice === '1' ? null : '1')}
            className="flex w-full items-center justify-between rounded-md border bg-background px-4 py-3 text-left text-sm font-medium hover:bg-muted"
          >
            <span>Як правильно описати проблему</span>
            <span className="text-lg">{activeAdvice === '1' ? '∨' : '∧'}</span>
          </button>
          {activeAdvice === '1' && (
            <div className="rounded-md border border-t-0 bg-muted px-4 py-3 text-sm text-muted-foreground space-y-3">
              <p>
                Описуйте проблему чітко та по суті: де саме вона знаходиться, скільки
                триває, які наслідки має для мешканців. Це допомагає швидше знайти
                ефективне рішення.
              </p>
              <button className="inline-flex items-center rounded-md bg-primary px-3 py-1 text-xs font-medium text-primary-foreground hover:bg-primary/90">
                Більше
              </button>
            </div>
          )}

          <button
            type="button"
            onClick={() => setActiveAdvice(activeAdvice === '2' ? null : '2')}
            className="flex w-full items-center justify-between rounded-md border bg-background px-4 py-3 text-left text-sm font-medium hover:bg-muted"
          >
            <span>Що робити до оцінки для кращого результату</span>
            <span className="text-lg">{activeAdvice === '2' ? '∨' : '∧'}</span>
          </button>
          {activeAdvice === '2' && (
            <div className="rounded-md border border-t-0 bg-muted px-4 py-3 text-sm text-muted-foreground space-y-3">
              <p>
                Збирайте фото, точні координати та іншу важливу інформацію до подання
                проблеми — це значно пришвидшить її розгляд.
              </p>
              <button className="inline-flex items-center rounded-md bg-primary px-3 py-1 text-xs font-medium text-primary-foreground hover:bg-primary/90">
                Більше
              </button>
            </div>
          )}

          <button
            type="button"
            onClick={() => setActiveAdvice(activeAdvice === '3' ? null : '3')}
            className="flex w-full items-center justify-between rounded-md border bg-background px-4 py-3 text-left text-sm font-medium hover:bg-muted"
          >
            <span>Фотографії проблем: як зображати їх корисно?</span>
            <span className="text-lg">{activeAdvice === '3' ? '∨' : '∧'}</span>
          </button>
          {activeAdvice === '3' && (
            <div className="rounded-md border border-t-0 bg-muted px-4 py-3 text-sm text-muted-foreground space-y-3">
              <p>
                Робіть фото з різних ракурсів, додавайте загальний план і деталі —
                так простіше оцінити масштаб та пріоритет проблеми.
              </p>
              <button className="inline-flex items-center rounded-md bg-primary px-3 py-1 text-xs font-medium text-primary-foreground hover:bg-primary/90">
                Більше
              </button>
            </div>
          )}
        </div>
      </section>
    </div>
  )
}
