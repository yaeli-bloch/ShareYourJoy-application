import { useEffect, useState } from 'react'

import './App.css'
import Uploader from './components/upload'
import Login from './components/Login';

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem('authToken');
    if (token) {
      setIsAuthenticated(true);  // אם יש טוקן ב-localStorage, המשתמש מחובר
    }
  }, []);

  const handleLoginSuccess = () => {
    setIsAuthenticated(true);  // עדכון אם הלוגין מצליח
  };

  return (
    <>
      <div>hellooooooooooo</div>
      <Uploader/>
      <div>
      {isAuthenticated ? (
        <div>
          <h1>ברוך הבא!</h1>
          <button onClick={() => localStorage.removeItem('authToken')}>התנתק</button>
        </div>
      ) : (
        <Login onLoginSuccess={handleLoginSuccess} />
      )}
    </div>
    </>
  )
}

export default App
