import React, { useState } from 'react';
import './Trophy.css'; 

function Trophy() {
  const [showTrophy, setShowTrophy] = useState(false);

  return (
    <div className={`awards-container ${showTrophy ? 'blurred' : ''}`}>
      <button className="award-btn" onClick={() => setShowTrophy(true)}>Receive Award</button>

      {showTrophy && (
        <div className="modal-overlay">
          <div className="trophy-modal">
            <h2>Congratulations!</h2>
            <p>You’ve earned a trophy for completing your goal </p>
            <button className="close-btn" onClick={() => setShowTrophy(false)}>Close</button>
          </div>
        </div>
      )}
    </div>
  );
}

export default Trophy;
