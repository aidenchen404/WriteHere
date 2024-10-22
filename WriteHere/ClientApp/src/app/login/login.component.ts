import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, CommonValue,  LookupPack, WeatherForecast } from '../types';

@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html',
 })

export class LoginComponent {
  public forecasts: WeatherForecast[] = [];
  public ge: CommonValue[] = [];
  private _baseUrl: string;
  private _http: HttpClient;
  public user: User;
  public msg: string = '';
    

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;
    this.user = new User();
    //this.loadweather();
    this.loadGenre();
    this.loadPack();
  }

  public getUser() {
    var str = localStorage.getItem('user');
    if (str) { return JSON.parse(str) as User; }
    return null;
  }

  public login() {
    this._http.get<User>(this._baseUrl + 'api/User/Login?username=' + this.user.userName
      + '&password=' + this.user.loginPassword)
      .subscribe(result => {
        var loggedUser = result as User;

        if (loggedUser == null) {
          this.user = new User();
          this.msg = 'Login Failed. Please check your username and password';
        }
        else {
          //sucessfully logged in
       
          if (loggedUser.id != null) {
            localStorage.setItem('user', JSON.stringify(loggedUser));
            location.replace("/memberdashboard");

          }
        }
      }, error => console.error(error));
  }


  public loadweather() {
    this._http.get<WeatherForecast[]>(this._baseUrl + 'weatherforecast')
      .subscribe(result => {
        this.forecasts = result;
        alert(this.forecasts);
        alert(this.forecasts[0]);
        alert(this.forecasts[0].date);

      }, error => console.error(error));
  }

  public loadGenre() {
    this._http.get<CommonValue[]>(this._baseUrl + 'lookup/GetGenre')
      .subscribe(result => {
        localStorage.setItem('genres', JSON.stringify(result));
      }, error => console.error(error));
  }


  public loadPack() {
    alert('-loadPack-');
    this._http.get<LookupPack>(this._baseUrl + 'lookup/GetPack')
      .subscribe(result => {
        
        alert('loadPack:'+ result);
        alert(result.genre);
        alert(result.assignPurpose);
        var genres = result.genre;
        localStorage.setItem('genres', JSON.stringify(genres));
        var ap = result.assignPurpose;
        localStorage.setItem('assignPurposes', JSON.stringify(ap));
        //var bb = JSON.parse(localStorage.getItem('genres')) as Lookup[];
        
        alert(result.genre[0].text);
        alert(result.assignPurpose[0].text);

        if (genres) {
          alert(genres.length);
        } else { alert('nothing'); }
      }, error => console.error(error));
  }


}
