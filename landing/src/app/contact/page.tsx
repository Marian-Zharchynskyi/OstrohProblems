"use client"

import { useState } from 'react'
import { Card, CardContent } from '@/components/ui/card'
import { MapPin } from 'lucide-react'

export default function ContactPage() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    subject: '',
    phone: '',
    message: '',
  })

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target
    setFormData((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    alert("Дякуємо за вашу заявку! Ми зв'яжемося з вами найближчим часом.")
    setFormData({ name: '', email: '', subject: '', phone: '', message: '' })
  }

  const handleMapRedirect = () => {
    const ostrohCoords = '50.3275,26.5125'
    window.open(`https://www.google.com/maps?q=${ostrohCoords}`, '_blank')
  }

  return (
    <div className="space-y-12 max-w-5xl mx-auto">
      {/* Header */}
      <section className="text-center">
        <h1 className="font-heading text-4xl font-bold mb-4">Контакти</h1>
        <p className="text-lg text-muted-foreground">
          Зв&apos;яжіться з нами, якщо у вас є питання або пропозиції
        </p>
      </section>

      {/* Contact Info */}
      <section>
        <Card className="overflow-hidden border border-muted bg-background">
          <div className="flex flex-col md:flex-row">
            <div className="md:w-1/2">
              {/* eslint-disable-next-line @next/next/no-img-element */}
              <img
                src="https://img.freepik.com/free-vector/hand-drawn-compliment-illustration_52683-107992.jpg?t=st=1740341293~exp=1740344893~hmac=4a1a610d6a546ae274b0c3a2dfadbd21b2739787c7f9f847b27a08e2a1dfb0da&w=900"
                alt="Happy person giving thumbs up"
                className="h-full w-full object-cover"
              />
            </div>
            <CardContent className="md:w-1/2 border-l-0 md:border-l-2 border-primary p-6 flex flex-col justify-center space-y-3">
              <h2 className="font-heading text-2xl font-bold text-slate-900 mb-1">
                Наші контакти
              </h2>
              <p className="text-sm text-muted-foreground">
                м. Острог – вул. Академічна, 2
              </p>
              <p className="text-sm text-muted-foreground">+380 67 123 45 67</p>
              <p className="text-sm text-muted-foreground">info@ostrogbetter.ua</p>
              <button
                type="button"
                onClick={handleMapRedirect}
                className="inline-flex items-center gap-2 text-sm font-medium text-rose-600 hover:text-rose-700 mt-2"
              >
                <MapPin className="h-4 w-4" />
                <span>Переглянути на мапі</span>
              </button>
            </CardContent>
          </div>
        </Card>
      </section>

      {/* Feedback Form */}
      <section>
        <Card className="border border-muted bg-background">
          <CardContent className="p-6">
            <h2 className="font-heading text-2xl font-bold text-slate-900 mb-6">
              Поділіться думкою
            </h2>
            <form onSubmit={handleSubmit} className="space-y-6">
              <div className="grid gap-6 md:grid-cols-2">
                <div className="space-y-2">
                  <label className="block text-sm font-medium text-slate-800">
                    Ваше ім&apos;я <span className="text-rose-600">*</span>
                  </label>
                  <input
                    type="text"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    required
                    className="w-full rounded-md border border-input px-3 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring"
                    placeholder="Введіть тут"
                  />
                </div>
                <div className="space-y-2">
                  <label className="block text-sm font-medium text-slate-800">
                    Email <span className="text-rose-600">*</span>
                  </label>
                  <input
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                    className="w-full rounded-md border border-input px-3 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring"
                    placeholder="Введіть тут"
                  />
                </div>
              </div>

              <div className="grid gap-6 md:grid-cols-2">
                <div className="space-y-2">
                  <label className="block text-sm font-medium text-slate-800">
                    Тема <span className="text-rose-600">*</span>
                  </label>
                  <input
                    type="text"
                    name="subject"
                    value={formData.subject}
                    onChange={handleChange}
                    required
                    className="w-full rounded-md border border-input px-3 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring"
                    placeholder="Введіть тут"
                  />
                </div>
                <div className="space-y-2">
                  <label className="block text-sm font-medium text-slate-800">
                    Номер телефону
                  </label>
                  <input
                    type="tel"
                    name="phone"
                    value={formData.phone}
                    onChange={handleChange}
                    className="w-full rounded-md border border-input px-3 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring"
                    placeholder="Введіть тут"
                  />
                </div>
              </div>

              <div className="space-y-2">
                <label className="block text-sm font-medium text-slate-800">
                  Повідомлення <span className="text-rose-600">*</span>
                </label>
                <textarea
                  name="message"
                  value={formData.message}
                  onChange={handleChange}
                  required
                  className="min-h-[140px] w-full rounded-md border border-input px-3 py-2 text-sm focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring"
                  placeholder="Напишіть, що ви хочете сказати нам"
                />
              </div>

              <div className="flex justify-end">
                <button
                  type="submit"
                  className="inline-flex items-center rounded-md bg-primary px-6 py-2 text-sm font-medium text-primary-foreground hover:bg-primary/90"
                >
                  Надіслати
                </button>
              </div>
            </form>
          </CardContent>
        </Card>
      </section>
    </div>
  )
}
