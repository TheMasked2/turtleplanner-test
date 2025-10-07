import { Outlet } from "react-router-dom"

export function LoginLayout() {
    return (
        <div>
            <main>
                <Outlet></Outlet>
            </main>
        </div>
    )
}