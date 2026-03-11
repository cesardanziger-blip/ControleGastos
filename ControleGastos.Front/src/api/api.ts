import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7260/api"
});

export default api;