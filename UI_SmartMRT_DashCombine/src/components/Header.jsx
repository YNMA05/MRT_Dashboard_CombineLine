import { useState } from "react";
import smartmrt from "../assets/smartmrt.png";
import {Sun,Moon} from "lucide-react";
import Clock from "./clock";


export default function Header({theme, setTheme}) {
const [showExportMenu, setShowExportMenu] = useState(false);
const handleExportExcel = async () => {

    const response = await fetch(
        `http://${window.location.hostname}:5000/api/report/excel`
    );

    const blob = await response.blob();

    const url = window.URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = "SmartMRT_Production_Report.xlsx";
    a.click();

    window.URL.revokeObjectURL(url);
};

const handleExportPDF = async () => {
    const response = await fetch(
        `http://${window.location.hostname}:5000/api/report/pdf`
    );

    if (!response.ok) {
        throw new Error(`PDF export failed: ${response.status}`);
    }

    const blob = await response.blob();

    const url = window.URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = "SmartMRT_Production_Report.pdf";
    document.body.appendChild(a);
    a.click();
    a.remove();

    window.URL.revokeObjectURL(url);
};

return (
<div
className="
flex
flex-col
sm:flex-row
items-center
justify-between
gap-3
w-full
"
>

    {/* LEFT */}
    <div
    className="
    flex
    items-center
    gap-3
    w-full
    sm:w-auto
    "
    >

       <img
        src={smartmrt}
        className="h-8 sm:h-10 lg:h-12 w-auto"
        alt="SmartMRT"
        onClick={() => window.location.reload()}
        style={{cursor: "pointer"}}
    />


        <div className="min-w-0">

            <h1
            className={`
            text-xl
            sm:text-4xl
            lg:text-6xl
            xl:text-7xl
            font-bold
            leading-tight
            whitespace-nowrap
            transition-all

            ${
            theme === "dark"
            ?
            "text-cyan-300 drop-shadow-[0_0_15px_#00ffff]"
            :
            "text-slate-800"
            }
            `}
            >
                Line Performance
            </h1>


            <div
            className={`
            mt-1
            h-1
            rounded-full

            ${
            theme === "dark"
            ?
            "bg-cyan-400 shadow-[0_0_12px_#00ffff]"
            :
            "bg-gradient-to-r from-cyan-500 to-blue-600"
            }
            `}
            />


            <p className="
            text-cyan-800
            dark:text-cyan-100
            ">
                SmartMRT Dashboard
            </p>

        </div>

    </div>


    {/* RIGHT */}
    <div
    className="
    flex
    items-center
    gap-3

    w-full
    sm:w-auto

    justify-center
    sm:justify-end
    "
    >

        {/* EXPORT */}
<div className="relative">
    <button
        onClick={() => setShowExportMenu(!showExportMenu)}
        className={`
            h-11
            w-11
            rounded-xl
            flex
            items-center
            justify-center
            transition-all
            duration-300

            ${
                theme === "dark"
                    ? `
                        bg-cyan-500/10
                        border
                        border-cyan-400/30
                        text-cyan-300
                        hover:bg-cyan-400/20
                        hover:shadow-[0_0_15px_#22d3ee]
                      `
                    : `
                        bg-white/10
                        border
                        border-slate-300
                        text-slate-700
                        hover:bg-slate-100
                        shadow-md
                      `
            }
        `}
        title="Export report"
    >
        ↓
    </button>

    {showExportMenu && (
        <div
            className={`
                absolute
                right-0
                top-14
                w-44
                rounded-xl
                border
                shadow-xl
                overflow-hidden
                z-50

                ${
                    theme === "dark"
                        ? "bg-slate-900 border-cyan-400/20"
                        : "bg-white border-slate-200"
                }
            `}
        >
            <button
                onClick={handleExportExcel}
                className="w-full px-4 py-3 text-left hover:bg-slate-100 dark:hover:bg-slate-800"
            >
                📊 Export Excel
            </button>

            <button
                onClick={handleExportPDF}
                className="w-full px-4 py-3 text-left hover:bg-slate-100 dark:hover:bg-slate-800"
            >
                📄 Download PDF
            </button>
        </div>
    )}
</div>


        {/* THEME */}
       <button
    onClick={() =>
        setTheme(theme === "dark" ? "light" : "dark")
    }
    className={`
        h-11
        w-11
        rounded-xl
        flex
        items-center
        justify-center
        transition-all
        duration-300

        ${
            theme === "dark"
                ? `
                    bg-cyan-500/10
                    border
                    border-cyan-400/30
                    text-cyan-300
                    hover:bg-cyan-400/20
                    hover:shadow-[0_0_15px_#22d3ee]
                  `
                : `
                    bg-white/10
                    border
                    border-slate-300
                    text-slate-700
                    hover:bg-slate-100
                    shadow-md
                  `
        }
    `}
>
    {theme === "dark"
        ? <Sun size={20} />
        : <Moon size={20} />}
</button>


        <Clock />

    </div>


</div>
);

}