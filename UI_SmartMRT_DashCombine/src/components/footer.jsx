export default function Footer() {

    return (

        <footer
            className="
                h-full
                flex
                flex-col
                sm:flex-row
                justify-center
                sm:justify-between
                items-center
                gap-1
                px-3
                text-xs
                sm:text-sm
                text-gray-100
                border-t
                border-cyan-500/20
            "
        >

            <span className="text-cyan-100">
                © 2026 Singapore Machinery Co.
            </span>

            <span
                className="
                    tracking-widest
                    text-cyan-300
                    drop-shadow-[0_0_8px_#00ffff]
                "
            >
                SMARTMRT // COMBINE LINE // v1.0
            </span>

        </footer>

    );

}