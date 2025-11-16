import { useEffect, useState } from 'react'
import { useSignalR } from '@/contexts/use-signalr'
import type { Comment } from '@/types'

export function useRealtimeComments(problemId: string | null, initialComments: Comment[] = []) {
  const { joinProblemGroup, leaveProblemGroup, onCommentReceived } = useSignalR()
  const [comments, setComments] = useState<Comment[]>(initialComments)

  useEffect(() => {
    setComments(initialComments)
  }, [initialComments])

  useEffect(() => {
    if (!problemId) return

    // Join the problem group to receive real-time comments
    joinProblemGroup(problemId)

    // Subscribe to new comments
    onCommentReceived((newComment) => {
      if (newComment.problemId === problemId) {
        setComments((prev) => {
          // Check if comment already exists to avoid duplicates
          const exists = prev.some((c) => c.id === newComment.id)
          if (exists) return prev
          return [...prev, newComment]
        })
      }
    })

    // Leave the group when component unmounts
    return () => {
      leaveProblemGroup(problemId)
    }
  }, [problemId, joinProblemGroup, leaveProblemGroup, onCommentReceived])

  return comments
}
