import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { useAppSelector } from '../store/hooks';
import './App.css';

function App() {

  const dispatch = useDispatch();
  const [postId, setPostID] = useState("");
  const{currentTab}=useAppSelector((state)=>state.toggle)
  console.log(currentTab);
  return (
    <div className="App">
      <header className="App-header">
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
 