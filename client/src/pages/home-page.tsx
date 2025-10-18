import { Link } from 'react-router-dom'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { FolderTree, MessageSquare, Star, AlertCircle, Flag } from 'lucide-react'

const features = [
  {
    title: 'Categories',
    description: 'Manage problem categories',
    icon: FolderTree,
    path: '/categories',
    color: 'text-blue-500',
  },
  {
    title: 'Statuses',
    description: 'Manage problem statuses',
    icon: Flag,
    path: '/statuses',
    color: 'text-green-500',
  },
  {
    title: 'Problems',
    description: 'View and manage reported problems',
    icon: AlertCircle,
    path: '/problems',
    color: 'text-red-500',
  },
  {
    title: 'Comments',
    description: 'Manage problem comments',
    icon: MessageSquare,
    path: '/comments',
    color: 'text-purple-500',
  },
  {
    title: 'Ratings',
    description: 'Manage problem ratings',
    icon: Star,
    path: '/ratings',
    color: 'text-yellow-500',
  },
]

export function HomePage() {
  return (
    <div className="space-y-8">
      <div>
        <h1 className="text-4xl font-bold tracking-tight">Welcome to Ostroh Problems</h1>
        <p className="text-muted-foreground mt-2">
          Manage and track community-reported problems efficiently
        </p>
      </div>

      <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
        {features.map((feature) => {
          const Icon = feature.icon
          return (
            <Link key={feature.path} to={feature.path}>
              <Card className="hover:shadow-lg transition-shadow cursor-pointer h-full">
                <CardHeader>
                  <div className="flex items-center gap-4">
                    <div className={`p-3 rounded-lg bg-muted ${feature.color}`}>
                      <Icon className="h-6 w-6" />
                    </div>
                    <CardTitle>{feature.title}</CardTitle>
                  </div>
                  <CardDescription>{feature.description}</CardDescription>
                </CardHeader>
                <CardContent>
                  <p className="text-sm text-muted-foreground">
                    Click to manage {feature.title.toLowerCase()}
                  </p>
                </CardContent>
              </Card>
            </Link>
          )
        })}
      </div>
    </div>
  )
}
