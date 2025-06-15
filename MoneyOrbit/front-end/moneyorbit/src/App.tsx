import { BrowserRouter, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage'; // Import the LoginPage component
import Dashboard from './Components/Dashboard/Dashboard'; // Import the new Dashboard component
import './Components/Dashboard/Dashboard.css'; // Import the CSS for the dashboard
import TransactionsPage from './Components/TransactionsPage/TransactionsPage'; // Import the TransactionsPage component
import OnboardingPage from './pages/OnboardingPage';

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
        <Route path="/TransactionsPage" element={<TransactionsPage darkMode={undefined} />} />
        <Route path="/onboarding" element={<OnboardingPage />} /> {/* <-- Add the route */}
        <Route path="/dashboard" element={
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
