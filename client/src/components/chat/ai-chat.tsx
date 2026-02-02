import { useState, useRef, useEffect } from 'react'
import { MessageCircle, X, Send, Mic, MicOff, Trash2, Loader2, Bot, User, MapPin, Star, Clock, Sparkles } from 'lucide-react'
import { cn } from '@/lib/utils'
import { chatService } from '@/services/chat.service'
import { useAuth } from '@/contexts/auth-context'
import type { ChatMessage, ProblemSummaryChat } from '@/types/chat'
import { useNavigate } from 'react-router-dom'
import ReactMarkdown from 'react-markdown'
import remarkGfm from 'remark-gfm'

export function AiChat() {
  const [isOpen, setIsOpen] = useState(false)
  const [messages, setMessages] = useState<ChatMessage[]>(() => {
    const saved = sessionStorage.getItem('ai-chat-messages')
    if (saved) {
      try {
        const parsed = JSON.parse(saved) as ChatMessage[]
        return parsed.map((msg) => ({
          ...msg,
          timestamp: new Date(msg.timestamp)
        }))
      } catch {
        return []
      }
    }
    return []
  })
  const [inputValue, setInputValue] = useState('')
  const [isLoading, setIsLoading] = useState(false)
  const [isRecording, setIsRecording] = useState(false)
  const [isProcessingVoice, setIsProcessingVoice] = useState(false)
  const messagesEndRef = useRef<HTMLDivElement>(null)
  const mediaRecorderRef = useRef<MediaRecorder | null>(null)
  const audioChunksRef = useRef<Blob[]>([])
  const streamRef = useRef<MediaStream | null>(null)
  const { getClerkToken } = useAuth()
  const navigate = useNavigate()

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' })
  }

  useEffect(() => {
    scrollToBottom()
  }, [messages])

  useEffect(() => {
    if (messages.length > 0) {
      sessionStorage.setItem('ai-chat-messages', JSON.stringify(messages))
    }
  }, [messages])

  useEffect(() => {
    if (isOpen && messages.length === 0) {
      setMessages([
        {
          id: crypto.randomUUID(),
          content: 'Привіт! 👋 Я AI-асистент платформи Острог Разом. Можу допомогти вам:\n\n• Знайти проблеми за категорією, статусом або рейтингом\n• Пояснити як користуватися сайтом\n• Відповісти на питання про вашу роль\n\nЗапитайте мене про що завгодно!',
          role: 'assistant',
          timestamp: new Date(),
          responseType: 'Help',
        },
      ])
    }
  }, [isOpen, messages.length])

  const handleSendMessage = async () => {
    if (!inputValue.trim()) return

    const userMessage: ChatMessage = {
      id: crypto.randomUUID(),
      content: inputValue.trim(),
      role: 'user',
      timestamp: new Date(),
    }

    setMessages((prev) => [...prev, userMessage])
    setInputValue('')
    setIsLoading(true)

    try {
      const token = await getClerkToken?.()

      const response = await chatService.sendMessage(
        { message: inputValue.trim() },
        token ?? null
      )

      const assistantMessage: ChatMessage = {
        id: crypto.randomUUID(),
        content: response.message,
        role: 'assistant',
        timestamp: new Date(),
        responseType: response.responseType,
        problems: response.problems ?? undefined,
      }

      setMessages((prev) => [...prev, assistantMessage])
    } catch (error) {
      console.error('Chat error:', error)
      const errorMessage: ChatMessage = {
        id: crypto.randomUUID(),
        content: 'Вибачте, сталася помилка. Спробуйте ще раз пізніше.',
        role: 'assistant',
        timestamp: new Date(),
        responseType: 'Error',
      }
      setMessages((prev) => [...prev, errorMessage])
    } finally {
      setIsLoading(false)
    }
  }

  const handleVoiceMessage = async (audioBlob: Blob) => {
    // Add user message indicator for voice
    const userMessage: ChatMessage = {
      id: crypto.randomUUID(),
      content: '🎤 Голосове повідомлення',
      role: 'user',
      timestamp: new Date(),
    }

    setMessages((prev) => [...prev, userMessage])
    setIsProcessingVoice(true)

    try {
      const token = await getClerkToken?.()

      const response = await chatService.sendVoiceMessage(audioBlob, token ?? null)

      const assistantMessage: ChatMessage = {
        id: crypto.randomUUID(),
        content: response.message,
        role: 'assistant',
        timestamp: new Date(),
        responseType: response.responseType,
        problems: response.problems ?? undefined,
      }

      setMessages((prev) => [...prev, assistantMessage])
    } catch (error) {
      console.error('Voice message error:', error)
      const errorMessage: ChatMessage = {
        id: crypto.randomUUID(),
        content: 'Не вдалося обробити голосове повідомлення. Спробуйте ще раз.',
        role: 'assistant',
        timestamp: new Date(),
        responseType: 'Error',
      }
      setMessages((prev) => [...prev, errorMessage])
    } finally {
      setIsProcessingVoice(false)
    }
  }

  const startRecording = async () => {
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ audio: true })
      streamRef.current = stream
      const mediaRecorder = new MediaRecorder(stream)
      mediaRecorderRef.current = mediaRecorder
      audioChunksRef.current = []

      mediaRecorder.ondataavailable = (event) => {
        if (event.data.size > 0) {
          audioChunksRef.current.push(event.data)
        }
      }

      mediaRecorder.onstop = () => {
        const audioBlob = new Blob(audioChunksRef.current, { type: 'audio/webm' })
        stream.getTracks().forEach((track) => track.stop())
        streamRef.current = null
        // Automatically send voice message for processing
        if (audioBlob.size > 0) {
          handleVoiceMessage(audioBlob)
        }
      }

      mediaRecorder.start()
      setIsRecording(true)
    } catch (error) {
      console.error('Error starting recording:', error)
      alert('Не вдалося отримати доступ до мікрофона')
    }
  }

  const stopRecording = () => {
    if (mediaRecorderRef.current && isRecording) {
      mediaRecorderRef.current.stop()
      setIsRecording(false)
    }
  }

  const cancelRecording = () => {
    if (mediaRecorderRef.current && isRecording) {
      // Remove onstop handler to prevent sending
      mediaRecorderRef.current.onstop = null
      mediaRecorderRef.current.stop()
    }
    if (streamRef.current) {
      streamRef.current.getTracks().forEach((track) => track.stop())
      streamRef.current = null
    }
    setIsRecording(false)
    audioChunksRef.current = []
  }

  const handleKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter' && !e.shiftKey) {
      e.preventDefault()
      handleSendMessage()
    }
  }

  const navigateToProblem = (problemId: string) => {
    navigate(`/problems/${problemId}`)
    setIsOpen(false)
  }

  const renderProblemCard = (problem: ProblemSummaryChat) => (
    <div
      key={problem.id}
      onClick={() => navigateToProblem(problem.id)}
      className="bg-white border border-gray-200 rounded-lg p-3 cursor-pointer hover:shadow-md transition-shadow"
    >
      <div className="flex items-start justify-between gap-2">
        <h4 className="font-medium text-sm text-gray-900 line-clamp-1">{problem.title}</h4>
        <span
          className={cn(
            'px-2 py-0.5 text-xs rounded-full whitespace-nowrap',
            problem.status === 'Нова' && 'bg-blue-100 text-blue-700',
            problem.status === 'В роботі' && 'bg-yellow-100 text-yellow-700',
            problem.status === 'Виконано' && 'bg-green-100 text-green-700',
            problem.status === 'Відхилено' && 'bg-red-100 text-red-700'
          )}
        >
          {problem.status}
        </span>
      </div>
      <p className="text-xs text-gray-600 mt-1 line-clamp-2">{problem.description}</p>
      <div className="flex flex-col gap-1 mt-2">
        <div className="flex items-start gap-1 text-xs text-gray-500">
          <MapPin className="w-3 h-3 mt-0.5 flex-shrink-0" />
          <div className="flex flex-col gap-0.5">
            {problem.categories.map((category, index) => (
              <span key={index}>{category}</span>
            ))}
          </div>
        </div>
        <div className="flex items-center gap-3 text-xs text-gray-500">
          {problem.averageRating && (
            <span className="flex items-center gap-1">
              <Star className="w-3 h-3 text-yellow-500" />
              {problem.averageRating.toFixed(1)}
            </span>
          )}
          <span className="flex items-center gap-1">
            <Clock className="w-3 h-3" />
            {new Date(problem.createdAt).toLocaleDateString('uk-UA')}
          </span>
        </div>
      </div>
    </div>
  )

  const renderMessage = (message: ChatMessage) => {
    const isUser = message.role === 'user'

    return (
      <div
        key={message.id}
        className={cn('flex gap-2 mb-4', isUser ? 'flex-row-reverse' : 'flex-row')}
      >
        <div
          className={cn(
            'w-8 h-8 rounded-full flex items-center justify-center flex-shrink-0',
            isUser ? 'bg-violet-500 text-white' : 'bg-gradient-to-br from-violet-500 to-purple-600 text-white'
          )}
        >
          {isUser ? <User className="w-4 h-4" /> : <Sparkles className="w-4 h-4" />}
        </div>
        <div
          className={cn(
            'max-w-[80%] rounded-2xl px-4 py-2',
            isUser
              ? 'bg-violet-500 text-white rounded-tr-sm'
              : 'bg-gray-100 text-gray-900 rounded-tl-sm'
          )}
        >
          {isUser ? (
            <div className="text-sm whitespace-pre-wrap">{message.content}</div>
          ) : (
            <div className="text-sm prose prose-sm max-w-none prose-headings:mt-2 prose-headings:mb-1 prose-p:my-1 prose-ul:my-1 prose-ol:my-1 prose-li:my-0.5">
              <ReactMarkdown remarkPlugins={[remarkGfm]}>{message.content}</ReactMarkdown>
            </div>
          )}
          {message.problems && message.problems.length > 0 && (
            <div className="mt-3 space-y-2">
              {message.problems.map(renderProblemCard)}
            </div>
          )}
          <div
            className={cn(
              'text-xs mt-1',
              isUser ? 'text-white/70' : 'text-gray-500'
            )}
          >
            {message.timestamp.toLocaleTimeString('uk-UA', {
              hour: '2-digit',
              minute: '2-digit',
            })}
          </div>
        </div>
      </div>
    )
  }

  return (
    <>
      {/* Chat Toggle Button */}
      <button
        onClick={() => setIsOpen(!isOpen)}
        className={cn(
          'fixed bottom-6 right-6 w-14 h-14 rounded-full shadow-lg flex items-center justify-center transition-transform duration-300 z-50 border-2 focus:outline-none focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-violet-300',
          isOpen
            ? 'bg-white border-gray-300 hover:bg-gray-50'
            : 'bg-gradient-to-br from-violet-500 to-purple-600 border-violet-600 hover:from-violet-600 hover:to-purple-700 shadow-violet-200'
        )}
        aria-label={isOpen ? 'Закрити чат' : 'Відкрити AI асистента'}
      >
        {isOpen ? (
          <X className="w-6 h-6 text-gray-700" />
        ) : (
          <MessageCircle className="w-6 h-6 text-white" />
        )}
      </button>

      {/* Chat Window */}
      <div
        className={cn(
          'fixed bottom-24 right-6 w-[32rem] max-w-[calc(100vw-3rem)] bg-white rounded-2xl shadow-2xl flex flex-col z-50 transition-all duration-300 origin-bottom-right',
          isOpen
            ? 'opacity-100 scale-100 translate-y-0'
            : 'opacity-0 scale-95 translate-y-4 pointer-events-none'
        )}
        style={{ height: 'min(600px, calc(100vh - 10rem))' }}
      >
        {/* Header */}
        <div className="bg-gradient-to-r from-violet-500 to-purple-600 text-white px-4 py-3 rounded-t-2xl flex items-center gap-3">
          <div className="w-10 h-10 bg-white/20 rounded-full flex items-center justify-center">
            <Bot className="w-6 h-6" />
          </div>
          <div>
            <h3 className="font-semibold">AI Асистент</h3>
            <p className="text-xs text-white/80">Острог разом</p>
          </div>
        </div>

        {/* Messages */}
        <div className="flex-1 overflow-y-auto p-4">
          {messages.map(renderMessage)}
          {isLoading && (
            <div className="flex gap-2 mb-4">
              <div className="w-8 h-8 rounded-full bg-gray-100 flex items-center justify-center flex-shrink-0">
                <Bot className="w-4 h-4 text-gray-600" />
              </div>
              <div className="bg-gray-100 rounded-2xl rounded-tl-sm px-4 py-3">
                <Loader2 className="w-5 h-5 animate-spin text-gray-500" />
              </div>
            </div>
          )}
          {isProcessingVoice && (
            <div className="flex gap-2 mb-4">
              <div className="w-8 h-8 rounded-full bg-gray-100 flex items-center justify-center flex-shrink-0">
                <Bot className="w-4 h-4 text-gray-600" />
              </div>
              <div className="bg-gray-100 rounded-2xl rounded-tl-sm px-4 py-3 flex items-center gap-2">
                <Loader2 className="w-4 h-4 animate-spin text-violet-500" />
                <span className="text-sm text-gray-600">Розпізнаю голос...</span>
              </div>
            </div>
          )}
          <div ref={messagesEndRef} />
        </div>

        {/* Audio Recording Indicator */}
        {isRecording && (
          <div className="px-4 py-2 bg-gray-50 border-t flex items-center justify-between">
            <div className="flex items-center gap-2">
              <div className="w-3 h-3 bg-red-500 rounded-full animate-pulse" />
              <span className="text-sm text-gray-600">Запис...</span>
            </div>
            <button
              onClick={cancelRecording}
              className="p-2 text-gray-500 bg-gray-100 rounded-lg hover:bg-gray-200 transition-colors focus:outline-none focus-visible:ring-violet-300 group"
              aria-label="Скасувати запис"
            >
              <span className="inline-flex items-center justify-center w-6 h-6 rounded-md border text-red-600 bg-white group-hover:bg-red-50 transition-colors">
                <Trash2 className="w-3.5 h-3.5" strokeWidth={2.3} />
              </span>
            </button>
          </div>
        )}

        {/* Input */}
        <div className="p-4 border-t">
          <div className="flex items-center gap-2">
            <button
              onClick={isRecording ? stopRecording : startRecording}
              disabled={isLoading || isProcessingVoice}
              className={cn(
                'p-2 rounded-full focus:outline-none focus-visible:ring-2 focus-visible:ring-violet-300 transition-none',
                isRecording
                  ? 'bg-red-500 text-white hover:bg-red-600'
                  : 'bg-violet-100 text-violet-600 hover:bg-violet-200'
              )}
              aria-label={isRecording ? 'Зупинити запис' : 'Почати запис'}
            >
              {isRecording ? (
                <MicOff className="w-5 h-5" />
              ) : (
                <Mic className="w-5 h-5" />
              )}
            </button>
            <textarea
              value={inputValue}
              onChange={(e) => setInputValue(e.target.value)}
              onKeyDown={handleKeyPress}
              placeholder="Напишіть повідомлення..."
              disabled={isLoading || isRecording || isProcessingVoice}
              rows={1}
              className="flex-1 px-4 py-2 bg-gray-100 rounded-2xl text-sm border border-transparent focus:outline-none focus:ring-0 focus:border-gray-400 disabled:opacity-50 resize-none overflow-y-auto"
              style={{ minHeight: '40px', maxHeight: '120px' }}
              onInput={(e) => {
                const target = e.target as HTMLTextAreaElement
                target.style.height = 'auto'
                target.style.height = Math.min(target.scrollHeight, 120) + 'px'
              }}
            />
            <button
              onClick={handleSendMessage}
              disabled={isLoading || isProcessingVoice || !inputValue.trim()}
              className="p-2 bg-violet-500 text-white rounded-full hover:bg-violet-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed focus:outline-none focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-violet-300"
              aria-label="Надіслати"
            >
              <Send className="w-5 h-5" />
            </button>
          </div>
        </div>
      </div>
    </>
  )
}
