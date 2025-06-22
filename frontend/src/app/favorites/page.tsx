'use client';
import { useEffect, useState } from 'react';


interface Product {
  id: number;
  name: string;
  price: number;
  imageUrl: string;
}

export default function FavoritesPage() {
  const [favorites, setFavorites] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch('http://localhost:5070/api/wishlist/1')
      .then(res => res.json())
      .then(data => setFavorites(data.data || []))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <div>Yükleniyor...</div>;

  return (
    <div>
      <div className="max-w-3xl mx-auto mt-8 bg-white p-6 rounded shadow">
        <h2 className="text-xl font-bold mb-4 text-black">Favorilerim</h2>
        {favorites.length === 0 ? (
          <p className="text-black">Favori ürününüz yok.</p>
        ) : (
          <ul className="grid grid-cols-1 md:grid-cols-2 gap-4">
            {favorites.map(product => (
              <li key={product.id} className="border rounded p-4 flex flex-col items-center">
                <img src={product.imageUrl || 'https://via.placeholder.com/150'} alt={product.name} className="w-24 h-24 object-cover mb-2" />
                <span className="font-semibold">{product.name}</span>
                <span className="text-indigo-600">{product.price} ₺</span>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}