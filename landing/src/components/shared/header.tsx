"use client"

import Link from 'next/link'
import { usePathname } from 'next/navigation'
import { cn } from '@/lib/utils'

const navItems = [
  { path: '/', label: 'Головна' },
  { path: '/about', label: 'Про нас' },
  { path: '/contact', label: 'Контакти' },
]

export const Header = () => {
  const pathname = usePathname()
  const appUrl = process.env.NEXT_PUBLIC_APP_URL || 'http://localhost:3000'

  return (
    <header className="border-b bg-white">
      <div className="container mx-auto px-4 py-4">
        <nav className="flex items-center justify-between">
          <Link href="/" className="text-2xl font-bold text-primary font-heading">
            Острог разом
          </Link>
          <div className="flex items-center gap-6">
            <ul className="flex gap-6">
              {navItems.map((item) => (
                <li key={item.path}>
                  <Link
                    href={item.path}
                    className={cn(
                      'text-sm font-medium transition-colors hover:text-primary',
                      pathname === item.path
                        ? 'text-primary'
                        : 'text-muted-foreground'
                    )}
                  >
                    {item.label}
                  </Link>
                </li>
              ))}
              <li>
                <a
                  href={`${appUrl}/map`}
                  className="text-sm font-medium transition-colors hover:text-primary text-muted-foreground"
                >
                  Карта
                </a>
              </li>
            </ul>
            <div className="flex items-center gap-4">
              <a
                href={`${appUrl}/login`}
                className="text-sm font-medium text-muted-foreground hover:text-primary transition-colors"
              >
                Увійти
              </a>
              <a
                href={`${appUrl}/register`}
                className="text-sm font-medium bg-primary text-primary-foreground px-4 py-2 rounded-md hover:bg-primary/90 transition-colors"
              >
                Реєстрація
              </a>
            </div>
          </div>
        </nav>
      </div>
    </header>
  )
}
