import { Outlet, NavLink} from "react-router-dom"

export function MainLayout() {
    return(
        <div>
            <nav>
                <NavLink to="/">Go to login page</NavLink>
            </nav>
            <main>
                <Outlet></Outlet>
            </main>        
        </div> 
    )
}