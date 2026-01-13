import { BrowserRouter } from 'react-router-dom'
import { QueryProvider } from '@/lib/react-query'
import { ClerkAuthProvider } from '@/contexts/clerk-auth-provider'
import { SignalRProvider } from '@/contexts/signalr-context.tsx'
import { AppRoutes } from '@/routes/app-routes'
import { SnackbarProvider } from '@/contexts/snackbar-provider'

function App() {
  return (
    <QueryProvider>
      <ClerkAuthProvider>
        <SignalRProvider>
          <SnackbarProvider>
            <BrowserRouter>
              <AppRoutes />
            </BrowserRouter>
          </SnackbarProvider>
        </SignalRProvider>
      </ClerkAuthProvider>
    </QueryProvider>
  )
}

export default App
