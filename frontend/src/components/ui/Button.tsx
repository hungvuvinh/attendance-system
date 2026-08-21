import type { ButtonHTMLAttributes } from "react";

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: "solid" | "quiet";
};

export function Button({ variant = "solid", className = "", ...props }: ButtonProps) {
  const styles = variant === "solid" ? "bg-ink text-paper hover:bg-ink/90" : "border border-ink/15 text-ink hover:bg-ink/5";
  return <button className={`rounded-lg px-4 py-2 text-sm font-semibold transition ${styles} ${className}`} {...props} />;
}
