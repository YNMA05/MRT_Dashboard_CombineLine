import { useEffect, useState } from "react";

export default function Clock() {

    const [time, setTime] = useState(new Date());

    useEffect(() => {

        const timer = setInterval(() => {
            setTime(new Date());
        }, 1000);

        return () => clearInterval(timer);

    }, []);

    return (
<div className="relative overflow-hidden rounded-xl">

    <div className="absolute inset-0 bg-cyan-500/5"></div>

    <div className="
        absolute
        top-0
        left-[-100%]
        h-full
        w-1/3
        bg-gradient-to-r
        from-transparent
        via-cyan-300/30
        to-transparent
        animate-[scan_3s_linear_infinite]
    "></div>

   
        <div className="
        rounded-xl
        border border-cyan-400/40
        bg-cyan-500/5
        backdrop-blur-md
        px-5 py-0
        shadow-[0_0_30px_rgba(0,255,255,0.25)]
    ">

        <div className="
            text-lg
            sm:text-xl
            lg:text-2xl
            xl:text-3xl
            tracking-[0.15em]
            text-cyan-900
            dark:text-cyan-300
            drop-shadow-[0_0_10px_#00ffff]
        ">
            {time.toLocaleTimeString()}
        </div>

        <div className="
            mt-1
            text-sm
            sm:text-lg
            lg:text-xl
            xl:text-2xl
            uppercase
            tracking-[0.35em]
            text-cyan-900
            dark:text-cyan-100/80
        ">
            {time.toLocaleDateString()}
        </div>

    </div>
</div>


    );
}