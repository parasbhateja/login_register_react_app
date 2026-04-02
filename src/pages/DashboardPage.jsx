import { useEffect, useState } from "react";
import api from "../services/api";

export default function DashboardPage() {
    const [user, setUser] = useState(null);

    useEffect(() => {
        api.get("/user/profile").then(res => setUser(res.data));
    }, []);

    const logout = () => {
        localStorage.removeItem("token");
        window.location.href = "/login";
    };

    return (
        <div>
            {user ? <h2>Welcome, {user.name}!</h2> : <p>Loading...</p>}
            <button onClick={logout}>Logout</button>
        </div>
    );
}
