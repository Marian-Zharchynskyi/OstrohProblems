import { useState, type CSSProperties } from 'react'
import { useNavigate } from 'react-router-dom'
import { User, FileText } from 'lucide-react'
import { designSystem } from '@/lib/design-system'
import { UserProblemsTab } from '@/features/problems/components/user-problems-tab'

export function MySubmittedProblemsPageWithTabs() {
  const navigate = useNavigate()
  const [activeTab, setActiveTab] = useState<'profile' | 'problems'>('problems')

  const getTabStyles = (isActive: boolean): CSSProperties => ({
    backgroundColor: isActive
      ? designSystem.colors.profile.tabs.activeBackground
      : designSystem.colors.profile.tabs.background,
    color: isActive
      ? designSystem.colors.profile.tabs.text
      : designSystem.colors.profile.tabs.inactiveText,
    borderColor: isActive
      ? designSystem.colors.profile.tabs.activeBorder
      : designSystem.colors.profile.tabs.border,
    boxShadow: 'none',
  })

  const handleTabClick = (tab: 'profile' | 'problems') => {
    if (tab === 'profile') {
      navigate('/profile')
    } else {
      setActiveTab(tab)
    }
  }

  return (
    <div>
      <div
        className="py-2"
        style={{ backgroundColor: designSystem.colors.profile.headerBackground}}
      >
        <div className="max-w-6xl mx-auto px-6">
          <div className="flex flex-wrap justify-center gap-4 mt-6">
            <button
              onClick={() => handleTabClick('profile')}
              className="flex items-center gap-2 px-6 py-3 rounded-full border-2 font-bold text-base transition-all duration-200"
              style={getTabStyles(activeTab === 'profile')}
            >
              <User className="w-5 h-5" strokeWidth={2.5} />
              <span className="font-['Mulish']">Мій профіль</span>
            </button>
            <button
              onClick={() => handleTabClick('problems')}
              className="flex items-center gap-2 px-6 py-3 rounded-full border-2 font-bold text-base transition-all duration-200"
              style={getTabStyles(activeTab === 'problems')}
            >
              <FileText className="w-5 h-5" strokeWidth={2.5} />
              <span className="font-['Mulish']">Подані проблеми</span>
            </button>
          </div>
          <div className="mt-6 h-px w-full relative">
            <div
              className="absolute inset-0"
              style={{
                backgroundColor: designSystem.colors.profile.tabs.border,
                boxShadow: `0 0 0 100vmax ${designSystem.colors.profile.tabs.border}`,
                clipPath: 'inset(0 -100vmax)',
              }}
            />
          </div>
        </div>
      </div>

      <div className="max-w-6xl mx-auto px-6 py-8">
        <UserProblemsTab />
      </div>
    </div>
  )
}
