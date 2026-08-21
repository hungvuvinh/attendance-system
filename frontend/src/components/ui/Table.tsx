import type { ReactNode } from "react";

export function Table({ headers, rows }: { headers: string[]; rows: ReactNode[][] }) {
  return (
    <div className="overflow-x-auto">
      <table className="w-full min-w-[520px] border-collapse text-left text-sm">
        <thead><tr className="border-b border-ink/10 text-xs uppercase tracking-wider text-ink/45">{headers.map((header) => <th key={header} className="px-3 py-3 font-semibold">{header}</th>)}</tr></thead>
        <tbody>{rows.map((row, index) => <tr key={index} className="border-b border-ink/5 last:border-0">{row.map((cell, cellIndex) => <td key={cellIndex} className="px-3 py-3">{cell}</td>)}</tr>)}</tbody>
      </table>
    </div>
  );
}
