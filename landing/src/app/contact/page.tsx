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
    <div className="space-y-12 max-w-7xl mx-auto">
      {/* Contact Info Section - Split Screen */}
      <section className="bg-white rounded-lg overflow-hidden">
        <div className="flex flex-col md:flex-row items-center">
          {/* Left Column - Illustration (40-50% width) */}
          <div className="w-full md:w-[45%] flex justify-center items-center">
            {/* eslint-disable-next-line @next/next/no-img-element */}
            <img
              src="/images/contacts_img.png"
              alt="Happy person giving thumbs up"
              className="w-[70%] h-auto object-contain"
            />
          </div>

          {/* Right Column - Content */}
          <div
            className="w-full md:w-[55%] px-8 md:px-24 pt-4 pb-8 flex flex-col items-start justify-center"
            style={{
              border: '3px solid #E5E5E5'
            }}
          >
            {/* Title */}
            <h1
              className="text-4xl md:text-5xl mb-12"
              style={{
                fontFamily: 'Sora, sans-serif',
                fontWeight: 600,
                color: '#1F2732'
              }}
            >
              Наші контакти
            </h1>

            {/* Main Contact Block with Pink Vertical Line */}
            <div className="flex gap-6 mb-10">
              {/* Pink Vertical Line */}
              <div
                className="w-[3px] flex-shrink-0"
                style={{
                  backgroundColor: '#D44374',
                  transform: 'scaleY(1.3)',
                  transformOrigin: 'center'
                }}
              />

              {/* Contact Information */}
              <div
                className="flex flex-col gap-6"
                style={{
                  fontFamily: 'Mulish, sans-serif',
                  fontWeight: 600,
                  color: '#606060'
                }}
              >
                <p className="text-base md:text-lg leading-relaxed">
                  м. Острог -<br />
                  вул. Академічна, 2
                </p>
                <p className="text-base md:text-lg">
                  +380 67 123 45 67
                </p>
                <p className="text-base md:text-lg">
                  info@ostrohbetter.ua
                </p>
              </div>
            </div>

            {/* Map Link */}
            <button
              type="button"
              onClick={handleMapRedirect}
              className="inline-flex items-center gap-2 text-base md:text-lg font-semibold transition-opacity hover:opacity-80"
              style={{
                color: '#FF2D63',
                fontFamily: 'Mulish, sans-serif'
              }}
            >
              <MapPin className="h-5 w-5" style={{ fill: '#FF2D63' }} />
              <span>Переглянути на мапі</span>
            </button>
          </div>
        </div>
      </section>

      {/* Feedback Form */}
      <section>
        <div
          className="bg-white p-8 md:p-12"
          style={{ border: '3px solid #E5E5E5' }}
        >
          <h2
            className="text-3xl md:text-4xl mb-8"
            style={{
              fontFamily: 'Sora, sans-serif',
              fontWeight: 600,
              color: '#1F2732'
            }}
          >
            Поділіться думкою
          </h2>
          <form onSubmit={handleSubmit} className="space-y-6">
            {/* First Row: Name and Email */}
            <div className="grid gap-6 md:grid-cols-2">
              <div className="space-y-2">
                <label
                  className="block text-sm"
                  style={{
                    fontFamily: 'Mulish, sans-serif',
                    fontWeight: 500,
                    color: '#464646'
                  }}
                >
                  Ваше ім'я <span style={{ color: '#E42556' }}>*</span>
                </label>
                <input
                  type="text"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  required
                  className="w-full px-4 py-3 bg-white focus:outline-none border-[3px] border-[#E5E5E5] focus:border-[#E42556] transition-colors"
                  style={{
                    fontFamily: 'Mulish, sans-serif',
                    fontWeight: 500
                  }}
                  placeholder="Введіть тут"
                />
              </div>
              <div className="space-y-2">
                <label
                  className="block text-sm"
                  style={{
                    fontFamily: 'Mulish, sans-serif',
                    fontWeight: 500,
                    color: '#464646'
                  }}
                >
                  Email <span style={{ color: '#E42556' }}>*</span>
                </label>
                <input
                  type="email"
                  name="email"
                  value={formData.email}
                  onChange={handleChange}
                  required
                  className="w-full px-4 py-3 bg-white focus:outline-none border-[3px] border-[#E5E5E5] focus:border-[#E42556] transition-colors"
                  style={{
                    fontFamily: 'Mulish, sans-serif',
                    fontWeight: 500
                  }}
                  placeholder="Введіть тут"
                />
              </div>
            </div>

            {/* Second Row: Subject and Phone */}
            <div className="grid gap-6 md:grid-cols-2">
              <div className="space-y-2">
                <label
                  className="block text-sm"
                  style={{
                    fontFamily: 'Mulish, sans-serif',
                    fontWeight: 500,
                    color: '#464646'
                  }}
                >
                  Тема <span style={{ color: '#E42556' }}>*</span>
                </label>
                <input
                  type="text"
                  name="subject"
                  value={formData.subject}
                  onChange={handleChange}
                  required
                  className="w-full px-4 py-3 bg-white focus:outline-none border-[3px] border-[#E5E5E5] focus:border-[#E42556] transition-colors"
                  style={{
                    fontFamily: 'Mulish, sans-serif',
                    fontWeight: 500
                  }}
                  placeholder="Введіть тут"
                />
              </div>
              <div className="space-y-2">
                <label
                  className="block text-sm"
                  style={{
                    fontFamily: 'Mulish, sans-serif',
                    fontWeight: 500,
                    color: '#464646'
                  }}
                >
                  Номер телефону
                </label>
                <input
                  type="tel"
                  name="phone"
                  value={formData.phone}
                  onChange={handleChange}
                  className="w-full px-4 py-3 bg-white focus:outline-none border-[3px] border-[#E5E5E5] focus:border-[#E42556] transition-colors"
                  style={{
                    fontFamily: 'Mulish, sans-serif',
                    fontWeight: 500
                  }}
                  placeholder="Введіть тут"
                />
              </div>
            </div>

            {/* Third Row: Message */}
            <div className="space-y-2">
              <label
                className="block text-sm"
                style={{
                  fontFamily: 'Mulish, sans-serif',
                  fontWeight: 500,
                  color: '#464646'
                }}
              >
                Повідомлення <span style={{ color: '#E42556' }}>*</span>
              </label>
              <textarea
                name="message"
                value={formData.message}
                onChange={handleChange}
                required
                className="min-h-[140px] w-full px-4 py-3 bg-white focus:outline-none border-[3px] border-[#E5E5E5] focus:border-[#E42556] transition-colors"
                style={{
                  fontFamily: 'Mulish, sans-serif',
                  fontWeight: 500
                }}
                placeholder="Подумайте, що ви хотіли сказати нам"
              />
            </div>

            {/* Submit Button */}
            <div className="flex justify-end">
              <button
                type="submit"
                className="px-8 py-3 transition-opacity hover:opacity-90"
                style={{
                  backgroundColor: '#E42556',
                  opacity: 0.8,
                  borderRadius: '20px',
                  fontFamily: 'Mulish, sans-serif',
                  fontWeight: 800,
                  color: '#FFFFFF'
                }}
              >
                Надіслати
              </button>
            </div>
          </form>
        </div>
      </section>
    </div>
  )
}
