import logo from './logo.svg';
import './App.css';

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

const GoalCard = ({ title, description }) => (
    <div style={goalcardStyle}>
        <h3 style={{ marginBottom: '1rem' }}>{title}</h3>
        <p>{description}</p>
    </div>
);
function App() {
  return (
    <div className="h-full w-64 bg-gradient-to-b from-[#001f3f] to-[#004466] text-white p-6 flex flex-col justify-between">
      <div className="App">
       <Sidebar />
       </div>
    <div className="App">
       <Sidebar />
        
        
    </div>
    </div>
    
  );
}

export default App;
