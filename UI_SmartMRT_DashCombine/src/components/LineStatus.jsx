export default function LineStatus({ data }) {
const wipData = [...data].sort(
    (a, b) => b.totalPieceCnt - a.totalPieceCnt
);

const defectData = [...data].sort(
    (a, b) => b.defect - a.defect
);

const efficiencyData = [...data].sort(
    (a, b) => b.efficiency - a.efficiency
);
    return (

    <div
    className="
    rounded-3xl
    p-6
    font-mono

    text-lg
    sm:text-xl
    lg:text-2xl
    xl:text-3xl

    border

    bg-cyan-900/10
    dark:bg-gradient-to-br
    dark:from-slate-900/95
    dark:to-slate-950/80

    border-cyan-500/10

    shadow-lg
    dark:shadow-[0_40px_60px_rgba(0,0,0,.9)]

    backdrop-blur-3xl

    transition-colors
    duration-300
    "
    >

 <div
    className="
        grid
        grid-cols-1
        lg:grid-cols-3
        gap-8
    "
>

                {/* Table 1 */}
                <div className="
            h-80
            overflow-y-auto
            scrollbar-thin
            scrollbar-thumb-cyan-500/30
            rounded-xl
            ">
        <table className="w-full text-left bg-slate-300 dark:bg-slate-900 ">

         <thead className="sticky top-0 bg-slate-700 dark:bg-slate-900 z-10">

            <tr>
                <th
                    colSpan="2"
                    className="p-2 text-green-400 text-center font-bold tracking-widest drop-shadow-[0_0_10px_rgba(34,197,94,0.8)]"
                >
                    LINE WIP
                </th>
            </tr>

            <tr className="border-b border-green-500/30">
                <th className="p-2 text-green-500">
                    Line
                </th>

                <th className="p-2 text-green-500 text-right">
                    Amount
                </th>

            </tr>
            </thead>
                <tbody>
                 {wipData.map((line) => (
                        <tr
                            key={line.lineNum}
                            className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300"
                        >
                            <td className="p-2">
                                L{line.lineNum}
                            </td>

                            <td className="p-2 text-right text-green-800 dark:text-green-500 font-bold">
                                {line.totalHangerIn}
                            </td>
                        </tr>
                    ))}
                    
                </tbody>
            </table>
        </div>
                {/* Table 2 */}                               
        
        <div className="
            h-80
            overflow-y-auto
            scrollbar-thin
            scrollbar-thumb-cyan-500/30
            rounded-xl
            ">
            <table className="w-full text-left bg-slate-300 dark:bg-slate-900">

                <thead className="sticky top-0 bg-slate-700 dark:bg-slate-900 z-10">
                    <tr>
                        <th colSpan="2" className="p-2 text-yellow-500 text-center font-bold tracking-widest drop-shadow-[0_0_10px_rgba(234,179,8,0.8)]">LINE DEFECT</th>
                    </tr>
                    <tr className="border-b border-yellow-500/30">
                        <th className="p-2 text-yellow-500">
                            Line
                        </th>

                        <th className="p-2 text-yellow-500 text-right">
                            Percentage (%)
                        </th>
                    </tr>
                </thead>
                <tbody>
                     {defectData.map((line) => (
                        <tr
                            key={line.lineNum}
                             className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                            <td className="p-2">
                                L{line.lineNum}
                            </td>

                            <td className="p-2 text-right text-yellow-700 dark:text-yellow-500 font-bold">
                                 {line.defect.toFixed(2)}%
                            </td>
                        </tr>
                    ))}                   
                </tbody>
            </table>
        </div>
                {/* Table 3 */}
                <div className="
            h-80
            overflow-y-auto
            scrollbar-thin
            scrollbar-thumb-cyan-500/30
            rounded-xl
            ">
         <table className="w-full text-left bg-slate-300 dark:bg-slate-900">

                <thead className="sticky top-0 bg-slate-700 dark:bg-slate-900 z-10">
                    <tr>
                        <th colSpan="2" className="p-2 text-red-500 text-center font-bold tracking-widest drop-shadow-[0_0_10px_rgba(197,34,56,0.9)]">LINE EFFICIENCY</th>
                    </tr>
                    <tr className="border-b text-red-500/30">
                        <th className="p-2 text-red-500">
                            Line
                        </th>

                        <th className="p-2 text-red-500 text-right">
                            Percentage (%)
                        </th>
                    </tr>
                </thead>
                <tbody>
                     {efficiencyData.map((line) => (
                        <tr
                            key={line.lineNum}
                            className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                            <td className="p-2">
                                L{line.lineNum}
                            </td>

                            <td className="p-2 text-right text-red-500 font-bold">
                                {line.efficiency.toFixed(2)}%
                            </td>
                        </tr>
                    ))}

                </tbody>
            </table>
            </div>

            </div>

        </div>

    );

}