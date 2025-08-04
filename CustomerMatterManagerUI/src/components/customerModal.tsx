// components/Modal.tsx
import React from 'react';

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  customerId: number;
}

const CustomerMatterModal: React.FC<ModalProps> = ({ isOpen, onClose, customerId }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 flex drop-shadow-md justify-center items-center">
      <div className="bg-white p-4 rounded shadow-lg">
        <h2 className="text-xl font-bold">Customer Matters</h2>
        <p>Id: {customerId}</p>
        <button
          className="mt-4 bg-red-500 text-white py-1 px-2 rounded"
          onClick={onClose}
        >
          Close
        </button>
      </div>
    </div>
  );
};

export default CustomerMatterModal;
