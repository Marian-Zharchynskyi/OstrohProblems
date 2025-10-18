import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { statusesApi } from '../api/statuses-api'
import type { CreateStatus } from '@/types'

export const STATUSES_QUERY_KEY = 'statuses'

export function useStatuses() {
  return useQuery({
    queryKey: [STATUSES_QUERY_KEY],
    queryFn: statusesApi.getAll,
  })
}

export function useStatus(id: string) {
  return useQuery({
    queryKey: [STATUSES_QUERY_KEY, id],
    queryFn: () => statusesApi.getById(id),
    enabled: !!id,
  })
}

export function useCreateStatus() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: statusesApi.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [STATUSES_QUERY_KEY] })
    },
  })
}

export function useUpdateStatus() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: CreateStatus }) =>
      statusesApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [STATUSES_QUERY_KEY] })
    },
  })
}

export function useDeleteStatus() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: statusesApi.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [STATUSES_QUERY_KEY] })
    },
  })
}
