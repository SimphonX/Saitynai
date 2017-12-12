import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';

export class Register extends React.Component<RouteComponentProps<{}>>  {
    hideLable(id: any) {
        var lable = document.getElementById(id);
        if (lable != null) lable.style.display = 'none';
    }
    public render() {
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
                <div id="signup">
                    <h1>Sign Up for Free</h1>

                    <form action="/" method="post">

                        <div className="top-row">
                            <div className="field-wrap">
                                <label id="fname">
                                    First Name<span className="req">*</span>
                                </label>
                                <input type="text" onClick={() => { this.hideLable("fname") }} required></input>
                            </div>

                            <div className="field-wrap">
                                <label id="lname">
                                    Last Name<span className="req">*</span>
                                </label>
                                <input type="text" onClick={() => { this.hideLable("lname") }} required ></input>
                            </div>
                        </div>

                        <div className="field-wrap">
                            <label id="Username">
                                Username<span className="req">*</span>
                            </label>
                            <input type="Text" onClick={() => { this.hideLable("Username") }} required ></input>
                        </div>

                        <div className="field-wrap">
                            <label id="Password">
                                Set A Password<span className="req">*</span>
                            </label>
                            <input type="password" onClick={() => { this.hideLable("Password") }} required ></input>
                        </div>

                        <button type="submit" className="button button-block" >Get Started</button>

                    </form>
                </div>
            </div>
        );
    }
}
