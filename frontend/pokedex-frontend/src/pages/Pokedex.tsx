import React, { useEffect, useState } from "react";
import type { Pokemon } from "../models/Pokemon";
import { getAllPokemon } from "../api/PokemonApi";
import { PokedexTable } from "../components/PokedexTable";



export function PokedexPage() {

    const [pokemons, setPokemons] = useState<Pokemon[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        async function loadPokemon() {
            try {
                const data = await getAllPokemon();
                setPokemons(data);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        }

        loadPokemon();
    }, []);


    if (isLoading) {
        return <h1 className="flex items-center justify-center text-orange-600">Lade Pokemon...</h1>;
    }

    return (
        <div className="h-full overflow-y-auto">
            <PokedexTable pokemons={pokemons} />
        </div>
    );
}