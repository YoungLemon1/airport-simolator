import { useState, useEffect } from "react";
import AirportDiagram from "./components/AirportDiagram";
import AirportStatus from "./components/AirportStatus";
import SimulatorControlContainer from "./components/SimulatorControlContainer";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

const base_api_url = "http://localhost:5140/api/Home";
const api_url = "http://localhost:5140/api/Home/Get/";

function App() {
  const [airport, setAirport] = useState();
  const [error, setError] = useState(null);

  useEffect(() => {
    setInterval(() => {
      fetch(api_url)
        .then((res) => res.json())
        .then((data) => {
          setAirport(data);
        })
        .catch((error) => setError(Error));
    }, 1000);
  }, []);

  if (!airport) return <h1>Loading...</h1>;
  if (error) return console.log(error);

  const GetFlightName = (stop, flights) => {
    const flightExsists = flights.some((f) => f.stopId === stop.id);
    if (flightExsists) return flights.find((f) => f.stopId === stop.id).name;
    else return "_";
  };

  return (
    <div className="App">
      <AirportDiagram
        stops={airport.stops}
        flights={airport.flights}
        GetFlightName={GetFlightName}
      />
      <AirportStatus
        stops={airport.stops}
        flights={airport.flights}
        GetFlightName={GetFlightName}
      />
      <SimulatorControlContainer url={base_api_url} />
    </div>
  );
}

export default App;
