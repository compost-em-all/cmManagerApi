// components/Modal.tsx
import React from 'react';

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  customerId: number;
}

const CustomerMatterModal: React.FC<ModalProps & { children?: React.ReactNode }> = ({ isOpen, onClose, customerId, children }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 flex drop-shadow-md justify-center items-center z-50">
      <div className="bg-white p-4 rounded shadow-lg min-w-[350px] max-w-lg w-full">
        <h2 className="text-xl font-bold mb-2">Customer Matters</h2>
        <p className="mb-2">Id: {customerId}</p>
        {children}
        <button
          className="mt-4 bg-red-500 text-white py-1 px-2 rounded w-full"
          onClick={onClose}
        >
          Close
        </button>
      </div>
    </div>
  );
};

export default CustomerMatterModal;
