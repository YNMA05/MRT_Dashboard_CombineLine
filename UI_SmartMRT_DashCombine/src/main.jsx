// src/main.jsx
// Standard Vite/React bootstrap — this is the file that actually
// starts your app. You won't need to touch this often.

import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App.jsx';
import './index.css'

ReactDOM.createRoot(document.getElementById('root')).render(
  //<React.StrictMode>
    <App />
  //</React.StrictMode>
  
);

