// src/App.jsx

import { useState, useEffect } from "react";
import Dashboard from "./Dashboard";

export default function App() {

    const [theme, setTheme] = useState(
        localStorage.getItem("theme") || "dark"
    );


    useEffect(() => {

        if(theme === "dark"){
            document.documentElement.classList.add("dark");
        }
        else{
            document.documentElement.classList.remove("dark");
        }

        localStorage.setItem("theme", theme);

    }, [theme]);


    return (
        <Dashboard
            theme={theme}
            setTheme={setTheme}
        />
    );

}