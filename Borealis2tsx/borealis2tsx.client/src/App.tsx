import LiveData from "./Components/LiveData/LiveData.tsx";
import NavBar from "./NavBar.tsx";

// This should be the body of the app
function App() {
    return(
        <div className={'h-screen overflow-y-auto'}>
            <NavBar/>
            <LiveData/>
        </div>
    )
}

export default App;