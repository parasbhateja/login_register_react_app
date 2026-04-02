import { useState } from "react";
import api from "../services/api";

export default function LoginForm() {
    const [form, setForm] = useState({ email: "", password: "" });

    const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const res = await api.post("/auth/login", {
                email: form.email,
                passwordHash: form.password
            });
            localStorage.setItem("token", res.data.token);
            alert("Login successful!");
            window.location.href = "/dashboard";
        } catch {
            alert("Invalid login credentials");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input name="email" type="email" placeholder="Email" onChange={handleChange} required />
            <input name="password" type="password" placeholder="Password" onChange={handleChange} required />
            <button type="submit">Login</button>
        </form>
    );
}
