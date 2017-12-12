import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={ '/' }>RateMe</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li onClick={()=>localStorage.clear()} style={{ display: localStorage["logedin"] == "true" ? 'list-item' : 'none' }} >
                            <NavLink to={'/'} exact activeClassName='active'>
                                <span className='glyphicon glyphicon-log-in'></span> Log out
                            </NavLink>
                        </li>
                        <li style={{ display: localStorage["logedin"] != "true" ? 'list-item' : 'none' }} >
                            <NavLink to={'/login'} exact activeClassName='active' disabled={true}>
                                <span className='glyphicon glyphicon-log-in'></span> User
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={ '/search' } exact activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Find
                            </NavLink>
                        </li>

                        <li style={{ display: localStorage["logedin"] == "true" ? 'list-item' : 'none' }} >
                            <NavLink to={ '/favorite' } activeClassName='active'>
                                <span className='glyphicon glyphicon-star-empty'></span> Favorite
                            </NavLink>
                        </li>

                        <li>
                            <NavLink to={ '/fetchdata' } activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Fetch data
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}
