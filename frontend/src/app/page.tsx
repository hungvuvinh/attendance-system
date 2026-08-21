import { Activity, ArrowUpRight, CalendarCheck2, Clock3, Users } from "lucide-react";
import { Badge } from "@/components/ui/Badge";
import { Card } from "@/components/ui/Card";
import { Table } from "@/components/ui/Table";

const metrics = [
  { label: "Present today", value: "184", detail: "+6.2% from yesterday", icon: CalendarCheck2 },
  { label: "Active employees", value: "212", detail: "Across 7 departments", icon: Users },
  { label: "Hours logged", value: "1,426", detail: "89% of expected hours", icon: Clock3 }
];

export default function DashboardPage() {
  return (
    <div className="reveal space-y-8">
      <section className="flex flex-col justify-between gap-5 border-b border-ink/10 pb-7 sm:flex-row sm:items-end">
        <div>
          <p className="mb-2 text-sm font-semibold uppercase tracking-[0.18em] text-coral">Operations overview</p>
          <h2 className="m-0 max-w-xl text-4xl font-semibold leading-tight sm:text-5xl">A clear view of the working day.</h2>
        </div>
        <Badge><Activity size={13} className="mr-1.5" /> Systems nominal</Badge>
      </section>

      <section className="grid gap-4 md:grid-cols-3">
        {metrics.map(({ label, value, detail, icon: Icon }) => (
          <Card key={label}>
            <Icon size={20} className="text-coral" />
            <p className="mb-1 mt-6 text-sm text-ink/55">{label}</p>
            <p className="m-0 text-4xl font-semibold">{value}</p>
            <p className="mb-0 mt-2 text-xs text-ink/50">{detail}</p>
          </Card>
        ))}
      </section>

      <section className="grid gap-5 lg:grid-cols-[1.35fr_0.65fr]">
        <Card>
          <div className="mb-5 flex items-center justify-between"><div><h3 className="m-0 text-xl font-semibold">Today&apos;s activity</h3><p className="mb-0 mt-1 text-sm text-ink/50">Latest attendance events</p></div><ArrowUpRight size={19} className="text-ink/40" /></div>
          <Table headers={["Employee", "Time", "Method", "Status"]} rows={[["Nguyen Van A", "07:52", "Fingerprint", <Badge key="a">Recorded</Badge>], ["Tran Thi B", "08:14", "Face", <Badge key="b" className="bg-coral/15">Review</Badge>], ["Le Minh C", "08:21", "Card", <Badge key="c">Recorded</Badge>]]} />
        </Card>
        <Card className="bg-ink text-paper">
          <p className="mb-2 text-sm uppercase tracking-[0.18em] text-mint">Next checkpoint</p>
          <h3 className="m-0 text-2xl font-semibold">Daily attendance review</h3>
          <p className="mt-3 text-sm leading-6 text-paper/65">Review exceptions and confirm today&apos;s records before the processing window closes.</p>
          <a href="/attendance" className="mt-8 inline-flex items-center gap-2 text-sm font-semibold text-mint hover:text-white">Open attendance <ArrowUpRight size={16} /></a>
        </Card>
      </section>
    </div>
  );
}
