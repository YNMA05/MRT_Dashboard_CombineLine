import smartmrt from "../assets/smartmrt.png";
import {Sun,Moon} from "lucide-react";
import Clock from "./clock";

export default function Header({theme, setTheme}) {
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
        className="
            h-8
            sm:h-10
            lg:h-12
            w-auto
        "
        alt="SmartMRT"
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

        {/* PRINT */}
        <button     className={`
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
            🖨
        </button>


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