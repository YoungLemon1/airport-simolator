const GetFlightIcon = (stop, flights) => {
  const flight = flights.find((f) => f.stopId === stop.id);
  // eslint-disable-next-line no-lone-blocks
  if (flight) {
    switch (flight.fType) {
      case 0:
        return "🛬";
      case 1:
        return "🛫";
      default:
        return "";
    }
  }
};
const AirportDiagram = ({ stops, flights, GetFlightName }) => {
  stops = stops.filter((stop) => stop.id >= 1 && stop.id <= 8);
  return (
    <div id="airport-stops" className="grid-container">
      {stops.map((stop) => (
        <div className={"grid-item stop" + stop.id} key={stop.id}>
          <p>
            ({stop.id}) {stop.name}
          </p>
          <p>
            {GetFlightName(stop, flights)}
            {GetFlightIcon(stop, flights)}
          </p>
        </div>
      ))}
      <div className="grid-item junction">
        <p>Shared Track</p>
      </div>
      <div className="grid-item terminal">
        <p>Terminal</p>
      </div>
    </div>
  );
};

export default AirportDiagram;
