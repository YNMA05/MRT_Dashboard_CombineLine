import GlassCard from "./GlassCard";


export default function KPISection({ totalOutput }) {

    return (

        <div className="grid grid-cols-1 gap-4">

            <GlassCard title="Current Production">

                <h2
                    className="
                        text-5xl
                        text-blue-300
                        border-2
                        border border-gray-400 p-4
                        font-semibold
                        text-center
                        font-serif
                    "
                >
                    {(totalOutput ?? 0).toLocaleString()}
                </h2>

            </GlassCard>



            <GlassCard title="Previous Production">

                <h2
                    className="
                    text-5xl
                    text-indigo-300
                    border border-gray-400 p-4
                    font-semibold
                    text-center
                    font-serif
                    "
                >
                    18019
                </h2>

            </GlassCard>


        </div>

    );

}