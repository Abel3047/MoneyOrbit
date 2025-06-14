// App.js
import React from 'react';
import Sidebar from './components/Sidebar';
import Header from './components/Header';
import DashboardContent from './components/DashboardContent';

function App() {
  return (
    <div style={{ display: 'flex', height: '100vh', backgroundColor: '#f0f2f5' }}> {/* Main container */}
      <Sidebar />
      <div style={{ flexGrow: 1, display: 'flex', flexDirection: 'column' }}>
        <Header />
        <div style={{ flexGrow: 1, padding: '20px', overflowY: 'auto' }}>
          <DashboardContent />
        </div>
      </div>
      {/* You would typically control the visibility of UserMenu with state in a parent component */}
      {/* <UserMenu /> */}
    </div>
  );
}

export default App;


