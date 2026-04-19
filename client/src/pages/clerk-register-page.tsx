import { SignUp } from '@clerk/clerk-react';

export function ClerkRegisterPage() {
  return (
    <div className="min-h-screen w-full flex items-center justify-center bg-[#F5F5F5] p-4">
      <div className="flex flex-col md:flex-row w-full max-w-[1100px] overflow-hidden rounded-2xl shadow-[0_20px_60px_-15px_rgba(0,0,0,0.3)] bg-white min-h-[700px]">
        {/* Left section - Welcome text */}
        <div className="w-full md:w-[50%] bg-[#1F2732] p-8 md:p-12 lg:p-16 flex flex-col justify-center relative">
          <div className="z-10 max-w-lg mx-auto md:mx-0">
            <h1 className="text-white text-4xl md:text-5xl lg:text-5xl font-semibold mb-6 font-['Sora'] leading-tight">Ласкаво просимо</h1>
            <div className="w-56 md:w-64 h-2.5 bg-[#C2C2C2] rounded-full mb-10"></div>
            <p className="text-[#EAEAEA] text-lg md:text-xl font-medium font-['Mulish'] leading-relaxed opacity-90">Будь ласка, введіть ваші дані для реєстрації нового акаунту</p>
          </div>
        </div>

        {/* Right section - Clerk SignUp form */}
        <div className="w-full md:w-[50%] bg-white p-8 md:p-12 flex flex-col justify-center items-center">
          <div className="w-full max-w-md">
            <SignUp
              signInUrl="/login"
              fallbackRedirectUrl="/map"
              appearance={{
                layout: {
                  socialButtonsPlacement: 'top',
                  socialButtonsVariant: 'blockButton',
                },
                elements: {
                  rootBox: 'w-full',
                  cardBox: 'w-full shadow-none',
                  card: 'w-full shadow-none bg-transparent p-0',
                  header: 'hidden',
                  headerTitle: 'hidden',
                  headerSubtitle: 'hidden',
                  main: 'w-full',
                  form: 'w-full',
                  socialButtonsBlockButton: "w-full border-2 border-[#1F2732] rounded-[20px] py-4 font-['Mulish'] font-bold hover:bg-gray-50 transition-all duration-200",
                  socialButtonsBlockButtonText: "font-['Mulish'] font-bold text-black text-base",
                  socialButtonsProviderIcon: 'w-6 h-6',
                  dividerLine: 'bg-gray-300',
                  dividerText: "font-['Mulish'] font-bold text-gray-500 uppercase text-sm px-4",
                  dividerRow: 'my-6',
                  formFieldLabel: "font-['Mulish'] font-bold text-gray-700 text-sm mb-2",
                  formFieldInput:
                    "w-full border-2 border-gray-200 rounded-[12px] font-['Mulish'] font-medium focus:border-[#1F2732] focus:ring-0 py-3 px-4 text-base transition-colors duration-200",
                  formFieldRow: 'mb-4',
                  formButtonPrimary:
                    "w-full bg-[#E42556] hover:bg-[#D44374] rounded-[20px] font-['Mulish'] font-extrabold uppercase py-4 text-base transition-all duration-200 shadow-lg hover:shadow-xl",
                  formFieldAction: "font-['Mulish'] text-[#E42556] hover:text-[#D44374] font-semibold",
                  footerAction: 'w-full flex justify-center items-center gap-2 mt-4',
                  footerActionLink: "text-[#E42556] hover:text-[#D44374] font-['Mulish'] font-bold",
                  footerActionText: "font-['Mulish'] text-gray-600",
                  identityPreview: 'bg-gray-50 rounded-xl p-4 border border-gray-200',
                  identityPreviewText: "font-['Mulish'] font-medium text-gray-800",
                  identityPreviewEditButton: "text-[#E42556] hover:text-[#D44374] font-['Mulish'] font-semibold",
                  formResendCodeLink: "text-[#E42556] hover:text-[#D44374] font-['Mulish'] font-semibold",
                  otpCodeFieldInput: "border-2 border-gray-200 rounded-lg focus:border-[#1F2732] font-['Mulish'] text-xl",
                  alternativeMethodsBlockButton: "w-full border-2 border-gray-200 rounded-[12px] py-3 font-['Mulish'] font-medium hover:bg-gray-50 transition-all duration-200",
                  backLink: "text-[#E42556] hover:text-[#D44374] font-['Mulish'] font-semibold",
                  alertText: "font-['Mulish'] text-sm",
                  formFieldWarningText: "font-['Mulish'] text-yellow-600 text-sm",
                  formFieldErrorText: "font-['Mulish'] text-red-600 text-sm mt-1",
                  formFieldSuccessText: "font-['Mulish'] text-green-600 text-sm",
                },
              }}
            />
          </div>
        </div>
      </div>
    </div>
  );
}
