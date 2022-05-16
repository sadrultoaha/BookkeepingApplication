import axios from "axios";

let axiosInstance = axios.create({
    baseURL: process.env.VUE_APP_APIURL,
    headers: {
        "Content-type": "application/json",
    }
});

export default axiosInstance;
