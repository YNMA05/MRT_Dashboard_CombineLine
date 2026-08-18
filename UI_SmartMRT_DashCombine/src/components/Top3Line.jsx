export default function Top3Line({ theme , data}) {

const lines = [...(data ?? [])]
    .map(line => ({
        line: `L${line.lineNum}`,
        efficiency: line.lineEff
    }));

// Fill empty Top 3 positions
while (lines.length < 3) {
    lines.push({
        line: `L${lines.length + 1}`,
        efficiency: 0
    });
}

    return (

        <div className="
            flex
            flex-col
            gap-4
            h-full
            justify-center
        ">
{lines.map((item, index) => (

<div
key={item.line}
className={`
    flex
    items-center
    justify-between
    rounded-2xl
    px-6
    py-4

    bg-gradient-to-r

    ${
    theme === "dark"
    ?
    `
    from-pink-600/80
    to-purple-800/90
    border-pink-300/30
    shadow-[0_0_25px_rgba(236,72,153,0.35)]
    `
    :
    `
    ${
        index === 0
        ? 
        "from-gray-300 to-gray-600 border-gray-400 shadow-md"

        : index === 1
        ?
        "from-neutral-200 to-gray-500 border-gray-300 shadow-md"

        :
        "from-neutral-100 to-gray-400 border-gray-300 shadow-md"
    }
    `
    }

    border

    ${
        index === 0 && theme === "dark"
        ? "animate-[pulse_3s_ease-in-out_infinite]"
        : ""
    }
`}
>
        <div
            className="
                dark:text-white
                text-2xl
                sm:text-3xl
                font-bold
            "
        >
            #{index + 1} {item.line}
        </div>


        <div
            className="
                dark:text-white
                text-4xl
                sm:text-5xl
                font-bold
                drop-shadow-[0_0_10px_white]
            "
        >
            {item.efficiency}%
        </div>

    </div>

))}


        </div>

    );
}