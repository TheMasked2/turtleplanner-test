import { useState, type FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../lib/api";
import "./login.css";
import logoUrl from "./../assets/images/Turtle-planner-logo.png";

export default function Login() {
  const nav = useNavigate();
  const [email, setEmail] = useState("");        // was username
  const [password, setPassword] = useState("");
  const [err, setErr] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  async function onSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setErr(null);

    if (!email || !password) {
      setErr("Please fill both fields.");
      return;
    }

    setBusy(true);
    try {
      const user = await login(email.trim(), password); // returns {id,email,name,role}
      nav(user.role === "Admin" ? "/dashboard" : "/app", { replace: true });
    } catch (err: any) {
      setErr(err?.message || "Login failed.");
    } finally {
      setBusy(false);
    }
  }

  return (
    <div className="container">
      <section className="login-screen">
        <div className="row login-nav">
          <img className="logo" src={logoUrl} alt="Company Logo" />
          <h2>Turtle planner</h2>
        </div>

        <div className="row login-hero">
          <div className="login-box">
            <div className="titlebox">
              <h2>Welcome back</h2>
            </div>

            <form onSubmit={onSubmit}>
              <div className="form-group">
                <label htmlFor="email">Email</label>
                <input id="email" value={email} onChange={(e) => setEmail(e.target.value)} />
              </div>

              <div className="form-group">
                <label htmlFor="password">Password</label>
                <input id="password" type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
              </div>

              {err && <div className="error" role="alert">{err}</div>}

              <div className="button-group">
                <button type="submit" disabled={busy}>
                  {busy ? "Logging in..." : "Login"}
                </button>
              </div>
            </form>
          </div>
        </div>

        <div className="row login-footer" />
      </section>
    </div>
  );
}
