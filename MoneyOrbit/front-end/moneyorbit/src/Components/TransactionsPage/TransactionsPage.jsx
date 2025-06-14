import {
  Box,
  Grid,
  Paper,
  Typography,
  LinearProgress,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  useTheme,
} from '@mui/material';
import React, { useState } from 'react';
import { 
    FiHome, FiRepeat, FiAward, FiBell, FiPocket, FiSettings, 
    FiChevronDown, FiChevronUp, FiMessageSquare, FiPlus, FiSun, FiMoon, 
    FiMoreHorizontal, FiArrowLeft, FiUser
} from 'react-icons/fi';
import { Doughnut } from 'react-chartjs-2';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
//import TransactionsPage from './TransactionsPage'; // Assuming this is the correct path to your TransactionsPage component
import './TransactionsPage.css'; // Import the CSS for the transactions page
// Register Chart.js components
ChartJS.register(ArcElement, Tooltip, Legend);

const getGlassmorphismStyles = (isDarkMode) => ({
  background: isDarkMode 
    ? 'linear-gradient(135deg, rgba(0, 0, 0, 0.3), rgba(50, 50, 50, 0.2))'
    : 'linear-gradient(135deg, rgba(255, 255, 255, 0.3), rgba(240, 240, 240, 0.2))',
  backgroundColor: isDarkMode ? 'rgba(0, 0, 0, 0.3)' : 'rgba(255, 255, 255, 0.3)',
  color: isDarkMode ? '#fff' : '#000',
  backdropFilter: 'blur(20px) saturate(200%)',
  WebkitBackdropFilter: 'blur(20px) saturate(200%)',
  border: isDarkMode 
    ? '1px solid rgba(255, 255, 255, 0.1)' 
    : '1px solid rgba(255, 255, 255, 0.3)',
  borderRadius: '16px',
  boxShadow: isDarkMode 
    ? '0 4px 30px rgba(0, 0, 0, 0.5)' 
    : '0 4px 30px rgba(0, 0, 0, 0.1)',
  padding: '1rem',
});


  const TransactionsPage = () => {
  const [isOrbitPocketOpen, setOrbitPocketOpen] = useState(false);
  const [isLinkedAccountsOpen, setLinkedAccountsOpen] = useState(false);
  const darkMode = false; // Set this based on your app's theme state
  return (
    <Box sx={{ display: 'flex' }}>
        <TransactionsContent darkMode={darkMode} />   
    </Box>
  );
};
const TransactionsContent = ({ darkMode }) => {
  const theme = useTheme();
  const [isOrbitPocketOpen, setOrbitPocketOpen] = useState(false);
  const [isLinkedAccountsOpen, setLinkedAccountsOpen] = useState(false);
  // Sample data for uncategorized transactions
  const uncategorizedTransactions = [
    { description: 'Clothing purchase', date: '02/03/20', progress: 70 },
    { description: 'Groceries', date: '05/03/20', progress: 40 },
    { description: 'Online course subscription', date: '10/03/20', progress: 60 },
    { description: 'Restaurant bill', date: '15/03/20', progress: 80 },
    { description: 'Gym membership', date: '20/03/20', progress: 50 },
    { description: 'Utility bill', date: '25/03/20', progress: 30 },                              

    ];
  const spendingData = [
    { category: 'Food & Dining', amount: 350, percentage: 35, color: '#F44336' },
    { category: 'Shopping', amount: 200, percentage: 20, color: '#2196F3' },
    { category: 'Transport', amount: 150, percentage: 15, color: '#FFC107' },
    { category: 'Utilities', amount: 100, percentage: 10, color: '#4CAF50' },
    { category: 'Entertainment', amount: 100, percentage: 10, color: '#9C27B0' },
    { category: 'Other', amount: 100, percentage: 10, color: '#795548' },
  ];


  const totalMonthlySpending = spendingData.reduce((sum, item) => sum + item.amount, 0);

  const chartDataSpending = {
    labels: spendingData.map(item => item.category),
    datasets: [
      {
        data: spendingData.map(item => item.amount),
        backgroundColor: spendingData.map(item => item.color),
        borderColor: spendingData.map(item => item.color),
        borderWidth: 1,
      },
    ],
  };

  const chartOptionsSpending = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        position: 'right',
        labels: {
          color: theme.palette.text.primary,
        }
      },
      tooltip: {
        callbacks: {
          label: function(context) {
            let label = context.label || '';
            if (label) {
              label += ': ';
            }
            if (context.parsed !== null) {
              label += `P${context.parsed}`;
            }
            return label;
          }
        }
      }
    },
  };

  const spendingTrendData = [
    { month: 'Jan', amount: 750 },
    { month: 'Feb', amount: 800 },
    { month: 'Mar', amount: 650 },
    { month: 'Apr', amount: 900 },
    { month: 'May', amount: 850 },
    { month: 'Jun', amount: 1000 },
  ];

  const topMerchantsData = [
    { name: 'Amazon', amount: 250 },
    { name: 'Grocery Store X', amount: 180 },
    { name: 'Restaurant Y', amount: 120 },
    { name: 'Online Courses Inc.', amount: 100 },
    { name: 'Public Transport', amount: 90 },
  ];


  return (
    <Box
      component="main"
      sx={{
        flexGrow: 1,
        p: 3,
        width: { sm: `calc(100% - 240px)` }, // drawerWidth is 240px, hardcoded for self-contained component
        minHeight: '100vh',
        backgroundColor: theme.palette.mode === 'dark' ? '#0a0a0a' : '#e8eaf6',
        backgroundImage: theme.palette.mode === 'dark' ? 'linear-gradient(135deg, #0f0f0f 0%, #1a1a1a 100%)' : 'linear-gradient(135deg, #e0e7fa 0%, #c5cae9 100%)',
        transition: theme.transitions.create('background-color', {
          duration: theme.transitions.duration.shortest,
        }),
      }}
    >
      {/* Spacer for the app bar */}
      <Box sx={{ height: 64 }} /> {/* Assuming AppBar height is around 64px */}

      <Typography variant="h4" sx={{ mb: 4, fontWeight: 'bold', color: theme.palette.text.primary }}>
        Transactions
      </Typography>

      <Grid container spacing={4}>
        {/* Uncategorized Transactions Section */}
        <Grid item xs={12} md={6}>
          <Box sx={{ mb: 4 }}>
            <Typography variant="h6" sx={{ mb: 2, color: theme.palette.text.primary }}>Uncategorised Transactions</Typography>
            <Paper sx={{ p: 3, ...getGlassmorphismStyles(darkMode) }}>
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <Box sx={{ flexGrow: 1 }}>
                  <Typography variant="subtitle1" sx={{ fontWeight: 'bold', color: theme.palette.text.primary }}>Clothing purchase</Typography>
                  <Typography variant="caption" sx={{ color: theme.palette.text.secondary }}>02/03/20</Typography>
                </Box>
                <LinearProgress
                  variant="determinate"
                  value={70} // Example value
                  sx={{
                    width: '60%',
                    height: 10,
                    borderRadius: 5,
                    backgroundColor: 'rgba(255,255,255,0.3)',
                    '& .MuiLinearProgress-bar': {
                      backgroundColor: '#4CAF50', // Green for progress
                    },
                  }}
                />
              </Box>
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <Box sx={{ flexGrow: 1 }}>
                  <Typography variant="subtitle1" sx={{ fontWeight: 'bold', color: theme.palette.text.primary }}>Groceries</Typography>
                  <Typography variant="caption" sx={{ color: theme.palette.text.secondary }}>05/03/20</Typography>
                </Box>
                <LinearProgress
                  variant="determinate"
                  value={40} // Example value
                  sx={{
                    width: '60%',
                    height: 10,
                    borderRadius: 5,
                    backgroundColor: 'rgba(255,255,255,0.3)',
                    '& .MuiLinearProgress-bar': {
                      backgroundColor: '#FFC107', // Amber for progress
                    },
                  }}
                />
              </Box>
            </Paper>
          </Box>
        </Grid>

        {/* Monthly Spending Summary Widget */}
        <Grid item xs={12} md={6}>
          <Box sx={{ mb: 4 }}>
            <Typography variant="h6" sx={{ mb: 2, color: theme.palette.text.primary }}>Monthly Spending Summary</Typography>
            <Paper sx={{ p: 3, textAlign: 'center', ...getGlassmorphismStyles(darkMode) }}>
              <Typography variant="h5" sx={{ fontWeight: 'bold', color: theme.palette.text.primary, mb: 1 }}>
                P{totalMonthlySpending}
              </Typography>
              <Typography variant="body2" sx={{ color: theme.palette.text.secondary, mb: 2 }}>
                Total spending this month
              </Typography>
              <Box sx={{ display: 'flex', justifyContent: 'space-around', mt: 2 }}>
                <Box>
                  <Typography variant="body2" sx={{ fontWeight: 'bold', color: theme.palette.text.primary }}>Previous Month</Typography>
                  <Typography variant="body1" sx={{ color: theme.palette.text.secondary }}>P950</Typography>
                </Box>
                <Box>
                  <Typography variant="body2" sx={{ fontWeight: 'bold', color: theme.palette.text.primary }}>Average</Typography>
                  <Typography variant="body1" sx={{ color: theme.palette.text.secondary }}>P800</Typography>
                </Box>
              </Box>
            </Paper>
          </Box>
        </Grid>

        {/* Spending Categories Widget */}
        <Grid item xs={12} md={6}>
          <Box sx={{ mb: 4 }}>
            <Typography variant="h6" sx={{ mb: 2, color: theme.palette.text.primary }}>Spending Categories</Typography>
            <Paper sx={{ p: 3, ...getGlassmorphismStyles(darkMode) }}>
              <Box sx={{ height: 200, mb: 2 }}>
                <Doughnut data={chartDataSpending} options={chartOptionsSpending} />
              </Box>
              <List dense>
                {spendingData.map((item, index) => (
                  <ListItem key={index} disablePadding>
                    <ListItemIcon sx={{ minWidth: 'auto', mr: 1 }}>
                      <Box sx={{ width: 12, height: 12, borderRadius: '50%', backgroundColor: item.color }} />
                    </ListItemIcon>
                    <ListItemText
                      primary={<Typography variant="body2" sx={{ color: theme.palette.text.primary }}>{item.category}</Typography>}
                      secondary={<Typography variant="body2" sx={{ color: theme.palette.text.secondary }}>P{item.amount} ({item.percentage}%)</Typography>}
                    />
                  </ListItem>
                ))}
              </List>
            </Paper>
          </Box>
        </Grid>


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

        {/* Spending Trend (Last 6 Months) Widget */}
        <Grid item xs={12} md={6}>
          <Box sx={{ mb: 4 }}>
            <Typography variant="h6" sx={{ mb: 2, color: theme.palette.text.primary }}>Spending Trend (Last 6 Months)</Typography>
            <Paper sx={{ p: 3, ...getGlassmorphismStyles(darkMode) }}>
              <Grid container spacing={1} alignItems="flex-end">
                {spendingTrendData.map((data, index) => (
                  <Grid item xs key={index} sx={{ textAlign: 'center' }}>
                    <Box
                      sx={{
                        height: `${data.amount / 10}%`, // Scale bar height
                        maxHeight: 100, // Max height for bars
                        width: '80%',
                        backgroundColor: theme.palette.primary.main,
                        borderRadius: '4px 4px 0 0',
                        margin: '0 auto',
                        transition: 'height 0.3s ease-in-out',
                      }}
                    />
                    <Typography variant="caption" sx={{ color: theme.palette.text.secondary, mt: 0.5 }}>
                      {data.month}
                    </Typography>
                    <Typography variant="caption" sx={{ color: theme.palette.text.primary, fontWeight: 'bold' }}>
                      P{data.amount}
                    </Typography>
                  </Grid>
                ))}
              </Grid>
            </Paper>
          </Box>
        </Grid>

        {/* Top Merchants/Categories Widget */}
        <Grid item xs={12} md={6}>
          <Box sx={{ mb: 4 }}>
            <Typography variant="h6" sx={{ mb: 2, color: theme.palette.text.primary }}>Top Merchants/Categories</Typography>
            <Paper sx={{ p: 3, ...getGlassmorphismStyles(darkMode) }}>
              <List dense>
                {topMerchantsData.map((item, index) => (
                  <ListItem key={index} disablePadding sx={{ mb: 1 }}>
                    <ListItemText
                      primary={<Typography variant="body1" sx={{ color: theme.palette.text.primary, fontWeight: 'bold' }}>{item.name}</Typography>}
                      secondary={<Typography variant="body2" sx={{ color: theme.palette.text.secondary }}>P{item.amount}</Typography>}
                    />
                  </ListItem>
                ))}
              </List>
            </Paper>
          </Box>
        </Grid>

        {/* Recent Transactions Section (Existing) */}
        <Grid item xs={12} md={6}>
          <Box>
            <Typography variant="h6" sx={{ mb: 2, color: theme.palette.text.primary }}>Recent Transactions</Typography>
            <Paper sx={{ p: 3, ...getGlassmorphismStyles(darkMode) }}>
              <Grid container spacing={2} sx={{ mb: 1, fontWeight: 'bold', color: theme.palette.text.secondary }}>
                <Grid item xs={3}><Typography variant="body2">Date</Typography></Grid>
                <Grid item xs={9}><Typography variant="body2">Description</Typography></Grid>
              </Grid>
              <hr style={{ border: `0.5px solid ${theme.palette.divider}`, marginBottom: theme.spacing(2) }} />

              {/* Example Transaction 1 */}
              <Grid container spacing={2} alignItems="center" sx={{ mb: 2 }}>
                <Grid item xs={3}><Typography variant="body1" sx={{ color: theme.palette.text.primary }}>06/06/25</Typography></Grid>
                <Grid item xs={9}>
                  <Typography variant="body1" sx={{ color: theme.palette.text.primary }}>Markham shopping spree</Typography>
                </Grid>
              </Grid>

              {/* Example Transaction 2 */}
              <Grid container spacing={2} alignItems="center" sx={{ mb: 2 }}>
                <Grid item xs={3}><Typography variant="body1" sx={{ color: theme.palette.text.primary }}>05/06/25</Typography></Grid>
                <Grid item xs={9}>
                  <Typography variant="body1" sx={{ color: theme.palette.text.primary }}>Dinner with friends</Typography>
                </Grid>
              </Grid>

              {/* Example Transaction 3 */}
              <Grid container spacing={2} alignItems="center" sx={{ mb: 2 }}>
                <Grid item xs={3}><Typography variant="body1" sx={{ color: theme.palette.text.primary }}>04/06/25</Typography></Grid>
                <Grid item xs={9}>
                  <Typography variant="body1" sx={{ color: theme.palette.text.primary }}>Online course subscription</Typography>
                </Grid>
              </Grid>
            </Paper>
          </Box>
        </Grid>
      </Grid>
    </Box>
  );
};

export default TransactionsContent;
