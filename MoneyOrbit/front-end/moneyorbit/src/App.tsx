import { BrowserRouter, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage'; // Import the LoginPage component
import Dashboard from './Components/Dashboard/Dashboard'; // Import the new Dashboard component
import './Components/Dashboard/Dashboard.css'; // Import the CSS for the dashboard

function App() {
  return (
    // BrowserRouter wraps your entire application to enable routing
    <BrowserRouter>
      {/* 
        The <Routes> component is a container for all your individual routes.
        It will only render the first <Route> that matches the current URL.
      */}
      <Routes>
        {/* Route for the login page. We'll make it the default page ('/') */}
        <Route path="/" element={<LoginPage />} />
        <Route path="/login" element={<LoginPage />} />

        {/* Route for the dashboard page */}

        <Route
          path="/dashboard"
          element={
            <div className="app-background">
              <Dashboard />
            </div>
          }
        />

      </Routes>
    </BrowserRouter>
  );
}

export default App;
