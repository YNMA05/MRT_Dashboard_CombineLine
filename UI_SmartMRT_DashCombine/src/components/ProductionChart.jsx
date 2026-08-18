import { useState, useEffect } from "react";
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



export default function ProductionChart({ data, theme }) {

const [loaded,setLoaded] = useState(false);

useEffect(()=>{

    const timer = setTimeout(()=>{
        setLoaded(true);
    },1500);

    return ()=>clearTimeout(timer);

},[data]);

   const labelSize =
    window.innerWidth > 2000
        ? 30
        : window.innerWidth > 1600
        ? 25
        : window.innerWidth > 1000
        ? 20
        : 18;

    
    return (

<div
className="
relative
rounded-[30px]
border
border-cyan-500/10
dark:bg-gradient-to-br
from-slate-900/70
to-slate-950/60
"
>
  {/* FIXED LEGEND */}
    <div
    className="
        flex
        justify-end
        items-center
        gap-5
        px-4
        py-2
        border-b
        border-cyan-500/20
        text-lg
        sm:text-xl
        lg:text-2xl
    "
    >

        <div className="flex items-center gap-2 ">

       <span
        className={`
            h-5
            w-5
            rounded-full
            block

            ${
                theme === "dark"
                ?
                `
                bg-[#4f5c6e]
                shadow-[0_0_10px_rgba(12,26,231,0.8)]
                `
                :
                `
                bg-[#5f9be0]
                shadow-md
                `
            }
        `}
        />

        <span className="text-cyan-900 dark:text-cyan-100">
            Target
        </span>

    </div>


    <div className="flex items-center gap-2">
        <span
        className={`
            h-5
            w-5
            rounded-full
            block

            ${
                theme === "dark"
                ?
                `
                bg-[#154f7e]
                shadow-[0_0_10px_rgba(12,26,231,0.8)]
                `
                :
                `
                bg-[#7797a3]
                shadow-md
                `
            }
        `}
        />

        <span className="text-cyan-900 dark:text-cyan-100" >
            Running Target
        </span>

    </div>



    <div className="flex items-center gap-2">

                <span
        className={`
            h-5
            w-5
            rounded-full
            block

            ${
                theme === "dark"
                ?
                `
                bg-[#36a5ca]
                shadow-[0_0_10px_rgba(12,26,231,0.8)]
                `
                :
                `
                bg-[#70cceb]
                shadow-md
                `
            }
        `}
        />

        <span className="text-cyan-900 dark:text-cyan-100">
            Output
        </span>

    </div>

    </div>
    <div className="overflow-x-auto scrollbar-thin scrollbar-thumb-cyan-500/30">
    <div
        className="
        min-w-[1000px]
        h-[365px]
        "
    >
    <ResponsiveContainer width="100%" height="100%">
            <BarChart barCategoryGap="25%" barGap={6} data={data} margin={{top: 10,right: 20, left: 20,bottom: 20}} >

                {/* Put gradients here */}
                <defs>
<linearGradient id="blueGradient" x1="0" y1="0" x2="0" y2="1">
    <stop
        offset="0%"
        stopColor={theme === "dark" ? "#36a5ca" : "#06B6D4"}
    />
    <stop
        offset="100%"
        stopColor={theme === "dark" ? "#38add4" : "#06B6D4"}
    />
</linearGradient>
<linearGradient id="greenGradient" x1="0" y1="0" x2="0" y2="1">
    <stop
        offset="0%"
        stopColor={theme === "dark" ? "#154f7e" : "#0891B2"}
    />
    <stop
        offset="100%"
        stopColor={theme === "dark" ? "#154f7e" : "#0891B2"}
    />
</linearGradient>

  <linearGradient id="redGradient" x1="0" y1="0" x2="0" y2="1">
    <stop
        offset="0%"
        stopColor={theme === "dark" ? "#4f5c6e" : "#2563EB"}
    />
    <stop
        offset="100%"
        stopColor={theme === "dark" ? "#404c5c" : "#2563EB"}
    />
</linearGradient>
    <filter id="barGlow">
    <feGaussianBlur stdDeviation="4" result="blur"/>
    <feMerge>
        <feMergeNode in="blur"/>
        <feMergeNode in="SourceGraphic"/>
    </feMerge>
</filter>


<filter id="barShadow">
    <feDropShadow
        dx="0"
        dy="4"
        stdDeviation="5"
        floodOpacity="0.25"
    />
</filter>
                </defs>

                <CartesianGrid
                    vertical={false}
                    stroke="#334155"
                    strokeOpacity={0.15}
                />
                <XAxis
                   dataKey="name"
                    tickFormatter={(value) => `Line ${value}`}
                    axisLine={false}
                    tickLine={false}
                    tick={{  fill: theme === "dark"
                            ? "#f0f4f5"
                            : "#0e0f0f",
                        fontSize: labelSize
                    }}
                />

                <YAxis
                    hide
                    axisLine={false}
                    tickLine={false}
                    tick={{
                        fill: theme === "dark"
                            ? "#f0f4f5"
                            : "#30a8b1",
                        fontSize: labelSize
                    }}
                />
                <Tooltip
                    contentStyle={{
                        background:
                            theme === "dark"
                            ? "rgba(10,15,30,.96)"
                            : "#f0f4f5",

                        color:
                            theme === "dark"
                            ? "#ffffff"
                            : "#111827",

                        backdropFilter: "blur(12px)",
                        borderRadius: 18,
                        border:"1px solid rgba(56,189,248,.3)",
                        boxShadow:"0 15px 40px rgba(0,0,0,.5)"
                    }}

                    cursor={{
                        fill:"rgba(0,255,255,.06)"
                    }}
                />
                           
            
                <Bar
                    dataKey="target"
                    name = "Target"
                    stackId="a"
                    fill="url(#redGradient)"
                    filter="url(#barGlow)"
                    animationDuration={loaded ? 0 : 1200}
                    animationEasing="ease-out"
                    >
                 <LabelList
                    dataKey="target"
                    position="center"
                    fill={theme === "dark" ? "#f0f4f5" : "#0F172A"}
                    fontSize={labelSize}
            />      
        </Bar>

                <Bar
                    dataKey="runningTarget"
                    name = "Running Target"
                    stackId="a"
                    fill="url(#greenGradient)"
                    filter="url(#barGlow)"
                    animationDuration={loaded ? 0 : 1600}
                    animationEasing="ease-out"
                    >
                 <LabelList
                    dataKey="runningTarget"
                    position="right"
                    fill={theme === "dark" ? "#f0f4f5" : "#0F172A"}
                    fontSize={labelSize}
                />
        </Bar>

        <Bar radius={[8,8,0,0]}
                dataKey="output"
                name = "Output"
                stackId="a"
                fill="url(#blueGradient)"
                filter="url(#barGlow)"
                animationDuration={loaded ? 0 : 1200}
                animationEasing="ease-out"
            >
            <LabelList
                dataKey="output"
                position="center"
                fill={theme === "dark" ? "#f0f4f5" : "#0F172A"}
                fontSize={labelSize}
            /> 
                 
        </Bar>

            </BarChart>
        </ResponsiveContainer>
        </div>
</div>
        </div>

    );

}