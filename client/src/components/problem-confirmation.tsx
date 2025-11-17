import { useState } from 'react'
import { problemsApi } from '@/features/problems/api/problems-api'
import { Button } from '@/components/ui/button'
import { Textarea } from '@/components/ui/textarea'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import type { Problem } from '@/types'

interface ProblemConfirmationProps {
  problem: Problem
  onConfirmed: () => void
}

export function ProblemConfirmation({ problem, onConfirmed }: ProblemConfirmationProps) {
  const [comment, setComment] = useState('')
  const [loading, setLoading] = useState(false)

  // Статус "Виконано" - показуємо кнопки підтвердження
  const isCompleted = problem.problemStatus?.name === 'Виконано'
  const isPending = problem.userConfirmationStatus === 0

  if (!isCompleted || !isPending) {
    return null
  }

  const handleConfirm = async () => {
    setLoading(true)
    try {
      await problemsApi.confirmByUser(problem.id!, 1) // 1 = Confirmed
      onConfirmed()
    } catch (err) {
      console.error('Помилка підтвердження:', err)
    } finally {
      setLoading(false)
    }
  }

  const handleReject = async () => {
    if (!comment.trim()) {
      alert('Будь ласка, вкажіть причину відхилення')
      return
    }
    setLoading(true)
    try {
      await problemsApi.confirmByUser(problem.id!, 2) // 2 = Rejected
      onConfirmed()
    } catch (err) {
      console.error('Помилка відхилення:', err)
    } finally {
      setLoading(false)
    }
  }

  return (
    <Card className="border-blue-500">
      <CardHeader>
        <CardTitle className="text-lg">Підтвердження виконання</CardTitle>
      </CardHeader>
      <CardContent>
        <p className="text-sm text-gray-600 mb-4">
          Координатор повідомив, що проблему вирішено. Будь ласка, підтвердіть або відхиліть виконання.
        </p>
        {problem.coordinatorComment && (
          <div className="bg-gray-50 p-3 rounded mb-4">
            <p className="text-sm font-semibold">Коментар координатора:</p>
            <p className="text-sm">{problem.coordinatorComment}</p>
          </div>
        )}
        <div className="space-y-3">
          <Button onClick={handleConfirm} disabled={loading} className="w-full">
            ✓ Підтвердити виконання
          </Button>
          <Textarea
            placeholder="Якщо проблема не вирішена, опишіть причину..."
            value={comment}
            onChange={(e) => setComment(e.target.value)}
            rows={3}
          />
          <Button onClick={handleReject} disabled={loading} variant="destructive" className="w-full">
            ✗ Не виконано / виконано неякісно
          </Button>
        </div>
      </CardContent>
    </Card>
  )
}
