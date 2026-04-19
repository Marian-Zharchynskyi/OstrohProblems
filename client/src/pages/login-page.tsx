import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '@/contexts/auth-context';
import { PasswordInput } from '@/components/ui/password-input';
import { getDefaultRouteForUser } from '@/lib/auth-routes';

export function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [rememberMe, setRememberMe] = useState(false);
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const [showChangePassword, setShowChangePassword] = useState(false);
  const [resetEmail, setResetEmail] = useState('');
  const [oldPassword, setOldPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [resetError, setResetError] = useState('');
  const [resetSuccess, setResetSuccess] = useState('');
  const [isResetLoading, setIsResetLoading] = useState(false);

  const { signIn } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setIsLoading(true);

    try {
      const user = await signIn(email, password, rememberMe);
      if (user) {
        navigate(getDefaultRouteForUser(user));
      }
    } catch (err: unknown) {
      const errorMessage = typeof err === 'string' ? err : 'Invalid email or password. Please try again.';
      setError(errorMessage);
      console.error('Login error:', err instanceof Error ? err.message : String(err));
    } finally {
      setIsLoading(false);
    }
  };

  const handleChangePassword = async (e: React.FormEvent) => {
    e.preventDefault();
    setResetError('');
    setResetSuccess('');
    setIsResetLoading(true);

    try {
      const [{ authService }, { userService }, { toast }, { decodeToken }] = await Promise.all([
        import('@/services/auth.service'),
        import('@/services/user.service'),
        import('@/lib/toast'),
        import('@/lib/jwt-utils'),
      ]);

      const tokens = await authService.signIn({ email: resetEmail, password: oldPassword });
      const user = decodeToken(tokens.accessToken);
      if (!user?.id) throw new Error('Authentication failed');

      await userService.changePassword(user.id, { currentPassword: oldPassword, newPassword }, tokens.accessToken);

      toast.success('Пароль успішно змінено');

      setShowChangePassword(false);
      setResetEmail('');
      setOldPassword('');
      setNewPassword('');
    } catch (err: unknown) {
      console.error('Error changing password:', err);
      setResetError(err instanceof Error ? err.message : 'Не вдалося змінити пароль');
    } finally {
      setIsResetLoading(false);
    }
  };

  return (
    <div className="min-h-screen w-full flex items-center justify-center bg-[#F5F5F5] p-4">
      <div className="flex flex-col md:flex-row w-full max-w-[1100px] overflow-hidden rounded-2xl shadow-[0_20px_60px_-15px_rgba(0,0,0,0.3)] bg-white min-h-[700px]">
        {/* Left Column (Branding & Welcome) - 40% */}
        <div className="w-full md:w-[50%] bg-[#1F2732] p-8 md:p-12 flex flex-col justify-center relative">
          <div className="z-10">
            <h1 className="text-white text-4xl md:text-5xl font-semibold mb-6 font-['Sora'] leading-tight whitespace-nowrap">Ласкаво просимо</h1>

            <div className="w-56 md:w-70 h-2.5 bg-[#C2C2C2] rounded-full mb-12"></div>

            <p className="text-[#EAEAEA] text-lg font-medium font-['Mulish'] leading-relaxed opacity-90">Будь ласка, введіть ваші дані для входу до вашого акаунту</p>
          </div>
        </div>

        {/* Right Column (Login Form) - 60% */}
        <div className="w-full md:w-[50%] bg-white p-8 md:p-12 lg:p-16 flex flex-col justify-center">
          <div className="w-full max-w-md mx-auto space-y-8">
            {/* Social Login Buttons - Simplified for brevity in replace */}
            <div className="flex flex-col gap-4">
              <button
                type="button"
                className="flex items-center justify-center gap-3 w-full bg-white border border-[#1F2732] rounded-[20px] py-3 px-4 hover:bg-gray-50 transition-colors group">
                {/* Google Icon */}
                <svg className="w-5 h-5" viewBox="0 0 24 24">
                  <path d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z" fill="#4285F4" />
                  <path d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z" fill="#34A853" />
                  <path d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z" fill="#FBBC05" />
                  <path d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z" fill="#EA4335" />
                </svg>
                <span className="font-['Mulish'] font-bold text-black text-base">Увійти через Google</span>
              </button>

              <button
                type="button"
                className="flex items-center justify-center gap-3 w-full bg-white border border-[#1F2732] rounded-[20px] py-3 px-4 hover:bg-gray-50 transition-colors group">
                {/* Facebook Icon */}
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
            {error && <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-lg text-sm text-center">{error}</div>}

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
                  className="w-full px-4 py-3 border border-gray-200 rounded-[8px] text-[#464646] font-['Mulish'] font-medium placeholder-gray-400 focus:outline-none focus:ring-0 focus:border-[#1F2732] transition-all hover:border-gray-300"
                  placeholder="you@example.com"
                />
              </div>

              <div className="space-y-2">
                <label htmlFor="password" className="block text-sm font-['Mulish'] font-bold text-gray-700">
                  Пароль
                </label>
                <PasswordInput
                  id="password"
                  required
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  className="w-full px-4 py-3 border border-gray-200 rounded-[8px] text-[#464646] font-['Mulish'] font-medium placeholder-gray-400 focus:outline-none focus:ring-0 focus:border-[#1F2732] transition-all hover:border-gray-300 h-auto"
                  placeholder="••••••••"
                />
              </div>

              <div className="flex items-center justify-between mt-6">
                <div className="flex items-center">
                  <input
                    id="remember-me"
                    type="checkbox"
                    checked={rememberMe}
                    onChange={(e) => setRememberMe(e.target.checked)}
                    className="h-4 w-4 text-[#E42556] focus:ring-[#E42556] border-gray-300 rounded cursor-pointer"
                  />
                  <label htmlFor="remember-me" className="ml-2 block text-sm font-['Mulish'] font-medium text-black cursor-pointer">
                    Запам'ятати мене
                  </label>
                </div>

                <div className="text-sm">
                  <button
                    type="button"
                    onClick={() => setShowChangePassword(true)}
                    className="font-['Mulish'] font-medium text-[#E42556] hover:text-[#D44374] underline bg-transparent border-0 p-0 cursor-pointer focus:outline-none focus-visible:outline-none focus:ring-0 focus-visible:ring-0">
                    Проблеми з паролем
                  </button>
                </div>
              </div>

              <button
                type="submit"
                disabled={isLoading}
                className="w-full bg-[#E42556] hover:bg-[#D44374] text-white py-4 px-4 rounded-[20px] font-['Mulish'] font-extrabold text-base uppercase tracking-wide shadow-md hover:shadow-lg transition-all transform hover:-translate-y-0.5 disabled:opacity-50 disabled:cursor-not-allowed disabled:transform-none">
                {isLoading ? 'ВХІД...' : 'УВІЙТИ'}
              </button>
            </form>

            <div className="text-center pt-4">
              <p className="font-['Mulish'] text-black font-bold text-sm">
                Ще не маєте акаунту ?{' '}
                <Link to="/register" className="text-blue-600 hover:text-blue-800 underline">
                  Зареєструйтесь
                </Link>
              </p>
            </div>
          </div>
        </div>
      </div>

      {/* Change Password Modal */}
      {showChangePassword && (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
          <div className="bg-white rounded-xl shadow-xl w-full max-w-md overflow-hidden relative animate-in fade-in zoom-in-95 duration-200">
            {/* Dark Header */}
            <div className="px-6 py-4 flex items-center justify-between">
              <h2 className="text-xl font-semibold text-black font-heading">Зміна паролю</h2>
              <button onClick={() => setShowChangePassword(false)} className="text-white/70 hover:text-white transition-colors p-1">
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12" />
                </svg>
              </button>
            </div>

            <div className="p-6 flex flex-col gap-6">
              <p className="text-sm text-gray-600 font-['Mulish']">Введіть email, старий та новий пароль для зміни</p>

              {resetError && (
                <div className="bg-red-50 text-red-700 p-3 rounded-lg text-sm flex items-center gap-2">
                  <svg className="w-4 h-4 shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                  {resetError}
                </div>
              )}

              {resetSuccess && (
                <div className="bg-green-50 text-green-700 p-3 rounded-lg text-sm flex items-center gap-2">
                  <svg className="w-4 h-4 shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M5 13l4 4L19 7" />
                  </svg>
                  {resetSuccess}
                </div>
              )}

              {!resetSuccess && (
                <form onSubmit={handleChangePassword} className="space-y-6">
                  <div>
                    <label className="block text-sm font-bold text-gray-700 mb-2 font-['Mulish']">Email</label>
                    <input
                      type="email"
                      required
                      value={resetEmail}
                      onChange={(e) => setResetEmail(e.target.value)}
                      className="w-full px-4 py-2.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-0 focus:border-[#1F2732] transition-all font-['Mulish']"
                      placeholder="you@example.com"
                    />
                  </div>

                  <div>
                    <label className="block text-sm font-bold text-gray-700 mb-2 font-['Mulish']">Старий пароль</label>
                    <PasswordInput
                      required
                      value={oldPassword}
                      onChange={(e) => setOldPassword(e.target.value)}
                      className="w-full px-4 py-2.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-0 focus:border-[#1F2732] transition-all h-auto"
                      placeholder="••••••••"
                    />
                  </div>

                  <div>
                    <label className="block text-sm font-bold text-gray-700 mb-2 font-['Mulish']">Новий пароль</label>
                    <PasswordInput
                      required
                      minLength={6}
                      value={newPassword}
                      onChange={(e) => setNewPassword(e.target.value)}
                      className="w-full px-4 py-2.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-0 focus:border-[#1F2732] transition-all h-auto"
                      placeholder="••••••••"
                    />
                  </div>

                  <div className="pt-6">
                    <button
                      type="submit"
                      disabled={isResetLoading}
                      className="w-full border-2 border-[#E42556] text-[#E42556] bg-transparent hover:bg-[#E42556] hover:text-white py-3 rounded-xl font-extrabold font-['Mulish'] uppercase tracking-wide transition-all duration-200 disabled:opacity-50 disabled:cursor-not-allowed transform hover:-translate-y-0.5 active:translate-y-0">
                      {isResetLoading ? 'Змінюємо...' : 'Змінити'}
                    </button>
                  </div>
                </form>
              )}
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
