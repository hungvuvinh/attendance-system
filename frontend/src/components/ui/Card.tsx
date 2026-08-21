import type { HTMLAttributes } from "react";

export function Card({ className = "", ...props }: HTMLAttributes<HTMLElement>) {
  return <section className={`rounded-xl border border-ink/10 bg-white/70 p-5 shadow-[0_12px_30px_rgba(19,34,56,0.05)] ${className}`} {...props} />;
}
