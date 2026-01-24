"use client"

import { useState } from 'react'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { Target, Users, Heart, Lightbulb } from 'lucide-react'
import { designSystem } from '@/lib/design-system'

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

export default function AboutPage() {
  const [activeGoal, setActiveGoal] = useState<string | null>(null)
  const [activeAdvice, setActiveAdvice] = useState<string | null>(null)

  return (
    <div className="space-y-16 max-w-6xl mx-auto">
      {/* Goals & Tasks Section */}
      <section
        className="grid gap-12 md:grid-cols-[minmax(0,1fr)_minmax(0,2fr)] items-start mt-16"
        style={{ backgroundColor: '#FFFFFF' }}
      >
        {/* Left Column - Decorative Title */}
        <div className="flex flex-col items-center md:items-start space-y-1">
          <h2
            className="text-6xl md:text-7xl"
            style={{
              fontFamily: designSystem.typography.fontFamily.heading,
              fontWeight: designSystem.typography.weights.semibold,
              color: '#C2C2C2',
              lineHeight: '1.1',
            }}
          >
            Цілі
          </h2>
          <h2
            className="text-4xl md:text-5xl"
            style={{
              fontFamily: designSystem.typography.fontFamily.heading,
              fontWeight: designSystem.typography.weights.semibold,
              color: '#C2C2C2',
              lineHeight: '1.1',
            }}
          >
            ТА
          </h2>
          <h2
            className="text-6xl md:text-7xl"
            style={{
              fontFamily: designSystem.typography.fontFamily.heading,
              fontWeight: designSystem.typography.weights.semibold,
              color: '#C2C2C2',
              lineHeight: '1.1',
            }}
          >
            Завдання
          </h2>
        </div>

        {/* Right Column - Accordion */}
        <div className="space-y-0">
          {/* Item 1 - Виявлення проблем */}
          {/* Item 1 - Виявлення проблем */}
          <div>
            <button
              type="button"
              onClick={() => setActiveGoal(activeGoal === '0' ? null : '0')}
              className="flex w-full items-center justify-between py-4 text-left transition-colors border-b"
              style={{ borderColor: '#C3C3C3' }}
            >
              <span
                className="text-lg"
                style={{
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.semibold,
                  color: activeGoal === '0' ? '#FF2D63' : '#42545E',
                }}
              >
                1. Виявлення проблем
              </span>
              <span
                className="text-2xl font-bold"
                style={{ color: '#000000' }}
              >
                {activeGoal === '0' ? '−' : '+'}
              </span>
            </button>
            {activeGoal === '0' && (
              <div className="pb-4 pt-4">
                <p
                  className="text-base leading-relaxed"
                  style={{
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.regular,
                    color: '#464646',
                  }}
                >
                  Ми допомагаємо користувачам легко знаходити проблеми в Острозі — від
                  аварійного стану доріг до питань екології чи соціальних конфліктів.
                </p>
              </div>
            )}
          </div>

          {/* Item 2 - Підтримка локальних ініціатив */}
          {/* Item 2 - Підтримка локальних ініціатив */}
          <div>
            <button
              type="button"
              onClick={() => setActiveGoal(activeGoal === '1' ? null : '1')}
              className="flex w-full items-center justify-between py-4 text-left transition-colors border-b"
              style={{ borderColor: '#C3C3C3' }}
            >
              <span
                className="text-lg"
                style={{
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.semibold,
                  color: activeGoal === '1' ? '#FF2D63' : '#42545E',
                }}
              >
                2. Підтримка локальних ініціатив
              </span>
              <span
                className="text-2xl font-bold"
                style={{ color: '#000000' }}
              >
                {activeGoal === '1' ? '−' : '+'}
              </span>
            </button>
            {activeGoal === '1' && (
              <div className="pb-4 pt-4">
                <p
                  className="text-base leading-relaxed"
                  style={{
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.regular,
                    color: '#464646',
                  }}
                >
                  Ми підтримуємо локальні проєкти мешканців, що спрямовані на розвиток
                  міста та покращення якості життя громади.
                </p>
              </div>
            )}
          </div>

          {/* Item 3 - Співпраця з владою */}
          {/* Item 3 - Співпраця з владою */}
          <div>
            <button
              type="button"
              onClick={() => setActiveGoal(activeGoal === '2' ? null : '2')}
              className="flex w-full items-center justify-between py-4 text-left transition-colors border-b"
              style={{ borderColor: '#C3C3C3' }}
            >
              <span
                className="text-lg"
                style={{
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.semibold,
                  color: activeGoal === '2' ? '#FF2D63' : '#42545E',
                }}
              >
                3. Співпраця з владою
              </span>
              <span
                className="text-2xl font-bold"
                style={{ color: '#000000' }}
              >
                {activeGoal === '2' ? '−' : '+'}
              </span>
            </button>
            {activeGoal === '2' && (
              <div className="pb-4 pt-4">
                <p
                  className="text-base leading-relaxed"
                  style={{
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.regular,
                    color: '#464646',
                  }}
                >
                  Працюємо разом з місцевою владою для прозорої та ефективної комунікації
                  щодо міських проблем.
                </p>
              </div>
            )}
          </div>

          {/* Item 4 - Підвищення обізнаності */}
          {/* Item 4 - Підвищення обізнаності */}
          <div>
            <button
              type="button"
              onClick={() => setActiveGoal(activeGoal === '3' ? null : '3')}
              className="flex w-full items-center justify-between py-4 text-left transition-colors border-b"
              style={{ borderColor: '#C3C3C3' }}
            >
              <span
                className="text-lg"
                style={{
                  fontFamily: designSystem.typography.fontFamily.body,
                  fontWeight: designSystem.typography.weights.semibold,
                  color: activeGoal === '3' ? '#FF2D63' : '#42545E',
                }}
              >
                4. Підвищення обізнаності
              </span>
              <span
                className="text-2xl font-bold"
                style={{ color: '#000000' }}
              >
                {activeGoal === '3' ? '−' : '+'}
              </span>
            </button>
            {activeGoal === '3' && (
              <div className="pb-4 pt-4">
                <p
                  className="text-base leading-relaxed"
                  style={{
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.regular,
                    color: '#464646',
                  }}
                >
                  Поширюємо інформацію про важливі ініціативи та проблеми, щоб залучити
                  якомога більше мешканців до спільної роботи.
                </p>
              </div>
            )}
          </div>
        </div>
      </section>

      {/* How It Works - Dark Mode Section */}
      <section className="grid grid-cols-1 md:grid-cols-3 gap-0 md:gap-1">
        {/* Card 1 - Виявлення проблеми */}
        <div
          className="relative overflow-hidden p-8 flex flex-col justify-between min-h-[400px]"
          style={{ backgroundColor: '#000000' }}
        >
          {/* Decorative Number */}
          <div
            className="absolute inset-0 flex items-center justify-center pointer-events-none"
            style={{
              fontSize: '300px',
              fontFamily: designSystem.typography.fontFamily.heading,
              fontWeight: designSystem.typography.weights.bold,
              color: '#000000',
              WebkitTextStroke: '2px #313131',
              lineHeight: '0.75',
              opacity: 0.5,
              transform: 'scaleY(2)',
            }}
          >
            01
          </div>

          {/* Content */}
          <div className="relative z-10 space-y-4">
            <h3
              className="text-2xl"
              style={{
                fontFamily: designSystem.typography.fontFamily.heading,
                fontWeight: designSystem.typography.weights.bold,
                color: '#EAEAEA',
              }}
            >
              Виявлення проблеми
            </h3>
            <p
              className="text-base leading-relaxed"
              style={{
                fontFamily: designSystem.typography.fontFamily.body,
                fontWeight: designSystem.typography.weights.semibold,
                color: '#EAEAEA',
              }}
            >
              Користувачі можуть легко повідомляти про проблеми в місті через зручний інтерфейс платформи, додаючи фото та опис ситуації.
            </p>
          </div>

          {/* Footer Link */}
          <div className="relative z-10 flex items-center gap-2 mt-8">
            <span
              className="text-sm"
              style={{
                fontFamily: designSystem.typography.fontFamily.body,
                fontWeight: designSystem.typography.weights.semibold,
                color: '#EAEAEA',
              }}
            >
              Більше
            </span>
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M1 8H15M15 8L8 1M15 8L8 15"
                stroke="#EAEAEA"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          </div>
        </div>

        {/* Card 2 - Передача інформації */}
        <div
          className="relative overflow-hidden p-8 flex flex-col justify-between min-h-[400px]"
          style={{ backgroundColor: '#232323' }}
        >
          {/* Decorative Number */}
          <div
            className="absolute inset-0 flex items-center justify-center pointer-events-none"
            style={{
              fontSize: '300px',
              fontFamily: designSystem.typography.fontFamily.heading,
              fontWeight: designSystem.typography.weights.bold,
              color: '#232323',
              WebkitTextStroke: '2px #313131',
              lineHeight: '0.75',
              opacity: 0.5,
              transform: 'scaleY(2)',
            }}
          >
            02
          </div>

          {/* Content */}
          <div className="relative z-10 space-y-4">
            <h3
              className="text-2xl"
              style={{
                fontFamily: designSystem.typography.fontFamily.heading,
                fontWeight: designSystem.typography.weights.bold,
                color: '#EAEAEA',
              }}
            >
              Передача інформації
            </h3>
            <p
              className="text-base leading-relaxed"
              style={{
                fontFamily: designSystem.typography.fontFamily.body,
                fontWeight: designSystem.typography.weights.semibold,
                color: '#EAEAEA',
              }}
            >
              Коли проблема відповідає критеріям, вона автоматично передається відповідальним службам для швидкого реагування.
            </p>
          </div>

          {/* Footer Link */}
          <div className="relative z-10 flex items-center gap-2 mt-8">
            <span
              className="text-sm"
              style={{
                fontFamily: designSystem.typography.fontFamily.body,
                fontWeight: designSystem.typography.weights.semibold,
                color: '#EAEAEA',
              }}
            >
              Більше
            </span>
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M1 8H15M15 8L8 1M15 8L8 15"
                stroke="#EAEAEA"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          </div>
        </div>

        {/* Card 3 - Відстеження прогресу */}
        <div
          className="relative overflow-hidden p-8 flex flex-col justify-between min-h-[400px]"
          style={{ backgroundColor: '#323232' }}
        >
          {/* Decorative Number */}
          <div
            className="absolute inset-0 flex items-center justify-center pointer-events-none"
            style={{
              fontSize: '300px',
              fontFamily: designSystem.typography.fontFamily.heading,
              fontWeight: designSystem.typography.weights.bold,
              color: '#323232',
              WebkitTextStroke: '2px #3E3E3E',
              lineHeight: '0.75',
              opacity: 0.5,
              transform: 'scaleY(2)',
            }}
          >
            03
          </div>

          {/* Content */}
          <div className="relative z-10 space-y-4">
            <h3
              className="text-2xl"
              style={{
                fontFamily: designSystem.typography.fontFamily.heading,
                fontWeight: designSystem.typography.weights.bold,
                color: '#EAEAEA',
              }}
            >
              Відстеження прогресу
            </h3>
            <p
              className="text-base leading-relaxed"
              style={{
                fontFamily: designSystem.typography.fontFamily.body,
                fontWeight: designSystem.typography.weights.semibold,
                color: '#EAEAEA',
              }}
            >
              Кожна проблема отримує унікальний статус, який користувачі можуть відстежувати в режимі реального часу до повного вирішення.
            </p>
          </div>

          {/* Footer Link */}
          <div className="relative z-10 flex items-center gap-2 mt-8">
            <span
              className="text-sm"
              style={{
                fontFamily: designSystem.typography.fontFamily.body,
                fontWeight: designSystem.typography.weights.semibold,
                color: '#EAEAEA',
              }}
            >
              Більше
            </span>
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M1 8H15M15 8L8 1M15 8L8 15"
                stroke="#EAEAEA"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          </div>
        </div>
      </section>

      {/* Tips for reporting problems */}
      {/* Tips for reporting problems */}
      <section className="bg-white">
        <div className="space-y-4 pb-4 border-b" style={{ borderColor: '#E5E5E5' }}>
          <h2
            className="text-2xl"
            style={{
              fontFamily: designSystem.typography.fontFamily.heading,
              fontWeight: designSystem.typography.weights.semibold,
              color: '#1F2732',
            }}
          >
            Поради щодо подання проблем
          </h2>
          <p
            className="text-base"
            style={{
              fontFamily: designSystem.typography.fontFamily.body,
              fontWeight: designSystem.typography.weights.medium,
              color: '#969696',
            }}
          >
            Дізнайтесь, які деталі важливо вказати для ефективної комунікації та як <br /> чітко сформолювати проблеми, щоб їх швидше вирішували
          </p>
        </div>

        <div className="space-y-0">
          {/* Item 1 - Як правильно описати проблему */}
          <div className="border-b" style={{ borderColor: '#E5E5E5' }}>
            <button
              type="button"
              onClick={() => setActiveAdvice(activeAdvice === '0' ? null : '0')}
              className="flex w-full items-start gap-3 py-6 text-left transition-colors"
            >
              <div className="mt-1 flex-shrink-0">
                {activeAdvice === '0' ? (
                  <svg
                    width="24"
                    height="24"
                    viewBox="0 0 24 24"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M18 15L12 9L6 15"
                      stroke="#1F2732"
                      strokeWidth="2"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    />
                  </svg>
                ) : (
                  <svg
                    width="24"
                    height="24"
                    viewBox="0 0 24 24"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M6 9L12 15L18 9"
                      stroke="#1F2732"
                      strokeWidth="2"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    />
                  </svg>
                )}
              </div>
              <span
                className="text-lg"
                style={{
                  fontFamily: designSystem.typography.fontFamily.heading,
                  fontWeight: designSystem.typography.weights.semibold,
                  color: '#1F2732',
                }}
              >
                Як правильно описати проблему
              </span>
            </button>

            {activeAdvice === '0' && (
              <div className="pl-9 pb-6 pr-4 space-y-4">
                <p
                  className="text-base leading-relaxed"
                  style={{
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.medium,
                    color: '#969696',
                  }}
                >
                  Щоб ваша проблема була зрозумілою для відповідальних служб та інших
                  мешканців, описуйте її чітко та лаконічно. Вкажіть точне місце, час
                  виникнення та можливі причини. Детальний опис допоможе швидше знайти
                  рішення.
                </p>
                <button
                  className="px-6 py-2 text-sm transition-colors hover:opacity-90"
                  style={{
                    backgroundColor: '#FF2D63',
                    color: '#FFFFFF',
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.bold,
                    borderRadius: '20px',
                  }}
                >
                  Більше
                </button>
              </div>
            )}
          </div>

          {/* Item 2 - Що додати до опису */}
          <div className="border-b" style={{ borderColor: '#E5E5E5' }}>
            <button
              type="button"
              onClick={() => setActiveAdvice(activeAdvice === '1' ? null : '1')}
              className="flex w-full items-start gap-3 py-6 text-left transition-colors"
            >
              <div className="mt-1 flex-shrink-0">
                {activeAdvice === '1' ? (
                  <svg
                    width="24"
                    height="24"
                    viewBox="0 0 24 24"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M18 15L12 9L6 15"
                      stroke="#1F2732"
                      strokeWidth="2"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    />
                  </svg>
                ) : (
                  <svg
                    width="24"
                    height="24"
                    viewBox="0 0 24 24"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M6 9L12 15L18 9"
                      stroke="#1F2732"
                      strokeWidth="2"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    />
                  </svg>
                )}
              </div>
              <span
                className="text-lg"
                style={{
                  fontFamily: designSystem.typography.fontFamily.heading,
                  fontWeight: designSystem.typography.weights.semibold,
                  color: '#1F2732',
                }}
              >
                Що додати до опису, щоб проблему вирішили швидше?
              </span>
            </button>

            {activeAdvice === '1' && (
              <div className="pl-9 pb-6 pr-4 space-y-4">
                <p
                  className="text-base leading-relaxed"
                  style={{
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.medium,
                    color: '#969696',
                  }}
                >
                  Додавайте до опису конкретні факти: як довго існує проблема, чи були
                  спроби її вирішення раніше, чи є свідки. Важливо також зазначити, як
                  саме ця проблема впливає на життя громади.
                </p>
                <button
                  className="px-6 py-2 text-sm transition-colors hover:opacity-90"
                  style={{
                    backgroundColor: '#FF2D63',
                    color: '#FFFFFF',
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.bold,
                    borderRadius: '20px',
                  }}
                >
                  Більше
                </button>
              </div>
            )}
          </div>

          {/* Item 3 - Фотографії проблем */}
          <div className="border-b" style={{ borderColor: '#E5E5E5' }}>
            <button
              type="button"
              onClick={() => setActiveAdvice(activeAdvice === '2' ? null : '2')}
              className="flex w-full items-start gap-3 py-6 text-left transition-colors"
            >
              <div className="mt-1 flex-shrink-0">
                {activeAdvice === '2' ? (
                  <svg
                    width="24"
                    height="24"
                    viewBox="0 0 24 24"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M18 15L12 9L6 15"
                      stroke="#1F2732"
                      strokeWidth="2"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    />
                  </svg>
                ) : (
                  <svg
                    width="24"
                    height="24"
                    viewBox="0 0 24 24"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M6 9L12 15L18 9"
                      stroke="#1F2732"
                      strokeWidth="2"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    />
                  </svg>
                )}
              </div>
              <span
                className="text-lg"
                style={{
                  fontFamily: designSystem.typography.fontFamily.heading,
                  fontWeight: designSystem.typography.weights.semibold,
                  color: '#1F2732',
                }}
              >
                Фотографії проблем: як зображати їх корисно?
              </span>
            </button>

            {activeAdvice === '2' && (
              <div className="pl-9 pb-6 pr-4 space-y-4">
                <p
                  className="text-base leading-relaxed"
                  style={{
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.medium,
                    color: '#969696',
                  }}
                >
                  Робіть фотографії при хорошому освітленні. Зробіть декілька знімків:
                  загальний план (щоб зрозуміти локацію) та детальний план (щоб
                  побачити суть проблеми). Уникайте розмитих фото.
                </p>
                <button
                  className="px-6 py-2 text-sm transition-colors hover:opacity-90"
                  style={{
                    backgroundColor: '#FF2D63',
                    color: '#FFFFFF',
                    fontFamily: designSystem.typography.fontFamily.body,
                    fontWeight: designSystem.typography.weights.bold,
                    borderRadius: '20px',
                  }}
                >
                  Більше
                </button>
              </div>
            )}
          </div>
        </div>
      </section>
    </div>
  )
}
