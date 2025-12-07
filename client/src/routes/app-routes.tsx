import { Routes, Route } from 'react-router-dom'
import { Layout } from '@/components/shared/layout'
import { ProtectedRoute } from '@/components/auth/protected-route'
import { PublicRoute } from '@/components/auth/public-route'
import { PublicHomePage } from '@/pages/public-home-page'
import { AboutPage } from '@/pages/about-page'
import { ContactPage } from '@/pages/contact-page'
import { ErrorPage } from '@/pages/error-page'
import { HomePage } from '@/pages/home-page'
import { LoginPage } from '@/pages/login-page'
import { RegisterPage } from '@/pages/register-page'
import { CategoriesPage } from '@/pages/categories-page'
import { ProblemsPage } from '@/pages/problems-page'
import { CommentsPage } from '@/pages/comments-page'
import { RatingsPage } from '@/pages/ratings-page'
import { MapPage } from '@/pages/map-page'
import { ProfilePage } from '@/pages/profile-page'
import { AdminUsersPage } from '@/pages/admin-users-page'
import { CreateIssuePage } from '@/pages/create-issue-page'
import { ProblemDetailPage } from '@/pages/problem-detail-page'
import { MyProblemsPage } from '@/pages/my-problems-page'
import CoordinatorPage from '@/pages/coordinator-page'

export function AppRoutes() {
  return (
    <Routes>
      {/* Public routes with layout */}
      <Route
        path="/"
        element={
          <Layout>
            <PublicHomePage />
          </Layout>
        }
      />
      <Route
        path="/about"
        element={
          <Layout>
            <AboutPage />
          </Layout>
        }
      />
      <Route
        path="/contact"
        element={
          <Layout>
            <ContactPage />
          </Layout>
        }
      />
      <Route
        path="/login"
        element={
          <PublicRoute>
            <LoginPage />
          </PublicRoute>
        }
      />
      <Route
        path="/register"
        element={
          <PublicRoute>
            <RegisterPage />
          </PublicRoute>
        }
      />

      {/* Admin only routes */}
      <Route
        path="/dashboard"
        element={
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <HomePage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/categories"
        element={
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <CategoriesPage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/problems"
        element={
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <ProblemsPage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/comments"
        element={
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <CommentsPage />
            </Layout>
          </ProtectedRoute>
        }
      />
      <Route
        path="/ratings"
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
          <ProtectedRoute allowedRoles={['Administrator']}>
            <Layout>
              <MapPage />
            </Layout>
          </ProtectedRoute>
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

      {/* User routes */}
      <Route
        path="/my-problems"
        element={
          <ProtectedRoute allowedRoles={['User']}>
            <Layout>
              <MyProblemsPage />
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
