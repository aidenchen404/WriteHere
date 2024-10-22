import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, CommonValue, LookupPack } from '../types';


@Component({
  selector: 'app-register-component',
  templateUrl: './register.component.html',
 // styleUrls: ['.././site.component.css']
})
export class RegisterComponent {
  private _baseUrl: string;
  private _http: HttpClient;
  public user: User;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;
    this.user = new User();
  }


    public redirectTo() {
    location.replace("/memberdashboard")
  }
}
