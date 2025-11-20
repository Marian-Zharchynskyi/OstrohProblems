import { Link } from 'react-router-dom'
import { useState } from 'react'
import { Button } from '@/components/ui/button'
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '@/components/ui/card'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog'

interface ServiceInfo {
  title: string
  body: string
}

const services: ServiceInfo[] = [
  {
    title: 'Вивчення проблем міста',
    body:
      'Ми аналізуємо та вивчаємо проблеми міста, щоб знайти найкращі рішення для їх вирішення. Наша команда працює над збором даних, проведенням опитувань та аналізом інформації для розробки ефективних стратегій.',
  },
  {
    title: 'Голосування за ініціативи',
    body:
      'Ми організовуємо голосування за ініціативи, щоб залучити громаду до прийняття важливих рішень. Кожен мешканець може висловити свою думку та вплинути на розвиток міста, підтримуючи ті ініціативи, які вважає найважливішими.',
  },
  {
    title: 'Аналітика та статистика',
    body:
      'Ми надаємо аналітичні та статистичні дані, щоб допомогти зрозуміти поточний стан міста та його потреби. Наші звіти включають детальну інформацію про різні аспекти міського життя, що дозволяє приймати обґрунтовані рішення.',
  },
]

export function PublicHomePage() {
  const [selectedService, setSelectedService] = useState<ServiceInfo | null>(null)
  const [isServiceDialogOpen, setIsServiceDialogOpen] = useState(false)
  const [isSupportDialogOpen, setIsSupportDialogOpen] = useState(false)

  const openServiceDialog = (service: ServiceInfo) => {
    setSelectedService(service)
    setIsServiceDialogOpen(true)
  }

  return (
    <div className="space-y-16">
      {/* Hero Section */}
      <section className="relative min-h-[70vh] overflow-hidden rounded-xl bg-black text-white">
        <div
          className="absolute inset-0 bg-cover bg-center opacity-60"
          style={{
            backgroundImage:
              "url('https://i.ytimg.com/vi/UGxWIEgcWZk/maxresdefault.jpg')",
          }}
        />
        <div className="absolute inset-0 bg-gradient-to-r from-black/80 via-black/60 to-black/50" />
        <div className="relative z-10 flex min-h-[70vh] items-center px-6 py-16 md:px-12 lg:px-20">
          <div className="max-w-2xl space-y-6">
            <h1 className="font-heading text-4xl md:text-5xl lg:text-6xl font-bold leading-tight">
              Зробимо Острог кращим разом
            </h1>
            <p className="text-lg md:text-xl text-gray-100/90">
              Подавайте проблеми, долучайтесь до змін, створюйте успішне місто вже
              сьогодні.
            </p>
            <div className="flex flex-wrap gap-4 pt-4">
              <Button asChild size="lg" className="bg-rose-600 hover:bg-rose-700">
                <Link to="/problems/create">ПОДАТИ ПРОПОЗИЦІЮ</Link>
              </Button>
              <Button asChild variant="outline" size="lg" className="border-gray-300 bg-white/10 text-white hover:bg-white/20">
                <Link to="/register">Приєднатися</Link>
              </Button>
            </div>
          </div>
        </div>
      </section>

      {/* Partnerships Section */}
      <section className="space-y-8">
        <div className="text-center">
          <h2 className="font-heading text-3xl md:text-4xl font-bold text-slate-900">
            Співпраця з партнерами
          </h2>
        </div>

        <div className="grid gap-6 md:grid-cols-3">
          {/* Міська адміністрація */}
          <Card className="relative h-[320px] overflow-hidden border-0 shadow-md">
            <div
              className="absolute inset-0 bg-cover bg-center"
              style={{
                backgroundImage:
                  "url('https://ostrog.rayon.in.ua/storage/cache/images/upload/news/8/2021-07/29-dnNBRTxZ/700x371-610293e5f1a6f.webp')",
              }}
            />
            <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/60 to-black/40" />
            <CardHeader className="relative z-10 h-full justify-end text-white">
              <CardTitle className="font-heading text-2xl mb-4">
                Міська адміністрація
              </CardTitle>
              <CardDescription className="text-base text-gray-100">
                Співпрацюємо з місцевою владою для швидкого вирішення проблем.
              </CardDescription>
            </CardHeader>
          </Card>

          {/* Волонтери */}
          <Card className="relative h-[320px] overflow-hidden border-0 shadow-md">
            <div
              className="absolute inset-0 bg-cover bg-center"
              style={{
                backgroundImage:
                  "url('https://www.prostir.ua/wp-content/uploads/2024/03/IMAGE-2024-03-13-140814.jpg')",
              }}
            />
            <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/60 to-black/40" />
            <CardHeader className="relative z-10 h-full justify-end text-white">
              <CardTitle className="font-heading text-2xl mb-4">Волонтери</CardTitle>
              <CardDescription className="text-base text-gray-100">
                Активісти, які допомагають у вирішенні питань громади.
              </CardDescription>
            </CardHeader>
          </Card>

          {/* Місцевий бізнес */}
          <Card className="relative h-[320px] overflow-hidden border-0 shadow-md">
            <div
              className="absolute inset-0 bg-cover bg-center"
              style={{
                backgroundImage:
                  "url('https://suitt.edu.ua/wp-content/uploads/2022/11/BSC-1.jpg')",
              }}
            />
            <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/60 to-black/40" />
            <CardHeader className="relative z-10 h-full justify-end text-white">
              <CardTitle className="font-heading text-2xl mb-4">
                Місцевий бізнес
              </CardTitle>
              <CardDescription className="text-base text-gray-100">
                Підприємства, які підтримують розвиток громади.
              </CardDescription>
            </CardHeader>
          </Card>
        </div>

        <div className="text-center">
          <Dialog open={isSupportDialogOpen} onOpenChange={setIsSupportDialogOpen}>
            <DialogTrigger>
              <Button
                size="lg"
                className="bg-rose-600 px-8 font-medium text-white hover:bg-rose-700"
              >
                Підтримати
              </Button>
            </DialogTrigger>
            <DialogContent>
              <DialogHeader>
                <DialogTitle>Підтримати проєкт</DialogTitle>
                <DialogDescription>
                  Дякуємо за вашу підтримку! Ви можете підтримати наш проєкт
                  наступними способами:
                </DialogDescription>
              </DialogHeader>
              <div className="space-y-2 text-sm text-muted-foreground">
                <ul className="list-disc pl-5 space-y-1">
                  <li>Переказ коштів на рахунок: 1234567890</li>
                  <li>Зв'язатися з нами за телефоном: +380 362 554 123</li>
                  <li>Написати на email: support@ostroh.ua</li>
                </ul>
              </div>
            </DialogContent>
          </Dialog>
        </div>
      </section>

      {/* Services Section */}
      <section className="space-y-8">
        <div className="text-center space-y-2">
          <h2 className="font-heading text-3xl md:text-4xl font-bold text-rose-600">
            Послуги
          </h2>
          <p className="font-heading text-xl md:text-2xl font-semibold text-slate-900">
            Наші найкращі послуги для покращення міста
          </p>
        </div>

        <div className="space-y-4">
          {services.map((service) => (
            <Card key={service.title} className="bg-slate-50">
              <CardContent className="flex flex-col items-center justify-between gap-4 py-4 md:flex-row">
                <h3 className="font-heading text-lg md:text-xl font-semibold text-slate-900 text-center md:text-left">
                  {service.title}
                </h3>
                <button
                  type="button"
                  onClick={() => openServiceDialog(service)}
                  className="text-sm font-medium text-muted-foreground hover:text-rose-600"
                >
                  Детальніше
                  <span className="ml-1 align-middle">→</span>
                </button>
              </CardContent>
            </Card>
          ))}
        </div>

        <Dialog open={isServiceDialogOpen} onOpenChange={setIsServiceDialogOpen}>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>{selectedService?.title}</DialogTitle>
              {selectedService && (
                <DialogDescription className="whitespace-pre-line">
                  {selectedService.body}
                </DialogDescription>
              )}
            </DialogHeader>
          </DialogContent>
        </Dialog>
      </section>
    </div>
  )
}
