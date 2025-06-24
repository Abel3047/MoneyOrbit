
export interface Preferences {
  countryDialingCode: string;
  phoneNumber: string;
  nextPage: string;
  user: {
    id: string, // Initialize with empty strings or null
    accessLevel: string,
  }
}