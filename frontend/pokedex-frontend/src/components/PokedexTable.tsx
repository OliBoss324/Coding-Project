import type { Pokemon } from "../models/Pokemon"
import { getTypeColour } from "../utils/pokemonTypeColors";
import { TypeBadge } from "./TypeBadge";


type PokedexTableProps = {
    pokemons: Pokemon[];
};

function capitalizeFirstLetter(value: string | null | undefined): string {
    if (!value) {
        return "";
    }

    return value.charAt(0).toUpperCase() + value.slice(1);
}


export function PokedexTable({ pokemons }: PokedexTableProps) {
    return (

        <div className="flex justify-center">
            <table className="table-auto border-collapse border-spacing-0">
                <thead >
                    <tr>
                        <th></th>
                        <th className="sticky top-0 z-10 bg-zinc-700 px-4 py-2 text-left">Id</th>
                        <th className="sticky top-0 z-10 bg-zinc-700 px-4 py-2 text-left">Name</th>
                        <th className="sticky top-0 z-10 bg-zinc-700 px-4 py-2 text-left" >Types</th>
                    </tr>
                </thead>
                <tbody>
                    {pokemons.map((pokemon) => (
                        <tr key={pokemon.id}>
                            <td>
                                <img />
                            </td>
                            <td className="px-2 py-1 border-y border-zinc-500">{pokemon.id}</td>
                            <td className="px-2 py-1 border-y border-zinc-500">{capitalizeFirstLetter(pokemon.name)}</td>
                            <td className="px-2 py-1 border-y border-zinc-500">
                                <div className="flex gap-2">
                                    <TypeBadge type={pokemon.type1} />
                                    <TypeBadge type={pokemon.type2} />
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>

    )
}