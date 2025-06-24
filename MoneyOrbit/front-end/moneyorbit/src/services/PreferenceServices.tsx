import { Preferences } from "../Models/Preferences";

// This forces TypeScript to use the types from your interface (e.g., `string`)
// instead of inferring literal types (e.g., `""`).
const defaultPreferences: Preferences = {
  countryDialingCode: '',
  phoneNumber: '',
  nextPage: '',
  user: {
    id: '', 
    accessLevel: '', 
  },
};

export async function getPreferences(): Promise<Preferences> {
  try {
    const prefsString = localStorage.getItem('prefs');

    if (prefsString) {
      const storedPrefs = JSON.parse(prefsString) as Preferences;
      return { ...defaultPreferences, ...storedPrefs };
    } else {
      return defaultPreferences;
    }
  } catch (error) {
    console.error("Failed to fetch/parse preferences:", error);
    return defaultPreferences;
  }
}
export async function setPreferences(preferences: Preferences) {
    localStorage.setItem('prefs', JSON.stringify(preferences));
  }