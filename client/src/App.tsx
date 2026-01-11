import { BrowserRouter } from 'react-router-dom'
import { QueryProvider } from '@/lib/react-query'
import { AuthProvider } from '@/contexts/auth-provider'
import { SignalRProvider } from '@/contexts/signalr-context.tsx'
import { AppRoutes } from '@/routes/app-routes'
import { SnackbarProvider } from '@/contexts/snackbar-context'

function App() {
  return (
    <QueryProvider>
      <AuthProvider>
        <SignalRProvider>
          <SnackbarProvider>
            <BrowserRouter>
              <AppRoutes />
            </BrowserRouter>
          </SnackbarProvider>
        </SignalRProvider>
      </AuthProvider>
    </QueryProvider>
  )
}

export default App
