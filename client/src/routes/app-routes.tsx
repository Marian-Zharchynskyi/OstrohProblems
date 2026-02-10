import { Routes, Route, Navigate } from 'react-router-dom'
import { Layout } from '@/components/shared/layout'
import { ProtectedRoute } from '@/components/auth/protected-route'
import { PublicRoute } from '@/components/auth/public-route'
import { ErrorPage } from '@/pages/error-page'
import { HomePage } from '@/pages/home-page'
import { ClerkLoginPage } from '@/pages/clerk-login-page'
import { ClerkRegisterPage } from '@/pages/clerk-register-page'
import { ProblemsPage } from '@/pages/problems-page'
import { CommentsPage } from '@/pages/comments-page'
import { RatingsPage } from '@/pages/ratings-page'
import { MapPage } from '@/pages/map-page'
import { ProfilePage } from '@/pages/profile-page'
import { AdminUsersPage } from '@/pages/admin-users-page'
import { CreateIssuePage } from '@/pages/create-issue-page'
import { ProblemDetailPage } from '@/pages/problem-detail-page'
import { ProblemCommentsPage } from '@/pages/problem-comments-page'
import { MySubmittedProblemsPageWithTabs } from '@/pages/my-submitted-problems-page-with-tabs'
import CoordinatorPage from '@/pages/coordinator-page'
import CoordinatorUpdatePage from '@/pages/coordinator-update-page'

export function AppRoutes() {
  return (
    <Routes>
      {/* Root redirect */}
      <Route path="/" element={<Navigate to="/map" replace />} />

      {/* Public routes - landing pages moved to Next.js landing project */}
      <Route
        path="/login/*"
        element={
          <PublicRoute>
            <ClerkLoginPage />
          </PublicRoute>
        }
      />
      <Route
        path="/register/*"
        element={
          <PublicRoute>
            <ClerkRegisterPage />
          </PublicRoute>
        }
      />

      {/* Admin only routes */}
      <Route
        path="/admin/dashboard"
        element={
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <HomePage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/admin/problems"
        element={
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <ProblemsPage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/admin/comments"
        element={
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <CommentsPage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/admin/ratings"
        element={
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <RatingsPage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/map"
        element={
          <Layout>
            <MapPage />
          </Layout>
        }
      />
      <Route
        path="/admin/users"
        element={
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <AdminUsersPage />
            </Layout>
          </ProtectedRoute>
        }
      />

      {/* Coordinator only routes */}
      <Route
        path="/coordinator"
        element={
          <ProtectedRoute allowedRoles={['Coordinator']}>
            <Layout>
              <CoordinatorPage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/coordinator/problems/:id/update"
        element={
          <ProtectedRoute allowedRoles={['Coordinator']}>
            <Layout>
              <CoordinatorUpdatePage />
            </Layout>
          </ProtectedRoute>
        }
      />

      {/* User routes */}
      <Route
        path="/problems/my"
        element={
          <ProtectedRoute allowedRoles={['User']} redirectTo="/map">
            <Layout>
              <MySubmittedProblemsPageWithTabs />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/problems/create"
        element={
          <ProtectedRoute allowedRoles={['User', 'Administrator']}>
            <Layout>
              <CreateIssuePage />
            </Layout>
          </ProtectedRoute>
        }
      />

      {/* Shared protected routes */}
      <Route
        path="/problems/:id"
        element={
          <ProtectedRoute>
            <Layout>
              <ProblemDetailPage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/problems/:id/comments"
        element={
          <ProtectedRoute>
            <Layout>
              <ProblemCommentsPage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/profile"
        element={
          <ProtectedRoute allowedRoles={['User', 'Administrator']}>
            <Layout>
              <ProfilePage />
            </Layout>
          </ProtectedRoute>
        }
      />

      {/* Error page */}
      <Route
        path="*"
        element={
          <Layout>
            <ErrorPage />
          </Layout>
        }
      />
    </Routes>
  )
}
