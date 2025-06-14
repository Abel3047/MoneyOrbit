import './App.css';
import Goals from './pages/GoalsPage'; 

const sidebarStyle = {
  width: '240px',
  background: '#f5f6fa',
  height: '100vh',
  padding: '2rem 1rem',
  boxSizing: 'border-box',
  borderRight: '1px solid #e1e1e1',
  position: 'fixed',
  left: 0,
  top: 0,
};

const mainStyle = {
  marginLeft: '260px',
  padding: '2rem',
  minHeight: '100vh',
  background: '#fafbfc',
  display: 'flex',
  flexWrap: 'wrap',
  gap: '2rem',
};

const goalcardStyle = {
  background: '#fff',
  borderRadius: '12px',
  boxShadow: '0 2px 8px rgba(0,0,0,0.06)',
  padding: '1.5rem',
  width: '300px',
  minHeight: '180px',
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'center',
};

const Sidebar = () => (
  <aside style={sidebarStyle}>
    <h2 style={{ marginBottom: '2rem' }}>Goals</h2>
    <nav>
      <ul style={{ listStyle: 'none', padding: 0 }}>
        <li style={{ marginBottom: '1rem' }}><a href="#">All Goals</a></li>
        <li style={{ marginBottom: '1rem' }}><a href="#">Active</a></li>
        <li style={{ marginBottom: '1rem' }}><a href="#">Completed</a></li>
        <li><a href="#">Add Goal</a></li>
      </ul>
    </nav>
  </aside>
);

const goals = [
  { title: 'Buy a Car', description: 'Save $10,000 for a new car by 2025.' },
  { title: 'Vacation Fund', description: 'Save $3,000 for a trip to Japan.' },
  { title: 'Emergency Fund', description: 'Build a $5,000 emergency fund.' },
  { title: 'Home Renovation', description: 'Save $8,000 for kitchen remodel.' },
];

function App() {
  return (
    <div className="App" style={{ display: 'flex' }}>
      <Sidebar />
      <main style={mainStyle}>
        <Goals goals={goals} />
      </main>
    </div>
  );
}

export default App;
