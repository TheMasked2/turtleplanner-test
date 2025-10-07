// src/routes/RequireAuth.tsx
import { useEffect, useState, type ReactNode } from "react";
import { Navigate } from "react-router-dom";
import { getSession } from "../lib/api";

export default function RequireAuth({
  children,
  role,
}: {
  children: ReactNode;            // more flexible than JSX.Element
  role?: "Admin" | "User";
}) {
  const [ok, setOk] = useState<null | boolean>(null);

  useEffect(() => {
    getSession()
      .then((s) => setOk(s.isAuthenticated && (!role || s.role === role)))
      .catch(() => setOk(false));
  }, [role]);

  if (ok === null) return null;   // or a spinner
  return ok ? <>{children}</> : <Navigate to="/login" replace />;
}
