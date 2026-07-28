// src/App.jsx
// This is the root component. Right now it just renders the Dashboard,
// but as the project grows this is where you'd add routing, a login
// screen, multiple dashboard views, etc.

import Dashboard from "./Dashboard";

export default function App() {

    return (

        <div className="min-h-screen bg-slate-950">

            <Dashboard/>

        </div>

    );

}
