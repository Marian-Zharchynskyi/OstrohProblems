import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { ratingsApi } from '../api/ratings-api'
import type { CreateRating } from '@/types'

export const RATINGS_QUERY_KEY = 'ratings'

export function useRatings() {
  return useQuery({
    queryKey: [RATINGS_QUERY_KEY],
    queryFn: ratingsApi.getAll,
  })
}

export function useRatingsPaged(page: number = 1, pageSize: number = 10) {
  return useQuery({
    queryKey: [RATINGS_QUERY_KEY, 'paged', page, pageSize],
    queryFn: () => ratingsApi.getPaged(page, pageSize),
  })
}

export function useRating(id: string) {
  return useQuery({
    queryKey: [RATINGS_QUERY_KEY, id],
    queryFn: () => ratingsApi.getById(id),
    enabled: !!id,
  })
}

export function useCreateRating() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ratingsApi.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [RATINGS_QUERY_KEY] })
    },
  })
}

export function useUpdateRating() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: CreateRating }) =>
      ratingsApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [RATINGS_QUERY_KEY] })
    },
  })
}

export function useDeleteRating() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ratingsApi.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [RATINGS_QUERY_KEY] })
    },
  })
}
