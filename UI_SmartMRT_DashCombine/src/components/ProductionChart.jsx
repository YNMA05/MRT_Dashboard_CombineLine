import {
    ResponsiveContainer,
    BarChart,
    CartesianGrid,
    XAxis,
    YAxis,
    Tooltip,
    Legend,
    Bar,
    LabelList
} from "recharts";

export default function ProductionChart({ data }) {

    return (

    <div
    className="
    relative
    rounded-[30px]
    border
    border-cyan-500/10
    bg-gradient-to-br
    from-slate-900/95
    to-slate-950/80
    backdrop-blur-3xl
    shadow-[0_40px_60px_rgba(0,0,0,.9)]
    overflow-hidden
    "
    >
<div className="h-[250px] md:h-[400px]">
    <ResponsiveContainer width="100%" height="100%">
            <BarChart barCategoryGap="25%" barGap={2} data={data} margin={{top: 10,right: 20, left: 20,bottom: 20}} >

                {/* Put gradients here */}
                <defs>
                    <linearGradient id="blueGradient" x1="0" y1="0" x2="0" y2="1">
                        <stop offset="0%" stopColor="#57184e" />
                        <stop offset="100%" stopColor="#db36c0" />
                    </linearGradient>

                    <linearGradient id="greenGradient" x1="0" y1="0" x2="0" y2="1">
                        <stop offset="0%" stopColor="#2d9c9c" />
                        <stop offset="100%" stopColor="#63dcf1" />
                    </linearGradient>

                    <linearGradient id="redGradient" x1="0" y1="0" x2="0" y2="1">
                        <stop offset="0%" stopColor="#0c1ae7" />
                        <stop offset="100%" stopColor="#1c255c" />
                    </linearGradient>
     <filter id="barGlow">
        <feDropShadow
            dx="0"
            dy="0"
            stdDeviation="7"
            floodColor="#b4d8e9"
            floodOpacity="0.6"
        />
    </filter>
                </defs>

                <CartesianGrid
                    vertical={false}
                    stroke="#1E293B"
                    strokeOpacity={0.25}
                    strokeDasharray="3 8"
                />
                <XAxis
                   dataKey="lineNum"
                    tickFormatter={(value) => `Line ${value}`}
                    axisLine={false}
                    tickLine={false}
                    tick={{ fill: "#ffffff", fontSize: 25 }}
                />

                <YAxis
                    axisLine={false}
                    tickLine={false}
                    tick={{ fill: "#f5f5f5", fontSize: 25 }}
                />
                <Tooltip
                contentStyle={{
                background:"rgba(15,23,42,.95)",
                borderRadius:20,
                border:"1px solid rgba(56,189,248,.3)",
                boxShadow:"0 15px 40px rgba(0,0,0,.5)"
            }}
                cursor={{
                    fill:"rgba(0,255,255,.06)"
                }}
                />
                <Legend  verticalAlign="top" align="right" iconType="circle" iconSize={20}
                    formatter={(value) => (
                        <span
                            style={{
                                color: "#CBD5E1",
                                fontSize: 23,
                                fontWeight: 500
                            }}
                        >
                            {value}
                        </span>
                    )}
                />
            
                <Bar
                    dataKey="target"
                    name = "Target"
                    stackId="a"
                    fill="url(#redGradient)"
                    filter="url(#barGlow)"
                    animationDuration={1200}
                    animationEasing="ease-out"
                    >
                 <LabelList
                dataKey="target"
                position="center"
                fill="#f0f4f5"
                fontSize={25}
            />      
        </Bar>

                <Bar
                    dataKey="runningTarget"
                    name = "Running Target"
                    stackId="a"
                    fill="url(#greenGradient)"
                    filter="url(#barGlow)"
                    animationDuration={1600}
                    animationEasing="ease-out"
                    >
                 <LabelList
                dataKey="runningTarget"
                position="right"
                fill="#f0f4f5"
                fontSize={25}
            />
        </Bar>

        <Bar
                dataKey="output"
                name = "Output"
                stackId="a"
                fill="url(#blueGradient)"
                filter="url(#barGlow)"
                animationDuration={1200}
                animationEasing="ease-out"
            >
            <LabelList
                dataKey="output"
                position="center"
                fill="#f0f4f5"
                fontSize={25}
            />      
        </Bar>

            </BarChart>
        </ResponsiveContainer>
</div>
        </div>

    );

}