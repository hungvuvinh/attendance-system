import Link from "next/link";
import { Activity, CalendarDays, LayoutDashboard, Users, Workflow } from "lucide-react";

const links = [
  { href: "/", label: "Overview", icon: LayoutDashboard },
  { href: "/employees", label: "Employees", icon: Users },
  { href: "/attendance", label: "Attendance", icon: CalendarDays },
  { href: "/timesheets", label: "Timesheets", icon: Workflow },
  { href: "/health", label: "System health", icon: Activity }
];

export function Sidebar() {
  return (
    <aside className="border-b border-ink/10 bg-ink px-5 py-6 text-paper lg:min-h-screen lg:border-b-0 lg:border-r lg:px-6">
      <div className="flex items-center justify-between lg:block">
        <Link href="/" className="inline-flex items-center gap-3">
          <span className="grid h-10 w-10 place-items-center rounded-full bg-mint text-ink">AS</span>
          <span>
            <strong className="block text-lg leading-none">Attendance</strong>
            <span className="text-xs text-paper/60">operations workspace</span>
          </span>
        </Link>
        <span className="hidden text-xs uppercase tracking-[0.2em] text-paper/40 lg:mt-14 lg:block">Workspace</span>
      </div>
      <nav className="mt-5 flex gap-2 overflow-x-auto lg:mt-4 lg:block lg:space-y-1">
        {links.map(({ href, label, icon: Icon }) => (
          <Link key={href} href={href} className="flex shrink-0 items-center gap-3 rounded-lg px-3 py-2.5 text-sm text-paper/70 transition hover:bg-paper/10 hover:text-paper">
            <Icon size={17} strokeWidth={1.8} aria-hidden="true" />
            {label}
          </Link>
        ))}
      </nav>
    </aside>
  );
}
