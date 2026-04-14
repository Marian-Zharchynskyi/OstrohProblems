import { useState, useEffect, useRef, useCallback } from 'react'
import { useQuery } from '@tanstack/react-query'
import { useAuth } from '@/contexts/auth-context'
import { chatService } from '@/services/chat.service'
import { Link } from 'react-router-dom'
import ReactMarkdown from 'react-markdown'
import remarkGfm from 'remark-gfm'
import {
    BarChart3,
    TrendingUp,
    TrendingDown,
    Users,
    MessageSquare,
    AlertTriangle,
    CheckCircle,
    Clock,
    Star,
    Send,
    Bot,
    User,
    Sparkles,
    Activity,
    Target,
    Zap,
    ArrowUpRight,
    X,
    Filter,
} from 'lucide-react'
import {
    BarChart,
    Bar,
    XAxis,
    YAxis,
    Tooltip,
    ResponsiveContainer,
    PieChart,
    Pie,
    Cell,
    AreaChart,
    Area,
    CartesianGrid,
    Legend,
} from 'recharts'
import type { ChatMessage, DashboardFilter } from '@/types/chat'

const STATUS_COLORS: Record<string, string> = {
    'Нова': '#3B82F6',
    'В роботі': '#F59E0B',
    'Виконано': '#10B981',
    'Відхилено': '#EF4444',
}

const PRIORITY_COLORS: Record<string, string> = {
    'Критичний': '#EF4444',
    'Високий': '#F59E0B',
    'Середній': '#3B82F6',
    'Низький': '#10B981',
}

const CATEGORY_COLORS = [
    '#E42556', '#3B82F6', '#10B981', '#F59E0B', '#8B5CF6',
    '#EC4899', '#14B8A6', '#F97316', '#6366F1', '#84CC16',
]

function AnimatedCounter({ end, duration = 1500, suffix = '' }: { end: number; duration?: number; suffix?: string }) {
    const [count, setCount] = useState(0)
    useEffect(() => {
        let start = 0
        const step = end / (duration / 16)
        const timer = setInterval(() => {
            start += step
            if (start >= end) {
                setCount(end)
                clearInterval(timer)
            } else {
                setCount(Math.floor(start))
            }
        }, 16)
        return () => clearInterval(timer)
    }, [end, duration])
    return <span>{count}{suffix}</span>
}

const QUICK_PROMPTS = [
    { label: '📊 Загальний звіт', message: 'Дай мені загальний звіт по системі' },
    { label: '🔥 Критичні проблеми', message: 'Покажи критичні проблеми' },
    { label: '📈 Аналіз трендів', message: 'Проаналізуй тренди за останні місяці' },
    { label: '⚡ Рекомендації', message: 'Дай рекомендації щодо покращення роботи' },
    { label: '🏆 Топ категорій', message: 'Яка категорія найпроблемніша і чому?' },
    { label: '⏱️ Ефективність', message: 'Оціни ефективність вирішення проблем' },
]

export function AdminDashboardPage() {
    const { getClerkToken } = useAuth()
    const [activeFilter, setActiveFilter] = useState<DashboardFilter | null>(null)
    const [chatMessages, setChatMessages] = useState<ChatMessage[]>([])
    const [chatInput, setChatInput] = useState('')
    const [isChatLoading, setIsChatLoading] = useState(false)
    const [isChatExpanded, setIsChatExpanded] = useState(false)
    const chatEndRef = useRef<HTMLDivElement>(null)
    const inputRef = useRef<HTMLInputElement>(null)

    const { data: stats, isLoading, error } = useQuery({
        queryKey: ['dashboardStatistics'],
        queryFn: async () => {
            if (!getClerkToken) throw new Error('No auth')
            const token = await getClerkToken()
            if (!token) throw new Error('No token')
            return chatService.getDashboardStatistics(token)
        },
        refetchInterval: 60000,
    })

    useEffect(() => {
        chatEndRef.current?.scrollIntoView({ behavior: 'smooth' })
    }, [chatMessages])

    const sendMessage = useCallback(async (message: string) => {
        if (!message.trim() || !getClerkToken) return

        const userMsg: ChatMessage = {
            id: crypto.randomUUID(),
            content: message,
            role: 'user',
            timestamp: new Date(),
        }
        setChatMessages(prev => [...prev, userMsg])
        setChatInput('')
        setIsChatLoading(true)
        setIsChatExpanded(true)

        try {
            const token = await getClerkToken()
            if (!token) throw new Error('No token')
            const response = await chatService.sendAdminMessage(message, token)

            const aiMsg: ChatMessage = {
                id: crypto.randomUUID(),
                content: response.message,
                role: 'assistant',
                timestamp: new Date(),
                responseType: response.responseType,
                suggestedFilter: response.suggestedFilter || undefined,
            }
            setChatMessages(prev => [...prev, aiMsg])

            if (response.suggestedFilter) {
                setActiveFilter(response.suggestedFilter)
            }
        } catch {
            const errorMsg: ChatMessage = {
                id: crypto.randomUUID(),
                content: 'Вибачте, сталася помилка. Спробуйте ще раз.',
                role: 'assistant',
                timestamp: new Date(),
                responseType: 'Error',
            }
            setChatMessages(prev => [...prev, errorMsg])
        } finally {
            setIsChatLoading(false)
        }
    }, [getClerkToken])

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault()
        sendMessage(chatInput)
    }

    const clearFilter = () => setActiveFilter(null)

    if (isLoading) {
        return (
            <div className="min-h-screen flex items-center justify-center">
                <div className="flex flex-col items-center gap-4">
                    <div className="w-12 h-12 rounded-full border-4 border-gray-200 border-t-[#E42556] animate-spin" />
                    <p className="text-gray-500 font-medium">Завантаження статистики...</p>
                </div>
            </div>
        )
    }

    if (error || !stats) {
        return (
            <div className="min-h-screen flex items-center justify-center">
                <div className="text-center">
                    <AlertTriangle className="w-12 h-12 text-red-400 mx-auto mb-4" />
                    <p className="text-gray-600 font-medium">Помилка завантаження статистики</p>
                    <p className="text-gray-400 text-sm mt-1">Спробуйте оновити сторінку</p>
                </div>
            </div>
        )
    }

    const monthlyChange = stats.problemsLastMonth > 0
        ? Math.round(((stats.problemsThisMonth - stats.problemsLastMonth) / stats.problemsLastMonth) * 100)
        : 0

    // Filter stats based on active filter
    const filteredCategoryStats = activeFilter?.category
        ? stats.categoryStats.filter(c => c.category === activeFilter.category)
        : stats.categoryStats

    const filteredStatusStats = activeFilter?.status
        ? stats.statusStats.filter(s => s.status === activeFilter.status)
        : stats.statusStats

    const filteredPriorityStats = activeFilter?.priority
        ? stats.priorityStats.filter(p => p.priority === activeFilter.priority)
        : stats.priorityStats

    return (
        <div className="space-y-6 pb-8">
            {/* Header */}
            <div className="flex items-center justify-between">
                <div>
                    <h1 className="text-3xl font-bold tracking-tight text-gray-900 flex items-center gap-3">
                        <div className="w-10 h-10 rounded-xl bg-gradient-to-br from-[#E42556] to-[#c8204b] flex items-center justify-center shadow-lg">
                            <BarChart3 className="w-5 h-5 text-white" />
                        </div>
                        Панель адміністратора
                    </h1>
                    <p className="text-gray-500 mt-1 ml-[52px]">Аналітика та управління платформою «Острог Разом»</p>
                </div>

                {activeFilter && (
                    <button
                        onClick={clearFilter}
                        className="flex items-center gap-2 px-4 py-2 rounded-full bg-[#E42556]/10 text-[#E42556] text-sm font-medium hover:bg-[#E42556]/20 transition-colors"
                    >
                        <Filter className="w-4 h-4" />
                        Фільтр AI: {activeFilter.status || activeFilter.category || activeFilter.priority}
                        <X className="w-4 h-4" />
                    </button>
                )}
            </div>

            {/* Stats Grid */}
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
                <StatCard
                    icon={<Activity className="w-5 h-5" />}
                    label="Всього проблем"
                    value={stats.totalProblems}
                    color="#3B82F6"
                    subtitle={`${stats.problemsThisMonth} цього місяця`}
                    trend={monthlyChange}
                />
                <StatCard
                    icon={<Clock className="w-5 h-5" />}
                    label="Нових"
                    value={stats.newProblems}
                    color="#F59E0B"
                    subtitle={`${stats.inProgressProblems} в роботі`}
                />
                <StatCard
                    icon={<CheckCircle className="w-5 h-5" />}
                    label="Виконано"
                    value={stats.completedProblems}
                    color="#10B981"
                    subtitle={`${stats.resolutionRate}% вирішення`}
                />
                <StatCard
                    icon={<AlertTriangle className="w-5 h-5" />}
                    label="Критичних"
                    value={stats.criticalProblems}
                    color="#EF4444"
                    subtitle={`${stats.highPriorityProblems} високого пріорітету`}
                />
            </div>

            {/* Secondary Stats */}
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
                <MiniStatCard icon={<Users className="w-4 h-4" />} label="Користувачів" value={stats.totalUsers} />
                <MiniStatCard icon={<MessageSquare className="w-4 h-4" />} label="Коментарів" value={stats.totalComments} />
                <MiniStatCard icon={<Star className="w-4 h-4" />} label="Середній рейтинг" value={stats.averageRating} suffix="/5" />
                <MiniStatCard icon={<Target className="w-4 h-4" />} label="Час вирішення" value={stats.avgResolutionTimeDays} suffix=" дн" />
            </div>

            {/* Charts Row */}
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                {/* Monthly Trends */}
                <div className="bg-white rounded-2xl border border-gray-100 shadow-sm p-6 hover:shadow-md transition-shadow">
                    <h3 className="font-semibold text-gray-900 mb-4 flex items-center gap-2">
                        <TrendingUp className="w-5 h-5 text-[#E42556]" />
                        Тренди по місяцях
                    </h3>
                    <ResponsiveContainer width="100%" height={260}>
                        <AreaChart data={stats.monthlyTrends}>
                            <defs>
                                <linearGradient id="colorCreated" x1="0" y1="0" x2="0" y2="1">
                                    <stop offset="5%" stopColor="#3B82F6" stopOpacity={0.2} />
                                    <stop offset="95%" stopColor="#3B82F6" stopOpacity={0} />
                                </linearGradient>
                                <linearGradient id="colorResolved" x1="0" y1="0" x2="0" y2="1">
                                    <stop offset="5%" stopColor="#10B981" stopOpacity={0.2} />
                                    <stop offset="95%" stopColor="#10B981" stopOpacity={0} />
                                </linearGradient>
                            </defs>
                            <CartesianGrid strokeDasharray="3 3" stroke="#f0f0f0" />
                            <XAxis dataKey="month" tick={{ fontSize: 12 }} stroke="#94a3b8" />
                            <YAxis tick={{ fontSize: 12 }} stroke="#94a3b8" />
                            <Tooltip
                                contentStyle={{ borderRadius: '12px', border: '1px solid #e5e7eb', boxShadow: '0 4px 12px rgba(0,0,0,0.1)' }}
                            />
                            <Legend />
                            <Area type="monotone" dataKey="created" name="Створено" stroke="#3B82F6" fill="url(#colorCreated)" strokeWidth={2} />
                            <Area type="monotone" dataKey="resolved" name="Вирішено" stroke="#10B981" fill="url(#colorResolved)" strokeWidth={2} />
                        </AreaChart>
                    </ResponsiveContainer>
                </div>

                {/* Status Distribution */}
                <div className="bg-white rounded-2xl border border-gray-100 shadow-sm p-6 hover:shadow-md transition-shadow">
                    <h3 className="font-semibold text-gray-900 mb-4 flex items-center gap-2">
                        <Zap className="w-5 h-5 text-[#E42556]" />
                        Розподіл за статусами
                    </h3>
                    <ResponsiveContainer width="100%" height={260}>
                        <PieChart>
                            <Pie
                                data={filteredStatusStats}
                                dataKey="count"
                                nameKey="status"
                                cx="50%"
                                cy="50%"
                                innerRadius={60}
                                outerRadius={100}
                                paddingAngle={3}
                                animationBegin={0}
                                animationDuration={800}
                            >
                                {filteredStatusStats.map((entry, index) => (
                                    <Cell key={`status-${index}`} fill={STATUS_COLORS[entry.status] || '#94a3b8'} />
                                ))}
                            </Pie>
                            <Tooltip
                                contentStyle={{ borderRadius: '12px', border: '1px solid #e5e7eb', boxShadow: '0 4px 12px rgba(0,0,0,0.1)' }}
                                formatter={((value: number | string | undefined, name: string | undefined) => [`${value ?? 0} (${filteredStatusStats.find(s => s.status === name)?.percentage ?? 0}%)`, name ?? '']) as never}
                            />
                            <Legend />
                        </PieChart>
                    </ResponsiveContainer>
                </div>
            </div>

            {/* Category and Priority */}
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                {/* Categories */}
                <div className="bg-white rounded-2xl border border-gray-100 shadow-sm p-6 hover:shadow-md transition-shadow">
                    <h3 className="font-semibold text-gray-900 mb-4 flex items-center gap-2">
                        <BarChart3 className="w-5 h-5 text-[#E42556]" />
                        За категоріями
                    </h3>
                    <ResponsiveContainer width="100%" height={280}>
                        <BarChart data={filteredCategoryStats} layout="vertical">
                            <CartesianGrid strokeDasharray="3 3" stroke="#f0f0f0" />
                            <XAxis type="number" tick={{ fontSize: 12 }} stroke="#94a3b8" />
                            <YAxis dataKey="category" type="category" tick={{ fontSize: 11 }} width={120} stroke="#94a3b8" />
                            <Tooltip
                                contentStyle={{ borderRadius: '12px', border: '1px solid #e5e7eb', boxShadow: '0 4px 12px rgba(0,0,0,0.1)' }}
                                formatter={((value: number | string | undefined) => [value ?? 0, 'Проблем']) as never}
                            />
                            <Bar dataKey="count" radius={[0, 6, 6, 0]} animationDuration={800}>
                                {filteredCategoryStats.map((_, index) => (
                                    <Cell key={`cat-${index}`} fill={CATEGORY_COLORS[index % CATEGORY_COLORS.length]} />
                                ))}
                            </Bar>
                        </BarChart>
                    </ResponsiveContainer>
                </div>

                {/* Priority */}
                <div className="bg-white rounded-2xl border border-gray-100 shadow-sm p-6 hover:shadow-md transition-shadow">
                    <h3 className="font-semibold text-gray-900 mb-4 flex items-center gap-2">
                        <AlertTriangle className="w-5 h-5 text-[#E42556]" />
                        За пріоритетами
                    </h3>
                    <ResponsiveContainer width="100%" height={280}>
                        <PieChart>
                            <Pie
                                data={filteredPriorityStats}
                                dataKey="count"
                                nameKey="priority"
                                cx="50%"
                                cy="50%"
                                outerRadius={110}
                                animationBegin={200}
                                animationDuration={800}
                            >
                                {filteredPriorityStats.map((entry, index) => (
                                    <Cell key={`pri-${index}`} fill={PRIORITY_COLORS[entry.priority] || '#94a3b8'} />
                                ))}
                            </Pie>
                            <Tooltip
                                contentStyle={{ borderRadius: '12px', border: '1px solid #e5e7eb', boxShadow: '0 4px 12px rgba(0,0,0,0.1)' }}
                                formatter={((value: number | string | undefined, name: string | undefined) => [`${value ?? 0} (${filteredPriorityStats.find(p => p.priority === name)?.percentage ?? 0}%)`, name ?? '']) as never}
                            />
                            <Legend />
                        </PieChart>
                    </ResponsiveContainer>
                </div>
            </div>

            {/* Problems Lists */}
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                {/* Recent Problems */}
                <div className="bg-white rounded-2xl border border-gray-100 shadow-sm p-6 hover:shadow-md transition-shadow">
                    <h3 className="font-semibold text-gray-900 mb-4 flex items-center gap-2">
                        <Clock className="w-5 h-5 text-[#E42556]" />
                        Останні проблеми
                    </h3>
                    <div className="space-y-3">
                        {stats.recentProblems.map((problem) => (
                            <Link
                                key={problem.id}
                                to={`/problems/${problem.id}`}
                                className="block p-3 rounded-xl bg-gray-50 hover:bg-gray-100 transition-colors group"
                            >
                                <div className="flex items-start justify-between gap-3">
                                    <div className="min-w-0 flex-1">
                                        <p className="font-medium text-gray-900 truncate group-hover:text-[#E42556] transition-colors">
                                            {problem.title}
                                        </p>
                                        <div className="flex items-center gap-2 mt-1 flex-wrap">
                                            <span
                                                className="text-xs px-2 py-0.5 rounded-full text-white font-medium"
                                                style={{ backgroundColor: STATUS_COLORS[problem.status] || '#94a3b8' }}
                                            >
                                                {problem.status}
                                            </span>
                                            <span
                                                className="text-xs px-2 py-0.5 rounded-full text-white font-medium"
                                                style={{ backgroundColor: PRIORITY_COLORS[problem.priority] || '#94a3b8' }}
                                            >
                                                {problem.priority}
                                            </span>
                                            <span className="text-xs text-gray-400">
                                                {new Date(problem.createdAt).toLocaleDateString('uk-UA')}
                                            </span>
                                        </div>
                                    </div>
                                    <ArrowUpRight className="w-4 h-4 text-gray-400 group-hover:text-[#E42556] transition-colors flex-shrink-0 mt-1" />
                                </div>
                            </Link>
                        ))}
                    </div>
                </div>

                {/* Top Rated */}
                <div className="bg-white rounded-2xl border border-gray-100 shadow-sm p-6 hover:shadow-md transition-shadow">
                    <h3 className="font-semibold text-gray-900 mb-4 flex items-center gap-2">
                        <Star className="w-5 h-5 text-[#E42556]" />
                        Найвищий рейтинг
                    </h3>
                    <div className="space-y-3">
                        {stats.topRatedProblems.map((problem, idx) => (
                            <Link
                                key={problem.id}
                                to={`/problems/${problem.id}`}
                                className="block p-3 rounded-xl bg-gray-50 hover:bg-gray-100 transition-colors group"
                            >
                                <div className="flex items-center gap-3">
                                    <div className="w-8 h-8 rounded-full bg-gradient-to-br from-[#E42556] to-[#c8204b] flex items-center justify-center text-white text-sm font-bold flex-shrink-0">
                                        {idx + 1}
                                    </div>
                                    <div className="min-w-0 flex-1">
                                        <p className="font-medium text-gray-900 truncate group-hover:text-[#E42556] transition-colors">
                                            {problem.title}
                                        </p>
                                        <div className="flex items-center gap-2 mt-0.5">
                                            <div className="flex items-center gap-1">
                                                <Star className="w-3 h-3 text-yellow-500 fill-yellow-500" />
                                                <span className="text-xs font-medium text-gray-600">
                                                    {problem.averageRating?.toFixed(1) || 'N/A'}
                                                </span>
                                            </div>
                                            <span className="text-xs text-gray-400">•</span>
                                            <span className="text-xs text-gray-400">{problem.commentsCount} коментарів</span>
                                        </div>
                                    </div>
                                    <ArrowUpRight className="w-4 h-4 text-gray-400 group-hover:text-[#E42556] transition-colors flex-shrink-0" />
                                </div>
                            </Link>
                        ))}
                    </div>
                </div>
            </div>

            {/* AI Chat Panel */}
            <div className="bg-white rounded-2xl border border-gray-100 shadow-sm overflow-hidden hover:shadow-md transition-shadow">
                {/* Chat Header */}
                <div className="bg-gradient-to-r from-[#1F2732] to-[#2d3848] p-4 flex items-center justify-between">
                    <div className="flex items-center gap-3">
                        <div className="w-10 h-10 rounded-xl bg-white/10 backdrop-blur-sm flex items-center justify-center">
                            <Sparkles className="w-5 h-5 text-white" />
                        </div>
                        <div>
                            <h3 className="text-white font-semibold">AI Аналітик</h3>
                            <p className="text-white/60 text-xs">Задайте питання про статистику та тренди</p>
                        </div>
                    </div>
                    <div className="flex items-center gap-2">
                        <div className="w-2 h-2 rounded-full bg-green-400 animate-pulse" />
                        <span className="text-white/60 text-xs">Онлайн</span>
                    </div>
                </div>

                {/* Quick Prompts */}
                <div className="p-4 border-b border-gray-100 bg-gray-50/50">
                    <div className="flex flex-wrap gap-2">
                        {QUICK_PROMPTS.map((prompt) => (
                            <button
                                key={prompt.label}
                                onClick={() => sendMessage(prompt.message)}
                                disabled={isChatLoading}
                                className="px-3 py-1.5 rounded-full bg-white border border-gray-200 text-sm text-gray-700 hover:border-[#E42556] hover:text-[#E42556] transition-all hover:shadow-sm disabled:opacity-50 disabled:cursor-not-allowed"
                            >
                                {prompt.label}
                            </button>
                        ))}
                    </div>
                </div>

                {/* Chat Messages */}
                <div
                    className={`overflow-y-auto transition-all duration-300 ${isChatExpanded ? 'max-h-[500px] min-h-[200px]' : 'max-h-[200px]'
                        }`}
                >
                    {chatMessages.length === 0 ? (
                        <div className="flex flex-col items-center justify-center py-12 text-gray-400">
                            <Bot className="w-12 h-12 mb-3 text-gray-300" />
                            <p className="font-medium text-gray-500">Привіт! Я — ваш AI-аналітик</p>
                            <p className="text-sm mt-1">Запитайте мене про статистику, тренди або рекомендації</p>
                        </div>
                    ) : (
                        <div className="p-4 space-y-4">
                            {chatMessages.map((msg) => (
                                <div
                                    key={msg.id}
                                    className={`flex gap-3 ${msg.role === 'user' ? 'justify-end' : 'justify-start'}`}
                                >
                                    {msg.role === 'assistant' && (
                                        <div className="w-8 h-8 rounded-full bg-gradient-to-br from-[#E42556] to-[#c8204b] flex items-center justify-center flex-shrink-0 mt-1">
                                            <Bot className="w-4 h-4 text-white" />
                                        </div>
                                    )}
                                    <div
                                        className={`max-w-[80%] rounded-2xl px-4 py-3 ${msg.role === 'user'
                                            ? 'bg-[#1F2732] text-white rounded-br-md'
                                            : 'bg-gray-100 text-gray-800 rounded-bl-md'
                                            }`}
                                    >
                                        {msg.role === 'assistant' ? (
                                            <div className="prose prose-sm max-w-none prose-headings:text-gray-900 prose-p:text-gray-700 prose-li:text-gray-700 prose-strong:text-gray-900">
                                                <ReactMarkdown remarkPlugins={[remarkGfm]}>
                                                    {msg.content}
                                                </ReactMarkdown>
                                            </div>
                                        ) : (
                                            <p className="text-sm">{msg.content}</p>
                                        )}
                                        {msg.suggestedFilter && (
                                            <div className="mt-2 pt-2 border-t border-gray-200">
                                                <div className="flex items-center gap-1.5 text-xs text-[#E42556] font-medium">
                                                    <Filter className="w-3 h-3" />
                                                    Фільтр застосовано на дашборд
                                                </div>
                                            </div>
                                        )}
                                        <span className={`text-[10px] mt-1 block ${msg.role === 'user' ? 'text-white/50' : 'text-gray-400'}`}>
                                            {msg.timestamp.toLocaleTimeString('uk-UA', { hour: '2-digit', minute: '2-digit' })}
                                        </span>
                                    </div>
                                    {msg.role === 'user' && (
                                        <div className="w-8 h-8 rounded-full bg-gray-200 flex items-center justify-center flex-shrink-0 mt-1">
                                            <User className="w-4 h-4 text-gray-600" />
                                        </div>
                                    )}
                                </div>
                            ))}

                            {isChatLoading && (
                                <div className="flex gap-3 justify-start">
                                    <div className="w-8 h-8 rounded-full bg-gradient-to-br from-[#E42556] to-[#c8204b] flex items-center justify-center flex-shrink-0">
                                        <Bot className="w-4 h-4 text-white" />
                                    </div>
                                    <div className="bg-gray-100 rounded-2xl rounded-bl-md px-4 py-3">
                                        <div className="flex gap-1.5">
                                            <div className="w-2 h-2 rounded-full bg-gray-400 animate-bounce" style={{ animationDelay: '0ms' }} />
                                            <div className="w-2 h-2 rounded-full bg-gray-400 animate-bounce" style={{ animationDelay: '150ms' }} />
                                            <div className="w-2 h-2 rounded-full bg-gray-400 animate-bounce" style={{ animationDelay: '300ms' }} />
                                        </div>
                                    </div>
                                </div>
                            )}
                            <div ref={chatEndRef} />
                        </div>
                    )}
                </div>

                {/* Chat Input */}
                <form onSubmit={handleSubmit} className="border-t border-gray-100 p-4 flex gap-3 items-center">
                    <input
                        ref={inputRef}
                        type="text"
                        value={chatInput}
                        onChange={(e) => setChatInput(e.target.value)}
                        placeholder="Запитайте AI про статистику..."
                        disabled={isChatLoading}
                        className="flex-1 px-4 py-2.5 rounded-xl bg-gray-100 text-sm text-gray-900 placeholder:text-gray-400 focus:outline-none focus:ring-2 focus:ring-transparent focus:bg-white border border-transparent focus:border-gray-300 transition-all disabled:opacity-50"
                    />
                    <button
                        type="submit"
                        disabled={isChatLoading || !chatInput.trim()}
                        className="group relative inline-flex items-center justify-center gap-2 px-5 py-2.5 rounded-2xl text-sm font-semibold text-[#E42556] bg-white border border-[#E42556]/40 shadow-[0_10px_25px_rgba(228,37,86,0.15)] transition-all hover:-translate-y-0.5 hover:bg-gradient-to-r hover:from-[#E42556] hover:to-[#c8204b] hover:text-white hover:shadow-[0_12px_30px_rgba(228,37,86,0.35)] focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 focus-visible:ring-[#E42556]/40 disabled:opacity-60 disabled:cursor-not-allowed disabled:hover:-translate-y-0"
                    >
                        <Send className="w-4 h-4 transition-transform duration-300 group-hover:translate-x-0.5" />
                    </button>
                </form>
            </div>
        </div>
    )
}

// Stat Card Component
function StatCard({
    icon,
    label,
    value,
    color,
    subtitle,
    trend,
}: {
    icon: React.ReactNode
    label: string
    value: number
    color: string
    subtitle?: string
    trend?: number
}) {
    return (
        <div className="bg-white rounded-2xl border border-gray-100 shadow-sm p-5 hover:shadow-md transition-all hover:-translate-y-0.5 group">
            <div className="flex items-center justify-between mb-3">
                <div
                    className="w-10 h-10 rounded-xl flex items-center justify-center transition-transform group-hover:scale-110"
                    style={{ backgroundColor: `${color}15`, color }}
                >
                    {icon}
                </div>
                {trend !== undefined && trend !== 0 && (
                    <div className={`flex items-center gap-1 text-xs font-medium ${trend > 0 ? 'text-red-500' : 'text-green-500'}`}>
                        {trend > 0 ? <TrendingUp className="w-3 h-3" /> : <TrendingDown className="w-3 h-3" />}
                        {Math.abs(trend)}%
                    </div>
                )}
            </div>
            <p className="text-2xl font-bold text-gray-900">
                <AnimatedCounter end={value} />
            </p>
            <p className="text-sm text-gray-500 mt-0.5">{label}</p>
            {subtitle && <p className="text-xs text-gray-400 mt-1">{subtitle}</p>}
        </div>
    )
}

// Mini Stat Card
function MiniStatCard({
    icon,
    label,
    value,
    suffix = '',
}: {
    icon: React.ReactNode
    label: string
    value: number
    suffix?: string
}) {
    return (
        <div className="bg-gradient-to-r from-gray-50 to-white rounded-xl border border-gray-100 p-4 flex items-center gap-3 hover:shadow-sm transition-all">
            <div className="w-8 h-8 rounded-lg bg-gray-100 flex items-center justify-center text-gray-500">
                {icon}
            </div>
            <div>
                <p className="text-lg font-bold text-gray-900">
                    {typeof value === 'number' && Number.isInteger(value) ? (
                        <AnimatedCounter end={value} />
                    ) : (
                        value
                    )}
                    {suffix}
                </p>
                <p className="text-xs text-gray-500">{label}</p>
            </div>
        </div>
    )
}
