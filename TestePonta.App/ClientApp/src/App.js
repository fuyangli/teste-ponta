import React, { Component, useState, useEffect } from 'react';
import { Route, Routes, useNavigate, Navigate } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import Login from './components/Login';
import { Layout } from './components/Layout';
import api from './api.js';
import './custom.css';


function App() {
    const [token, setToken] = useState('');
    const navigate = useNavigate();


    useEffect(() => {
        const savedToken = localStorage.getItem('token');
        if (savedToken) {
            return setToken(savedToken);
        }
        return navigate('/login');
    }, [navigate]);

    var setTokenStorage = function (token) {
        localStorage.setItem('token', token);
        setToken(token);
        api.get('/api/users/logged',
            {
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`
            }
            }).then(x => {
                localStorage.setItem('user', JSON.stringify(x.data));
            })
        
    }

    var useCleanTokenStorage = function () {
        localStorage.removeItem('token');
        setToken(null);
        window.location.reload();
    }

    return (
        <Layout logout={useCleanTokenStorage}>
            <Routes>
                <Route
                    path="/login"
                    element={<Login setToken={setTokenStorage} />}
                />
                {AppRoutes.map((route, index) => {
                    const { element, ...rest } = route;
                    return <Route key={index} {...rest} element={element} />;
                })}
            </Routes>
        </Layout>
    );
}

export default App;