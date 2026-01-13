import { SignIn, useSignIn } from '@clerk/clerk-react'
import { useNavigate } from 'react-router-dom'
import { useEffect } from 'react'

export function ClerkLoginPage() {
  const { isLoaded, signIn } = useSignIn()
  const navigate = useNavigate()

  useEffect(() => {
    if (isLoaded && signIn?.status === 'complete') {
      navigate('/')
    }
  }, [isLoaded, signIn, navigate])

  return (
    <div className="min-h-screen w-full flex items-center justify-center bg-[#F5F5F5] p-4">
      <div className="flex flex-col md:flex-row w-full max-w-[1100px] overflow-hidden rounded-2xl shadow-[0_20px_60px_-15px_rgba(0,0,0,0.3)] bg-white min-h-[700px]">
        <div className="w-full md:w-[50%] bg-[#1F2732] p-8 md:p-12 flex flex-col justify-center relative">
          <div className="z-10">
            <h1 className="text-white text-4xl md:text-5xl font-semibold mb-6 font-['Sora'] leading-tight whitespace-nowrap">
              Ласкаво просимо
            </h1>
            <div className="w-56 md:w-70 h-2.5 bg-[#C2C2C2] rounded-full mb-12"></div>
            <p className="text-[#EAEAEA] text-lg font-medium font-['Mulish'] leading-relaxed opacity-90">
              Будь ласка, введіть ваші дані для входу до вашого акаунту
            </p>
          </div>
        </div>

        <div className="w-full md:w-[50%] bg-white p-8 md:p-12 lg:p-16 flex flex-col justify-center">
          <div className="w-full max-w-md mx-auto">
            <SignIn 
              routing="path"
              path="/login"
              signUpUrl="/register"
              appearance={{
                elements: {
                  rootBox: 'w-full',
                  card: 'shadow-none bg-transparent',
                  headerTitle: 'hidden',
                  headerSubtitle: 'hidden',
                  socialButtonsBlockButton: 'border-[#1F2732] rounded-[20px] py-3 font-[\'Mulish\'] font-bold hover:bg-gray-50',
                  socialButtonsBlockButtonText: 'font-[\'Mulish\'] font-bold text-black',
                  dividerLine: 'bg-black',
                  dividerText: 'font-[\'Mulish\'] font-bold text-black uppercase',
                  formFieldLabel: 'font-[\'Mulish\'] font-bold text-gray-700',
                  formFieldInput: 'border-gray-200 rounded-[8px] font-[\'Mulish\'] font-medium focus:border-[#1F2732]',
                  formButtonPrimary: 'bg-[#E42556] hover:bg-[#D44374] rounded-[20px] font-[\'Mulish\'] font-extrabold uppercase',
                  footerActionLink: 'text-blue-600 hover:text-blue-800',
                  identityPreviewText: 'font-[\'Mulish\']',
                  formResendCodeLink: 'text-[#E42556] hover:text-[#D44374]',
                }
              }}
            />
          </div>
        </div>
      </div>
    </div>
  )
}
