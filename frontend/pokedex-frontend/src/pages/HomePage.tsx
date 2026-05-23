import React from "react";
import "../index.css"


export function HomePage() {
    return (
        <>
            <form className="flex items-center justify-center gap-2 my-2">
                <input
                    type="search" placeholder="Pokemon suchen..."
                    className="border-2 rounded-xl px-2 py-1" />
                <button type="submit"
                    className="btn">
                    Suchen
                </button>
            </form>
        </>
    )
}