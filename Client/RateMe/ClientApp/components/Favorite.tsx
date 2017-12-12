import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Favorites extends React.Component<RouteComponentProps<{}>> {
    public render() {

        return (
            <div>
                <form>
                    <input className="searchbar" type="search" placeholder="Search" />
                </form>
                <div className="GameList">
                    {this.Games()}
                </div>
            </div>
        );
    }
    Games() {
        return <div className="Game" onMouseMove={(e) => { this.moredata(1, e) }} onClick={() => { this.props.history.push('/gameinfo') }}>
            <div className="tooltip-content" id="1">
                xzczxczxczxczxcasdasdasdasdasdasdasdad
            </div>
        </div>
    }
    UserAction() {
        var xhttp = new XMLHttpRequest();
        xhttp.open("get", "http://localhost:5001/api/game/1", true);
        //xhttp.setRequestHeader("Content-type", "application/json");
        xhttp.send();
        var response = JSON.parse(xhttp.responseText);
    }
    moredata(id: any, e: any) {
        var tip = document.getElementById(id);
        var x = e.clientX,
            y = e.clientY;
        if (tip != null) {
            tip.style.top = (y + 20) + 'px';
            tip.style.left = (x + 20) + 'px';
        }
    }
}
