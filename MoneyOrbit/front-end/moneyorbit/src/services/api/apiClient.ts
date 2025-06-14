import axios, { AxiosError, InternalAxiosRequestConfig } from "axios";
import { environment } from "../environments/environment";

// Create a new Axios instance
const apiClient = axios.create({
  baseURL: environment.apiUrl, // Set your base API URL
});

export default apiClient;