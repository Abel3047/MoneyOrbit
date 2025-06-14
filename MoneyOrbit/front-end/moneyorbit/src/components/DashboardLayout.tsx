// src/components/DashboardLayout.jsx
import React from "react";

const Sidebar = () => (
  <div className="h-full w-64 bg-gradient-to-b from-[#001f3f] to-[#004466] text-white p-6 flex flex-col justify-between">
    <div>
      <div className="mb-10">
        <h1 className="text-sm text-teal-400 font-bold">FNB</h1>
        <h2 className="text-md font-semibold">MoneyOrbit</h2>
      </div>
      <nav className="flex flex-col gap-5 text-sm">
        <a href="#" className="text-white font-semibold">● Dashboard</a>
        <a href="#" className="text-white hover:underline">○ Badges and Awards</a>
        <a href="#" className="text-white hover:underline">○ Notifications</a>
      </nav>
    </div>
    <div className="text-sm space-y-2">
      <a href="#" className="text-white hover:underline">○ Settings</a>
      <a href="#" className="text-white hover:underline">Log Out</a>
    </div>
  </div>
);

const DashboardContent = ({ name }) => (
  <div className="flex-1 bg-gradient-to-br from-[#1E2A45] to-[#76B8C9] p-10 rounded-xl m-4 text-white">
    <h1 className="text-xl font-semibold">Hello, {name}</h1>
  </div>
);

const DashboardLayout = () => {
  return (
    <div className="h-screen w-screen flex bg-slate-900 overflow-hidden">
      <Sidebar />
      <DashboardContent name="Tami" />
    </div>
  );
};

export default DashboardLayout;
