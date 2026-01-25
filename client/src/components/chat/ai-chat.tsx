import { useState, useRef, useEffect } from 'react'
import { MessageCircle, X, Send, Mic, MicOff, Trash2, Loader2, Bot, User, MapPin, Star, Clock } from 'lucide-react'
import { cn } from '@/lib/utils'
import { chatService } from '@/services/chat.service'
import { useAuth } from '@/contexts/auth-context'
import type { ChatMessage, ProblemSummaryChat } from '@/types/chat'
import { useNavigate } from 'react-router-dom'

export function AiChat() {
  const [isOpen, setIsOpen] = useState(false)
  const [messages, setMessages] = useState<ChatMessage[]>([])
  const [inputValue, setInputValue] = useState('')
  const [isLoading, setIsLoading] = useState(false)
  const [isRecording, setIsRecording] = useState(false)
  const [audioBlob, setAudioBlob] = useState<Blob | null>(null)
  const messagesEndRef = useRef<HTMLDivElement>(null)
  const mediaRecorderRef = useRef<MediaRecorder | null>(null)
  const audioChunksRef = useRef<Blob[]>([])
  const { getClerkToken } = useAuth()
  const navigate = useNavigate()

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' })
  }

  useEffect(() => {
    scrollToBottom()
  }, [messages])

  useEffect(() => {
    if (isOpen && messages.length === 0) {
      setMessages([
        {
          id: crypto.randomUUID(),
          content: 'Привіт! 👋 Я AI-асистент платформи Ostroh Problems. Можу допомогти вам:\n\n• Знайти проблеми за категорією, статусом або рейтингом\n• Пояснити як користуватися сайтом\n• Відповісти на питання про вашу роль\n\nЗапитайте мене про що завгодно!',
          role: 'assistant',
          timestamp: new Date(),
          responseType: 'Help',
        },
      ])
    }
  }, [isOpen, messages.length])

  const handleSendMessage = async () => {
    if (!inputValue.trim() && !audioBlob) return

    const userMessage: ChatMessage = {
      id: crypto.randomUUID(),
      content: inputValue.trim() || '🎤 Голосове повідомлення',
      role: 'user',
      timestamp: new Date(),
    }

    setMessages((prev) => [...prev, userMessage])
    setInputValue('')
    setAudioBlob(null)
    setIsLoading(true)

    try {
      const token = await getClerkToken?.()
      
      let messageToSend = inputValue.trim()
      
      if (audioBlob) {
        messageToSend = await transcribeAudio(audioBlob)
      }

      const response = await chatService.sendMessage(
        { message: messageToSend },
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

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const transcribeAudio = async (blob: Blob): Promise<string> => {
    // TODO: Integrate with Google Speech-to-Text API for real transcription
    return 'Голосове повідомлення (транскрипція недоступна в демо-версії)'
  }

  const startRecording = async () => {
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ audio: true })
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
        setAudioBlob(audioBlob)
        stream.getTracks().forEach((track) => track.stop())
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
      mediaRecorderRef.current.stop()
    }
    setIsRecording(false)
    setAudioBlob(null)
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
      <div className="flex items-center gap-3 mt-2 text-xs text-gray-500">
        <span className="flex items-center gap-1">
          <MapPin className="w-3 h-3" />
          {problem.categories[0] || 'Інше'}
        </span>
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
            isUser ? 'bg-primary text-white' : 'bg-gray-100 text-gray-600'
          )}
        >
          {isUser ? <User className="w-4 h-4" /> : <Bot className="w-4 h-4" />}
        </div>
        <div
          className={cn(
            'max-w-[80%] rounded-2xl px-4 py-2',
            isUser
              ? 'bg-primary text-white rounded-tr-sm'
              : 'bg-gray-100 text-gray-900 rounded-tl-sm'
          )}
        >
          <div className="text-sm whitespace-pre-wrap">{message.content}</div>
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
          'fixed bottom-6 right-6 w-14 h-14 rounded-full shadow-lg flex items-center justify-center transition-all duration-300 z-50',
          isOpen
            ? 'bg-gray-600 hover:bg-gray-700 rotate-0'
            : 'bg-primary hover:bg-primary/90 animate-pulse'
        )}
        aria-label={isOpen ? 'Закрити чат' : 'Відкрити AI асистента'}
      >
        {isOpen ? (
          <X className="w-6 h-6 text-white" />
        ) : (
          <MessageCircle className="w-6 h-6 text-white" />
        )}
      </button>

      {/* Chat Window */}
      <div
        className={cn(
          'fixed bottom-24 right-6 w-96 max-w-[calc(100vw-3rem)] bg-white rounded-2xl shadow-2xl flex flex-col z-50 transition-all duration-300 origin-bottom-right',
          isOpen
            ? 'opacity-100 scale-100 translate-y-0'
            : 'opacity-0 scale-95 translate-y-4 pointer-events-none'
        )}
        style={{ height: 'min(600px, calc(100vh - 10rem))' }}
      >
        {/* Header */}
        <div className="bg-primary text-white px-4 py-3 rounded-t-2xl flex items-center gap-3">
          <div className="w-10 h-10 bg-white/20 rounded-full flex items-center justify-center">
            <Bot className="w-6 h-6" />
          </div>
          <div>
            <h3 className="font-semibold">AI Асистент</h3>
            <p className="text-xs text-white/80">Ostroh Problems</p>
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
          <div ref={messagesEndRef} />
        </div>

        {/* Audio Recording Indicator */}
        {(isRecording || audioBlob) && (
          <div className="px-4 py-2 bg-gray-50 border-t flex items-center justify-between">
            <div className="flex items-center gap-2">
              {isRecording ? (
                <>
                  <div className="w-3 h-3 bg-red-500 rounded-full animate-pulse" />
                  <span className="text-sm text-gray-600">Запис...</span>
                </>
              ) : (
                <>
                  <div className="w-3 h-3 bg-green-500 rounded-full" />
                  <span className="text-sm text-gray-600">Записано</span>
                </>
              )}
            </div>
            <button
              onClick={cancelRecording}
              className="p-1 hover:bg-gray-200 rounded-full transition-colors"
            >
              <Trash2 className="w-4 h-4 text-gray-500" />
            </button>
          </div>
        )}

        {/* Input */}
        <div className="p-4 border-t">
          <div className="flex items-center gap-2">
            <button
              onClick={isRecording ? stopRecording : startRecording}
              disabled={isLoading}
              className={cn(
                'p-2 rounded-full transition-colors',
                isRecording
                  ? 'bg-red-500 text-white hover:bg-red-600'
                  : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
              )}
              aria-label={isRecording ? 'Зупинити запис' : 'Почати запис'}
            >
              {isRecording ? (
                <MicOff className="w-5 h-5" />
              ) : (
                <Mic className="w-5 h-5" />
              )}
            </button>
            <input
              type="text"
              value={inputValue}
              onChange={(e) => setInputValue(e.target.value)}
              onKeyPress={handleKeyPress}
              placeholder="Напишіть повідомлення..."
              disabled={isLoading || isRecording}
              className="flex-1 px-4 py-2 bg-gray-100 rounded-full text-sm focus:outline-none focus:ring-2 focus:ring-primary/50 disabled:opacity-50"
            />
            <button
              onClick={handleSendMessage}
              disabled={isLoading || (!inputValue.trim() && !audioBlob)}
              className="p-2 bg-primary text-white rounded-full hover:bg-primary/90 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
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
