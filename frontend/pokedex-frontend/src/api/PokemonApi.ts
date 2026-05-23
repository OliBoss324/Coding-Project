import type { Pokemon } from "../models/Pokemon";

const API_BASE_URL = "http://localhost:5032"; // an deinen Backend-Port anpassen

export async function getAllPokemon(): Promise<Pokemon[]> {
    const response = await fetch(`${API_BASE_URL}/api/v1/pokemon/pokedex`);

    if (!response.ok) {
        throw new Error("Pokemon konnten nicht geladen werden.");
    }

    return await response.json();
}