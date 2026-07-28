export default function DashboardCard({title, children}) {

    return (

        <div
            className="rounded-3xl
            bg-[#576288]/90
            border-cyan-700/50
            shadow-lg
            shadow-sky-900/10
            p-5
            "
        >

            <h2
                className="
                text-xl
                font-bold
                text-white
                mb-4
                "
            >
                {title}
            </h2>


            {children}


        </div>

    );

}