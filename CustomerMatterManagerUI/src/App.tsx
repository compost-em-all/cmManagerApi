import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import DataTable from './components/dataTable';
import type { ColumnDef } from '@tanstack/react-table';
import type { CustomerDTO } from './models/customerDto';

const columns: ColumnDef<CustomerDTO>[] = [
  {
    accessorKey: 'customerId',
    header: 'ID',
    cell: info => info.getValue(),
  },
  {
    accessorKey: 'customerName',
    header: 'Name',
    cell: info => info.getValue(),
  },
  {
    accessorKey: 'phoneNum',
    header: 'Phone Number',
    cell: info => info.getValue(),
  },
];

function App() {
  const [count, setCount] = useState(0)
  const [data, setData] = useState<CustomerDTO[]>([]);

  const handleRowAction = (row: CustomerDTO) => {
    alert(`Open modal for ${row.name}`);
  };

  // get data from API
  useEffect(() => {
    const fetchData = async () => {
      console.log(`${import.meta.env.VITE_API_URL}`);

      const response = await fetch(`${import.meta.env.VITE_API_URL}/customer`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      console.log('Response status:', response);

    };

    fetchData();

    // fetch(`${import.meta.env.VITE_API_URL}/customers`)
    //   .then(response => response.json())
    //   .then(data => setData(data))
    //   .catch(error => console.error('Error fetching data:', error));
  }, []);

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <h1 className="text-3xl font-bold underline">
        Hello Vite + React + TailwindCSS!
      </h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
      <div className="mt-8">
        <DataTable data={data} columns={columns} onRowAction={handleRowAction} />
      </div>
    </>
  )
}

export default App
