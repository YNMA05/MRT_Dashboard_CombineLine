import GlassCard from "./GlassCard";

export default function LineOnline({ data = [] }) {
    return (
        <GlassCard title="CONNECTION STATUS">     
            <div className="space-y-0">
                {data.map(line => (
                    <div
                        key={line.lineNum}
                        className="flex items-center justify-between rounded-lg border border-slate-700 bg-slate-400/70 dark:bg-slate-900/40 px-3 py-2"
                    >
                        <div className="flex items-center gap-3">
                            <div
                                className={`h-3 w-3 rounded-full ${
                                    line.isOnline
                                        ? "bg-green-700 dark:bg-green-400 animate-glow-pulse"
                                        : "bg-red-500 shadow-[0_0_10px_#ef4444]"
                                }`}
                            />
                            <span className="font-semibold text-cyan-950 dark:text-white">
                                LINE {line.lineNum}
                            </span>
                        </div>

                        <span
                            className={`text-sm font-medium ${
                                line.isOnline
                                    ? "text-green-700 dark:text-green-300"
                                    : "text-red-700 dark:text-red-300"
                            }`}
                        >
                            {line.isOnline ? "Connected" : "Offline"}
                        </span>
                    </div>
                ))}
            </div>
        </GlassCard>
    );
}