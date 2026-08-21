import { Bell, Search } from "lucide-react";

export function Navbar() {
  return (
    <header className="flex items-center justify-between gap-4 border-b border-ink/10 bg-paper/90 px-5 py-4 backdrop-blur sm:px-8">
      <div>
        <p className="m-0 text-xs uppercase tracking-[0.2em] text-ink/45">Thursday, 20 August 2026</p>
        <h1 className="m-0 mt-1 text-xl font-semibold">Good morning, team</h1>
      </div>
      <div className="flex items-center gap-2">
        <button type="button" title="Search" aria-label="Search" className="grid h-9 w-9 place-items-center rounded-full border border-ink/10 text-ink/60 hover:bg-ink/5">
          <Search size={17} />
        </button>
        <button type="button" title="Notifications" aria-label="Notifications" className="grid h-9 w-9 place-items-center rounded-full border border-ink/10 text-ink/60 hover:bg-ink/5">
          <Bell size={17} />
        </button>
        <span className="hidden h-9 w-9 place-items-center rounded-full bg-coral text-sm font-semibold text-white sm:grid">HR</span>
      </div>
    </header>
  );
}
