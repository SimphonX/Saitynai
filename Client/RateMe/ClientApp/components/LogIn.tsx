import * as React from 'react';
import { RouteComponentProps, withRouter } from 'react-router';
import { Link, NavLink, BrowserRouter } from 'react-router-dom';

export class LogIn extends React.Component<RouteComponentProps<{}>>  {
    loading: boolean;
    username: string;
    password: string;
    componentWillMount()
    {
        if (localStorage["logedin"] == "true") this.props.history.push('/');
    }
    start() {
        if (localStorage["toLogIn"] == "false") return;
        var FD = new URLSearchParams();
        FD.append("grant_type", "password");
        FD.append("username", localStorage["username"]);
        FD.append("password", localStorage["password"]);
        FD.append("client_id", "client");
        FD.append("client_secret", "67A20C10A94248DBA64B4F1EB00CFD6A");
        FD.append("scope", "api1 offline_access");
        console.log(FD);
        fetch("http://localhost:5000/connect/token", {
            method: "POST",
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: FD
        })
            .then(res => {
                return res.json();
            })
            .then(resJson => {
                localStorage.setItem("load", "false");
                localStorage.setItem("access_token", resJson.access_token);
                localStorage.setItem("refresh_token", resJson.refresh_token);
                localStorage.setItem("logedin", "true");
                localStorage["toLogIn"] = "false"
                this.props.history.push('/');
            })
            .catch(reson => localStorage.setItem("error", reson.error));
        
    }
    hideLable(id:any)
    {
        var lable = document.getElementById(id);
        if(lable != null) lable.style.display = 'none';
    }
    public render() {
        console.log(localStorage);
        return (
            <div>
                <ul className="tab-group">
                
                        <li>
                            <NavLink to={'/login'} exact activeClassName='active'>
                                <span className='glyphicon glyphicon-log-in'></span> Log In
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/register'} exact activeClassName='active'>
                                <span className='glyphicon glyphicon-user'></span> Register
                            </NavLink>
                        </li>
                </ul>
                <div id="login">

                    <form action="/" method="post">

                        <div className="field-wrap" >
                            <label id="Username">
                                    Username<span className="req">*</span>
                            </label>
                            <input type="text" name="username" placeholder="" onChange={(e) => localStorage.setItem("password", e.target.value)} onClick={() => { this.hideLable("Username") }} required ></input>
                        </div>

                        <div className="field-wrap">
                            <label id="Password">
                                    Password<span className="req">*</span>
                            </label>
                            <input type="password" id="password" placeholder="" onChange={(e) => localStorage.setItem("username", e.target.value)} onClick={() => { this.hideLable("Password") }} required ></input>
                        </div>

                        <p className="forgot"><a href="#">Forgot Password?</a></p>
                        <div className="button button-block" >
                            <NavLink onClick={() => { localStorage.setItem("toLogIn", "true"); this.start(); }} to={'/login'} exact activeClassName='active'>
                                    <span className='glyphicon glyphicon-log-in'></span> Log In
                                </NavLink>
                        </div>
          
                    </form>

                </div>
            </div>
        );
    }
    UserAction() {
        
    }
}
