'use client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';

export default function Navbar() {
  const pathname = usePathname();
  return (
    <nav className="bg-white shadow mb-6">
      <div className="max-w-7xl mx-auto px-4 py-3 flex justify-between items-center">
        <Link href="/" className="font-bold text-indigo-600 text-xl">RoboSales</Link>
        <div className="flex gap-4">
          <Link href="/cart" className={pathname === '/cart' ? 'text-indigo-600' : ''}>Sepet</Link>
          <Link href="/favorites" className={pathname === '/favorites' ? 'text-indigo-600' : ''}>Favoriler</Link>
        </div>
      </div>
    </nav>
  );
}