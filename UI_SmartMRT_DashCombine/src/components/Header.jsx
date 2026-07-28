import smartmrt from "../assets/smartmrt.png";
import Clock from "./clock";

export default function Header() {

    return (

        <div className="flex justify-between items-center mb-1">

            <h1
                className="
                    text-6xl
                    font-bold
                    text-cyan-300
                    drop-shadow-[0_0_15px_#00ffff]
                "
            >
                Line Performance
            </h1>
    
<div>
    <Clock/>
</div>
        </div>

    );
}