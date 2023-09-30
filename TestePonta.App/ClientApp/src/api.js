import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7001', // Substitua pela URL da sua API
});

export default api;
