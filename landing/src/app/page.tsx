'use client'

import { useState } from 'react'
import Link from 'next/link'
import Image from 'next/image'
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
import { designSystem } from '@/lib/design-system'
import { ArrowRight } from 'lucide-react'

interface ServiceInfo {
  title: string
  body: string
}

const services: ServiceInfo[] = [
  {
    title: 'Виявлення проблем міста',
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

export default function HomePage() {
  const appUrl = process.env.NEXT_PUBLIC_APP_URL || 'http://localhost:3000'
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
      <section
        className="relative overflow-hidden px-6 py-8 md:py-10 lg:py-12"
        style={{
          background: designSystem.colors.hero.backgroundGradient,
          borderRadius: designSystem.borderRadius.floating,
        }}
      >
        <div className="container mx-auto max-w-7xl">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 lg:gap-12 items-center">
            {/* Текстова частина (зліва) */}
            <div className="space-y-6 lg:pr-8">
              <h1
                className="text-4xl md:text-5xl lg:text-6xl leading-tight"
                style={{
                  fontFamily: designSystem.typography.fontFamily.heading,
                  fontWeight: designSystem.typography.weights.bold,
                  color: designSystem.colors.hero.headingText,
                }}
              >
                Зробимо Острог кращим разом
              </h1>
              <p
                className="text-lg md:text-xl leading-relaxed"
                style={{
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.bold,
                  color: designSystem.colors.hero.bodyText,
                }}
              >
                Подавайте проблеми, долучайтесь до змін, створюйте успішне місто вже сьогодні
              </p>
              <div className="pt-4">
                <a
                  href={`${appUrl}/problems/create`}
                  className="inline-block px-8 py-4 uppercase tracking-wide transition-all duration-300 hover:scale-105"
                  style={{
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.extrabold,
                    color: designSystem.colors.hero.ctaButtonText,
                    backgroundColor: designSystem.colors.hero.ctaButton,
                    borderRadius: designSystem.borderRadius.button,
                    boxShadow: designSystem.shadows.button,
                  }}
                  onMouseEnter={(e) => {
                    e.currentTarget.style.backgroundColor = designSystem.colors.hero.ctaButtonHover
                    e.currentTarget.style.boxShadow = designSystem.shadows.buttonHover
                  }}
                  onMouseLeave={(e) => {
                    e.currentTarget.style.backgroundColor = designSystem.colors.hero.ctaButton
                    e.currentTarget.style.boxShadow = designSystem.shadows.button
                  }}
                >
                  Подати проблему
                </a>
              </div>
            </div>

            {/* Ілюстрація (справа) */}
            <div className="relative w-full h-[250px] md:h-[320px] lg:h-[400px]">
              <Image
                src="/images/hero_section_cover.png"
                alt="Острозька академія - архітектурний ансамбль"
                fill
                className="object-contain"
                priority
              />
            </div>
          </div>
        </div>
      </section>

      {/* Partnerships Section */}
      <section className="space-y-8">
        <div>
          <h2
            className="text-3xl md:text-4xl"
            style={{
              fontFamily: designSystem.typography.fontFamily.heading,
              fontWeight: designSystem.typography.weights.semibold,
              color: designSystem.colors.partnerships.sectionTitle,
            }}
          >
            Співпраця з партнерами
          </h2>
        </div>

        <div className="grid gap-6 md:grid-cols-3 justify-items-center">
          {/* Міська адміністрація */}
          <div
            className="relative overflow-hidden p-6 flex flex-col w-[350px] h-[500px]"
            style={{
              backgroundColor: designSystem.colors.partnerships.cardBackground,
              borderRadius: designSystem.borderRadius.card,
              boxShadow: designSystem.shadows.card,
            }}
          >
            {/* Header with title and icon */}
            <div className="flex justify-between items-start mb-4">
              <h3
                className="text-xl"
                style={{
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.bold,
                  color: designSystem.colors.partnerships.cardTitle,
                }}
              >
                Міська адміністрація
              </h3>
              {/* Building icon */}
              <svg
                width="32"
                height="32"
                viewBox="0 0 32 32"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M4 28H28M6 28V12L16 6L26 12V28M10 16H12V18H10V16ZM10 20H12V22H10V20ZM10 24H12V26H10V24ZM14 16H16V18H14V16ZM14 20H16V22H14V20ZM14 24H16V26H14V24ZM18 16H20V18H18V16ZM18 20H20V22H18V20ZM18 24H20V26H18V24ZM22 16H24V18H22V16ZM22 20H24V22H22V20ZM22 24H24V26H22V24Z"
                  stroke={designSystem.colors.partnerships.iconColor}
                  strokeWidth="2"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
              </svg>
            </div>

            {/* Image */}
            <div className="relative w-full h-40 mb-4">
              <Image
                src="/images/cards/government.jpg"
                alt="Міська адміністрація"
                fill
                className="object-cover rounded-lg"
              />
            </div>

            {/* Description */}
            <p
              className="text-base"
              style={{
                fontFamily: designSystem.typography.fontFamily.body,
                fontWeight: designSystem.typography.weights.semibold,
                color: designSystem.colors.partnerships.cardDescription,
              }}
            >
              Співпрацюємо з місцевою владою для швидкого вирішення
            </p>
          </div>

          {/* Волонтери */}
          <div
            className="relative overflow-hidden p-6 flex flex-col w-[350px] h-[500px]"
            style={{
              backgroundColor: designSystem.colors.partnerships.cardBackground,
              borderRadius: designSystem.borderRadius.card,
              boxShadow: designSystem.shadows.card,
            }}
          >
            {/* Header with title and icon */}
            <div className="flex justify-between items-start mb-4">
              <h3
                className="text-xl"
                style={{
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.bold,
                  color: designSystem.colors.partnerships.cardTitle,
                }}
              >
                Волонтери
              </h3>
              {/* Hand with heart icon */}
              <svg
                width="32"
                height="32"
                viewBox="0 0 32 32"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M16 28C16 28 4 21 4 12C4 9.87827 4.84285 7.84344 6.34315 6.34315C7.84344 4.84285 9.87827 4 12 4C13.5913 4 15.1174 4.63214 16.3137 5.75736C17.51 6.88258 18.2929 8.40869 18.5 10C18.7071 8.40869 19.49 6.88258 20.6863 5.75736C21.8826 4.63214 23.4087 4 25 4C27.1217 4 29.1566 4.84285 30.6569 6.34315C32.1571 7.84344 33 9.87827 33 12C33 21 21 28 16 28Z"
                  stroke={designSystem.colors.partnerships.iconColor}
                  strokeWidth="2"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <path
                  d="M8 24C8 24 6 26 4 28M12 22L10 26M20 22L22 26M24 24C24 24 26 26 28 28"
                  stroke={designSystem.colors.partnerships.iconColor}
                  strokeWidth="2"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
              </svg>
            </div>

            {/* Image */}
            <div className="relative w-full h-40 mb-4">
              <Image
                src="/images/cards/volunteers.jpg"
                alt="Волонтери"
                fill
                className="object-cover rounded-lg"
              />
            </div>

            {/* Description */}
            <p
              className="text-base"
              style={{
                fontFamily: designSystem.typography.fontFamily.body,
                fontWeight: designSystem.typography.weights.semibold,
                color: designSystem.colors.partnerships.cardDescription,
              }}
            >
              Активісти, які допомагають у вирішенні питань громади
            </p>
          </div>

          {/* Підприємства */}
          <div
            className="relative overflow-hidden p-6 flex flex-col w-[350px] h-[500px]"
            style={{
              backgroundColor: designSystem.colors.partnerships.cardBackground,
              borderRadius: designSystem.borderRadius.card,
              boxShadow: designSystem.shadows.card,
            }}
          >
            {/* Header with title and icon */}
            <div className="flex justify-between items-start mb-4">
              <h3
                className="text-xl"
                style={{
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.bold,
                  color: designSystem.colors.partnerships.cardTitle,
                }}
              >
                Підприємства
              </h3>
              {/* Store icon */}
              <svg
                width="32"
                height="32"
                viewBox="0 0 32 32"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M4 10L6 4H26L28 10M4 10V28H28V10M4 10H28M12 14V18M20 14V18M16 10V4"
                  stroke={designSystem.colors.partnerships.iconColor}
                  strokeWidth="2"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <circle
                  cx="24"
                  cy="8"
                  r="3"
                  stroke={designSystem.colors.partnerships.iconColor}
                  strokeWidth="2"
                />
                <path
                  d="M24 6V10M22 8H26"
                  stroke={designSystem.colors.partnerships.iconColor}
                  strokeWidth="2"
                  strokeLinecap="round"
                />
              </svg>
            </div>

            {/* Image */}
            <div className="relative w-full h-40 mb-4">
              <Image
                src="/images/cards/buisness.jpg"
                alt="Підприємства"
                fill
                className="object-cover rounded-lg"
              />
            </div>

            {/* Description */}
            <p
              className="text-base"
              style={{
                fontFamily: designSystem.typography.fontFamily.body,
                fontWeight: designSystem.typography.weights.semibold,
                color: designSystem.colors.partnerships.cardDescription,
              }}
            >
              Місцевий бізнес, який підтримує розвиток громади
            </p>
          </div>
        </div>

        {/* Support Button */}
        <div className="text-center">
          <Dialog open={isSupportDialogOpen} onOpenChange={setIsSupportDialogOpen}>
            <DialogTrigger asChild>
              <button
                className="transition-all duration-300 hover:opacity-90"
                style={{
                  width: '340px',
                  height: '65px',
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.extrabold,
                  color: designSystem.colors.partnerships.buttonText,
                  backgroundColor: designSystem.colors.partnerships.buttonBackground,
                  borderRadius: designSystem.borderRadius.button,
                }}
              >
                Підтримати
              </button>
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
                  <li>Зв&apos;язатися з нами за телефоном: +380 362 554 123</li>
                  <li>Написати на email: support@ostroh.ua</li>
                </ul>
              </div>
            </DialogContent>
          </Dialog>
        </div>
      </section>

      {/* Services Section */}
      <section className="space-y-12 py-16">
        {/* Header */}
        <div className="space-y-3">
          <p
            className="uppercase tracking-wide"
            style={{
              fontSize: '24px',
              fontFamily: designSystem.typography.fontFamily.body,
              fontWeight: designSystem.typography.weights.semibold,
              color: designSystem.colors.services.eyebrowText,
              lineHeight: '1.2',
            }}
          >
            ПОСЛУГИ
          </p>
          <h2
            style={{
              fontSize: '30px',
              fontFamily: designSystem.typography.fontFamily.heading,
              fontWeight: designSystem.typography.weights.semibold,
              color: designSystem.colors.services.headingText,
              lineHeight: '1.2',
            }}
          >
            Наші найкращі послуги для покращення міста
          </h2>
        </div>

        {/* Service Cards */}
        <div className="space-y-10">
          {services.map((service) => (
            <div
              key={service.title}
              className="flex items-center justify-between px-8 transition-all duration-200 hover:shadow-md cursor-pointer"
              style={{
                height: '117px',
                backgroundColor: designSystem.colors.services.cardBackground,
                borderRadius: designSystem.borderRadius.floating,
              }}
              onClick={() => openServiceDialog(service)}
            >
              {/* Service Title */}
              <h3
                style={{
                  fontSize: '22px',
                  fontFamily: designSystem.typography.fontFamily.heading,
                  fontWeight: designSystem.typography.weights.semibold,
                  color: designSystem.colors.services.cardTitle,
                }}
              >
                {service.title}
              </h3>

              {/* Action Link */}
              <button
                type="button"
                className="flex items-center gap-2 uppercase tracking-wide transition-opacity hover:opacity-70"
                style={{
                  fontSize: '15px',
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.semibold,
                  color: designSystem.colors.services.linkText,
                }}
              >
                Детальніше
                <ArrowRight
                  style={{
                    width: '18px',
                    height: '18px',
                    color: designSystem.colors.services.linkIcon
                  }}
                />
              </button>
            </div>
          ))}
        </div>

        {/* Dialog */}
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
