import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { problemsApi } from '../api/problems-api'
import type { CreateProblem } from '@/types'

export const PROBLEMS_QUERY_KEY = 'problems'

export function useProblems() {
  return useQuery({
    queryKey: [PROBLEMS_QUERY_KEY],
    queryFn: problemsApi.getAll,
  })
}

export function useProblemsForMap() {
  return useQuery({
    queryKey: [PROBLEMS_QUERY_KEY, 'for-map'],
    queryFn: problemsApi.getForMap,
  })
}

export function useProblemsPaged(page: number = 1, pageSize: number = 10) {
  return useQuery({
    queryKey: [PROBLEMS_QUERY_KEY, 'paged', page, pageSize],
    queryFn: () => problemsApi.getPaged(page, pageSize),
  })
}

export function useProblem(id: string) {
  return useQuery({
    queryKey: [PROBLEMS_QUERY_KEY, id],
    queryFn: () => problemsApi.getById(id),
    enabled: !!id,
  })
}

export function useCreateProblem() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: problemsApi.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [PROBLEMS_QUERY_KEY] })
    },
  })
}

export function useUpdateProblem() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: CreateProblem }) =>
      problemsApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [PROBLEMS_QUERY_KEY] })
    },
  })
}

export function useDeleteProblem() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: problemsApi.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [PROBLEMS_QUERY_KEY] })
    },
  })
}

export function useUploadProblemImages() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ({ id, files }: { id: string; files: FileList }) =>
      problemsApi.uploadImages(id, files),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [PROBLEMS_QUERY_KEY] })
    },
  })
}

export function useDeleteProblemImage() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ({ problemId, imageId }: { problemId: string; imageId: string }) =>
      problemsApi.deleteImage(problemId, imageId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [PROBLEMS_QUERY_KEY] })
    },
  })
}

// New filtering hooks
export function useProblemsByUser(userId: string) {
  return useQuery({
    queryKey: [PROBLEMS_QUERY_KEY, 'by-user', userId],
    queryFn: () => problemsApi.getByUser(userId),
    enabled: !!userId,
  })
}

export function useProblemsByCoordinator(coordinatorId: string) {
  return useQuery({
    queryKey: [PROBLEMS_QUERY_KEY, 'by-coordinator', coordinatorId],
    queryFn: () => problemsApi.getByCoordinator(coordinatorId),
    enabled: !!coordinatorId,
  })
}

export function useProblemsByStatus(status: string) {
  return useQuery({
    queryKey: [PROBLEMS_QUERY_KEY, 'by-status', status],
    queryFn: () => problemsApi.getByStatus(status),
    enabled: !!status,
  })
}
