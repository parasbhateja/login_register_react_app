import { useState } from "react";
import api from "../services/api";

export default function RegisterForm() {
    const [form, setForm] = useState({ name: "", email: "", password: "", confirmPassword: "" });

    const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (form.password !== form.confirmPassword) {
            alert("Passwords do not match!");
            return;
        }
        await api.post("/auth/register", {
            name: form.name,
            email: form.email,
            passwordHash: form.password
        });
        alert("User registered successfully!");
    };

    return (
        <form onSubmit={handleSubmit}>
            <input name="name" placeholder="Name" onChange={handleChange} required />
            <input name="email" type="email" placeholder="Email" onChange={handleChange} required />
            <input name="password" type="password" placeholder="Password" onChange={handleChange} required />
            <input name="confirmPassword" type="password" placeholder="Confirm Password" onChange={handleChange} required />
            <button type="submit">Register</button>
        </form>
    );
}
