import apiClient from "./api/apiClient";
import { Goal, ApiResponse } from "./types/Goal"; // Import both types

/**
 * Fetches goals from the backend and adapts the response shape.
 */
export const getGoals = async (): Promise<Goal[]> => {
  // The backend returns a single object inside the wrapper, not an array.
  const response = await apiClient.get<ApiResponse<Goal>>("/api/goals/get-goals");

  // Check if the response or the result is empty
  if (!response.data || !response.data.result) {
    return []; // Return an empty array if there's no data
  }

  // The backend sent a single object, but our UI expects an array.
  // We must "unwrap" the result and then put it inside an array.
  const singleGoal = response.data.result;
  
  // Return an array containing the single goal object
  return [singleGoal]; 
};