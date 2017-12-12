import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Search } from './components/Search';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { LogIn } from './components/LogIn'
import { Register } from './components/Register'
import { Favorites } from './components/Favorite'
import { GameInfo } from './components/GameInfo'

export const routes = <Layout>
    <Route path='/login' component={LogIn} />
    <Route path='/search' component={Search } />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata' component={FetchData} />
    <Route path='/register' component={Register} />
    <Route path='/favorite' component={Favorites} />
    <Route path='/gameinfo' component={GameInfo} />
</Layout>;
