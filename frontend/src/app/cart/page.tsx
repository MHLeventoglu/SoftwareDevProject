'use client';
import { useEffect, useState } from 'react';


interface CartItem {
  id: number;
  productId: number;
  quantity: number;
  product: {
    name: string;
    price: number;
    imageUrl: string;
  };
}

export default function CartPage() {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch('http://localhost:5070/api/cart/getbyuserid/1') // userId'i dinamik yapabilirsin
      .then(res => res.json())
      .then(data => setCartItems(data.data.items || []))
      .finally(() => setLoading(false));
  }, []);

  const removeFromCart = async (itemId: number) => {
    await fetch(`http://localhost:5070/api/cartitem/delete/${itemId}`, { method: 'DELETE' });
    setCartItems(items => items.filter(i => i.id !== itemId));
  };

  if (loading) return <div>Yükleniyor...</div>;

  return (
    <div>
      <div className="max-w-3xl mx-auto mt-8 bg-white p-6 rounded shadow">
        <h2 className="text-xl font-bold mb-4 text-black">Sepetim</h2>
        {cartItems.length === 0 ? (
          <p className="text-black">Sepetiniz boş.</p>
        ) : (
          <ul>
            {cartItems.map(item => (
              <li key={item.id} className="flex justify-between items-center border-b py-2">
                <div>
                  <span className="font-semibold text-black">{item.product.name}</span>
                  <span className="ml-2 text-black">{item.quantity} adet</span>
                </div>
                <div className="flex items-center gap-4">
                  <span className="text-black">{(item.product.price * item.quantity).toFixed(2)} ₺</span>
                  <button onClick={() => removeFromCart(item.id)} className="text-red-500">Sil</button>
                </div>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}