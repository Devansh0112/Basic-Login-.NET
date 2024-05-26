import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Line } from 'react-chartjs-2';

const Dashboard = () => {
  const [chartData, setChartData] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      const result = await axios.get('http://localhost:5099/api/data/validation');
      setChartData(result.data);
    };
    fetchData();
  }, []);

  return (
    <div>
      <h2>Validation Dashboard</h2>
      <Line data={chartData} />
    </div>
  );
};

export default Dashboard;
