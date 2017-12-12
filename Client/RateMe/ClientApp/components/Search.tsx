import * as React from 'react';
import { RouteComponentProps } from 'react-router';
interface FetchData
{
    load: boolean;
    list: Array<number>;
    next: any;
    page: number;
    pagenum: number;
    loadgameinfo: boolean;
    loadgames: number;
}
export class Search extends React.Component<RouteComponentProps<{}>, FetchData> {
    constructor() {
        super();
        this.state = {
            load: false,
            list: [] ,
            next: "",
            page: 0,
            pagenum: 1,
            loadgameinfo: true,
            loadgames:0
        };
    }
    componentDidUpdate()
    {
        this.LoadGameInfo(this.state.list[this.state.loadgames]);
    }
    componentWillUnmount() {
        window.removeEventListener('scroll', this.handleOnScroll);
    }
    componentWillMount()
    {
        window.addEventListener('scroll', this.handleOnScroll.bind(this));
        fetch("http://localhost:5001/api/game", {
            method: "GET",
        })
            .then(res => {

                var size = res.headers.get("size") == null ? 0 : Number(res.headers.get("size"));
                var next = res.headers.get("nextpage") ;
                this.setState({
                    next: next, page:size/20});
                return res.json();
            })
            .then(resJson => {
                var gamelist = [];
                for (var i = 0; i < resJson.length; i++)
                    gamelist.push(resJson[i].id);
                this.setState({list:gamelist, load:true})
            })
            .catch(e => console.log(e));
    }
    /* <header>
    <form>
        <input className="searchbar" type="search" placeholder="Search" />
    </form>
</header>*/
    public render() {
        return (<div id="header">
           
            <div className="GameList">
                {this.state.list.map(value => {
                    return this.RenderGameInfo(value);
                })}
            </div>
            {this.LoadMoreGames()}
            <footer>
                
            </footer>
        </div>
        );
        
    }
    LoadGameInfo(id: any)
    {
        if (this.state.loadgames <this.state.pagenum *20 && id != undefined) {
            this.getData(id);
            this.setState({ loadgames: this.state.loadgames + 1 });
        }
    }
    RenderGameInfo(value:number)
    {
        return < div className="Game" id={"a" + value.toString()} key={value.toString()} onMouseMove={(e) => { this.moredata(value, e) }} onClick={() => { localStorage.setItem("gameId", value.toString()); this.props.history.push('/gameinfo'); window.removeEventListener('scroll', this.handleOnScroll); }}>
            <div className="cover" >
                <img keyParams="image" src="//images.igdb.com/igdb/image/upload/t_thumb/qly3lqrhwefbray4gh0w.jpg" className={ "stretch " + value.toString() } alt="" />
            </div>
            <div>
                <a className={"gameName " + value.toString() } keyParams="gameName" > </a>
            </div>
            <div className="tooltip-content "  id={value.toString()} key={value.toString()}>

                <div className={"gamename "+ value.toString() }>
                </div>
                <div className={"rating "+ value.toString() }>
                </div>
                <div className={"summary "+ value.toString() }>
                    
                </div>
                <div className={"Gendres "+ value.toString() }>
                </div>

                <div className={"platform "+ value.toString() }>
                    
                </div>

            </div>
        </div>
    }
    LoadMoreGames() {
        if (this.state.load) {
            return (
                <div className="data-loading">
                    <i className="fa fa-refresh fa-spin"></i>
                </div>
            );
        } else {
            return (
                <div className="GameList">
                    {this.state.list.map(value => {
                        return this.RenderGameInfo(value)
                    })}
                </div>
            );
        }
    }
    moredata(id:any, e:any)
    {
        var tip = document.getElementById(id);
        var x = e.clientX,
            y = e.clientY;
        if (tip != null)
        {
            tip.style.top = y+50  + 'px';
            tip.style.left = x-250  + 'px';
        }
    }
    handleOnScroll() {
        // http://stackoverflow.com/questions/9439725/javascript-how-to-detect-if-browser-window-is-scrolled-to-bottom
        var scrollTop = (document.documentElement && document.documentElement.scrollTop) || document.body.scrollTop;
        var scrollHeight = (document.documentElement && document.documentElement.scrollHeight) || document.body.scrollHeight;
        var clientHeight = document.documentElement.clientHeight || window.innerHeight;
        var scrolledToBottom = Math.ceil(scrollTop + clientHeight) >= scrollHeight;
        if (scrolledToBottom) {
            this.loadMore();
        }
    }

    loadMore() {
        if (this.state.page + 1 > this.state.pagenum && this.state.load) {
            this.setState({ load: false });
            
            fetch("http://localhost:5001/api/game/NextPage", {
                method: "GET",
                headers: {
                    key: this.state.next
                }
            })
                .then(res => {
                    return res.json();
                })
                .then(resJson => {
                    var gamelist = this.state.list;
                    for (var i = 0; i < resJson.length; i++)
                        gamelist.push(resJson[i].id);
                    setTimeout(this.setState({ list: gamelist, load: true, pagenum: this.state.pagenum + 1 }), 2000);
                })
                .catch(e => console.log(e));
            
        }
        
    }
    getData(id: string)
    {
        this.setState({ loadgameinfo: true });
        fetch("http://localhost:5001/api/game/" + id , {
            method: "GET",
            headers: {
                type:"list"
            }
        })
            .then(res => {
                return res.json();
            })
            .then(resJson => {
                var gamelist =[];
                for (var i = 0; i < resJson.length; i++)
                    gamelist.push(resJson[i]);
                this.setState({ loadgameinfo: false })
                this.renderData(gamelist[0])
            })
            .catch(e => console.log(e));
    }
    renderData(gamedata: any) {
        /*var style = {
            backgroundImage: "url(" + gamedata.cover.url  + ")"
        }*/
        var data = document.getElementsByClassName(gamedata.id);
        data.item(0).setAttribute("src", gamedata.cover.url);
        data.item(1).innerHTML = gamedata.name;
        data.item(2).innerHTML = gamedata.name;
        data.item(3).innerHTML = Math.round(gamedata.total_rating).toString();
        data.item(4).innerHTML = gamedata.summary;
        var str = gamedata.genres[0].name;
        for (var sk = 1; sk < gamedata.genres.length; sk++)
            str += ", " + gamedata.genres[sk].name;
        data.item(5).innerHTML = str;
        data.item(6).innerHTML = gamedata.platforms[0].name;
        var div = document.getElementById("a"+gamedata.id);
        if (div != null) {
            div.style.display = "flex";
            div.style.animation = 'fade 1.5s';
        }
        /*var style = document.getElementById(gamedata.id);
        
        if (style != null)
            style.style.backgroundImage = "url("+gamedata.cover.url+")";*/
    }
}

