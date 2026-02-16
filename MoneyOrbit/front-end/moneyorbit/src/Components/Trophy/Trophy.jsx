import React from 'react';
import './Trophy.css';

// We create the trophy icon as a separate component for cleanliness
const TrophyIcon = () => (
  <svg width="80" height="80" viewBox="0 0 80 80" fill="none" xmlns="http://www.w3.org/2000/svg">
    <path d="M40 0L48.9805 26.6811L64.4533 16.4891L57.9401 35.5467L78.5109 38.0109L57.9401 40.4533L64.4533 59.5109L48.9805 49.3189L40 76L31.0195 49.3189L15.5467 59.5109L22.0599 40.4533L1.48912 38.0109L22.0599 35.5467L15.5467 16.4891L31.0195 26.6811L40 0Z" fill="url(#paint0_linear_101_2)"/>
    <path d="M28 62L28 78L52 78V62L40 54L28 62Z" fill="#22a297"/>
    <defs>
      <linearGradient id="paint0_linear_101_2" x1="40" y1="0" x2="40" y2="76" gradientUnits="userSpaceOnUse">
        <stop stopColor="#FFD700"/>
        <stop offset="1" stopColor="#FFA500"/>
      </linearGradient>
    </defs>
  </svg>
);


const Trophy = ({ isOpen, onClose }) => {
  // If the modal isn't open, render nothing.
  if (!isOpen) {
    return null;
  }

  return (
    <div className="backdrop">
      <div className="modalCard">
        <TrophyIcon />
        <h2 className="title">Goal Completed!</h2>
        <p className="message">
          Motivation Message about keeping it up!
        </p>
        <button className="continueButton" onClick={onClose}>
          Continue
        </button>
      </div>
    </div>
  );
};

export default Trophy;