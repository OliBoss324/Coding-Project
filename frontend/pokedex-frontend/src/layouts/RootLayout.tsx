import { NavLink, Outlet } from "react-router-dom";


const navLinkClassName = ({ isActive }: { isActive: boolean }) =>
    isActive
        ? "font-semibold text-blue-600"
        : "text-gray-600 hover:text-blue-600";




export function RootLayout() {
    return (
        <div className="h-screen flex flex-col overflow-hidden text-zinc-100">
            <header className=" shrink-0 border-b border-gray-200 px-4 py-2">
                <nav className="flex gap-4 justify-center">
                    <NavLink
                        to="/"
                        className={navLinkClassName}
                    > Home
                    </NavLink>
                    <NavLink
                        to="/pokedex"
                        className={navLinkClassName}
                    > Pokedex
                    </NavLink>
                </nav>
            </header>
            <main className="flex-1 overflow-hidden">
                <Outlet />
            </main>
        </div>
    );
}