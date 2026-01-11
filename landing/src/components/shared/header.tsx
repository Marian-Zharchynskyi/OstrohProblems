"use client"

import Link from 'next/link'
import { usePathname } from 'next/navigation'
import { cn } from '@/lib/utils'
import { designSystem } from '@/lib/design-system'

const navItems = [
  { path: '/contact', label: 'Контакти' },
  { path: '/problems', label: 'Всі проблеми' },
  { path: '/about', label: 'Про нас' },
]

export const Header = () => {
  const pathname = usePathname()
  const appUrl = process.env.NEXT_PUBLIC_APP_URL || 'http://localhost:5146'

  return (
    <header className="py-4 bg-transparent">
      <div className="container mx-auto px-4">
        <nav
          className="flex items-center justify-between px-6 py-4 rounded-[30px] shadow-[0_4px_12px_rgba(0,0,0,0.25)]"
          style={{
            backgroundColor: designSystem.colors.header.background,
          }}
        >
          {/* Logo */}
          <Link
            href="/home"
            className="text-xl font-heading font-semibold text-black"
          >
            Острог Разом
          </Link>

          {/* Center Navigation */}
          <ul className="hidden md:flex items-center gap-2">
            {navItems.map((item) => {
              const isActive = pathname === item.path
              return (
                <li key={item.path}>
                  <Link
                    href={item.path}
                    className={cn(
                      'text-sm font-heading font-semibold px-5 py-2.5 rounded-full transition-all duration-200',
                      isActive
                        ? 'bg-black text-white shadow-md transform scale-105'
                        : 'text-black/70 hover:text-black hover:bg-black/5'
                    )}
                  >
                    {item.label}
                  </Link>
                </li>
              )
            })}
          </ul>

          {/* Right Side Actions */}
          <div className="flex items-center gap-4">
            {/* No Profile Icon as per requirements */}
            <a
              href={`${appUrl}/login`}
              className="text-sm font-heading font-semibold text-black hover:bg-black/5 px-5 py-2.5 rounded-full transition-all"
            >
              Увійти
            </a>
          </div>
        </nav>
      </div>
    </header>
  )
}
