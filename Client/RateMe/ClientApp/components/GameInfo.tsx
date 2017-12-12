import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
interface FetchData {
    load: number;
    loadgame: any[];
    loadcomments: any[];
    favorite: boolean;
    value: string;
}
export class GameInfo extends React.Component<RouteComponentProps<{}>, FetchData> {
    constructor() {
        super();
        this.state = {
            load: 0,
            loadcomments: [],
            loadgame: [],
            favorite: false,
            value: "enter comment"
        }
    }
    
    componentWillMount() {
        console.log(localStorage.getItem("gameId"))
        this.setState({ load: 2 });
        if (this.state.load == 0) {
            fetch("http://localhost:5001/api/game/" + localStorage.getItem("gameId"), {
                method: "GET",
                headers: {
                    type: "gg"
                }
            })
                .then(res => {
                    return res.json();
                })
                .then(resJson => {
                    var gamelist = [];
                    for (var i = 0; i < resJson.length; i++)
                        gamelist.push(resJson[i]);
                    this.setState({ load: this.state.load - 1, loadgame: gamelist })
                })
                .catch(e => console.log(e));
            fetch("http://localhost:5001/api/comment", {
                method: "GET",
                headers: {
                    game: localStorage.getItem("gameId")
                }
            })
                .then(res => {
                    return res.json();
                })
                .then(resJson => {
                    var commentList = [];
                    for (var i = 0; i < resJson.length; i++)
                        commentList.push(resJson[i]);
                    this.setState({ load: this.state.load - 1, loadcomments: commentList })
                })
                .catch(e => console.log(e));
        }
    }
    public render() {
        if (this.state.load != 0) return <p><em>Loading...</em></p>
        var rate = Math.round(this.state.loadgame[0].rating)
        console.log(this.state.loadgame[0])
        return (
            <div>
                <div id="background">
                    <img src={this.state.loadgame[0].screenshots[0].url} className="stretch" alt="" />
                </div>
                <div style={{ border: "2px solid #777777", padding: "10px", height:"50px" }}>
                    <div className="title">{this.state.loadgame[0].name}
                    </div>
                    <div className="title"> Rating:{rate}
                    </div>
                    <div className={this.favstart()} id="star" onClick={() =>this.addFavorite()} onMouseEnter={e => { this.favorite(true) }} onMouseLeave={e => { this.favorite(false) }} style={{ display: localStorage["logedin"] == "true" ? 'block' : 'none', fontSize: "200%" }} />
                </div>
                <div style={{ border: "2px solid #777777", padding: "10px" }}>
                    <a className="gameName">Summary </a>
                    
                    {this.state.loadgame[0].summary}
                </div>
                <div style={{ border: "2px solid #777777", padding: "10px" }}>
                    <div style={{ padding: "10px", display: "inline"}}>
                        <a className="gameName">Pegi: </a>
                        {this.state.loadgame[0].pegi.synopsis}
                    </div>
                    <div style={{ padding: "10px", display: "inline" }}>
                        <a className="gameName"> Rating: </a>
                        {this.state.loadgame[0].pegi.rating}
                    </div>
                </div>
                <div style={{ border: "2px solid #777777", padding: "10px", display: localStorage["logedin"] == "true" ? 'block' : 'none', }}>
                    <a className="gameName">Comment</a>
                    <textarea id="comment" onChange={e => { this.setState({ value: e.target.value }) }} value={this.state.value} />
                    <button onClick={()=> this.addComment()} >Summit</button>
                </div>
                <div style={{ border: "2px solid #777777", padding: "10px" }}>
                    {this.state.loadcomments.map(value => {
                        return <div key={value.id}>
                            <a style={{ textAlign: "left" }} className="gameName">{value.commenter}</a>
                            
                            
                            <div className='glyphicon glyphicon-plus' onClick={() => this.inc(value.id)} key={"+" + value.id} style={{ display: localStorage["logedin"] == "true" ? 'block' : 'none', float: "right", padding: "2px" }} />
                            <div className='glyphicon glyphicon-minus' onClick={() => this.disc(value.id)} key={"-" + value.id} style={{ display: localStorage["logedin"] == "true" ? 'block' : 'none', float: "right", padding: "2px" }} />
                            <a className="gameName" id={value.id} style={{ float: "right", padding: "2px" }}  >Score: {value.score}</a>
                            <div style={{ border: "2px solid #777777", padding: "10px" }}>
                                {value.text}
                            </div>
                        </div>
                    }
                    )}
                </div>
            </div>
        );
    }
    UserAction() {
        var xhttp = new XMLHttpRequest();
        xhttp.open("get", "http://localhost:5001/api/game/1", true);
        //xhttp.setRequestHeader("Content-type", "application/json");
        xhttp.send();
        var response = JSON.parse(xhttp.responseText);
    }
    favorite(enter: boolean) {
        var star = document.getElementById("star");
        if (star != null) {
            if (!enter && !this.state.favorite || this.state.favorite && enter)
                star.className = "glyphicon glyphicon-star-empty";
            if (enter && !this.state.favorite || this.state.favorite && !enter )
                star.className = "glyphicon glyphicon-star";
        }
    }
    favstart()
    {
        
        if (!this.state.favorite)
            return "glyphicon glyphicon-star-empty";
        if (this.state.favorite)
            return "glyphicon glyphicon-star";
    }
    removeComment(id: number) {

    }
    inc(id: number) {
        fetch("http://localhost:5001/api/comment/inc?id=" + id.toString(), {
            method: "PUT",
            headers: {
                Authorization: "Bearer " + localStorage.getItem("access_token")
            }
        })
            .then(res => {
                return res.json();
            })
            .then(resJson => {
                console.log(resJson)
                var star = document.getElementById(id.toString());
                if (star != null)
                    star.innerHTML = "Score: " + resJson;
            })
            .catch(e => console.log(e));

    }
    disc(id: number) {
        fetch("http://localhost:5001/api/comment/dec?id=" + id.toString(), {
            method: "PUT",
            headers: {
                Authorization: "Bearer " + localStorage.getItem("access_token")
            }
        })
            .then(res => {
                return res.json();
            })
            .then(resJson => {
                console.log(resJson)
                var star = document.getElementById(id.toString());
                if (star != null)
                    star.innerHTML = "Score: " + resJson;
            })
            .catch(e => console.log(e));
    }
    addComment()
    {
        
        var text = document.getElementById("comment");
        if (text != null) {
            console.log(JSON.stringify({
                "rate": 85,
                "text": text.innerHTML,
                "commenter": localStorage["username"],
                "game": localStorage["gameId"]
            }))
            fetch("http://localhost:5001/api/comment", {
                method: "POST",
                headers: {
                    Accept: 'application/json',
                    "Content-Type": 'application/json',
                    Authorization: "Bearer " + localStorage.getItem("access_token")
                },
                body: JSON.stringify({
                    "rate": 85,
                    "text": text.innerHTML,
                    "commenter": localStorage["username"],
                    "game": localStorage["gameId"]
                })
            })
                .catch(e => console.log(e));
        }
       
    }
    addFavorite() {
        if (this.state.favorite) {
            /*fetch("http://localhost:56311/api/favorites", {
                method: "POST",
                headers: {
                    Accept: 'application/json',
                    "Content-Type": 'application/json',
                    Authorization: "Bearer " + localStorage.getItem("access_token")
                },
                body: JSON.stringify({
                    "commenter": localStorage["username"],
                    "game": localStorage["gameId"]
                })
            })
                .catch(e => console.log(e));
            this.setState({ favorite: false });*/
        }
        else {
            fetch("http://localhost:5001/api/favorites", {
                method: "POST",
                headers: {
                    Accept: 'application/json',
                    "Content-Type": 'application/json',
                    Authorization: "Bearer " + localStorage.getItem("access_token")
                },
                body: JSON.stringify({
                    "follower": localStorage["username"],
                    "game": localStorage["gameId"]
                })
            })
                .catch(e => console.log(e));
            this.setState({ favorite: true });
        }
    }

}
