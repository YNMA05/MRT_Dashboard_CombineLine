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

        <div className="text-right">

            <div className="text-4xl font-bold text-cyan-300 drop-shadow-[0_0_15px_#00ffff]">
                {time.toLocaleTimeString()}
            </div>

            <div className="text-2xl font-bold text-gray-300">
                {time.toLocaleDateString()}
            </div>

        </div>

    );
}