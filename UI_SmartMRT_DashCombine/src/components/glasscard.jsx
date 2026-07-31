export default function GlassCard({children, title}) {

    return (

        <div
            className="
                flex
                flex-col
                min-h-0
                overflow-hidden
                bg-white/10
                backdrop-blur-5xl
                border
                border-cyan-400/40
                rounded-3xl
                p-2
                shadow-[0_0_30px_rgba(0,255,255,0.15)]
            "
        >

            {title && (
                <h2
                    className="
                        text-cyan-100
                        text-2xl
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