import React from "react"
import ReactDOM from "react-dom/client"
import { createBrowserRouter, RouterProvider} from "react-router-dom"

import Login from "./routes/login"
import './styles/global.css'
import RequireAuth from "./routes/RequireAuth";
import AppHome from "./routes/index";        // your user home
import Dashboard from "./routes/dashboard.tsx";  // your admin page

// Creating a router connection.
const router = createBrowserRouter([
  { path: "/login", element: <Login /> },
  { path: "/app", element: <RequireAuth><AppHome /></RequireAuth> },
  { path: "/dashboard", element: <RequireAuth role="Admin"><Dashboard /></RequireAuth> },
  { path: "/", element: <RequireAuth><AppHome /></RequireAuth> },
]);


// Connecting the router to the app.
ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
);