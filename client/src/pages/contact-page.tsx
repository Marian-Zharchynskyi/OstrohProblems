import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Mail, MapPin, Phone, Clock } from 'lucide-react'

export function ContactPage() {
  return (
    <div className="space-y-12 max-w-4xl mx-auto">
      {/* Header */}
      <section className="text-center">
        <h1 className="font-heading text-4xl font-bold mb-4">Контакти</h1>
        <p className="text-lg text-muted-foreground">
          Зв'яжіться з нами, якщо у вас є питання або пропозиції
        </p>
      </section>

      {/* Contact Cards */}
      <section className="grid gap-6 md:grid-cols-2">
        <Card>
          <CardHeader>
            <div className="flex items-center gap-3 mb-2">
              <div className="p-3 rounded-lg bg-primary/10">
                <MapPin className="h-6 w-6 text-primary" />
              </div>
              <CardTitle className="font-heading">Адреса</CardTitle>
            </div>
            <CardDescription className="text-base">
              м. Острог, Рівненська область, Україна
            </CardDescription>
          </CardHeader>
        </Card>

        <Card>
          <CardHeader>
            <div className="flex items-center gap-3 mb-2">
              <div className="p-3 rounded-lg bg-primary/10">
                <Phone className="h-6 w-6 text-primary" />
              </div>
              <CardTitle className="font-heading">Телефон</CardTitle>
            </div>
            <CardDescription className="text-base">
              +380 XX XXX XX XX
            </CardDescription>
          </CardHeader>
        </Card>

        <Card>
          <CardHeader>
            <div className="flex items-center gap-3 mb-2">
              <div className="p-3 rounded-lg bg-primary/10">
                <Mail className="h-6 w-6 text-primary" />
              </div>
              <CardTitle className="font-heading">Email</CardTitle>
            </div>
            <CardDescription className="text-base">
              info@ostrohrazom.ua
            </CardDescription>
          </CardHeader>
        </Card>

        <Card>
          <CardHeader>
            <div className="flex items-center gap-3 mb-2">
              <div className="p-3 rounded-lg bg-primary/10">
                <Clock className="h-6 w-6 text-primary" />
              </div>
              <CardTitle className="font-heading">Графік роботи</CardTitle>
            </div>
            <CardDescription className="text-base">
              Пн-Пт: 9:00 - 18:00
              <br />
              Сб-Нд: Вихідний
            </CardDescription>
          </CardHeader>
        </Card>
      </section>

      {/* Contact Form */}
      <section>
        <Card>
          <CardHeader>
            <CardTitle className="font-heading text-2xl">Напишіть нам</CardTitle>
            <CardDescription>
              Заповніть форму нижче, і ми зв'яжемося з вами найближчим часом
            </CardDescription>
          </CardHeader>
          <CardContent>
            <form className="space-y-4">
              <div>
                <label htmlFor="name" className="block text-sm font-medium mb-2">
                  Ім'я
                </label>
                <input
                  type="text"
                  id="name"
                  className="w-full px-3 py-2 border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                  placeholder="Ваше ім'я"
                />
              </div>
              <div>
                <label htmlFor="email" className="block text-sm font-medium mb-2">
                  Email
                </label>
                <input
                  type="email"
                  id="email"
                  className="w-full px-3 py-2 border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                  placeholder="your@email.com"
                />
              </div>
              <div>
                <label htmlFor="subject" className="block text-sm font-medium mb-2">
                  Тема
                </label>
                <input
                  type="text"
                  id="subject"
                  className="w-full px-3 py-2 border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                  placeholder="Тема повідомлення"
                />
              </div>
              <div>
                <label htmlFor="message" className="block text-sm font-medium mb-2">
                  Повідомлення
                </label>
                <textarea
                  id="message"
                  rows={5}
                  className="w-full px-3 py-2 border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                  placeholder="Ваше повідомлення..."
                />
              </div>
              <button
                type="submit"
                className="w-full bg-primary text-primary-foreground px-4 py-2 rounded-md hover:bg-primary/90 transition-colors font-medium"
              >
                Надіслати
              </button>
            </form>
          </CardContent>
        </Card>
      </section>
    </div>
  )
}
