import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '.././api';
import './Login.css';

function Login({ setToken }) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const response = await api.post('/api/auth/login', { Email: email, Password: password });
            const { token } = response.data;
            setToken(token); // Armazena o token no estado global da aplicação
            navigate('/');
        } catch (err) {
            setError('Credenciais inválidas');
        }
    };

    return (
        <div class="wrapper fadeInDown">
            <div id="formContent">
                <div class="fadeIn first">
                    <img src="/unnamed.png" id="icon" alt="User Icon" />
                </div>
                <h2>Login</h2>
                <form onSubmit={handleLogin}>
                    <input value={email} type="text" class="fadeIn second" placeholder="Email" onChange={(e) => setEmail(e.target.value)} />
                    <input value={password} type="password" class="fadeIn third" placeholder="Senha" onChange={(e) => setPassword(e.target.value)} />
                    <input type="submit" class="fadeIn fourth" value="Entrar" />
                </form>
            </div>
        </div>
    );
}

export default Login;