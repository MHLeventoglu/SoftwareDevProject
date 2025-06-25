'use client';
import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';


interface Product {
  id: number;
  name: string;
  price: number;
  imageUrl: string;
  categoryId: number;
  brandId: number;
  description: string;
  category?: string; 
  brand?: string; 
}

interface Category {
  id: number;
  name: string;
}

interface Brand {
  id: number;
  name: string;
}

export default function HomePage() {
  const router = useRouter();
  const [products, setProducts] = useState<Product[]>([]);
  const [filteredProducts, setFilteredProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [brands, setBrands] = useState<Brand[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedCategories, setSelectedCategories] = useState<number[]>([]);
  const [selectedBrands, setSelectedBrands] = useState<number[]>([]);
  const [sortOption, setSortOption] = useState<string>('featured');
  const [favorites, setFavorites] = useState<number[]>([]);
  const [showMobileFilters, setShowMobileFilters] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  // API'den veri çekme
  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        setError('');
        
        // Paralel istekler için Promise.all
        const [productsRes, categoriesRes, brandsRes] = await Promise.all([
          fetch('http://localhost:5070/api/product/getall'),
          fetch('http://localhost:5070/api/category/getall'),
          fetch('http://localhost:5070/api/brand/getall')
        ]);

        // HTTP hata kontrolü
        if (!productsRes.ok) throw new Error('Ürünler alınamadı');
        if (!categoriesRes.ok) throw new Error('Kategoriler alınamadı');
        if (!brandsRes.ok) throw new Error('Markalar alınamadı');

        // JSON verilerini işleme
        const productsData = await productsRes.json();
        const categoriesData = await categoriesRes.json();
        const brandsData = await brandsRes.json();

        const productsWithNames = productsData.data.map((product: Product) => ({
          ...product,
          category: categoriesData.data.find((c: Category) => c.id === product.categoryId)?.name || '',
          brand: brandsData.data.find((b: Brand) => b.id === product.brandId)?.name || ''
        }));        

        // State güncelleme
        setProducts(productsData.data);
        setFilteredProducts(productsData.data);
        setCategories(categoriesData.data);
        setBrands(brandsData.data);

      } catch (err) {
        console.error('API Hatası:', err);
        setError('Veriler yüklenirken hata oluştu. Lütfen tekrar deneyin.');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  // Filtreleme ve sıralama
  useEffect(() => {
    let result = [...products];

    // Arama filtresi
   if (searchTerm) {
      result = result.filter(product =>
        product.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
        product.description.toLowerCase().includes(searchTerm.toLowerCase()))
      }

    // Kategori filtresi
    if (selectedCategories.length > 0) {
      result = result.filter(product =>
        selectedCategories.includes(product.categoryId))
    }

    // Marka filtresi
    if (selectedBrands.length > 0) {
      result = result.filter(product =>
        selectedBrands.includes(product.brandId))
    }

    // Sıralama
    switch (sortOption) {
      case 'price-low':
        result.sort((a, b) => a.price - b.price);
        break;
      case 'price-high':
        result.sort((a, b) => b.price - a.price);
        break;
      default:
        break;
    }

    setFilteredProducts(result);
  }, [searchTerm, selectedCategories, selectedBrands, sortOption, products]);

  // Yardımcı fonksiyonlar
  const toggleCategory = (categoryId: number) => {
    setSelectedCategories(prev =>
      prev.includes(categoryId)
        ? prev.filter(id => id !== categoryId)
        : [...prev, categoryId]
    );
  };

  const toggleBrand = (brandId: number) => {
    setSelectedBrands(prev =>
      prev.includes(brandId)
        ? prev.filter(id => id !== brandId)
        : [...prev, brandId]
    );
  };

  const toggleFavorite = (productId: number) => {
    setFavorites(prev =>
      prev.includes(productId)
        ? prev.filter(id => id !== productId)
        : [...prev, productId]
    );
  };

  const addToCart = async (productId: number) => {
    try {
      const response = await fetch('http://localhost:5070/api/cart/add', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ productId, quantity: 1 })
      });

      if (!response.ok) throw new Error('Sepete eklenemedi');

      alert('Ürün sepete eklendi!');
    } catch (err) {
      console.error('Sepet Hatası:', err);
      alert('Sepete ekleme başarısız oldu');
    }
  };

 const addToFavorites = async (productId: number) => {
    await fetch('http://localhost:5070/api/wishlist/add', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ userId: 1, productId })
    });
  };


  const clearFilters = () => {
    setSelectedCategories([]);
    setSelectedBrands([]);
    setSearchTerm('');
    setSortOption('featured');
  };

    const navigateToFavorites = () => {
    router.push('/favorites');
  };

  const navigateToCart = () => {
    router.push('/cart');
  };

  // Yükleme ve hata durumları
  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-xl">Yükleniyor...</div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-red-500 text-xl">{error}</div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <header className="bg-white shadow-sm py-4 px-6">
        <div className="max-w-7xl mx-auto flex justify-between items-center">
          <h1 className="text-2xl font-bold text-indigo-600">RoboSales</h1>
          <div className="relative w-1/3">
            <input
              type="text"
              placeholder="Ürün ara..."
              className="w-full py-2 px-4 rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-indigo-500"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </div>
          <div className="flex items-center space-x-4">
            <button 
              onClick={() => router.push('/favorites')}
              className="p-2 relative text-black hover:text-indigo-600 transition-colors"
            >
              Favoriler ({favorites.length})
            </button>
            <button 
              onClick={() => router.push('/cart')}
              className="p-2 relative text-black hover:text-indigo-600 transition-colors"
            >
              Sepet (0)
            </button>
          </div>
        </div>
      </header>

      <main className="max-w-7xl mx-auto py-6 px-4">
        <div className="flex flex-col md:flex-row gap-6">
          {/* Sidebar - Filters */}
          <aside
            className={`${showMobileFilters ? 'block' : 'hidden'} md:block w-full md:w-64 bg-white p-4 rounded-lg shadow-sm h-fit sticky top-4`}
          >
            <div className="flex justify-between items-center mb-4">
              <h2 className="text-lg font-semibold text-black">Filtreler</h2>
              <button
                onClick={clearFilters}
                className="text-sm text-indigo-600 hover:underline"
              >
                Temizle
              </button>
            </div>

            <div className="mb-6">
              <h3 className="font-medium mb-2 flex items-center justify-between text-black">
                <span>Kategoriler</span>
                {selectedCategories.length > 0 && (
                  <span className="text-xs bg-indigo-100 text-indigo-800 px-2 py-1 rounded-full">
                    {selectedCategories.length}
                  </span>
                )}
              </h3>
              <div className="max-h-60 overflow-y-auto">
                {categories.length > 0 ? (
                  categories.map(cat => (
                    <div key={cat.id} className="flex items-center py-1">
                      <input
                        type="checkbox"
                        id={`cat-${cat.id}`}
                        checked={selectedCategories.includes(cat.id)}
                        onChange={() => toggleCategory(cat.id)}
                        className="h-4 w-4 text-indigo-600 rounded focus:ring-indigo-500"
                      />
                      <label 
                        htmlFor={`cat-${cat.id}`} 
                        className="ml-2 text-sm text-black cursor-pointer whitespace-nowrap"
                      >
                        {cat.name}
                      </label>
                    </div>
                  ))
                ) : (
                  <p className="text-sm text-gray-500">Kategori bulunamadı</p>
                )}
              </div>
            </div>

            <div>
              <h3 className="font-medium mb-2 flex items-center justify-between text-black">
                <span>Markalar</span>
                {selectedBrands.length > 0 && (
                  <span className="text-xs bg-indigo-100 text-indigo-800 px-2 py-1 rounded-full">
                    {selectedBrands.length}
                  </span>
                )}
              </h3>
              <div className="max-h-60 overflow-y-auto">
                {brands.length > 0 ? (
                  brands.map(brand => (
                    <div key={brand.id} className="flex items-center py-1">
                      <input
                        type="checkbox"
                        id={`brand-${brand.id}`}
                        checked={selectedBrands.includes(brand.id)}
                        onChange={() => toggleBrand(brand.id)}
                        className="h-4 w-4 text-indigo-600 rounded focus:ring-indigo-500"
                      />
                      <label 
                        htmlFor={`brand-${brand.id}`} 
                        className="ml-2 text-sm text-black cursor-pointer whitespace-nowrap"
                      >
                        {brand.name}
                      </label>
                    </div>
                  ))
                ) : (
                  <p className="text-sm text-gray-500">Marka bulunamadı</p>
                )}
              </div>
            </div>
          </aside>

          {/* Main Content */}
          <section className="flex-1">
            {/* Sorting and results info */}
            <div className="bg-white p-4 rounded-lg shadow-sm mb-4 flex flex-col sm:flex-row justify-between items-center">
              <div className="mb-2 sm:mb-0">
                <span className="text-sm text-gray-600">
                  {filteredProducts.length} ürün bulundu
                </span>
              </div>
              <div className="flex items-center space-x-2">
                <span className="text-sm text-gray-600">Sırala:</span>
                <select
                  value={sortOption}
                  onChange={(e) => setSortOption(e.target.value)}
                  className="border rounded-lg px-3 py-1 text-sm focus:outline-none focus:ring-1 focus:ring-indigo-500"
                >
                  <option value="featured">Öne Çıkanlar</option>
                  <option value="price-low">Fiyat: Düşükten Yükseğe</option>
                  <option value="price-high">Fiyat: Yüksekten Düşüğe</option>
                </select>
              </div>
            </div>

            {/* Product Grid */}
            {filteredProducts.length === 0 ? (
              <div className="bg-white rounded-lg shadow-sm p-8 text-center">
                <p className="text-gray-500">Aradığınız kriterlere uygun ürün bulunamadı.</p>
                <button
                  onClick={clearFilters}
                  className="mt-4 text-indigo-600 hover:underline"
                >
                  Filtreleri temizle
                </button>
              </div>
            ) : (
              <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                {filteredProducts.map(product => (
                  <div
                    key={product.id}
                    className="bg-white border rounded-lg overflow-hidden shadow-sm hover:shadow-md transition-shadow flex flex-col"
                  >
                    <div className="relative h-48 overflow-hidden">
                      <img
                        src={product.imageUrl || 'https://via.placeholder.com/300'}
                        alt={product.name}
                        className="w-full h-full object-cover hover:scale-105 transition-transform duration-300"
                      />
                      {/* Favoriye ekle butonu
                      <button
                        onClick={() => addToFavorites(product.id)}
                        className="absolute top-2 right-2 p-2 rounded-full text-red-500 bg-white/80 backdrop-blur-sm"
                      >
                        ❤️
                      </button>           */}
                      <button
                        onClick={() => toggleFavorite(product.id)}
                        className={`absolute top-2 right-2 p-2 rounded-full ${favorites.includes(product.id) ? 'text-red-500' : 'text-gray-300'} bg-white/80 backdrop-blur-sm`}
                      >
                        {favorites.includes(product.id) ? '❤️' : '♡'}
                      </button>
                    </div>
                    <div className="p-4 flex flex-col flex-grow">
                      <h3 className="font-semibold text-lg mb-1 text-gray-900">
                        {product.name}
                      </h3>
                      <div className="flex items-center mb-1">
                        <span className="text-sm text-indigo-600 bg-indigo-50 px-2 py-1 rounded mr-2">
                          {(product as any).categoryName}
                        </span>
                        <span className="text-sm text-gray-600 bg-gray-50 px-2 py-1 rounded">
                          {(product as any).brandName}
                        </span>
                      </div>
                      <p className="text-gray-500 text-sm mb-3 line-clamp-2">
                        {product.description}
                      </p>
                      <div className="mt-auto">
                        <div className="flex justify-between items-center mb-3">
                          <span className="font-bold text-indigo-600 text-lg">
                            {product.price.toFixed(2)} ₺
                          </span>
                        </div>
                        <button
                          onClick={() => addToCart(product.id)}
                          className="w-full bg-indigo-600 hover:bg-indigo-700 text-white py-2 px-4 rounded-lg transition-colors"
                        >
                          Sepete Ekle
                        </button>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </section>
        </div>
      </main>
    </div>
  );
}