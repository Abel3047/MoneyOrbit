import React from 'react';

const GoalCard = ({ title, description }) => {
  const cardStyle = {
    background: '#fff',
    borderRadius: '12px',
    boxShadow: '0 2px 8px rgba(0,0,0,0.06)',
    padding: '1.5rem',
    width: '300px',
    minHeight: '180px',
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    color: '#333',
  };

  return (
    <div style={cardStyle}>
      <h3 style={{ marginBottom: '1rem' }}>{title}</h3>
      <p>{description}</p>
    </div>
  );
};

export default GoalCard;
