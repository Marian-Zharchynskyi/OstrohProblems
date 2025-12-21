'use client'

import { useState } from 'react'
import { Facebook, Twitter, Instagram, AtSign, Mail, MapPin, Phone, Send } from 'lucide-react'
import { designSystem } from '@/lib/design-system'

export const Footer = () => {
  const [email, setEmail] = useState('')

  const handleSubscribe = (e: React.FormEvent) => {
    e.preventDefault()
    console.log('Subscribe email:', email)
    setEmail('')
  }

  return (
    <footer className="mt-auto">
      <div className="w-full">
        <div
          className="px-8 py-12"
          style={{ backgroundColor: designSystem.colors.footer.background }}
        >
          <div className="grid grid-cols-1 md:grid-cols-3 gap-12">
            <div>
              <h3 className="font-heading font-bold text-lg mb-6 text-white">
                Ми у соцмережах
              </h3>
              <div className="grid grid-cols-2 gap-3 max-w-[200px]">
                <a
                  href="https://facebook.com"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="flex items-center justify-center w-12 h-12 rounded-lg transition-colors hover:bg-white/10"
                  style={{ backgroundColor: designSystem.colors.footer.elementBackground }}
                  aria-label="Facebook"
                >
                  <Facebook className="w-5 h-5 text-white" />
                </a>
                <a
                  href="https://twitter.com"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="flex items-center justify-center w-12 h-12 rounded-lg transition-colors hover:bg-white/10"
                  style={{ backgroundColor: designSystem.colors.footer.elementBackground }}
                  aria-label="Twitter"
                >
                  <Twitter className="w-5 h-5 text-white" />
                </a>
                <a
                  href="https://instagram.com"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="flex items-center justify-center w-12 h-12 rounded-lg transition-colors hover:bg-white/10"
                  style={{ backgroundColor: designSystem.colors.footer.elementBackground }}
                  aria-label="Instagram"
                >
                  <Instagram className="w-5 h-5 text-white" />
                </a>
                <a
                  href="https://threads.net"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="flex items-center justify-center w-12 h-12 rounded-lg transition-colors hover:bg-white/10"
                  style={{ backgroundColor: designSystem.colors.footer.elementBackground }}
                  aria-label="Threads"
                >
                  <AtSign className="w-5 h-5 text-white" />
                </a>
              </div>
            </div>

            <div>
              <h3 className="font-heading font-bold text-lg mb-6 text-white">
                Зв&apos;яжіться з нами
              </h3>
              <ul className="space-y-4">
                <li className="flex items-start gap-3">
                  <MapPin
                    className="w-5 h-5 mt-0.5 flex-shrink-0"
                    style={{ color: designSystem.colors.footer.accent }}
                  />
                  <span className="text-sm font-heading font-regular text-white">
                    вул. Академічна, 2, м. Острог
                  </span>
                </li>
                <li className="flex items-start gap-3">
                  <Phone
                    className="w-5 h-5 mt-0.5 flex-shrink-0"
                    style={{ color: designSystem.colors.footer.accent }}
                  />
                  <span className="text-sm font-heading font-regular text-white">
                    +380 362 554 123
                  </span>
                </li>
                <li className="flex items-start gap-3">
                  <Mail
                    className="w-5 h-5 mt-0.5 flex-shrink-0"
                    style={{ color: designSystem.colors.footer.accent }}
                  />
                  <span className="text-sm font-heading font-regular text-white">
                    info@ostroh.ua
                  </span>
                </li>
              </ul>
            </div>

            <div>
              <h3 className="font-heading font-bold text-lg mb-6 text-white">
                Підписка на новини
              </h3>
              <p className="text-sm font-heading font-regular mb-4" style={{ color: designSystem.colors.footer.textSecondary }}>
                Введіть ваш email та отримуйте новини Острога
              </p>
              <form onSubmit={handleSubscribe} className="relative">
                <input
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  placeholder="Підпишіться"
                  className="w-full px-4 py-3 pr-12 rounded-lg text-sm text-white placeholder:text-gray-400 focus:outline-none focus:ring-2 focus:ring-white/20"
                  style={{ backgroundColor: designSystem.colors.footer.elementBackground }}
                  required
                />
                <button
                  type="submit"
                  className="absolute right-2 top-1/2 -translate-y-1/2 p-2 rounded-md hover:bg-white/10 transition-colors"
                  aria-label="Підписатися"
                >
                  <Send className="w-4 h-4 text-white" />
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </footer>
  )
}
