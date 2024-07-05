const FlightTable = ({ flights }) => {
  const GetFormatedCreatedTime = (flight) => {
    return new Intl.DateTimeFormat("es", {
      dateStyle: "short",
      timeStyle: "short",
      hour12: false,
    }).format(new Date(flight.createdTime));
  };
  return (
    <table id="flights">
      <thead>
        <tr id="flights-headers">
          <th>Flight ID</th>
          <th>Flight Name</th>
          <th>Stop ID</th>
          <th>Flight Type</th>
          <th>Created time</th>
        </tr>
      </thead>
      <tbody>
        {flights
          .filter((f) => f.stopId)
          .map((flight) => (
            <tr key={flight.id}>
              <td>{flight.id}</td>
              <td>{flight.name}</td>
              <td>{flight.stopId ? flight.stopId : "STANDBY"}</td>
              <td>{flight.fType === 0 ? "Landing" : "Takeoff"}</td>
              <td>{GetFormatedCreatedTime(flight)}</td>
            </tr>
          ))}
      </tbody>
    </table>
  );
};
export default FlightTable;
