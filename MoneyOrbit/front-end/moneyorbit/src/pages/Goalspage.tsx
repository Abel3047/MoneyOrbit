import React from "react";
import { useQuery } from "@tanstack/react-query";
import { getGoals } from "../services/goalservice"; // Import our service function
import { Goal } from "../services/types/Goal"; // Import our type definition

export default function GoalsPage() {
  // This is the core "middleware" hook from React Query
  const { 
    data: goals,      // 'goals' will hold our array of GoalDto objects on success
    isLoading,        // A boolean that is true while the request is in progress
    isError,          // A boolean that is true if the request fails
    error,            // The error object if isError is true
    refetch,          // A function to manually trigger the API call again
    isFetching,       // True when refetching, different from the initial isLoading
  } = useQuery({
    queryKey: ["goals"],    // A unique key for this query. React Query uses this for caching.
    queryFn: getGoals,      // The function that will be executed to fetch the data.
    enabled: false,         // IMPORTANT: This prevents the query from running automatically on page load.
  });

  // Event handler for our button click
  const handleGetGoalsClick = () => {
    // Calling refetch() executes the queryFn (getGoals) on demand.
    refetch();
  };

  return (
    <div className="container py-8">
      <h1 className="text-2xl font-bold">My Goals</h1>
      <p className="text-muted-foreground mb-6">
        Click the button to fetch your goals from the server.
      </p>

      <div className="flex items-center space-x-4">
        <button type="button" onClick={handleGetGoalsClick} disabled={isFetching}>
          {isFetching ? "Fetching..." : "Get Goals"}
        </button>
      </div>
      
      {/* --- UI Rendering Logic --- */}

      <div className="mt-8">
        {isLoading && <p>Loading...</p>}

        {isError && (
          <p className="text-destructive">
            An error occurred: {error.message}
          </p>
        )}

        {goals && (
          <ul className="space-y-4">
            {goals.map((goal: Goal) => (
              <li key={goal.id} className="p-4 border rounded-md shadow-sm">
                <h3 className="font-semibold">{goal.goalName}</h3>
                <p className="text-sm text-muted-foreground">{goal.description}</p>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}