import FlightTable from "./FlightsTable";
import StopsTable from "./StopsTable";

const AirportStatus = ({ stops, flights, GetFlightName }) => {
  return (
    <div id="airport-status">
      <StopsTable
        stops={stops}
        flights={flights}
        GetStopFlightName={GetFlightName}
      />
      <FlightTable flights={flights} />
    </div>
  );
};
export default AirportStatus;
