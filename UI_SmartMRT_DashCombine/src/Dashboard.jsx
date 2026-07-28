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

    const outputLines = dashboardData.filter(line =>
        line.moduleCode === "V_PRODOUTPUT"
    );

    console.log("Output lines:", outputLines);

}, [dashboardData]);
    const productionKpi = dashboardData.find(
        x => x.moduleCode === "V_PRODOUTPUT"
    );
    
    const data = [
    {
        name: "LINE 10",
        output: 120,
        target: 100,
        defect: 150
    },
    {
        name: "LINE 11",
        output: 150,
        target: 160,
        defect: 250
    },
    {
        name: "LINE 12",
        output: 130,
        target: 150,
        defect: 50
    },
        {
        name: "LINE 13",
        output: 120,
        target: 140,
        defect: 90
    },
    {
        name: "LINE 14",
        output: 550,
        target: 150,
        defect: 70
    },
    {
        name: "LINE 15",
        output: 130,
        target: 500,
        defect: 90
    },    {
        name: "LINE 16",
        output: 550,
        target: 150,
        defect: 70
    },
    {
        name: "LINE 17",
        output: 130,
        target: 200,
        defect: 190
    }
];
    return (
        /* Using fixed and z-0 on the wrapper ensures the background stays strictly under everything */
        <div className="relative min-h-screen bg-[#08152f] text-white overflow-hidden ">

            <img
                src={backImage}
                alt="Background"
                className="absolute inset-0 w-full h-full object-cover opacity-70"
            />
            <div
            className="absoluteinset-0 bg-[radial-gradient(circle_at_center,_rgba(0,255,255,0.15),transparent_40%)]
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
<div
    className="
        relative
        z-10
        p-3
        flex
        flex-col
        gap-2
        overflow-y-auto
    "
>

    <Header />


    {/* MAIN AREA */}
    <div 
        className="
            flex-1
            min-h-0
            grid
            grid-cols-1
            xl:grid-cols-5
            gap-2
        "
    >


        {/* LEFT SIDE */}
        <div 
            className="
                flex
                flex-col
                min-h-0
            "
        >


            {/* TOP */}
            <GlassCard title="Top 3 Line">

                <Top3Line />

            </GlassCard>
            <div className="mt-auto">
                <MRTPic/>
            </div>
            {/* PUSH KPI TO BOTTOM */}
            <div className="mt-4 xl:mt-auto">

<KPISection
    totalOutput={
        productionKpi?.data?.totalOutput ?? 0
    }
/>
            </div>


        </div>

        {/* RIGHT SIDE */}
        <div
            className="
                xl:col-span-4
                flex
                flex-col
                min-h-0
                gap-2
            "
        >


            {/* TOP CHART */}
            <GlassCard title="Production by Line">

                <ProductionChart data={data}/>

            </GlassCard>

            {/* PUSH LINE STATUS DOWN */}
            <div className="mt-4 xl:mt-auto">


                <GlassCard>

                    <LineStatus />

                </GlassCard>


            </div>


        </div>


    </div>

      

</div>

      <Footer />  
      

        </div>
    );
}
