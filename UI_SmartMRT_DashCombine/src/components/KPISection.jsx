import GlassCard from "./GlassCard";
export default function KPISection({ totalOutput }) {

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
                className="
                    flex-1
                    flex
                    flex-col
                    justify-center
                    items-center
                    bg-white/10
                    rounded-3xl
                    border
                    border-cyan-400/30
                    shadow-[0_0_25px_rgba(0,255,255,0.15)]
                "
            >

                <div
                    className="
                        text-2xl
                        text-cyan-300
                        tracking-widest
                    "
                >
                    CURRENT PRODUCTION
                </div>


                <div
                    className="
                        text-7xl
                        font-bold
                        text-blue-300
                        drop-shadow-[0_0_40px_#00ffff]
                    "
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
                    bg-white/10
                    rounded-3xl
                    border
                    border-indigo-400/30
                    shadow-[0_0_25px_rgba(99,102,241,0.15)]
                "
            >

                <div
                    className="
                        text-2xl
                        text-indigo-300
                        tracking-widest
                    "
                >
                    PREVIOUS PRODUCTION
                </div>


                <div
                    className="
                        text-7xl
                        font-bold
                        text-indigo-300
                        drop-shadow-[0_0_40px_#6366f1]
                    "
                >
                    18,019
                </div>

            </div>


        </div>

    );
}