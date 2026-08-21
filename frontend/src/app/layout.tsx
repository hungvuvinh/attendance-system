import type { Metadata } from "next";
import { Navbar } from "@/components/layout/Navbar";
import { Sidebar } from "@/components/layout/Sidebar";
import "./globals.css";

export const metadata: Metadata = {
  title: "Attendance System",
  description: "Attendance operations workspace"
};

export default function RootLayout({ children }: Readonly<{ children: React.ReactNode }>) {
  return (
    <html lang="en">
      <body>
        <div className="min-h-screen lg:grid lg:grid-cols-[248px_1fr]">
          <Sidebar />
          <div className="min-w-0">
            <Navbar />
            <main className="mx-auto max-w-7xl px-5 py-8 sm:px-8">{children}</main>
          </div>
        </div>
      </body>
    </html>
  );
}
