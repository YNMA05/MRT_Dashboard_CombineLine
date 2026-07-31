import smartmrt from "../assets/smartmrt.png";
import Clock from "./clock";

export default function Header() {

    return (

      <div className="flex items-center justify-between mb-3">

    {/* Left */}
    <div className="flex items-center gap-4">

        <img
            src={smartmrt}
            className="h-16 w-auto"
            alt="SmartMRT"
        />

        <div>

            <h1 className="
                text-6xl
                font-black
                tracking-wide
                text-cyan-300
                drop-shadow-[0_0_20px_#00ffff]
            ">
                LINE PERFORMANCE
            </h1>

            <p className="text-cyan-100 tracking-[0.35em] uppercase">
                SmartMRT Dashboard
            </p>

        </div>

    </div>

    {/* Right */}

    <Clock/>

</div>

    );
}