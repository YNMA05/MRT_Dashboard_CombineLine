import Header from "./components/Header";
import Top3Line from "./components/Top3Line";
import KPISection from "./components/KPISection";
import ProductionChart from "./components/ProductionChart";
import LineStatus from "./components/LineStatus";
import GlassCard from "./components/glasscard";
import Footer from "./components/footer";
import backImage from "./assets/ingmar-pcqlvmqdFmk-unsplash.jpg";
import MRTPic from "./components/SmartMRTpic";
import { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import LineOnline from "./components/LineOnline";
import { useMemo } from "react";

export default function Dashboard() {
    const [dashboardData, setDashboardData] = useState([]);
    useEffect(() => {

const HUB_URL = `http://${window.location.hostname}:5000/hubs/MRTdashCombine`;

const connection = new signalR.HubConnectionBuilder()
    .withUrl(HUB_URL)
    .withAutomaticReconnect()
    .build();

        connection.on("DashboardUpdate", (data) => {
            console.log("Received:", data);
            setDashboardData(data);
        });

        connection.start()
            .then(() => console.log("SignalR Connected"))
            .catch(err => console.log(err));

        return () => {

            connection.stop();

        };

    }, []);
    
    useEffect(() => {
    console.log("Dashboard changed:", dashboardData);
    const linestatus = dashboardData.filter(line =>
        line.moduleCode === "V_LINESTATUS"
    );
    const outputLines = dashboardData.filter(line =>
        line.moduleCode === "V_PRODOUTPUT"
    );
        const outputLines1 = dashboardData.filter(line =>
        line.moduleCode === "V_PRODCHART"
    );
    
    console.log("Output lines:", outputLines);
    console.log("Output lines1:", outputLines1);

}, [dashboardData]); 
    const lineOnline = dashboardData.find(
        x => x.moduleCode === "V_LINESTATUS"
    );
    const sortedLineStatus = useMemo(() => {

    const lines = lineOnline?.data ?? [];

    return [...lines].sort((a, b) => {

        // offline first
        if (a.isOnline === b.isOnline) {
            return a.lineNum - b.lineNum;
        }

        return a.isOnline ? 1 : -1;

    });

}, [lineOnline]);
   const productionKpi = dashboardData.find(
    x => x.moduleCode === "V_PRODOUTPUT"
    );
    const productionChart = dashboardData.find(
        x => x.moduleCode === "V_PRODCHART"
    );

    

    /*const chartData = (productionChart?.data ?? []).map(x => ({
        name: `LINE ${x.lineNum}`,
        output: x.output,
        target: x.target,
        runningTarget: x.runningTarget
    }));*/
    const dummyChartData = [
    { lineNum: 1, output: 420, target: 520, runningTarget: 380 },
    { lineNum: 2, output: 315, target: 480, runningTarget: 290 },
    { lineNum: 3, output: 510, target: 600, runningTarget: 430 },
    { lineNum: 4, output: 280, target: 200, runningTarget: 250 },
    { lineNum: 5, output: 640, target: 700, runningTarget: 610 },
    { lineNum: 6, output: 390, target: 520, runningTarget: 340 },
    { lineNum: 7, output: 455, target: 580, runningTarget: 410 },
    { lineNum: 8, output: 230, target: 400, runningTarget: 180 },
    { lineNum: 9, output: 570, target: 680, runningTarget: 530 },
    { lineNum: 10, output: 490, target: 5, runningTarget: 460 },
    { lineNum: 11, output: 55, target: 620, runningTarget: 6 },
    { lineNum: 12, output: 570, target: 100, runningTarget: 530 },
    { lineNum: 13, output: 490, target: 7, runningTarget: 460 },
    { lineNum: 14, output: 55, target: 620, runningTarget: 6 },
];
const useDummy = true;

const chartData = useDummy
    ? dummyChartData
    : (productionChart?.data ?? []).map(x => ({
          name: `Line ${x.lineNum}`,
          runningTarget: x.runningTarget,
          output: x.output,
          target: x.target,
      }));
    
    return (
        /* Using fixed and z-0 on the wrapper ensures the background stays strictly under everything */
        <div className="
    relative
    h-screen
    bg-[#08152f]
    text-white
    overflow-hidden
">

            <img
                src={backImage}
                alt="Background"
                className="absolute inset-0 w-full h-full object-cover opacity-70"
            />
            <div
            className="absolute inset-0 bg-[radial-gradient(circle_at_center,_rgba(0,255,255,0.15),transparent_40%)]
            "
            />

            {/* 2. GLOWING MAZE / GRID ANIMATION LAYER */}
            <div className="absolute inset-0 pointer-events-none overflow-hidden mix-blend-screen opacity-60">
                <svg className="w-full h-full" xmlns="http://w3.org">
                    <defs>
                        {/* Define the pattern for the maze grid lines */}
                        <pattern id="maze-grid" width="60" height="60" patternUnits="userSpaceOnUse">
                            <path d="M 60 0 L 0 0 0 60" fill="none" stroke="rgba(59, 130, 246, 0.15)" strokeWidth="1" />
                        </pattern>
                        
                        {/* Custom inline keyframes for the moving laser effects */}
                        <style>{`
                            @keyframes mazeRunnerHorizontal {
                                0% { stroke-dashoffset: 1200; opacity: 0; }
                                10% { opacity: 0.8; }
                                80% { opacity: 0.8; }
                                100% { stroke-dashoffset: 0; opacity: 0; }
                            }
                            @keyframes mazeRunnerVertical {
                                0% { stroke-dashoffset: -1200; opacity: 0; }
                                20% { opacity: 1; }
                                70% { opacity: 1; }
                                100% { stroke-dashoffset: 0; opacity: 0; }
                            }
                            .laser-line-1 {
                                stroke-dasharray: 150, 450;
                                animation: mazeRunnerHorizontal 8s linear infinite;
                            }
                            .laser-line-2 {
                                stroke-dasharray: 200, 400;
                                animation: mazeRunnerVertical 11s linear infinite;
                            }
                            .laser-line-3 {
                                stroke-dasharray: 100, 500;
                                animation: mazeRunnerHorizontal 6s linear infinite reverse;
                            }
                        `}</style>
                    </defs>

                    {/* Static Background Grid */}
                    <rect width="100%" height="100%" fill="url(#maze-grid)" />

                    {/* Animated Maze Laser Paths (Horizontal & Vertical intersections) */}
                    <g stroke="url(#laser-gradient)" strokeWidth="2" strokeLinecap="round">
                        {/* Row 1 Paths */}
                        <path d="M 0,120 L 400,120 L 400,300 L 900,300 L 900,180 L 1920,180" fill="none" stroke="#60a5fa" className="laser-line-1" />
                        
                        {/* Row 2 Paths */}
                        <path d="M 200,0 L 200,500 L 600,500 L 600,900 L 1200,900 L 1200,1080" fill="none" stroke="#3b82f6" className="laser-line-2" />
                        
                        {/* Row 3 Paths (Moving in Reverse) */}
                        <path d="M 1920,600 L 1400,600 L 1400,400 L 800,400 L 800,750 L 0,750" fill="none" stroke="#a855f7" className="laser-line-3" />
                    </g>
                    
                    {/* Glowing Accent Gradients */}
                    <linearGradient id="laser-gradient" x1="0%" y1="0%" x2="100%" y2="0%">
                        <stop offset="0%" stopColor="#3b82f6" stopOpacity="0.2" />
                        <stop offset="50%" stopColor="#60a5fa" stopOpacity="1" />
                        <stop offset="100%" stopColor="#a855f7" stopOpacity="0.2" />
                    </linearGradient>
                </svg>
            </div>

            {/* 3. DASHBOARD CONTENT LAYER */}
            {/* 'relative z-10' guarantees this sits perfectly on top of all animations */}
<div className="
    relative
    z-10
    p-3
    flex
    flex-col
    h-full
    gap-2
">

{/* TOP SECTION */}
<Header />

<div className="relative my-2">

    <div
        className="
            h-[2px]
            bg-gradient-to-r
            from-transparent
            via-cyan-400
            to-transparent
            shadow-[0_0_15px_#00ffff]
        "
    />

    <div
        className="
            absolute
            top-1/2
            left-0
            w-20
            h-[2px]
            bg-cyan-300
            shadow-[0_0_25px_#00ffff]
            animate-[scan_4s_linear_infinite]
        "
    />

</div>


{/* MAIN DASHBOARD AREA */}
<div className="
    flex-1
    min-h-0
    overflow-y-auto
">

    <div
    className="
        grid
        grid-cols-1
        xl:grid-cols-5
        gap-2
        h-full
        min-h-0
    "
>


        {/* LEFT SIDE */}
            <div
                className="
                    flex
                    flex-col
                    min-h-0
                    h-full
                "
            >

{/* TOP 3 */}
<div className="flex-[2] min-h-0">

    <GlassCard title="TOP 3 LINE">
        <Top3Line />
    </GlassCard>

</div>


{/* KPI AREA */}
<div
    className="
        flex-1
        flex
        items-center
        justify-center
        py-6
    "
>

    <div className="w-full h-[270px]">

        <KPISection
            totalOutput={
                productionKpi?.data?.totalOutput ?? 0
            }
        />

    </div>

</div>


{/* LINE ONLINE */}
<div
    className="
        flex-shrink-0
        max-h-[220px]
        overflow-y-auto
    "
>
    <LineOnline data={sortedLineStatus}/>
</div>
</div>
        {/* RIGHT SIDE */}
        <div
                className="
                    xl:col-span-4
                    flex
                    flex-col
                    min-h-0
                    h-full
                    gap-2
                "
            >



            {/* TOP CHART */}
            <div className="flex-1 min-h-0">
            <GlassCard title="PRODUCTION BY LINE">

              {/* <ProductionChart data={productionChart?.data ?? []}/>*/}
                <ProductionChart data={chartData} />

            </GlassCard>
            </div>

            {/* PUSH LINE STATUS DOWN */}
            <div className="mt-auto">
                <GlassCard>

                <div className="flex-1 min-h-0">
                    <LineStatus />
                </div>

                </GlassCard>


            </div>


        </div>

    </div>
    </div>
     {/* FOOTER */}
    <div    className="
        h-8
        flex-shrink-0
        bg-gray-500/70
    ">
        <Footer />
    </div>

    </div>



</div>
    );
}
