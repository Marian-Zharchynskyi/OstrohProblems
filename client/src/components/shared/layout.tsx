import { type ReactNode } from 'react';
import { Header } from './header';
import { AiChat } from '@/components/chat/ai-chat';

interface LayoutProps {
  children: ReactNode;
}

export const Layout = ({ children }: LayoutProps) => {
  return (
    <div className="min-h-screen bg-background flex flex-col">
      <Header />
      <main className="container mx-auto px-4 py-8 flex-1 flex flex-col">{children}</main>
      <AiChat />
    </div>
  );
};
