import React from 'react';

const cardStyle = {
  background: '#4a546e',
  height: '150px',
  borderRadius: '8px',
  padding: '15px',
  color: '#bbb',
};

const innerItemStyle = {
  flex: 1,
  background: '#5d6880',
  borderRadius: '5px',
  padding: '15px',
};

const gridStyle = {
  display: 'grid',
  gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))',
  gap: '20px',
};

const DashboardContent = () => {
  const apps = ['App Card 1', 'App Card 2', 'App Card 3', 'App Card 4'];
  const installedApps = ['Installed App Card 1', 'Installed App Card 2', 'Installed App Card 3'];

  return (
    <div>
      <h1>Dashboard</h1>

      {/* Section 1: Main Dashboard Area */}
      <div style={{ ...cardStyle, minHeight: '200px', marginBottom: '30px' }}>
        <p>Your main dashboard content goes here.</p>
        <div style={{ display: 'flex', gap: '20px', marginTop: '20px' }}>
          {[1, 2, 3].map((n) => (
            <div key={n} style={innerItemStyle}>Item {n}</div>
          ))}
        </div>
      </div>

      {/* Section 2: Apps in your plan */}
      <h2>Apps in your plan</h2>
      <div style={{ ...gridStyle, marginBottom: '30px' }}>
        {apps.map((app, i) => (
          <div key={i} style={cardStyle}>{app}</div>
        ))}
      </div>

      {/* Section 3: Installed */}
      <h2>Installed</h2>
      <div style={gridStyle}>
        {installedApps.map((app, i) => (
          <div key={i} style={cardStyle}>{app}</div>
        ))}
      </div>
    </div>
  );
};

export default DashboardContent;
