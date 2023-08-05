const StopsTable = ({ stops, flights, GetStopFlightName }) => {
  stops = stops.filter((stop) => stop.id >= 1 && stop.id <= 8);
  return (
    <table id="stops">
      <thead>
        <tr>
          <th>Stop ID</th>
          <th>Stop Name</th>
          <th>Flight Name</th>
        </tr>
      </thead>
      <tbody>
        {stops.map((stop) => (
          <tr key={stop.id}>
            <td>{stop.id}</td>
            <td>{stop.name}</td>
            <td>{GetStopFlightName(stop, flights)}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};
export default StopsTable;
