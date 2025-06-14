// Sidebar.js
import React, { useState } from 'react';
// Tailwind doesn't come with icons, so you'd typically use Heroicons, Font Awesome, or similar.
// For this example, I'll use simple placeholder text/emoji for icons or Heroicons.
// If you want Heroicons: npm install @heroicons/react
import {
  HomeIcon,
  UsersIcon,
  DocumentTextIcon,
  CalendarDaysIcon,
  CurrencyDollarIcon,
  MegaphoneIcon,
  PhotoIcon,
  SunIcon,
  MoonIcon,
} from '@heroicons/react/24/outline'; // Example Heroicons

function Sidebar() {
  const [openIncome, setOpenIncome] = useState(false);

  const handleClickIncome = () => {
    setOpenIncome(!openIncome);
  };

  return (
    <div className="w-60 bg-sidebarBg text-white p-5 flex flex-col justify-between">
      <div>
        {/* Logo/Brand */}
        <div className="mb-8 text-2xl font-bold">Frame</div>

        {/* Navigation Items */}
        <nav>
          <ul>
            <li className="mb-2">
              <a href="#" className="flex items-center p-2 rounded-lg hover:bg-gray-700">
                <HomeIcon className="h-6 w-6 mr-3" />
                Dashboard
              </a>
            </li>
            <li className="mb-2">
              <a href="#" className="flex items-center p-2 rounded-lg hover:bg-gray-700">
                <UsersIcon className="h-6 w-6 mr-3" />
                Audience
              </a>
            </li>
            <li className="mb-2">
              <a href="#" className="flex items-center p-2 rounded-lg hover:bg-gray-700 relative">
                <DocumentTextIcon className="h-6 w-6 mr-3" />
                Posts
                <span className="absolute right-2 top-1/2 -translate-y-1/2 bg-purple-600 text-white text-xs font-semibold px-2 py-0.5 rounded-full">4</span>
              </a>
            </li>
            <li className="mb-2">
              <a href="#" className="flex items-center p-2 rounded-lg hover:bg-gray-700 relative">
                <CalendarDaysIcon className="h-6 w-6 mr-3" />
                Schedules
                <span className="absolute right-2 top-1/2 -translate-y-1/2 bg-orange-500 text-white text-xs font-semibold px-2 py-0.5 rounded-full">8</span>
              </a>
            </li>

            <li className="mb-2">
              <button
                onClick={handleClickIncome}
                className="flex items-center w-full p-2 rounded-lg hover:bg-gray-700 focus:outline-none"
              >
                <CurrencyDollarIcon className="h-6 w-6 mr-3" />
                Income
                {openIncome ? (
                  <svg className="ml-auto w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M5 15l7-7 7 7"></path>
                  </svg>
                ) : (
                  <svg className="ml-auto w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7"></path>
                  </svg>
                )}
              </button>
              {openIncome && (
                <ul className="ml-8 mt-1 text-sm">
                  <li className="mb-1">
                    <a href="#" className="block p-1 rounded-lg hover:bg-gray-700">Earning</a>
                  </li>
                  <li className="mb-1">
                    <a href="#" className="block p-1 rounded-lg hover:bg-gray-700">Refunds</a>
                  </li>
                  <li className="mb-1">
                    <a href="#" className="block p-1 rounded-lg hover:bg-gray-700">Declines</a>
                  </li>
                  <li className="mb-1">
                    <a href="#" className="block p-1 rounded-lg hover:bg-gray-700">Payouts</a>
                  </li>
                </ul>
              )}
            </li>

            <li className="mb-2">
              <a href="#" className="flex items-center p-2 rounded-lg hover:bg-gray-700">
                <MegaphoneIcon className="h-6 w-6 mr-3" />
                Promote
              </a>
            </li>
          </ul>
        </nav>
      </div>

      {/* Upload New Image Section */}
      <div className="border border-dashed border-gray-600 p-5 text-center my-6 rounded-lg text-gray-400">
        <PhotoIcon className="h-10 w-10 mx-auto mb-2 text-gray-500" />
        <p className="text-white">Upload new image</p>
        <p className="text-xs text-gray-500">Drag and drop</p>
      </div>

      {/* Light/Dark Mode Toggle */}
      <div className="flex justify-center gap-2 mt-4">
        <button className="flex items-center px-4 py-2 rounded-md bg-cardBg text-white hover:bg-cardContentBg focus:outline-none">
          <SunIcon className="h-5 w-5 mr-2" />
          Light
        </button>
        <button className="flex items-center px-4 py-2 rounded-md bg-cardBg text-white hover:bg-cardContentBg focus:outline-none">
          <MoonIcon className="h-5 w-5 mr-2" />
          Dark
        </button>
      </div>
    </div>
  );
}

export default Sidebar;