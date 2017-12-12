class Data {
    UserLogin: boolean
    UserId: any
    UserName: any
    access_token : any
    refresh_token : any
    constructor() {
        this.UserId = 0;
        this.UserLogin = false;
        this.UserName = "";
        this.access_token = "";
        this.refresh_token = "";
    }
}
export default (new Data);
    