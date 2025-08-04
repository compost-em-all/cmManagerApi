import { useEffect, useState } from 'react'
import './App.css'
import DataTable from './components/dataTable';
import type { ColumnDef } from '@tanstack/react-table';
import type { CustomerDTO } from './models/customerDto';
import CustomerMatterModal from './components/customerModal';
import type { UserLoginDto, UserSignUpDTO } from './models/userDto';

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
  // Auth state
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [showSignUp, setShowSignUp] = useState(false);
  const [loginForm, setLoginForm] = useState<UserLoginDto>({ email: '', password: '' });
  const [signUpForm, setSignUpForm] = useState<UserSignUpDTO>({ email: '', password: '', firstName: '', lastName: '', firmName: '' });
  const [authError, setAuthError] = useState<string | null>(null);
  
  // Customer state
  const [data, setData] = useState<CustomerDTO[]>([]);
  const [newCustomer, setNewCustomer] = useState({ name: '', phoneNum: '' });
  const [customerError, setCustomerError] = useState<string | null>(null);
  
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedCustomerId, setSelectedCustomerId] = useState<number>(0);

  const handleRowAction = (row: CustomerDTO) => {
    console.log('Row action clicked:', row);
    setSelectedCustomerId(row.customerId);
    setIsModalOpen(true);
  };

  // Handle login
  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setAuthError(null);
    try {
      const response = await fetch(`${import.meta.env.VITE_API_URL}/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(loginForm),
      });
      if (!response.ok) throw new Error('Login failed');
      localStorage.setItem('token', (await response.json()).token);
      setIsLoggedIn(true);
    } catch (err: any) {
      setAuthError(err.message);
    }
  };

  // Handle sign up
  const handleSignUp = async (e: React.FormEvent) => {
    e.preventDefault();
    setAuthError(null);
    try {
      console.log('signUpForm', signUpForm);
      const response = await fetch(`${import.meta.env.VITE_API_URL}/auth/signup`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(signUpForm),
      });
      if (!response.ok) throw new Error('Sign up failed');
      setIsLoggedIn(true);
    } catch (err: any) {
      setAuthError(err.message);
    }
  };

  // Fetch customers
  useEffect(() => {
    if (!isLoggedIn) return;
    const fetchData = async () => {
      try {
        const response = await fetch(`${import.meta.env.VITE_API_URL}/customer`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('token')}`,
          },
        });
        if (!response.ok) throw new Error(`Response status: ${response.status}`);
        const data = await response.json();
        setData(data);
      } catch (error) {
        setCustomerError('Error fetching customers');
      }
    };
    fetchData();
  }, [isLoggedIn]);

// Auth UI
  if (!isLoggedIn) {
    return (
      <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
        <div className="bg-white p-8 rounded shadow-md w-full max-w-md">
          <h2 className="text-2xl font-bold mb-4">{showSignUp ? 'Sign Up' : 'Login'}</h2>
          {authError && <div className="text-red-500 mb-2">{authError}</div>}
          {showSignUp ? (
            <form onSubmit={handleSignUp} className="space-y-3">
              <input className="w-full border p-2 rounded" placeholder="Email" type="email" required value={signUpForm.email} onChange={e => setSignUpForm(f => ({ ...f, email: e.target.value }))} />
              <input className="w-full border p-2 rounded" placeholder="First Name" required value={signUpForm.firstName} onChange={e => setSignUpForm(f => ({ ...f, firstName: e.target.value }))} />
              <input className="w-full border p-2 rounded" placeholder="Last Name" required value={signUpForm.lastName} onChange={e => setSignUpForm(f => ({ ...f, lastName: e.target.value }))} />
              <input className="w-full border p-2 rounded" placeholder="Firm Name" required value={signUpForm.firmName} onChange={e => setSignUpForm(f => ({ ...f, firmName: e.target.value }))} />
              <input className="w-full border p-2 rounded" placeholder="Password" type="password" required value={signUpForm.password} onChange={e => setSignUpForm(f => ({ ...f, password: e.target.value }))} />
              <button className="w-full bg-blue-500 text-white py-2 rounded" type="submit">Sign Up</button>
            </form>
          ) : (
            <form onSubmit={handleLogin} className="space-y-3">
              <input className="w-full border p-2 rounded" placeholder="Email" type="email" required value={loginForm.email} onChange={e => setLoginForm(f => ({ ...f, email: e.target.value }))} />
              <input className="w-full border p-2 rounded" placeholder="Password" type="password" required value={loginForm.password} onChange={e => setLoginForm(f => ({ ...f, password: e.target.value }))} />
              <button className="w-full bg-blue-500 text-white py-2 rounded" type="submit">Login</button>
            </form>
          )}
          <button className="mt-4 text-blue-500 underline" onClick={() => setShowSignUp(s => !s)}>
            {showSignUp ? 'Already have an account? Login' : "Don't have an account? Sign Up"}
          </button>
        </div>
      </div>
    );
  }

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
