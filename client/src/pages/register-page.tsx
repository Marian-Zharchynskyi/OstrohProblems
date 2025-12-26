import { useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import { useAuth } from '@/contexts/auth-context'

export function RegisterPage() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [name, setName] = useState('')
  const [surname, setSurname] = useState('')
  const [error, setError] = useState('')
  const [isLoading, setIsLoading] = useState(false)

  const { signUp } = useAuth()
  const navigate = useNavigate()

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setError('')

    if (password.length < 8) {
      setError('Password must be at least 8 characters long')
      return
    }

    setIsLoading(true)

    try {
      await signUp(email, password, name || undefined, surname || undefined)
      navigate('/')
    } catch (err) {
      setError('Registration failed. Email may already be in use.')
      console.error('Registration error:', err)
    } finally {
      setIsLoading(false)
    }
  }

  return (
    <div className="min-h-screen w-full flex items-center justify-center bg-[#F5F5F5] p-4">
      <div className="flex flex-col md:flex-row w-full max-w-[1100px] overflow-hidden rounded-2xl shadow-[0_20px_60px_-15px_rgba(0,0,0,0.3)] bg-white min-h-[700px]">
        {/* Left Column (Branding & Welcome) - 50% */}
        <div className="w-full md:w-[50%] bg-[#1F2732] p-8 md:p-12 flex flex-col justify-center relative">
          <div className="z-10">
            <h1 className="text-white text-4xl md:text-5xl font-semibold mb-6 font-['Sora'] leading-tight whitespace-nowrap">
              Ласкаво просимо
            </h1>

            <div className="w-56 md:w-72 h-2.5 bg-[#C2C2C2] rounded-full mb-12"></div>

            <p className="text-[#EAEAEA] text-lg font-medium font-['Mulish'] leading-relaxed opacity-90">
              Будь ласка, введіть ваші дані для реєстрації вашого акаунту
            </p>
          </div>
        </div>

        {/* Right Column (Register Form) - 50% */}
        <div className="w-full md:w-[50%] bg-white p-8 md:p-12 lg:p-16 flex flex-col justify-center">
          <div className="w-full max-w-md mx-auto space-y-8">

            {/* Social Login Buttons */}
            <div className="flex flex-col gap-4">
              <button
                type="button"
                className="flex items-center justify-center gap-3 w-full bg-white border border-[#1F2732] rounded-[20px] py-3 px-4 hover:bg-gray-50 transition-colors group"
              >
                <svg className="w-5 h-5" viewBox="0 0 24 24">
                  <path
                    d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"
                    fill="#4285F4"
                  />
                  <path
                    d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"
                    fill="#34A853"
                  />
                  <path
                    d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z"
                    fill="#FBBC05"
                  />
                  <path
                    d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z"
                    fill="#EA4335"
                  />
                </svg>
                <span className="font-['Mulish'] font-bold text-black text-base">Увійти через Google</span>
              </button>

              <button
                type="button"
                className="flex items-center justify-center gap-3 w-full bg-white border border-[#1F2732] rounded-[20px] py-3 px-4 hover:bg-gray-50 transition-colors group"
              >
                <svg className="w-5 h-5 text-[#1877F2]" fill="currentColor" viewBox="0 0 24 24">
                  <path d="M24 12.073c0-6.627-5.373-12-12-12s-12 5.373-12 12c0 5.99 4.388 10.954 10.125 11.854v-8.385H7.078v-3.47h3.047V9.43c0-3.007 1.792-4.669 4.533-4.669 1.312 0 2.686.235 2.686.235v2.953H15.83c-1.491 0-1.956.925-1.956 1.874v2.208h3.328l-.532 3.47h-2.796v8.385C19.612 23.027 24 18.062 24 12.073z" />
                </svg>
                <span className="font-['Mulish'] font-bold text-black text-base">Увійти через Facebook</span>
              </button>
            </div>

            {/* Divider */}
            <div className="relative flex items-center justify-center py-2">
              <div className="flex-grow border-t border-black"></div>
              <span className="flex-shrink-0 mx-4 font-['Mulish'] font-bold text-black text-sm uppercase">або</span>
              <div className="flex-grow border-t border-black"></div>
            </div>

            {/* Error Message */}
            {error && (
              <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-lg text-sm text-center">
                {error}
              </div>
            )}

            {/* Form */}
            <form onSubmit={handleSubmit} className="space-y-6">
              <div className="space-y-2">
                <label htmlFor="email" className="block text-sm font-['Mulish'] font-bold text-gray-700">
                  Електронна пошта
                </label>
                <input
                  id="email"
                  type="email"
                  required
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  className="w-full px-4 py-3 border border-gray-200 rounded-[8px] text-[#464646] font-['Mulish'] font-medium placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-pink-500 focus:border-transparent transition-all hover:border-gray-300"
                  placeholder="you@example.com"
                />
              </div>

              <div className="space-y-2">
                <label htmlFor="password" className="block text-sm font-['Mulish'] font-bold text-gray-700">
                  Пароль
                </label>
                <input
                  id="password"
                  type="password"
                  required
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  className="w-full px-4 py-3 border border-gray-200 rounded-[8px] text-[#464646] font-['Mulish'] font-medium placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-pink-500 focus:border-transparent transition-all hover:border-gray-300"
                  placeholder="••••••••"
                />
              </div>

              <div className="space-y-2">
                <label htmlFor="name" className="block text-sm font-['Mulish'] font-bold text-gray-700">
                  Ім'я
                </label>
                <input
                  id="name"
                  type="text"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  className="w-full px-4 py-3 border border-gray-200 rounded-[8px] text-[#464646] font-['Mulish'] font-medium placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-pink-500 focus:border-transparent transition-all hover:border-gray-300"
                  placeholder="Петро"
                />
              </div>

              <div className="space-y-2">
                <label htmlFor="surname" className="block text-sm font-['Mulish'] font-bold text-gray-700">
                  Прізвище
                </label>
                <input
                  id="surname"
                  type="text"
                  value={surname}
                  onChange={(e) => setSurname(e.target.value)}
                  className="w-full px-4 py-3 border border-gray-200 rounded-[8px] text-[#464646] font-['Mulish'] font-medium placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-pink-500 focus:border-transparent transition-all hover:border-gray-300"
                  placeholder="Бурчак"
                />
              </div>

              <button
                type="submit"
                disabled={isLoading}
                className="w-full bg-[#E42556] hover:bg-[#D44374] text-white py-4 px-4 rounded-[20px] font-['Mulish'] font-extrabold text-base uppercase tracking-wide shadow-md hover:shadow-lg transition-all transform hover:-translate-y-0.5 disabled:opacity-50 disabled:cursor-not-allowed disabled:transform-none"
              >
                {isLoading ? 'РЕЄСТРАЦІЯ...' : 'ЗАРЕЄСТРУВАТИСЬ'}
              </button>
            </form>

            <div className="text-center pt-4">
              <p className="font-['Mulish'] text-black font-bold text-sm">
                Уже маєте акаунт ? {' '}
                <Link to="/login" className="text-blue-600 hover:text-blue-800 underline">
                  Увійти
                </Link>
              </p>
            </div>

          </div>
        </div>
      </div>
    </div>
  )
}
