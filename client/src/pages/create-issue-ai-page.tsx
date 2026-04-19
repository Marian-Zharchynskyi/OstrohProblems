import { useState, useRef, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import type { CreateProblem } from '@/types';
import type { ExtractedProblemData } from '@/types/chat';
import { CATEGORIES } from '@/constants/categories';
import { useCreateProblem, useUploadProblemImages } from '@/features/problems/hooks/use-problems';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Label } from '@/components/ui/label';
import { LocationPickerMap } from '@/components/location-picker-map';
import { toast } from '@/lib/toast';
import { chatService } from '@/services/chat.service';
import { useAuth } from '@/contexts/auth-context';
import { cn } from '@/lib/utils';
import { Send, Mic, MicOff, Loader2, Bot, User, Sparkles, Camera, Plus, ChevronDown, Check, Edit3, RotateCcw, Crosshair, Trash2, Lightbulb } from 'lucide-react';

const MAX_IMAGES_COUNT = 4;
const PRIORITIES = ['Низький', 'Середній', 'Високий', 'Критичний'];

interface ChatMessage {
  id: string;
  content: string;
  role: 'user' | 'assistant';
  timestamp: Date;
}

export function CreateIssueAiPage() {
  const navigate = useNavigate();
  const createMutation = useCreateProblem();
  const uploadImagesMutation = useUploadProblemImages();
  const { getClerkToken } = useAuth();

  const [messages, setMessages] = useState<ChatMessage[]>([
    {
      id: crypto.randomUUID(),
      content:
        'Привіт! 👋 Я допоможу вам створити звіт про проблему. Просто опишіть проблему текстом або голосом, і я автоматично заповню всі поля.\n\nНаприклад: "На вулиці Шевченка біля парку велика яма, через яку важко проїхати машиною"',
      role: 'assistant',
      timestamp: new Date(),
    },
  ]);
  const [inputValue, setInputValue] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [isRecording, setIsRecording] = useState(false);
  const [isProcessingVoice, setIsProcessingVoice] = useState(false);
  const messagesEndRef = useRef<HTMLDivElement>(null);
  const mediaRecorderRef = useRef<MediaRecorder | null>(null);
  const audioChunksRef = useRef<Blob[]>([]);
  const streamRef = useRef<MediaStream | null>(null);

  const [extractedData, setExtractedData] = useState<ExtractedProblemData | null>(null);
  const [isEditing, setIsEditing] = useState(false);
  const [formData, setFormData] = useState<CreateProblem>({
    title: '',
    latitude: 0,
    longitude: 0,
    description: '',
    categoryNames: [],
    priority: '',
  });
  const [files, setFiles] = useState<FileList | null>(null);
  const [imagePreviews, setImagePreviews] = useState<string[]>([]);
  const [streetName, setStreetName] = useState('');
  const [mapKey, setMapKey] = useState(0);
  const [tipsExpanded, setTipsExpanded] = useState(false);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  };

  useEffect(() => {
    scrollToBottom();
  }, [messages]);

  useEffect(() => {
    if (extractedData) {
      setFormData({
        title: extractedData.title,
        description: extractedData.description,
        categoryNames: extractedData.categories,
        priority: extractedData.priority,
        latitude: extractedData.latitude || 0,
        longitude: extractedData.longitude || 0,
      });
      if (extractedData.streetName) {
        setStreetName(extractedData.streetName);
      }
      // Refresh map if coordinates were provided by AI
      if (extractedData.latitude && extractedData.longitude) {
        setMapKey((prev) => prev + 1);
      }
    }
  }, [extractedData]);

  const handleSendMessage = async () => {
    if (!inputValue.trim()) return;

    const userMessage: ChatMessage = {
      id: crypto.randomUUID(),
      content: inputValue.trim(),
      role: 'user',
      timestamp: new Date(),
    };

    setMessages((prev) => [...prev, userMessage]);
    setInputValue('');
    setIsLoading(true);

    try {
      const token = await getClerkToken?.();
      if (!token) {
        throw new Error('Не авторизовано');
      }

      const response = await chatService.extractProblemData(inputValue.trim(), token);
      setExtractedData(response);

      const assistantMessage: ChatMessage = {
        id: crypto.randomUUID(),
        content: response.aiMessage || 'Дані успішно розпізнано! Перевірте preview справа.',
        role: 'assistant',
        timestamp: new Date(),
      };

      setMessages((prev) => [...prev, assistantMessage]);
    } catch (error) {
      console.error('Extract error:', error);
      const errorMessage: ChatMessage = {
        id: crypto.randomUUID(),
        content: 'Вибачте, сталася помилка. Спробуйте ще раз.',
        role: 'assistant',
        timestamp: new Date(),
      };
      setMessages((prev) => [...prev, errorMessage]);
    } finally {
      setIsLoading(false);
    }
  };

  const handleVoiceMessage = async (audioBlob: Blob) => {
    const userMessage: ChatMessage = {
      id: crypto.randomUUID(),
      content: '🎤 Голосове повідомлення',
      role: 'user',
      timestamp: new Date(),
    };

    setMessages((prev) => [...prev, userMessage]);
    setIsProcessingVoice(true);

    try {
      const token = await getClerkToken?.();
      if (!token) {
        throw new Error('Не авторизовано');
      }

      const response = await chatService.extractProblemDataFromVoice(audioBlob, token);
      setExtractedData(response);

      const assistantMessage: ChatMessage = {
        id: crypto.randomUUID(),
        content: response.aiMessage || 'Дані успішно розпізнано! Перевірте preview справа.',
        role: 'assistant',
        timestamp: new Date(),
      };

      setMessages((prev) => [...prev, assistantMessage]);
    } catch (error) {
      console.error('Voice extract error:', error);
      const errorMessage: ChatMessage = {
        id: crypto.randomUUID(),
        content: 'Не вдалося обробити голосове повідомлення. Спробуйте ще раз.',
        role: 'assistant',
        timestamp: new Date(),
      };
      setMessages((prev) => [...prev, errorMessage]);
    } finally {
      setIsProcessingVoice(false);
    }
  };

  const startRecording = async () => {
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
      streamRef.current = stream;
      const mediaRecorder = new MediaRecorder(stream);
      mediaRecorderRef.current = mediaRecorder;
      audioChunksRef.current = [];

      mediaRecorder.ondataavailable = (event) => {
        if (event.data.size > 0) {
          audioChunksRef.current.push(event.data);
        }
      };

      mediaRecorder.onstop = () => {
        const audioBlob = new Blob(audioChunksRef.current, { type: 'audio/webm' });
        stream.getTracks().forEach((track) => track.stop());
        streamRef.current = null;
        if (audioBlob.size > 0) {
          handleVoiceMessage(audioBlob);
        }
      };

      mediaRecorder.start();
      setIsRecording(true);
    } catch (error) {
      console.error('Error starting recording:', error);
      toast.error('Не вдалося отримати доступ до мікрофона');
    }
  };

  const stopRecording = () => {
    if (mediaRecorderRef.current && isRecording) {
      mediaRecorderRef.current.stop();
      setIsRecording(false);
    }
  };

  const cancelRecording = () => {
    if (mediaRecorderRef.current && isRecording) {
      mediaRecorderRef.current.onstop = null;
      mediaRecorderRef.current.stop();
    }
    if (streamRef.current) {
      streamRef.current.getTracks().forEach((track) => track.stop());
      streamRef.current = null;
    }
    setIsRecording(false);
    audioChunksRef.current = [];
  };

  const handleKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter' && !e.shiftKey) {
      e.preventDefault();
      handleSendMessage();
    }
  };

  const handleLocationChange = (lat: number, lng: number, street: string) => {
    setFormData({ ...formData, latitude: lat, longitude: lng });
    setStreetName(street);
  };

  const handleCategoryChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const val = e.target.value;
    setFormData({
      ...formData,
      categoryNames: val ? [val] : [],
    });
  };

  const handlePriorityChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setFormData({ ...formData, priority: e.target.value });
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFiles = e.target.files;
    if (selectedFiles && selectedFiles.length > MAX_IMAGES_COUNT) {
      toast.error(`Максимальна кількість фото: ${MAX_IMAGES_COUNT}`);
      e.target.value = '';
      setFiles(null);
      setImagePreviews([]);
      return;
    }
    setFiles(selectedFiles);

    // Generate image previews
    if (selectedFiles) {
      const previews: string[] = [];
      Array.from(selectedFiles).forEach((file) => {
        const reader = new FileReader();
        reader.onloadend = () => {
          previews.push(reader.result as string);
          if (previews.length === selectedFiles.length) {
            setImagePreviews([...previews]);
          }
        };
        reader.readAsDataURL(file);
      });
    }
  };

  const handleResetLocation = () => {
    setFormData({ ...formData, latitude: 0, longitude: 0 });
    setStreetName('');
    setMapKey((prev) => prev + 1);
  };

  const handleCenterLocation = () => {
    setMapKey((prev) => prev + 1);
  };

  const handleSubmit = async () => {
    if (!formData.title.trim()) {
      toast.error('Вкажіть назву проблеми');
      return;
    }

    if (formData.description.trim().length < 30) {
      toast.error('Опис має містити щонайменше 30 символів');
      return;
    }

    if (formData.latitude === 0 || formData.longitude === 0) {
      toast.error('Будь ласка, виберіть місце на карті');
      return;
    }

    if (formData.categoryNames.length === 0) {
      toast.error('Виберіть категорію');
      return;
    }

    try {
      const createdProblem = await createMutation.mutateAsync(formData);

      if (files && files.length > 0 && createdProblem.id) {
        await uploadImagesMutation.mutateAsync({ id: createdProblem.id, files });
      }

      toast.success('Проблему створено успішно');
      navigate('/map');
    } catch (error) {
      toast.error('Сталася помилка під час створення проблеми');
      console.error(error);
    }
  };

  const isSubmitting = createMutation.isPending || uploadImagesMutation.isPending;
  const selectedCategory = formData.categoryNames[0] || '';
  const inputBaseClasses = 'w-full bg-[#F5F6F7] border-none rounded-[10px] px-4 text-sm focus:ring-0 transition-all outline-none placeholder:text-gray-400';

  const renderMessage = (message: ChatMessage) => {
    const isUser = message.role === 'user';

    return (
      <div key={message.id} className={cn('flex gap-2 mb-4', isUser ? 'flex-row-reverse' : 'flex-row')}>
        <div
          className={cn(
            'w-8 h-8 rounded-full flex items-center justify-center flex-shrink-0',
            isUser ? 'bg-blue-500 text-white' : 'bg-gradient-to-br from-violet-500 to-purple-600 text-white'
          )}>
          {isUser ? <User className="w-4 h-4" /> : <Sparkles className="w-4 h-4" />}
        </div>
        <div className={cn('max-w-[85%] rounded-2xl px-4 py-2', isUser ? 'bg-blue-500 text-white rounded-tr-sm' : 'bg-gray-100 text-gray-900 rounded-tl-sm')}>
          <div className="text-sm whitespace-pre-wrap">{message.content}</div>
          <div className={cn('text-xs mt-1', isUser ? 'text-white/70' : 'text-gray-500')}>
            {message.timestamp.toLocaleTimeString('uk-UA', {
              hour: '2-digit',
              minute: '2-digit',
            })}
          </div>
        </div>
      </div>
    );
  };

  return (
    <div className="w-full max-w-[1600px] mx-auto py-8 px-4 md:px-8 font-sans">
      <div className="mb-8">
        <h1 className="text-[#1F2732] text-3xl font-bold font-heading flex items-center gap-3">
          <Sparkles className="w-8 h-8 text-violet-500" />
          Створення проблеми з AI
        </h1>
        <p className="text-gray-500 mt-1">Опишіть проблему голосом або текстом — AI заповнить форму автоматично</p>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
        <div className="space-y-6">
          {/* Chat Section */}
          <Card className="bg-white border shadow-sm rounded-xl overflow-hidden flex flex-col h-[700px]">
            <CardHeader className="border-b border-gray-100 bg-gradient-to-r from-violet-500 to-purple-600 text-white py-4">
              <CardTitle className="font-heading font-semibold text-lg flex items-center gap-2">
                <Bot className="w-5 h-5" />
                AI Асистент
              </CardTitle>
              <p className="text-sm text-white/80">Опишіть проблему, і я заповню форму</p>
            </CardHeader>

            <CardContent className="flex-1 overflow-y-auto p-4">
              {messages.map(renderMessage)}
              {isLoading && (
                <div className="flex gap-2 mb-4">
                  <div className="w-8 h-8 rounded-full bg-gradient-to-br from-violet-500 to-purple-600 flex items-center justify-center flex-shrink-0">
                    <Sparkles className="w-4 h-4 text-white" />
                  </div>
                  <div className="bg-gray-100 rounded-2xl rounded-tl-sm px-4 py-3">
                    <Loader2 className="w-5 h-5 animate-spin text-violet-500" />
                  </div>
                </div>
              )}
              {isProcessingVoice && (
                <div className="flex gap-2 mb-4">
                  <div className="w-8 h-8 rounded-full bg-gradient-to-br from-violet-500 to-purple-600 flex items-center justify-center flex-shrink-0">
                    <Sparkles className="w-4 h-4 text-white" />
                  </div>
                  <div className="bg-gray-100 rounded-2xl rounded-tl-sm px-4 py-3 flex items-center gap-2">
                    <Loader2 className="w-4 h-4 animate-spin text-violet-500" />
                    <span className="text-sm text-gray-600">Розпізнаю голос...</span>
                  </div>
                </div>
              )}
              <div ref={messagesEndRef} />
            </CardContent>

            {isRecording && (
              <div className="px-4 py-2 bg-red-50 border-t flex items-center justify-between">
                <div className="flex items-center gap-2">
                  <div className="w-3 h-3 bg-red-500 rounded-full animate-pulse" />
                  <span className="text-sm text-red-600 font-medium">Запис...</span>
                </div>
                <div className="flex items-center gap-2">
                  <span className="text-xs text-gray-500">Натисніть для видалення</span>
                  <button
                    onClick={cancelRecording}
                    className="p-2 text-gray-500 bg-gray-100 rounded-lg hover:bg-gray-200 transition-colors focus:outline-none"
                    aria-label="Скасувати запис">
                    <span className="inline-flex items-center justify-center w-6 h-6 rounded-md border text-red-600">
                      <Trash2 className="w-3.5 h-3.5" strokeWidth={2.3} />
                    </span>
                  </button>
                </div>
              </div>
            )}

            <div className="p-4 border-t bg-gray-50">
              <div className="flex items-start gap-2">
                <button
                  onClick={isRecording ? stopRecording : startRecording}
                  disabled={isLoading || isProcessingVoice}
                  className={cn(
                    'p-3 rounded-full focus:outline-none transition-all flex-shrink-0',
                    isRecording ? 'bg-red-500 text-white hover:bg-red-600 animate-pulse' : 'bg-violet-100 text-violet-600 hover:bg-violet-200'
                  )}
                  aria-label={isRecording ? 'Зупинити запис' : 'Почати запис'}>
                  {isRecording ? <MicOff className="w-5 h-5" /> : <Mic className="w-5 h-5" />}
                </button>
                <textarea
                  value={inputValue}
                  onChange={(e) => setInputValue(e.target.value)}
                  onKeyPress={handleKeyPress}
                  placeholder="Опишіть проблему..."
                  disabled={isLoading || isRecording || isProcessingVoice}
                  rows={1}
                  className="flex-1 px-4 py-3 bg-white rounded-2xl text-sm border border-gray-200 focus:outline-none focus:border-violet-400 disabled:opacity-50 resize-none overflow-y-auto"
                  style={{ minHeight: '48px', maxHeight: '120px' }}
                  onInput={(e) => {
                    const target = e.target as HTMLTextAreaElement;
                    target.style.height = 'auto';
                    target.style.height = Math.min(target.scrollHeight, 120) + 'px';
                  }}
                />
                <button
                  onClick={handleSendMessage}
                  disabled={isLoading || isProcessingVoice || !inputValue.trim()}
                  className="p-3 bg-violet-500 text-white rounded-full hover:bg-violet-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed focus:outline-none flex-shrink-0"
                  aria-label="Надіслати">
                  <Send className="w-5 h-5" />
                </button>
              </div>
            </div>
          </Card>

          {/* Tips Section */}
          <Card className="bg-gradient-to-br from-violet-50 to-purple-50 border border-violet-200 shadow-sm rounded-xl overflow-hidden">
            <div
              className="px-4 py-3 flex items-center justify-between cursor-pointer hover:bg-violet-100/50 transition-colors group select-none"
              onClick={() => setTipsExpanded(!tipsExpanded)}>
              <div className="flex items-center gap-2 text-violet-700">
                <Lightbulb className="w-5 h-5" />
                <span className="font-semibold">Поради для найкращого результату</span>
              </div>
              <ChevronDown className={cn('w-5 h-5 text-violet-400 group-hover:text-violet-600 transition-all duration-300', tipsExpanded && 'rotate-180')} />
            </div>
            <div className={cn('transition-all duration-300 overflow-hidden', tipsExpanded ? 'max-h-[400px]' : 'max-h-0')}>
              <div className="px-4 pb-4 space-y-3 text-sm text-gray-700">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
                  <div className="bg-white/70 rounded-lg p-3 border border-violet-100">
                    <p className="font-semibold text-violet-800 mb-1">✅ Рекомендовано:</p>
                    <ul className="space-y-1 text-gray-600">
                      <li>• Опишіть проблему детально та конкретно</li>
                      <li>• Вкажіть точну адресу або орієнтир</li>
                      <li>• Використовуйте назви вулиць Острога</li>
                      <li>• Говоріть чітко при голосовому вводі</li>
                    </ul>
                  </div>
                  <div className="bg-white/70 rounded-lg p-3 border border-violet-100">
                    <p className="font-semibold text-red-600 mb-1">❌ Не рекомендовано:</p>
                    <ul className="space-y-1 text-gray-600">
                      <li>• Занадто короткі описи ("яма")</li>
                      <li>• Відсутність локації</li>
                      <li>• Нецензурна лексика</li>
                      <li>• Неправдива інформація</li>
                    </ul>
                  </div>
                </div>
                <div className="bg-white/70 rounded-lg p-3 border border-violet-100">
                  <p className="font-semibold text-violet-800 mb-1">💡 Приклади хороших описів:</p>
                  <ul className="space-y-1 text-gray-600">
                    <li>• "Велика яма на вулиці Шевченка біля автовокзалу, небезпечно для машин"</li>
                    <li>• "Не працює ліхтар біля Старого корпусу академії вже тиждень"</li>
                    <li>• "Переповнені сміттєві баки біля АТБ на Гальшки Острозької"</li>
                  </ul>
                </div>
              </div>
            </div>
          </Card>
        </div>

        {/* Preview Section */}
        <div className="space-y-6">
          <Card className="bg-white border shadow-sm rounded-xl overflow-hidden">
            <CardHeader className="border-b border-gray-100 bg-white pb-4 flex flex-row items-center justify-between">
              <div>
                <CardTitle className="font-heading font-semibold text-lg text-[#1F2732] flex items-center gap-2">
                  {extractedData ? (
                    <>
                      <Check className="w-5 h-5 text-green-500" />
                      Попередній перегляд
                    </>
                  ) : (
                    'Попередній перегляд'
                  )}
                </CardTitle>
                <p className="text-sm text-gray-500">{extractedData ? 'Перевірте та відредагуйте за потреби' : "Дані з'являться після опису проблеми"}</p>
              </div>
              {extractedData && (
                <Button
                  variant="outline"
                  size="sm"
                  onClick={() => setIsEditing(!isEditing)}
                  className={cn(
                    'flex items-center gap-2 border-2 transition-all focus:ring-0 focus:outline-none',
                    isEditing
                      ? 'border-green-500 bg-green-50 text-green-700 hover:bg-green-100 hover:text-green-800'
                      : 'border-violet-400 bg-violet-50 text-violet-700 hover:bg-violet-100 hover:text-violet-800'
                  )}>
                  {isEditing ? <Check className="w-4 h-4" /> : <Edit3 className="w-4 h-4" />}
                  {isEditing ? 'Готово' : 'Редагувати'}
                </Button>
              )}
            </CardHeader>
            <CardContent className="p-6 space-y-6">
              {!extractedData ? (
                <div className="text-center py-12 text-gray-400">
                  <Sparkles className="w-12 h-12 mx-auto mb-4 opacity-50" />
                  <p>Опишіть проблему в чаті зліва</p>
                  <p className="text-sm mt-2">AI автоматично заповнить форму</p>
                </div>
              ) : (
                <>
                  <div className="flex flex-col gap-2 pb-2">
                    <Label className="text-[#1F2732] font-medium">Назва</Label>
                    {isEditing ? (
                      <input type="text" value={formData.title} onChange={(e) => setFormData({ ...formData, title: e.target.value })} className={`${inputBaseClasses} h-10`} />
                    ) : (
                      <p className="text-gray-700 bg-gray-50 rounded-lg px-4 py-2">{formData.title || '-'}</p>
                    )}
                  </div>

                  <div className="grid grid-cols-2 gap-4 pb-2">
                    <div className="flex flex-col gap-2">
                      <Label className="text-[#1F2732] font-medium">Категорія</Label>
                      {isEditing ? (
                        <div className="relative">
                          <select value={selectedCategory} onChange={handleCategoryChange} className={`${inputBaseClasses} h-10 appearance-none cursor-pointer`}>
                            <option value="" disabled hidden>
                              Виберіть
                            </option>
                            {CATEGORIES.map((cat) => (
                              <option key={cat.value} value={cat.value}>
                                {cat.label}
                              </option>
                            ))}
                          </select>
                          <ChevronDown className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 h-4 w-4 pointer-events-none" />
                        </div>
                      ) : (
                        <p className="text-gray-700 bg-gray-50 rounded-lg px-4 py-2">{selectedCategory || '-'}</p>
                      )}
                    </div>

                    <div className="flex flex-col gap-2">
                      <Label className="text-[#1F2732] font-medium">Пріоритет</Label>
                      {isEditing ? (
                        <div className="relative">
                          <select value={formData.priority || ''} onChange={handlePriorityChange} className={`${inputBaseClasses} h-10 appearance-none cursor-pointer`}>
                            <option value="" disabled hidden>
                              Виберіть
                            </option>
                            {PRIORITIES.map((p) => (
                              <option key={p} value={p}>
                                {p}
                              </option>
                            ))}
                          </select>
                          <ChevronDown className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 h-4 w-4 pointer-events-none" />
                        </div>
                      ) : (
                        <p className="text-gray-700 bg-gray-50 rounded-lg px-4 py-2">{formData.priority || '-'}</p>
                      )}
                    </div>
                  </div>

                  <div className="flex flex-col gap-2 pb-2">
                    <Label className="text-[#1F2732] font-medium">Опис</Label>
                    {isEditing ? (
                      <textarea
                        value={formData.description}
                        onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                        className={`${inputBaseClasses} min-h-[100px] py-2 resize-y`}
                      />
                    ) : (
                      <p className="text-gray-700 bg-gray-50 rounded-lg px-4 py-2 whitespace-pre-wrap">{formData.description || '-'}</p>
                    )}
                  </div>

                  {extractedData.streetName && (
                    <div className="flex flex-col gap-2">
                      <Label className="text-[#1F2732] font-medium">Розпізнана вулиця</Label>
                      <p className="text-violet-600 bg-violet-50 rounded-lg px-4 py-2">{extractedData.streetName}</p>
                    </div>
                  )}
                </>
              )}
            </CardContent>
          </Card>

          {extractedData && (
            <>
              <Card className="bg-white border shadow-sm rounded-xl overflow-hidden">
                <CardHeader className="border-b border-gray-100 bg-white pb-4">
                  <CardTitle className="font-heading font-semibold text-lg text-[#1F2732]">Фото (опціонально)</CardTitle>
                </CardHeader>
                <CardContent className="p-6">
                  <div className="grid grid-cols-4 gap-3">
                    <div
                      onClick={() => document.getElementById('ai-problem-images')?.click()}
                      className="aspect-square bg-[#F5F6F7] rounded-lg flex flex-col items-center justify-center cursor-pointer hover:bg-gray-200 transition-colors">
                      <Plus className="w-6 h-6 text-gray-400" />
                      <span className="text-xs text-gray-500 mt-1">Додати</span>
                      <input id="ai-problem-images" type="file" multiple accept="image/*" className="hidden" onChange={handleFileChange} />
                    </div>
                    {[...Array(3)].map((_, i) => (
                      <div key={i} className="aspect-square bg-[#F5F6F7] rounded-lg flex items-center justify-center text-gray-300 overflow-hidden">
                        {imagePreviews[i] ? <img src={imagePreviews[i]} alt={`Preview ${i + 1}`} className="w-full h-full object-cover" /> : <Camera className="w-6 h-6" />}
                      </div>
                    ))}
                  </div>
                  {files && files.length > 0 && (
                    <p className="text-sm text-gray-500 mt-2">
                      Обрано: {files.length} / {MAX_IMAGES_COUNT}
                    </p>
                  )}
                </CardContent>
              </Card>

              <Card className="bg-white border shadow-sm rounded-xl overflow-hidden">
                <CardHeader className="border-b border-gray-100 bg-white pb-4">
                  <div className="flex items-center gap-1">
                    <CardTitle className="font-heading font-semibold text-lg text-[#1F2732]">Місце на карті</CardTitle>
                    <span className="text-destructive text-lg">*</span>
                  </div>
                </CardHeader>
                <CardContent className="p-6">
                  <div className="relative rounded-lg overflow-hidden border border-gray-200">
                    <LocationPickerMap key={mapKey} latitude={formData.latitude} longitude={formData.longitude} onLocationChange={handleLocationChange} height="250px" />
                  </div>
                  <div className="flex items-center justify-between mt-4">
                    <div className="text-sm">
                      <span className="text-[#1F2732] font-medium">Вулиця:</span> <span className="text-gray-500">{streetName || 'Не вибрано'}</span>
                    </div>
                    <div className="flex gap-2">
                      <Button
                        type="button"
                        size="sm"
                        variant="outline"
                        onClick={handleResetLocation}
                        className="border-2 border-gray-400 bg-gray-50 text-gray-700 hover:bg-gray-100 hover:text-gray-900 focus:ring-0 focus:outline-none transition-all">
                        <RotateCcw className="w-4 h-4 mr-1" />
                        Скинути
                      </Button>
                      <Button
                        type="button"
                        size="sm"
                        variant="outline"
                        onClick={handleCenterLocation}
                        className="border-2 border-violet-400 bg-violet-50 text-violet-700 hover:bg-violet-100 hover:text-violet-800 focus:ring-0 focus:outline-none transition-all">
                        <Crosshair className="w-4 h-4 mr-1" />
                        Центр
                      </Button>
                    </div>
                  </div>
                </CardContent>
              </Card>

              <Button
                onClick={handleSubmit}
                disabled={isSubmitting || !extractedData.isComplete}
                className="w-full bg-gradient-to-r from-violet-500 to-purple-600 hover:from-violet-600 hover:to-purple-700 text-white font-bold text-lg py-6 rounded-xl shadow-lg transition-all">
                {isSubmitting ? (
                  <>
                    <Loader2 className="w-5 h-5 mr-2 animate-spin" />
                    Створення...
                  </>
                ) : (
                  <>
                    <Sparkles className="w-5 h-5 mr-2" />
                    Створити проблему
                  </>
                )}
              </Button>
            </>
          )}
        </div>
      </div>
    </div>
  );
}
