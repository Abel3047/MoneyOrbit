import apiClient from "./api/apiClient"; // Your pre-configured axios instance from the previous examples
import { Goal } from "./types/Goal"; // We will define this type in the next step

/**
 * Fetches all goals from the backend API.
 * This function knows the exact endpoint and what data shape to expect.
 */
export const getGoals = async (): Promise<Goal[]> => {
  console.log("Fetching goals from the API...");
  
  // This makes the GET request to: [your_base_url]/api/goals/get-goals
  const response = await apiClient.get<Goal[]>("/api/goals/get-goals");
  
  // The 'data' property on the response will contain the array of Goal objects
  return response.data;
};