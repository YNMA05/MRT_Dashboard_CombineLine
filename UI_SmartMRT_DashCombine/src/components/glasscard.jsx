export default function GlassCard({children, title}) {

    return (

        <div
            className="
                flex
                flex-col
                min-h-0
                overflow-hidden
                bg-[#E6F7FF]/80
                dark:bg-transparent
                backdrop-blur-5xl
                border
                border-cyan-400/40
                dark:border-cyan-500/20
                rounded-3xl
                p-1
                shadow-[0_0_30px_rgba(0,255,255,0.15)]
            "
        >

            {title && (
                <h2
                    className="
                        font-bold
                        text-cyan-950
                        dark:text-cyan-100
                        text-sm
                        sm:text-lg
                        lg:text-xl
                        xl:text-2xl
                        text-center
                        mb-2
                        flex-shrink-0
                        tracking-widest
                    "
                >
                    {title}
                </h2>
            )}

            <div
                className="
                    flex-1
                    min-h-0
                "
            >
                {children}
            </div>

        </div>

    );
}