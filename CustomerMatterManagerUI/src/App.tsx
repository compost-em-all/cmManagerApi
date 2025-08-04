import { useEffect, useState } from 'react'
import './App.css'
import DataTable from './components/dataTable';
import type { ColumnDef } from '@tanstack/react-table';
import type { CustomerDTO } from './models/customerDto';
import CustomerMatterModal from './components/customerModal';

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
  }
];

function App() {
  const [data, setData] = useState<CustomerDTO[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedCustomerId, setSelectedCustomerId] = useState<number>(0);

  const handleRowAction = (row: CustomerDTO) => {
    console.log('Row action clicked:', row);
    setSelectedCustomerId(row.customerId);
    setIsModalOpen(true);
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
        <header className="bg-blue-500 text-white p-4">
          <nav>
          </nav>
          <h1 className="text-white text-lg font-bold">Customer Matter Manager</h1>
        </header>
        <main className='flex-grow p-4'>
          <h2 className="text-2xl font-semibold mb-4">Customers</h2>
          <DataTable data={data} columns={columns} onRowAction={handleRowAction} />
        </main>
        <CustomerMatterModal
          isOpen={isModalOpen}
          onClose={() => setIsModalOpen(false)}
          customerId={selectedCustomerId}>
        </CustomerMatterModal>
      </div>
  )
}

export default App
