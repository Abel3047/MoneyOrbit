import React from "react";
import "./LoginForm.css"; 
import { FaUser } from "react-icons/fa"; 
import { FaLock } from "react-icons/fa";



const LoginForm = ({ onLogin }) => {

    return(
        <div className="wrapper">
            <form action= "">
                <h1>Login</h1>
                <div className="input-box">
                    <input type="text"
                     placeholder="Username" required />
                     <FaUser />
                     </div>
                     <div className="input-box">
                    <input type="text"
                     placeholder="Password" required />
                     <FaLock />
                     </div>

                     <div className="Remember forgot password">
                        <label><input type="checkbox"/> Remember me </label>
                        <a href ='#'> Forgot Password ?</a>
                        </div>

                        <button type ="submit" className="btn">Login</button>

                        <div className="register-link">
                            <p> Don't have an account? <a href ='#' > Register </a></p></div>

            </form>
            LoginForm</div>
    );
};
export default LoginForm;