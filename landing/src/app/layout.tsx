import type { Metadata } from "next";
import { Sora, Mulish } from "next/font/google";
import "./globals.css";
import { Header } from "@/components/shared/header";
import { Footer } from "@/components/shared/footer";

const sora = Sora({
  variable: "--font-sora",
  subsets: ["latin"],
  weight: ["400", "500", "600", "700"],
});

const mulish = Mulish({
  variable: "--font-mulish",
  subsets: ["latin"],
  weight: ["400", "500", "600", "700"],
});

export const metadata: Metadata = {
  title: "Острог разом",
  description: "Платформа для звітування та вирішення проблем у місті Острог",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="uk">
      <body className={`${sora.variable} ${mulish.variable} antialiased`}>
        <div className="min-h-screen bg-background flex flex-col">
          <Header />
          <main className="container mx-auto px-4 py-8 flex-1 flex flex-col">
            {children}
          </main>
          <Footer />
        </div>
      </body>
    </html>
  );
}
