import React, { useState } from 'react';
import { 
    FiHome, FiRepeat, FiAward, FiBell, FiPocket, FiSettings, 
    FiChevronDown, FiChevronUp, FiMessageSquare, FiPlus, FiSun, FiMoon, 
    FiMoreHorizontal, FiArrowLeft, FiUser
} from 'react-icons/fi';
import { FaRocket } from 'react-icons/fa';
import './Dashboard.css';

// --- Sub-components ---

// Circular Progress Bar for Goal Cards
const CircularProgress = ({ percentage, color }) => {
    const radius = 50;
    const circumference = 2 * Math.PI * radius;
    const offset = circumference - (percentage / 100) * circumference;

    return (
        <svg width="120" height="120" viewBox="0 0 120 120" className="circular-progress">
            <circle
                className="progress-background"
                strokeWidth="10"
                r={radius}
                cx="60"
                cy="60"
            />
            <circle
                className="progress-bar"
                strokeWidth="10"
                strokeDasharray={circumference}
                strokeDashoffset={offset}
                stroke={color}
                r={radius}
                cx="60"
                cy="60"
            />
            <text x="60" y="65" className="progress-text">{percentage}%</text>
        </svg>
    );
};

// Goal Card Component
const GoalCard = ({ title, emoji, percentage, color, text }) => (
    <div className="goal-card glass-card">
        <div className="goal-card-header">
            <h3>{title} {emoji}</h3>
        </div>
        <CircularProgress percentage={percentage} color={color} />
        <p className="goal-card-text">{text}</p>
        <FiMoreHorizontal className="more-icon" />
    </div>
);


// --- Main Dashboard Component ---

const Dashboard = () => {
    const [isOrbitPocketOpen, setOrbitPocketOpen] = useState(true);
    const [isLinkedAccountsOpen, setLinkedAccountsOpen] = useState(true);
    const [selectedStatus, setSelectedStatus] = useState(null);

    const rawGoals = [
  { title: "Liam's School fees", emoji: '🏆', percentage: 80, color: '#34d399' },
  { title: "House Topup", emoji: '💡', percentage: 59, color: '#f59e0b' },
  { title: "Black Tax", emoji: '👥', percentage: 10, color: '#ef4444' }
];
// goal color and quote changer
    const getGoalStatus = (percentage) => {
        if (percentage === 100) return 'Done!';
        if (percentage >= 80) return 'Doing Great';
        if (percentage >= 50) return 'Doing Okay';
        if (percentage <= 20) return 'Needs Attention';
    return 'attention';
  };

    const getMotivationalText = (status) => {
    switch (status) {
        case 'Done!':
        return "Excellent Job";
      case 'Doing Great':
        return "You're orbiting closer every day";
      case 'Doing Okay':
        return "You're doing well — keep it up";
      case 'Needs Attention':
        return "Small steps still move you forward";
      default:
        return "It’s never too late to realign";
    }
  };

  const getStatusColor = (status) => {
  switch (status) {
    case 'Done!': 
        return ' #0000FF';  //blue    
    case 'Doing Great': 
        return '#00FF00';     //green
    case 'Doing Okay': 
        return '#FFA500';      //orange
    case 'Needs Attention': 
        return '#FF0000';      //red
    default: 
        return '#6b7280';          
  }
};

  const goals = rawGoals.map(goal => {
  const status = getGoalStatus(goal.percentage);
  const color = getStatusColor(status);
  return {
    ...goal,
    status,
    color,
    text: getMotivationalText(status),
  };
});

const filteredGoals = selectedStatus
  ? goals.filter(goal => goal.status === selectedStatus)
  : goals;

    return (
        <div className="dashboard-container">
            {/* Background decorative shapes */}
            <div className="shape-1"></div>
            <div className="shape-2"></div>

            {/* Sidebar */}
            <aside className="sidebar">
                <div className="sidebar-header">
                    <div className="logo">
                        <svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M16 32C24.8366 32 32 24.8366 32 16C32 7.16344 24.8366 0 16 0C7.16344 0 0 7.16344 0 16C0 24.8366 7.16344 32 16 32Z" fill="url(#paint0_linear_401_2)"/>
                            <path d="M16.1147 19.3332C19.1673 19.3332 21.644 16.8565 21.644 13.8039C21.644 10.7513 19.1673 8.27466 16.1147 8.27466C13.062 8.27466 10.5854 10.7513 10.5854 13.8039C10.5854 15.1118 11.0827 16.3101 11.9015 17.2033C11.9015 17.2033 11.8467 17.1485 11.8467 17.1485L16.1147 23.7253L20.3553 17.1759C20.3553 17.1759 20.3279 17.2033 20.3279 17.2033C20.9193 16.3375 21.644 15.1392 21.644 13.8039" fill="white" stroke="#00AEEF" strokeWidth="1.5"/>
                            <defs>
                            <linearGradient id="paint0_linear_401_2" x1="0" y1="0" x2="32" y2="32" gradientUnits="userSpaceOnUse">
                            <stop stopColor="#00AEFF"/>
                            <stop offset="1" stopColor="#0072FF"/>
                            </linearGradient>
                            </defs>
                        </svg>
                    </div>
                </div>

                <nav className="sidebar-nav">
                    <ul>
                        <li className="nav-item active"><FiHome /> <span>Dashboard</span></li>
                        <li className="nav-item"><FiRepeat /> <span>Transactions</span></li>
                        <li className="nav-item">
                            <FiAward /> <span>Badges & Awards</span>
                            <span className="badge purple">4</span>
                        </li>
                        <li className="nav-item">
                            <FiBell /> <span>Notifications</span>
                            <span className="badge blue"><FiPlus size={10}/> 8</span>
                        </li>
                        
                        <li className="nav-item collapsible">
                            <div className="collapsible-header" onClick={() => setOrbitPocketOpen(!isOrbitPocketOpen)}>
                                <div><FiPocket /> <span>Orbit pocket</span></div>
                                {isOrbitPocketOpen ? <FiChevronUp /> : <FiChevronDown />}
                            </div>
                            {isOrbitPocketOpen && (
                                <ul className="sub-nav">
                                    <li className="sub-nav-item">Earning</li>
                                    <li className="sub-nav-item active">
                                        <div className="collapsible-header" onClick={() => setLinkedAccountsOpen(!isLinkedAccountsOpen)}>
                                            <span>Linked Accounts</span>
                                            {isLinkedAccountsOpen ? <FiChevronUp /> : <FiChevronDown />}
                                        </div>
                                        {isLinkedAccountsOpen && (
                                            <ul className="sub-sub-nav">
                                                <li>Manage Banking Connections</li>
                                                <li>Add or Remove accounts</li>
                                            </ul>
                                        )}
                                    </li>
                                </ul>
                            )}
                        </li>
                        
                        <li className="nav-item collapsible">
                             <div className="collapsible-header">
                                <div><FiUser/> <span>Financial Consultant</span></div>
                                <FiChevronDown />
                            </div>
                             <span className="nav-item-subtitle">(Optional Premium)</span>
                        </li>
                    </ul>
                </nav>

                <div className="upload-section">
                    <div className="upload-box">
                        <button className="upload-btn"><FiPlus size={24} /></button>
                        <span>Upload new image</span>
                        <p>Drag and drop</p>
                    </div>
                </div>

                <div className="theme-toggle">
                    <button className="toggle-btn active"><FiSun /> Light</button>
                    <button className="toggle-btn"><FiMoon /> Dark</button>
                </div>
            </aside>

            {/* Main Content */}
            <main className="main-content">
                <header className="main-header">
                    <FiArrowLeft className="header-icon" />
                    <h1>Start Orbiting <FaRocket className="rocket-icon" /></h1>
                    <div className="header-actions">
                        <FiMessageSquare className="header-icon" />
                        <div className="notification-icon-wrapper">
                            <FiBell className="header-icon" />
                            <div className="notification-dot"></div>
                        </div>
                        <img src="https://i.pravatar.cc/40?img=32" alt="User Avatar" className="user-avatar" />
                    </div>
                </header>

                <section className="progress-overview glass-card">
                    <div className="progress-info">
                        <h2>Orbit Pocket Progress</h2>
                        <p className="progress-description">You are on track to achieve your financial goals.</p>
                        <div className="progress-stats">
                            <div className="stat-item">
                                <span className="stat-label">Total Savings</span>
                                <span className="stat-value">P5000</span>
                            </div>
                            <div className="stat-item">
                                <span className="stat-label">Amount Saved</span>
                                <span className="stat-value">P3500</span>
                            </div>
                            <div className="stat-item">
                                <span className="stat-label">Goal Completion</span>
                                <span className="stat-value">70%</span>
                            </div>
                        </div>
                    </div>  
                    <div className="progress-bar-container">
                        <h2>You are 70% closer to achieving your goals. 🎉</h2>
                        <p className="month-label">July</p>
                        <div className="main-progress-bar-container">
                            <div className="main-progress-bar" style={{ width: '70%' }}>
                                <span className="progress-label">70%</span>
                            </div>
                        </div>
                        <div className="progress-markers">
                            <span>P0</span>
                            <span>P3500</span>
                            <span>P5000</span>
                        </div>
                    </div>
                    <div className="savings-summary">
                        <p>Total Savings: <strong>P5000</strong></p>
                        <p>Amount Saved: <strong>P3500</strong></p>
                    </div>
                </section>
                <section className="goals-tracker">
                    <div className="goals-header">
                        <h2>Quarterly Goals Tracker</h2>
                        <div className="goals-filters">
                            <span onClick={() => setSelectedStatus('Done')}>Congratulations</span>
                            <span onClick={() => setSelectedStatus('Doing Great')}>Doing Great</span>
                            <span onClick={() => setSelectedStatus('Doing Okay')}>May Need Help</span>
                            <span onClick={() => setSelectedStatus('Needs Attention')}>Needs Attention</span>
                            <span onClick={() => setSelectedStatus(null)} style={{ color: 'gray' }}>Show All</span>
                        </div>

                    </div>
                    <div className="goals-grid">
                        {filteredGoals.map(goal => (
                            <GoalCard key={goal.title} {...goal} />
                        ))}
                    </div> 
                </section>
                
                <button className="fab">
                    <FiPlus size={24}/>
                    <span className="fab-text">Add Goal</span>
                </button>
            </main>
        </div>
    );
};

export default Dashboard;