import axios from "axios";

// axios instance
const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "http://localhost:5185/api", // backend ASP.NET Core API adresi
  headers: {
    "Content-Type": "application/json",
  },
});

// Tip örneği: request/response interceptor eklemek istersen
api.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error("API Error:", error);
    return Promise.reject(error);
  }
);

export default api;
