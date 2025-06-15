
export const baseAPIPath = 'https://localhost:7134/MoneyOrbit/'; // Replace with your actual API base path


// This is now a simple async function, not a hook or component.
export async function getGoalByID(goalID: string) {
  try {
    // For a GET request, data is sent in the URL as a query parameter.
    const url = `${baseAPIPath}Goal/GetGoal?goalID=${goalID}`;

    const response = await fetch(url, {
      method: 'GET', // A 'body' is not used for GET requests
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      // Throw an error if the response is not 2xx
      throw new Error(`API call failed with status: ${response.status}`);
    }

    const data = await response.json();
    return data; // Return the data directly

  } catch (error) {
    console.error("Failed to fetch goal:", error);
    throw error; // Re-throw the error so the component can catch it
  }
}