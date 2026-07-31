export default function LineStatus() {

    return (

        <div className="rounded-3xl p-8 font-mono text-3xl bg-gradient-to-br
        border
        border-cyan-500/10 from-slate-900/95
        to-slate-950/80
        backdrop-blur-3xl
        shadow-[0_40px_60px_rgba(0,0,0,.9)]">

            <div className="grid grid-cols-3 gap-8">

                {/* Table 1 */}
        <div className="h-80 overflow-y-auto rounded-lg">
        <table className="w-full text-left ">

         <thead className="sticky top-0 bg-slate-900 z-10">

            <tr>
                <th
                    colSpan="2"
                    className="p-2 text-green-500 text-center font-bold tracking-widest drop-shadow-[0_0_10px_rgba(34,197,94,0.8)]"
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

                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L17
                        </td>

                        <td className="p-2 text-right text-green-500 font-bold">
                            209
                        </td>
                    </tr>


                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L16
                        </td>

                        <td className="p-2 text-right text-green-500 font-bold">
                            322
                        </td>
                    </tr>


                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L15
                        </td>

                        <td className="p-2 text-right text-green-500 font-bold">
                            344
                        </td>
                    </tr>

                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L14
                        </td>

                        <td className="p-2 text-right text-green-500 font-bold">
                            3445
                        </td>
                    </tr>

                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L13
                        </td>

                        <td className="p-2 text-right text-green-500 font-bold">
                            3432
                        </td>
                    </tr>

                     <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L12
                        </td>

                        <td className="p-2 text-right text-green-500 font-bold">
                            3499
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
                {/* Table 2 */}                               
        
        <div className="h-80 overflow-y-auto rounded-lg">
            <table className="w-full text-left">

                <thead className="sticky top-0 bg-slate-900 z-10">
                    <tr>
                        <th colSpan="2" className="p-2 text-yellow-500 text-center font-bold tracking-widest drop-shadow-[0_0_10px_rgba(34,197,94,0.8)]">LINE DEFECT</th>
                    </tr>
                    <tr className="border-b border-yellow-500/30">
                        <th className="p-2 text-yellow-300">
                            Line
                        </th>

                        <th className="p-2 text-yellow-500 text-right">
                            Percentage (%)
                        </th>
                    </tr>
                </thead>
                <tbody>

                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L17
                        </td>

                        <td className="p-2 text-right text-yellow-500 font-bold">
                            12
                        </td>
                    </tr>


                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L16
                        </td>

                        <td className="p-2 text-right text-yellow-500 font-bold">
                            5
                        </td>
                    </tr>


                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L15
                        </td>

                        <td className="p-2 text-right text-yellow-500 font-bold">
                            11
                        </td>
                    </tr>

                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L14
                        </td>

                        <td className="p-2 text-right text-yellow-500 font-bold">
                            2
                        </td>
                    </tr>

                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L13
                        </td>

                        <td className="p-2 text-right text-yellow-500 font-bold">
                            40
                        </td>
                    </tr>

                     <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L12
                        </td>

                        <td className="p-2 text-right text-yellow-500 font-bold">
                            9
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
                {/* Table 3 */}
        <div className="h-80 overflow-y-auto rounded-lg">
         <table className="w-full text-left">

                <thead className="sticky top-0 bg-slate-900 z-10">
                    <tr>
                        <th colSpan="2" className="p-2 text-red-500 text-center font-bold tracking-widest drop-shadow-[0_0_10px_rgba(34,197,94,0.8)]">LINE EFFICIENCY</th>
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

                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L17
                        </td>

                        <td className="p-2 text-right text-red-500 font-bold">
                            12
                        </td>
                    </tr>


                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L16
                        </td>

                        <td className="p-2 text-right text-red-500 font-bold">
                            5
                        </td>
                    </tr>


                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L15
                        </td>

                        <td className="p-2 text-right text-red-500 font-bold">
                            11
                        </td>
                    </tr>

                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L14
                        </td>

                        <td className="p-2 text-right text-red-500 font-bold">
                            2
                        </td>
                    </tr>

                    <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L13
                        </td>

                        <td className="p-2 text-right text-red-500 font-bold">
                            40
                        </td>
                    </tr>

                     <tr className="border-b border-white/10 hover:bg-cyan-400/10 transition-all duration-300">
                        <td className="p-2">
                            L12
                        </td>

                        <td className="p-2 text-right text-red-500 font-bold">
                            9
                        </td>
                    </tr>
                </tbody>
            </table>
            </div>

            </div>

        </div>

    );

}