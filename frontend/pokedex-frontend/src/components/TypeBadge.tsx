import { getTypeColour } from "../utils/pokemonTypeColors";

function capitalizeFirstLetter(value: string | null | undefined): string {
    if (!value) {
        return "";
    }

    return value.charAt(0).toUpperCase() + value.slice(1);
}

export function TypeBadge({ type }: { type: string | null }) {
    return (
        <>
            {type &&
                <span style={{ backgroundColor: getTypeColour(type) }}
                    className="border-2 border-white rounded-xl py-0.5 px-2 shadow-sm text-black text-sm font-bold text-center min-w-20">
                    {capitalizeFirstLetter(type)}
                </span>
            }
        </>
    )
}