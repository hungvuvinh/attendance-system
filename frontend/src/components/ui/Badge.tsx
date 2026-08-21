import type { HTMLAttributes } from "react";

export function Badge({ children, className = "", ...props }: HTMLAttributes<HTMLSpanElement>) {
  return <span className={`inline-flex items-center rounded-full bg-mint px-2.5 py-1 text-xs font-semibold text-ink ${className}`} {...props}>{children}</span>;
}
