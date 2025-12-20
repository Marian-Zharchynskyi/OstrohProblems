"use client"

import Link from 'next/link'
import { usePathname } from 'next/navigation'
import { User } from 'lucide-react'
import { cn } from '@/lib/utils'
import { designSystem } from '@/lib/design-system'

const navItems = [
  { path: '/contact', label: 'Контакти' },
  { path: '/problems', label: 'Всі проблеми' },
  { path: '/about', label: 'Про нас' },
]

export const Header = () => {
  const pathname = usePathname()
  const appUrl = process.env.NEXT_PUBLIC_APP_URL || 'http://localhost:3000'

  return (
    <header className="py-4">
      <div className="container mx-auto px-4">
        <nav 
          className="flex items-center justify-between px-6 py-4 rounded-[30px] shadow-[0_4px_12px_rgba(0,0,0,0.25)]"
          style={{ 
            backgroundColor: designSystem.colors.header.background,
          }}
        >
          <Link 
            href="/" 
            className="text-xl font-heading font-semibold text-black"
          >
            OstrohBetter
          </Link>
          
          <ul className="flex gap-8">
            {navItems.map((item) => (
              <li key={item.path}>
                <Link
                  href={item.path}
                  className={cn(
                    'text-sm font-heading font-semibold transition-colors hover:text-primary',
                    pathname === item.path
                      ? 'text-black'
                      : 'text-black/80'
                  )}
                >
                  {item.label}
                </Link>
              </li>
            ))}
          </ul>

          <div className="flex items-center gap-4">
            <a
              href={`${appUrl}/login`}
              className="text-sm font-heading font-semibold text-black hover:text-primary transition-colors"
            >
              Увійти
            </a>
            <a
              href={`${appUrl}/profile`}
              className="flex items-center justify-center w-10 h-10 rounded-full hover:bg-black/5 transition-colors"
              aria-label="Профіль користувача"
            >
              <User className="w-5 h-5 text-black" strokeWidth={2} />
            </a>
          </div>
        </nav>
      </div>
    </header>
  )
}
