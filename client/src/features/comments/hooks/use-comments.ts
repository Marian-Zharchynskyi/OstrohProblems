import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { commentsApi } from '../api/comments-api'
import type { CreateComment } from '@/types'

export const COMMENTS_QUERY_KEY = 'comments'

export function useComments() {
  return useQuery({
    queryKey: [COMMENTS_QUERY_KEY],
    queryFn: commentsApi.getAll,
  })
}

export function useCommentsPaged(page: number = 1, pageSize: number = 10) {
  return useQuery({
    queryKey: [COMMENTS_QUERY_KEY, 'paged', page, pageSize],
    queryFn: () => commentsApi.getPaged(page, pageSize),
  })
}

export function useComment(id: string) {
  return useQuery({
    queryKey: [COMMENTS_QUERY_KEY, id],
    queryFn: () => commentsApi.getById(id),
    enabled: !!id,
  })
}

export function useCreateComment() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: commentsApi.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [COMMENTS_QUERY_KEY] })
    },
  })
}

export function useUpdateComment() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: CreateComment }) =>
      commentsApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [COMMENTS_QUERY_KEY] })
    },
  })
}

export function useDeleteComment() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: commentsApi.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [COMMENTS_QUERY_KEY] })
    },
  })
}
