import GlassCard from "./GlassCard";
export default function KPISection({ totalOutput,previousOutput, theme }) {

    return (

        <div
            className="
                flex
                flex-col
                gap-2
                w-full
                h-full
            "
        >

            {/* CURRENT */}
            <div
className={`
    flex-1
    flex
    flex-col
    justify-center
    items-center

    rounded-3xl

    ${
        theme === "dark"
        ?
        `
        bg-transparent
        border
        border-cyan-400/30
        shadow-[0_0_25px_rgba(0,255,255,0.15)]
        `
        :
        `
        bg-[#E6F7FF]/80
        border
        border-cyan-200
        shadow-[0_8px_25px_rgba(0,85,105,0.15)]
        `
    }
`}
>

               <div
                className="
                text-sm
                sm:text-lg
                lg:text-xl
                xl:text-2xl
                font-bold
                dark:font-normal
                text-cyan-900 dark:text-cyan-300
                tracking-widest
                "
                >
                CURRENT PRODUCTION
                </div>


                <div
                className={`
                    text-3xl
                    sm:text-5xl
                    lg:text-6xl
                    xl:text-7xl

                    font-black
                    tracking-wider

                    text-cyan-900
                    dark:text-cyan-200

                    ${
                        theme === "dark"
                        ?
                        "drop-shadow-[0_0_40px_#00ffff]"
                        :
                        "drop-shadow-[0_4px_8px_rgba(0,85,105,0.35)]"
                    }
                `}
                >
                    {(totalOutput ?? 0).toLocaleString()}
                </div>

            </div>


            {/* PREVIOUS */}
            <div
                className="
                    flex-1
                    flex
                    flex-col
                    justify-center
                    items-center
                    bg-stone-100/70
                    dark:bg-transparent
                    rounded-3xl
                    border
                    border-indigo-400/30
                    shadow-[0_0_25px_rgba(99,102,241,0.15)]
                    tracking-wider
                "
            >

                <div
                    className="
                        text-sm
                        sm:text-lg
                        lg:text-xl
                        xl:text-2xl
                        font-bold
                        dark:font-normal
                        text-indigo-900 dark:text-indigo-300
                        tracking-widest
                    "
                >
                    PREVIOUS PRODUCTION
                </div>


                <div
                    className="
                        text-3xl
                        sm:text-5xl
                        lg:text-6xl
                        xl:text-7xl
                        font-black
                        text-indigo-900 dark:text-indigo-300
                        drop-shadow-[0_0_40px_#6366f1]
                    "
                >
                    {(previousOutput ?? 0).toLocaleString()}
                </div>

            </div>


        </div>

    );
}