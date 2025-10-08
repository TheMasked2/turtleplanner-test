// Frontend communicates with backend API here!

const base = "http://turtlebase.duckdns.org";

// Types that match the backend
export type User = { id: number; email: string; name: string; role: "Admin" | "User" };

// LOGIN: backend expects { email, password } and returns the user
export async function login(email: string, password: string) {
  const response = await fetch(`${base}/api/employee/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    credentials: "include",
    body: JSON.stringify({ email, password })
  });

  // If unauthorized or other error, throw with backend message
  const data = await response.json().catch(() => ({}));
  if (!response.ok) {
    const msg = (data && (data.message || data.error)) || `HTTP ${response.status}`;
    throw new Error(msg);
  }

  // Optionally call /me to normalize (but login already returns the user)
  return data as User;
}

// SESSION: backend uses /api/employee/me and returns 401 when not logged in
export async function getSession() {
  const res = await fetch(`${base}/api/employee/me`, { credentials: "include" });

  if (res.status === 401) {
    return { isAuthenticated: false as const, role: undefined as unknown as "Admin" | "User" };
  }

  const user = (await res.json()) as User;
  return { isAuthenticated: true as const, role: user.role };
}

// LOGOUT: same endpoint name, POST
export async function logout() {
  await fetch(`${base}/api/employee/logout`, {
    method: "POST",
    credentials: "include"
  });
}
