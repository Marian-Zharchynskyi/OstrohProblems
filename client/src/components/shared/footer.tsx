import { useState } from 'react'
import { Instagram, AtSign, MapPin, Smartphone, Send } from 'lucide-react'
import { designSystem } from '@/lib/design-system'

const XLogo = ({ className }: { className?: string }) => (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 24 24"
    className={className}
    role="img"
    aria-hidden="true"
  >
    <path
      fill="white"
      d="M19.633 3H21.75l-4.676 5.34L22 21h-5.72l-3.744-6.078L8.165 21H2l5.082-5.789L2.25 3h5.886l3.327 5.39zm-3.016 16.2h1.51L8.61 4.62H6.99z"
    />
  </svg>
)

const FacebookGlyph = ({ className }: { className?: string }) => (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 24 24"
    className={className}
    role="img"
    aria-hidden="true"
  >
    <path
      fill="white"
      d="M14.5 9.5h2.88l.45-3.2H14.5V4.6c0-.9.3-1.4 1.5-1.4h1.85V0h-2.6C11.7 0 10 1.8 10 4.8v1.5H7.5V9.5H10V24h4.5Z"
    />
  </svg>
)

const MailFilled = ({ className }: { className?: string }) => (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 24 24"
    className={className}
    role="img"
    aria-hidden="true"
  >
    <path
      fill={designSystem.colors.footer.accent}
      d="M2 6.75A2.75 2.75 0 0 1 4.75 4h14.5A2.75 2.75 0 0 1 22 6.75v10.5A2.75 2.75 0 0 1 19.25 20H4.75A2.75 2.75 0 0 1 2 17.25ZM4.75 5.5a1.25 1.25 0 0 0-1.168.815l8.117 5.472 8.52-5.51A1.25 1.25 0 0 0 19.25 5.5Zm15.5 3.03-8.28 5.353a.75.75 0 0 1-.83 0L3.25 8.53v8.72c0 .69.56 1.25 1.25 1.25h14.5c.69 0 1.25-.56 1.25-1.25Z"
    />
  </svg>
)

export const Footer = () => {
  const [email, setEmail] = useState('')

  const handleSubscribe = (e: React.FormEvent) => {
    e.preventDefault()
    console.log('Subscribe email:', email)
    setEmail('')
  }

  return (
    <footer className="w-full mt-auto" style={{ backgroundColor: designSystem.colors.footer.background }}>
      <div className="mx-auto w-full px-[10%]">
        <div className="py-[6px] md:py-[68px]">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-y-14 md:gap-x-[55px] lg:gap-x-[88px]">
            
            {/* Column 1: Socials */}
            <div>
              <h3 className="font-heading font-bold text-[20px] text-white mb-[36px] md:mb-[68px]">
                Ми у соцмережах
              </h3>
              <div className="grid grid-cols-2 gap-x-[40px] gap-y-[40px] w-fit">
                {[
                  { icon: FacebookGlyph, href: 'https://facebook.com', label: 'Facebook' },
                  { icon: XLogo, href: 'https://twitter.com', label: 'X' },
                  { icon: Instagram, href: 'https://instagram.com', label: 'Instagram' },
                  { icon: AtSign, href: 'https://threads.net', label: 'Threads' },
                ].map(({ icon: Icon, href, label }) => (
                  <a
                    key={label}
                    href={href}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="flex items-center justify-center w-[50px] h-[50px] rounded-[7px] transition-colors hover:bg-white/10"
                    style={{ backgroundColor: designSystem.colors.footer.elementBackground }}
                    aria-label={label}
                  >
                    <Icon className="w-[22px] h-[22px] text-white" />
                  </a>
                ))}
              </div>
            </div>

            {/* Column 2: Contacts */}
            <div>
              <h3 className="font-heading font-bold text-[20px] text-white mb-[46px] md:mb-[58px]">
                Зв'яжіться з нами
              </h3>
              <ul className="flex flex-col gap-[32px]">
                <li className="flex items-center">
                  <MapPin
                    className="w-[36px] h-[36px] flex-shrink-0"
                    style={{ color: designSystem.colors.footer.accent }}
                  />
                  <span className="text-white ml-[19px] text-[18px] font-heading">
                    вул. Академічна, 2, м. Острог
                  </span>
                </li>
                <li className="flex items-center">
                  <Smartphone
                    className="w-[36px] h-[36px] flex-shrink-0"
                    style={{ color: designSystem.colors.footer.accent }}
                  />
                  <span className="text-white ml-[19px] text-[18px] font-heading">
                    +380 362 554 123
                  </span>
                </li>
                <li className="flex items-center">
                  <MailFilled className="w-[36px] h-[36px] flex-shrink-0" />
                  <span className="text-white ml-[19px] text-[18px] font-heading">
                    info@ostroh.ua
                  </span>
                </li>
              </ul>
            </div>

            {/* Column 3: Newsletter */}
            <div>
              <h3 className="font-heading font-bold text-[20px] text-white mb-[46px] md:mb-[58px]">
                Підписка на новини
              </h3>
              <p className="text-white mb-[52px] leading-[1.6] font-heading text-[17px]">
                Введіть ваш email та отримуйте<br />новини Острога
              </p>
              <form onSubmit={handleSubscribe} className="w-full">
                <div 
                  className="relative flex items-center w-full h-[55px] rounded-[7px]"
                  style={{ backgroundColor: designSystem.colors.footer.elementBackground }}
                >
                  <input
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder="Підпишіться"
                    className="w-full h-full bg-transparent px-[18px] pr-[55px] text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-white/20 rounded-[7px] text-[16px]"
                    required
                  />
                  <button
                    type="submit"
                    className="absolute right-[16px] p-0 bg-transparent hover:opacity-80 transition-opacity flex items-center justify-center"
                    aria-label="Підписатися"
                  >
                    <Send className="w-5 h-5 text-white" />
                  </button>
                </div>
              </form>
            </div>

          </div>
        </div>
      </div>
    </footer>
  )
}
