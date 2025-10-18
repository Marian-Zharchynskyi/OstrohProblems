import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import { QueryProvider } from '@/lib/react-query'
import { Layout } from '@/components/shared/layout'
import { HomePage } from '@/pages/home-page'
import { CategoriesPage } from '@/pages/categories-page'
import { StatusesPage } from '@/pages/statuses-page'
import { ProblemsPage } from '@/pages/problems-page'
import { CommentsPage } from '@/pages/comments-page'
import { RatingsPage } from '@/pages/ratings-page'

function App() {
  return (
    <QueryProvider>
      <BrowserRouter>
        <Layout>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/categories" element={<CategoriesPage />} />
            <Route path="/statuses" element={<StatusesPage />} />
            <Route path="/problems" element={<ProblemsPage />} />
            <Route path="/comments" element={<CommentsPage />} />
            <Route path="/ratings" element={<RatingsPage />} />
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </Layout>
      </BrowserRouter>
    </QueryProvider>
  )
}

export default App
