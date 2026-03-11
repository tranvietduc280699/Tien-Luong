import axios from "axios";
const http = axios.create({
  baseURL: "https://localhost:44368/api"
});
export default http;
