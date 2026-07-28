# KPI dashboard — React client (Vite)

This is a standalone React app, separate from the C# backend. It talks to
the backend only over the network (via SignalR) — it doesn't need to sit
inside the same Visual Studio solution.

## What each file does (since you're new to React)

```
dashboard-client/
├── index.html          the page Vite actually serves — has one <div id="root">
├── vite.config.js       dev server settings (e.g. which port: 3000)
├── package.json         lists dependencies + the "dev"/"build" commands
└── src/
    ├── main.jsx          bootstraps React, mounts <App /> into #root
    ├── App.jsx           the root component — currently just renders Dashboard
    └── Dashboard.jsx      the actual dashboard UI (cards, chart, table)
```

You'll spend almost all your time in `Dashboard.jsx`. `main.jsx` and
`App.jsx` rarely need changes — they're just plumbing.

## Running it

You need Node.js installed (18+). Then, from this folder:

```
npm install
npm run dev
```

This starts a dev server, usually at `http://localhost:3000`. Open that
in a browser and you'll see the dashboard — it'll show "Connecting to
KPI service…" until your C# backend (from the other project) is also
running.

## Connecting to the backend

In `src/Dashboard.jsx`, this line points at your C# SignalR hub:
```js
const HUB_URL = 'http://localhost:5000/hubs/MRTdashCombine';
```
Update the host/port if your backend runs somewhere else.

## Building for the TV

`npm run dev` is for development only (auto-reloads on save, but isn't
optimized). For the actual kiosk display, build a static production
version:

```
npm run build
```

This creates a `dist/` folder with plain HTML/CSS/JS — no Node server
needed to serve it. You can host `dist/` with any static file server
(IIS, `serve`, nginx, or even open `dist/index.html` directly for testing).
Point `launch-dashboard.bat`'s `DASHBOARD_URL` at wherever you end up
hosting it.

## Relationship to the C# project

Keep them as two separate things you run side by side:
- C# Worker Service (`SmartMRTDashService_Combine`) — polls SQL, pushes over SignalR. No UI.
- This React app — pure frontend, connects to the SignalR hub, renders the UI.

You can open both folders in Visual Studio (File → Open → Folder) to see
them side by side without merging them into one project — VS will just
treat the React folder as a plain folder rather than trying to compile it.
