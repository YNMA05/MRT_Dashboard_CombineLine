export default function GlassCard({children, title}) {

    return (

        <div
            className="
            bg-white/10
            backdrop-blur-5xl
            border
            border-cyan-400/40
            rounded-3xl
            p-5
            shadow-[0_0_30px_rgba(0,255,255,0.15)]
            "
        >

            {title && (
                <h2 className="
                    text-black-400
                    text-3xl
                    text-center
                    mb-4
                ">
                    {title}
                </h2>
            )}

            {children}

        </div>

    );
}