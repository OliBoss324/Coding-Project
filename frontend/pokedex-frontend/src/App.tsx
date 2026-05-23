import { Suspense } from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { RootLayout } from "./layouts/RootLayout.tsx"
import { HomePage } from "./pages/HomePage.tsx";
import { PokedexPage } from "./pages/Pokedex.tsx";


function App() {
    const router = createBrowserRouter([
        {
            element: <RootLayout></RootLayout>,
            children: [
                {
                    index: true,
                    element: <HomePage />
                },
                {
                    path: "pokedex",
                    element: <PokedexPage />
                }
            ],
        }
    ]);


    return (
        <Suspense>
            <RouterProvider router={router}></RouterProvider>
        </Suspense>
    )
}

export default App;