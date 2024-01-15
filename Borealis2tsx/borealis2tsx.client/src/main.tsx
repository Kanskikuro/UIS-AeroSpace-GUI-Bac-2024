import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import AppContent from "./AppContent.tsx";

// TODO fix nav bar and side menu
ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <div>
          <AppContent />
      </div>
  </React.StrictMode>,
)
