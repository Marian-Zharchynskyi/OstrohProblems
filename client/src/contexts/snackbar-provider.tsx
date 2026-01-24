import React, { createContext, useContext, useState, useEffect, useCallback } from 'react'
import { X, CheckCircle, AlertCircle, Info } from 'lucide-react'
import { cn } from '@/lib/utils'

export type SnackbarType = 'success' | 'error' | 'info'

export interface SnackbarMessage {
    id: string
    message: string
    type: SnackbarType
    duration?: number
}

interface SnackbarContextType {
    show: (message: string, type?: SnackbarType, duration?: number) => void
}

const SnackbarContext = createContext<SnackbarContextType | undefined>(undefined)

export function useSnackbar() {
    const context = useContext(SnackbarContext)
    if (!context) throw new Error('useSnackbar must be used within SnackbarProvider')
    return context
}

export function SnackbarProvider({ children }: { children: React.ReactNode }) {
    const [snackbars, setSnackbars] = useState<SnackbarMessage[]>([])

    const removeSnackbar = useCallback((id: string) => {
        setSnackbars((prev) => prev.filter((s) => s.id !== id))
    }, [])

    const addSnackbar = useCallback((message: string, type: SnackbarType = 'info', duration = 3000) => {
        const id = Math.random().toString(36).substring(2, 9)
        const newSnackbar = { id, message, type, duration }
        setSnackbars((prev) => [...prev, newSnackbar])

        if (duration > 0) {
            setTimeout(() => {
                removeSnackbar(id)
            }, duration)
        }
    }, [removeSnackbar])

    // Listen for global events from non-react code (legacy toast.ts)
    useEffect(() => {
        const handleCustomEvent = (e: CustomEvent<{ message: string; type: SnackbarType; duration: number }>) => {
            addSnackbar(e.detail.message, e.detail.type, e.detail.duration)
        }
        window.addEventListener('show-snackbar', handleCustomEvent as EventListener)
        return () => window.removeEventListener('show-snackbar', handleCustomEvent as EventListener)
    }, [addSnackbar])


    return (
        <SnackbarContext.Provider value={{ show: addSnackbar }}>
            {children}
            <div className="fixed bottom-4 right-4 z-[100] flex flex-col gap-2 pointer-events-none">
                {snackbars.map((snackbar) => (
                    <div
                        key={snackbar.id}
                        className={cn(
                            "flex items-center gap-3 px-4 py-3 rounded-lg shadow-lg text-white font-medium min-w-[300px] max-w-[400px] pointer-events-auto transform transition-all duration-300 ease-in-out animate-in slide-in-from-right-full fade-in",
                            snackbar.type === 'success' && "bg-[#10B981]",
                            snackbar.type === 'error' && "bg-[#EF4444]",
                            snackbar.type === 'info' && "bg-[#3B82F6]"
                        )}
                    >
                        <div className="shrink-0">
                            {snackbar.type === 'success' && <CheckCircle className="w-5 h-5" />}
                            {snackbar.type === 'error' && <AlertCircle className="w-5 h-5" />}
                            {snackbar.type === 'info' && <Info className="w-5 h-5" />}
                        </div>

                        <p className="flex-1 text-sm">{snackbar.message}</p>

                        <button
                            onClick={() => removeSnackbar(snackbar.id)}
                            className="p-1 hover:bg-white/20 rounded-full transition-colors shrink-0 bg-transparent border-0 outline-none focus:outline-none"
                        >
                            <X className="w-4 h-4" />
                        </button>
                    </div>
                ))}
            </div>
        </SnackbarContext.Provider>
    )
}
