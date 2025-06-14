import React from 'react';
import GoalCard from '../components/GoalCard';

const GoalsPage = () => {
    // Example data
    const goals = [
        { title: 'Buy a Car', description: 'Save $10,000 for a new car by 2025.' },
        { title: 'Vacation Fund', description: 'Save $3,000 for a trip to Japan.' },
        { title: 'Emergency Fund', description: 'Build a $5,000 emergency fund.' },
        { title: 'Home Renovation', description: 'Save $8,000 for kitchen remodel.' },
    ];

const mainStyle = {
  display: 'grid',
  gridTemplateColumns: 'repeat(auto-fit, minmax(250px, 1fr))',
  gap: '20px',
  padding: '20px',
};

    return (
        <div>
           
            <main style={mainStyle}>
                {goals.map((goal, idx) => (
                    <GoalCard key={idx} title={goal.title} description={goal.description} />
                ))}
            </main>
        </div>
    );
};

export default GoalsPage;