import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, Article, ArticleQuery } from '../types';

@Component({
  selector: 'app-userlist-component',
  templateUrl: './userlist.component.html',
})

export class UserListComponent {
  private _baseUrl: string;
  private _http: HttpClient;
  public userlist?: User[];


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;

    this.getUserInfo();
  }
  public getUserInfo() {

    this._http.get<User[]>(this._baseUrl + 'api/User/GetUserList/')
      .subscribe(result => {
        this.userlist = result;
      }, error => console.error(error));
  }
}

  
