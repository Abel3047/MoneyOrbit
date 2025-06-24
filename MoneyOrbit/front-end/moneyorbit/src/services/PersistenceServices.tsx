import { Preferences } from "../Models/Preferences";

// --- Storage Keys (Centralized and Private to this module) ---
const PREFERENCES_KEY = 'userPreferences';
const AUTH_TOKEN_KEY = 'authToken';
// This forces TypeScript to use the types from your interface (e.g., `string`)
// instead of inferring literal types (e.g., `""`).
const defaultPreferences: Preferences = {
  countryDialingCode: '',
  phoneNumber: '',
  nextPage: '',
  user: {
    accessLevel: '',
  },
};

export async function getPreferences(): Promise<Preferences> {
  try {
    const prefsString = localStorage.getItem(PREFERENCES_KEY);

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
  localStorage.setItem(PREFERENCES_KEY, JSON.stringify(preferences));
}
export async function saveAuthToken(token: string) {
  try {
    // Tokens are just strings, no need for JSON.stringify
    localStorage.setItem(AUTH_TOKEN_KEY, token);
  } catch (error) {
    console.error("Failed to save auth token:", error);
  }
}
/**
 * Retrieves the authentication token from local storage.
 */
export async function getAuthToken(): Promise<string | null> {
  try {
    return Promise.resolve(localStorage.getItem(AUTH_TOKEN_KEY));
  } catch (error) {
    console.error("Failed to get auth token:", error);
    return Promise.resolve(null);
  }
}
/**
 * Removes the authentication token from local storage (e.g., on logout).
 */
export function removeAuthToken(): void {
  try {
    localStorage.removeItem(AUTH_TOKEN_KEY);
  } catch (error) {
    console.error("Failed to remove auth token:", error);
  }
}

export async function saveUserPreferencesAndToken(AccessLevel: string, token: string): Promise<void> {
  //Gets the preferences from local storage
  let prefs: Preferences = await getPreferences();

  // Ensure prefs.user exists before setting accessLevel
  if (!prefs.user) {
    prefs.user = { accessLevel: AccessLevel };
  } else {
    prefs.user.accessLevel = AccessLevel;
  }

  // Saves the preferences and token back to local storage
  await saveAuthToken(token);
  await setPreferences(prefs);
}