"use client";

import { useEffect, useState } from "react";
import { CheckCircle2, LoaderCircle, RefreshCw, XCircle } from "lucide-react";
import { Badge } from "@/components/ui/Badge";
import { Button } from "@/components/ui/Button";
import { Card } from "@/components/ui/Card";
import { getHealth } from "@/lib/api-client";
import type { HealthResponse } from "@/types/health";

type ConnectionState = "idle" | "loading" | "connected" | "disconnected";

export default function HealthPage() {
  const [state, setState] = useState<ConnectionState>("idle");
  const [health, setHealth] = useState<HealthResponse | null>(null);
  const [message, setMessage] = useState("Run a check to verify the API connection.");

  async function checkHealth() {
    const controller = new AbortController();
    const timeout = setTimeout(() => controller.abort(), 5000);
    setState("loading");
    setMessage("Contacting the backend...");
    try {
      const result = await getHealth(controller.signal);
      setHealth(result);
      setState("connected");
      setMessage("The backend is responding normally.");
    } catch {
      setHealth(null);
      setState("disconnected");
      setMessage("The backend could not be reached. Check that the API is running.");
    } finally {
      clearTimeout(timeout);
    }
  }

  useEffect(() => {
    void checkHealth();
  }, []);

  const connected = state === "connected";
  return (
    <div className="reveal max-w-3xl space-y-8">
      <div><p className="mb-2 text-sm font-semibold uppercase tracking-[0.18em] text-coral">Diagnostics</p><h2 className="m-0 text-4xl font-semibold">System health</h2><p className="mt-3 max-w-xl text-ink/60">A focused check of the local API connection and response.</p></div>
      <Card className="flex flex-col gap-6 sm:flex-row sm:items-center sm:justify-between">
        <div className="flex items-start gap-4">
          {state === "loading" ? <LoaderCircle className="animate-spin text-coral" /> : connected ? <CheckCircle2 className="text-emerald-600" /> : <XCircle className="text-coral" />}
          <div><div className="flex items-center gap-3"><h3 className="m-0 text-xl font-semibold">Backend API</h3><Badge className={connected ? "bg-mint" : "bg-coral/15"}>{connected ? "Connected" : state === "loading" ? "Checking" : "Disconnected"}</Badge></div><p className="mb-0 mt-2 text-sm text-ink/55">{message}</p>{health?.timestamp && <p className="mb-0 mt-2 text-xs text-ink/40">Last response: {new Date(health.timestamp).toLocaleString()}</p>}</div>
        </div>
        <Button type="button" variant="quiet" onClick={() => void checkHealth()}><RefreshCw size={15} className="mr-2 inline" /> Retry</Button>
      </Card>
    </div>
  );
}
