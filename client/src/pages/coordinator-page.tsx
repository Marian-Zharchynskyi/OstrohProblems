import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useProblemsByCoordinator, useProblemsByStatus } from '@/features/problems/hooks/use-problems';
import { problemsApi } from '@/features/problems/api/problems-api';
import { ProblemStatusConstants } from '@/types';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { useAuth } from '@/contexts/auth-context';
import { useSignalR } from '@/contexts/use-signalr';
import { useQueryClient } from '@tanstack/react-query';
import { toast } from '@/lib/toast';

type TabType = 'new' | 'my' | 'completed' | 'rejected';

export default function CoordinatorPage() {
  const { user } = useAuth();
  const { onProblemsUpdated } = useSignalR();
  const queryClient = useQueryClient();
  const navigate = useNavigate();

  const [activeTab, setActiveTab] = useState<TabType>('new');

  // Subscribe to SignalR refresh events for auto-refresh
  useEffect(() => {
    onProblemsUpdated(() => {
      queryClient.invalidateQueries({ queryKey: ['problems'] });
    });
  }, [onProblemsUpdated, queryClient]);

  // Fetch New problems (unassigned)
  const { data: allNewProblems = [], isLoading: loadingNew } = useProblemsByStatus(ProblemStatusConstants.New);

  // Fetch My problems (all assigned to me)
  const { data: myProblems = [], isLoading: loadingMy } = useProblemsByCoordinator(user?.id || '');

  const loading = loadingNew || loadingMy;

  // Derived lists
  const newProblems = allNewProblems.filter((p) => !p.coordinator);
  const myInProgressProblems = myProblems.filter((p) => p.status === ProblemStatusConstants.InProgress);
  const myCompletedProblems = myProblems.filter((p) => p.status === ProblemStatusConstants.Completed);
  const myRejectedProblems = myProblems.filter((p) => p.status === ProblemStatusConstants.Rejected);

  const tabConfig: { key: TabType; label: string; count: number }[] = [
    { key: 'new', label: 'Нові', count: newProblems.length },
    { key: 'my', label: 'Мої проблеми', count: myInProgressProblems.length },
    { key: 'completed', label: 'Виконано', count: myCompletedProblems.length },
    { key: 'rejected', label: 'Відхилені', count: myRejectedProblems.length },
  ];

  // Get problems to display based on active tab
  const getDisplayProblems = () => {
    switch (activeTab) {
      case 'new':
        return newProblems;
      case 'my':
        return myInProgressProblems;
      case 'completed':
        return myCompletedProblems;
      case 'rejected':
        return myRejectedProblems;
      default:
        return [];
    }
  };

  const handleRestoreProblem = async (problemId?: string) => {
    if (!problemId) return;
    try {
      await problemsApi.restoreProblem(problemId);
      toast.success('Проблему повернуто до нових');
      queryClient.invalidateQueries({ queryKey: ['problems'] });
    } catch {
      toast.error('Не вдалося повернути проблему');
    }
  };

  const displayProblems = getDisplayProblems();

  const getStatusBadgeClass = (status: string) => {
    switch (status) {
      case ProblemStatusConstants.New:
        return 'bg-blue-100 text-blue-800';
      case ProblemStatusConstants.InProgress:
        return 'bg-yellow-100 text-yellow-800';
      case ProblemStatusConstants.Completed:
        return 'bg-green-100 text-green-800';
      case ProblemStatusConstants.Rejected:
        return 'bg-red-100 text-red-800';
      default:
        return 'bg-gray-100 text-gray-800';
    }
  };

  if (loading) {
    return <div className="container mx-auto p-6">Завантаження...</div>;
  }

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-3xl font-bold mb-6">Панель координатора</h1>

      <div className="flex gap-2 mb-6 flex-wrap">
        {tabConfig.map((tab) => (
          <Button
            key={tab.key}
            variant={activeTab === tab.key ? 'default' : 'outline'}
            onClick={() => setActiveTab(tab.key)}
            size="sm"
            className={`justify-between min-w-[150px] focus:outline-none focus:ring-0 focus-visible:ring-0 focus-visible:ring-offset-0 ${
              activeTab === tab.key
                ? 'shadow-sm border-transparent !bg-[#FF3366] !text-white hover:!bg-[#E62E5C]'
                : 'border-[#D0D5DD] bg-transparent text-[#1F2732] hover:bg-[#F5F5F5] hover:text-[#1F2732]'
            }`}>
            {tab.label} ({tab.count})
          </Button>
        ))}
      </div>

      <div className="grid gap-4">
        {displayProblems.length === 0 ? (
          <p className="text-gray-500">Немає проблем у цій категорії</p>
        ) : (
          displayProblems.map((problem) => (
            <Card
              key={problem.id}
              className="cursor-pointer hover:shadow-md transition-shadow"
              onClick={() => {
                if (activeTab === 'new' && problem.id) {
                  navigate(`/coordinator/problems/${problem.id}/update`);
                } else if (problem.id) {
                  navigate(`/problems/${problem.id}`);
                }
              }}>
              <CardHeader>
                <CardTitle className="flex justify-between items-center">
                  <span>{problem.title}</span>
                  <span className={`inline-flex items-center rounded-full px-2 py-1 text-xs font-medium ${getStatusBadgeClass(problem.status)}`}>{problem.status}</span>
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="flex flex-col gap-4 sm:flex-row sm:items-stretch">
                  <div className="flex-1">
                    <p className="text-sm text-gray-600 mb-2 line-clamp-2 break-words" style={{ overflowWrap: 'anywhere' }}>
                      {problem.description}
                    </p>
                    <p className="text-xs text-gray-500">
                      Автор: {problem.createdBy?.email} | {new Date(problem.createdAt).toLocaleDateString('uk-UA')}
                    </p>
                  </div>
                  <div className="flex flex-col gap-2 shrink-0 min-w-[200px] justify-end sm:items-end" onClick={(e) => e.stopPropagation()}>
                    {activeTab === 'new' ? (
                      <Button
                        variant="outline"
                        size="sm"
                        disabled={!problem.id}
                        className="border-[#D0D5DD] text-[#1F2732] hover:bg-[#EAEAEA] hover:text-[#1F2732] transition-colors focus:outline-none focus:ring-0 focus-visible:ring-0 focus-visible:ring-offset-0"
                        onClick={() => problem.id && navigate(`/coordinator/problems/${problem.id}/update`)}>
                        Переглянути та призначити
                      </Button>
                    ) : (
                      <>
                        {activeTab === 'my' && (
                          <Button
                            variant="outline"
                            size="sm"
                            className="border-[#D0D5DD] text-[#1F2732] hover:bg-[#EAEAEA] hover:text-[#1F2732] transition-colors focus:outline-none focus:ring-0 focus-visible:ring-0 focus-visible:ring-offset-0"
                            onClick={() => problem.id && navigate(`/coordinator/problems/${problem.id}/update`)}>
                            Оновити
                          </Button>
                        )}
                        {activeTab === 'rejected' && (
                          <Button
                            variant="outline"
                            size="sm"
                            className="border-[#E42556] text-[#E42556] hover:bg-[#FFE5EC] hover:text-[#C41C47] transition-colors"
                            onClick={() => handleRestoreProblem(problem.id || undefined)}>
                            Повернути в роботу
                          </Button>
                        )}
                      </>
                    )}
                  </div>
                </div>
              </CardContent>
            </Card>
          ))
        )}
      </div>
    </div>
  );
}
