import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Register.css';
import image from '../../Assets/bio-lab.png';

const Register = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [isError, setIsError] = useState(false);
    const navigate = useNavigate();

    const handleRegistration = async (e) => {
        e.preventDefault();
        if (!validateEmail(username)) {
            setIsError(true);
            setErrorMessage('Please enter a valid email address');
            return;
        }
        if (!validatePassword(password)) {
            setIsError(true);
            setErrorMessage('Password must be atleast 8 characters and include a number');
            return;
        }
        try {
            const response = await axios.post('http://localhost:5099/api/auth/register', { username, password });
            if (response.data.success) {
                navigate('/dashboard');
            } else {
                alert('Invalid credentials');
            }
        } catch (error) {
            console.error('Login error', error);
        }
    };

    const redirectToLogIn = async () => {
        navigate('/login');
    };

    useEffect(() => {
        if (isError) {
            const timer = setTimeout(() => {
                setIsError(false);
            }, 10000);
            return () => clearTimeout(timer); // Cleanup the timer on component unmount
        }
    }, [isError]);

    const validateEmail = (email) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
        return emailRegex.test(email);
    };

    const validatePassword = (password) => {
        const passwordRegex = /^(?=.*\d).{8,}$/;
        return passwordRegex.test(password);
    };

    return (
        <div className='login-container'>
            <h1>Sign Up</h1>
            <img src={image} height={'300px'} width={'300px'} />
            <form onSubmit={handleRegistration}>
                <div>
                    <label>Email</label> <br />
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} required />
                </div>
                <div>
                    <label>Password</label> <br />
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                </div>
                <button type="submit">Register</button>
                <small className='user-sign-in' onClick={redirectToLogIn}>Already a user? Log In</small>
            </form>

            <div className={`toast ${isError ? 'show' : ''}`}>
                <span>{errorMessage}</span>
            </div>
        </div>
    );
};

export default Register;
