// Header.js
import React from 'react';

function Header() {
  return (
    <div style={{ background: '#36405c', color: 'white', padding: '15px 20px', display: 'flex', justifyContent: 'flex-end', alignItems: 'center', borderBottom: '1px solid #4a546e' }}>
      {/* Icons - Placeholder for actual icon components */}
      <span style={{ margin: '0 10px', cursor: 'pointer' }}>&#9993;</span> {/* Mail icon */}
      <span style={{ margin: '0 10px', cursor: 'pointer' }}>&#128276;</span> {/* Bell icon */}
      <img src="https://via.placeholder.com/40" alt="User Avatar" style={{ borderRadius: '50%', marginLeft: '15px', cursor: 'pointer' }} />
    </div>
  );
}

export default Header;