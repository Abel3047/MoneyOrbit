import React, { useState } from 'react';
import './TransactionsPage.css'; // We will create this CSS file next
import profilePic from '../TransactionsPage/Assets/money-orbit-logo.png'; // Importing a sample profile image

// Importing icons from react-icons
import {
  FiHome, FiUsers, FiAward, FiBell, FiSettings, FiChevronsLeft, FiChevronUp,
  FiChevronDown, FiPlus, FiSun, FiMoon, FiMessageSquare, FiUser, FiHeart, FiSend, FiLogOut
} from 'react-icons/fi';
import { BsReceipt } from 'react-icons/bs';
import { GiOrbit } from 'react-icons/gi';

// You can replace this with your actual profile image
 // Make sure to add a profile.jpg to your src folder or replace it

const TransactionsPage = () => {
  const [isSidebarCollapsed, setSidebarCollapsed] = useState(false);
  const [isProfileMenuOpen, setProfileMenuOpen] = useState(true); // Open by default as in the image
  const [theme, setTheme] = useState('light'); // 'light' or 'dark'

  const toggleTheme = () => {
    setTheme(theme === 'light' ? 'dark' : 'light');
  };

  return (
    <div className={`transactions-page ${theme}`}>
      {/* Sidebar */}
      <aside className={`sidebar ${isSidebarCollapsed ? 'collapsed' : ''}`}>
        <div className="sidebar-header">
          <button className="collapse-btn" onClick={() => setSidebarCollapsed(!isSidebarCollapsed)}>
            <FiChevronsLeft />
          </button>
        </div>

        <nav className="sidebar-nav">
          <a href="#" className="nav-item"><FiHome /><span>Dashboard</span></a>
          <a href="#" className="nav-item active"><BsReceipt /><span>Transactions</span></a>
          <a href="#" className="nav-item"><FiAward /><span>Badges & Awards</span><span className="badge purple">4</span></a>
          <a href="#" className="nav-item"><FiBell /><span>Notifications</span><span className="badge blue">8</span></a>

          <div className="nav-item collapsible open">
            <div className="collapsible-header">
              <GiOrbit /><span>Orbit pocket</span><FiChevronUp />
            </div>
            <div className="collapsible-content">
              <a href="#">Earning</a>
              <div className="sub-collapsible">
                <div className="collapsible-header">
                  <span>Linked Accounts</span><FiChevronDown />
                </div>
                {/* Sub-content would go here */}
              </div>
              <a href="#">Manage Banking Connections</a>
              <a href="#">Add or Remove accounts</a>
            </div>
          </div>

          <div className="nav-item collapsible">
            <div className="collapsible-header">
              <FiSettings /><span>Financial Consultant</span><FiChevronDown />
            </div>
          </div>
        </nav>

        <div className="sidebar-footer">
          <div className="upload-widget">
            <div className="upload-icon"><FiPlus /></div>
            <p>Upload new image</p>
            <span>Drag and drop</span>
          </div>

          <div className="theme-switcher">
            <button className={`theme-btn ${theme === 'light' ? 'active' : ''}`} onClick={() => setTheme('light')}>
              <FiSun /> Light
            </button>
            <button className={`theme-btn ${theme === 'dark' ? 'active' : ''}`} onClick={() => setTheme('dark')}>
              <FiMoon /> Dark
            </button>
          </div>
        </div>
      </aside>

      {/* Main Content */}
      <main className="main-content">
        <header className="main-header">
          <h1>Transactions</h1>
          <div className="header-actions">
            <FiMessageSquare className="header-icon" />
            <FiBell className="header-icon" />
            <div className="profile-menu-container">
              <img
                src={profilePic}
                alt="Profile"
                className="profile-pic"
                onClick={() => setProfileMenuOpen(!isProfileMenuOpen)}
              />
              {isProfileMenuOpen && (
                <div className="profile-dropdown glass-morphism">
                  <ul>
                    <li><FiSettings /><span>Profile Settings</span></li>
                    <li><FiUser /><span>View Profile</span></li>
                    <li className="divider"><FiUsers /><span>Explore Creators</span></li>
                    <li><FiHeart /><span>Manage Membership</span></li>
                    <li><FiSend /><span>Invite Creators</span></li>
                    <li className="divider"><FiLogOut /><span>Logout</span></li>
                  </ul>
                </div>
              )}
            </div>
          </div>
        </header>

        <section className="transactions-overview">
          <div className="uncategorised-transactions glass-morphism">
            <p className="section-subtitle">Uncategorised Transactions</p>
            <div className="transaction-card">
              <div className="card-content">
                <p className="card-title">Clothing purchase</p>
                <span className="card-date">02/03/25</span>
              </div>
              <div className="progress-bar">
                <div className="progress" style={{ width: '80%' }}></div>
              </div>
            </div>
          </div>

          return (
  <div className={`transactions-page ${theme}`}>
    {/* Sidebar */}
    <aside className={`sidebar ${isSidebarCollapsed ? 'collapsed' : ''}`}>
      <div className="sidebar-header">
        
        {/* ADD THIS LOGO ELEMENT */}
        <img src={profilePic} alt="MoneyOrbit Logo" className="sidebar-logo" />

        {/* This button was already here */}
        <button className="collapse-btn" onClick={() => setSidebarCollapsed(!isSidebarCollapsed)}>
          <FiChevronsLeft />
        </button>
      </div>

      {/* ... rest of the sidebar and main content ... */}
    </aside>

    {/* ... rest of your component */}
  </div>
);

          <div className="recent-transactions">
            <h2 className="section-title">Recent Transactions</h2>
            <div className="transaction-list">
              <div className="transaction-item glass-morphism">
                <div className="item-icon"></div>
                <span className="item-date">Date: 06/06/25</span>
                <span className="item-description">Description: Markham shopping spree</span>
                <div className="item-amount-placeholder"></div>
              </div>
              <div className="transaction-item glass-morphism">
                <div className="item-icon"></div>
                <div className="item-text-placeholder short"></div>
                <div className="item-text-placeholder long"></div>
              </div>
               <div className="transaction-item glass-morphism">
                <div className="item-icon"></div>
                <div className="item-text-placeholder short"></div>
                <div className="item-text-placeholder long"></div>
              </div>
            </div>
          </div>
        </section>
      </main>
    </div>
  );
};

export default TransactionsPage;