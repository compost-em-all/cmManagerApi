import { useEffect, useState } from 'react'
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
    accessorKey: 'name',
    header: 'Name',
    cell: info => info.getValue(),
  },
  {
    accessorKey: 'phoneNum',
    header: 'Phone Number',
    cell: info => info.getValue(),
  },
  {
    header: 'Actions',
    accessorKey: 'actions',
    cell: ({ row }) => (
      <button
        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded"
        onClick={() => alert(`Open modal for ${row.original.name}`)}
      >
        Open Modal
      </button>
    ),
  }
];

function App() {
  const [data, setData] = useState<CustomerDTO[]>([]);

  const handleRowAction = (row: CustomerDTO) => {
    alert(`Open modal for ${row.name}`);
  };

  // get data from API
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${import.meta.env.VITE_API_URL}/customer`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${import.meta.env.VITE_API_TOKEN}`, // Ensure you have set this in your .env file
          },
        });

        if (!response.ok) {
          throw new Error(`Response status: ${response.status}`);
        }

        const data = await response.json();
        setData(data);

      } catch (error) {
        console.error('Error fetching data:', error);
      }
      console.log(`${import.meta.env.VITE_API_URL}`);
    };

    fetchData();

    // fetch(`${import.meta.env.VITE_API_URL}/customers`)
    //   .then(response => response.json())
    //   .then(data => setData(data))
    //   .catch(error => console.error('Error fetching data:', error));
  }, []);

  return (
      <div className="flex flex-col h-screen">
        <header className="bg-gray-500 text-white p-4">
          <nav>
            {/* <ul className="flex space-x-4">
              <li>
                <a href="#" className="text-white hover:underline">Home</a>
              </li>
            </ul> */}
          </nav>
          <h1 className="text-white text-lg font-bold">Customer Matter Manager</h1>
        </header>
        <main className='flex-grow p-4'>
          <h2 className="text-2xl font-semibold mb-4">Customers</h2>
          <DataTable data={data} columns={columns} onRowAction={handleRowAction} />
        </main>
      </div>
  )
}

export default App
