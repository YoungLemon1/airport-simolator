import { useState } from "react";

const SimulatorControlContainer = ({ url }) => {
  const runSimulatorUrl = url + "/Start";
  const stopSimulatorUrl = url + "/Stop";

  const [startBtnDisabled, setStartBtnDisabled] = useState(false);
  const [stopBtnDisabled, setStopBtnDisabled] = useState(true);
  return (
    <div id="start-button-container">
      <button
        id="start-button"
        className="menu-btn"
        onClick={() => {
          setStartBtnDisabled(true);
          setStopBtnDisabled(false);
          fetch(runSimulatorUrl);
        }}
        disabled={startBtnDisabled}
      >
        Start Flight Generation
      </button>
      <button
        id="stop-button"
        className="menu-btn"
        onClick={() => {
          setStartBtnDisabled(false);
          setStopBtnDisabled(true);
          fetch(stopSimulatorUrl);
        }}
        disabled={stopBtnDisabled}
      >
        Stop Flight Generation
      </button>
    </div>
  );
};
export default SimulatorControlContainer;
