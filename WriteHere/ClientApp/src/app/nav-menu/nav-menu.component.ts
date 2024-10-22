import { Component } from '@angular/core';
import { User } from '../types';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent {
  public UserName: string
  public img: string = 'images/cover-page-logo.jpg';
  constructor() {
    this.UserName = this.getUsername();
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  public logOut() {
    localStorage.clear();
    location.replace("/home");
  }

  public getUser() {
    var str = localStorage.getItem('user');
    if (str) { return JSON.parse(str) as User; }
    return null;
  }


  public isLoggedIn() {
    return this.getUser() != null;
  }

  public isLoggedInWriter() {
    var user = this.getUser();
    return user != null && (user.isWriter || user.isAdmin);
  }

  public isLoggedInAdmin() {
    var user = this.getUser();
    return user != null && (user.isAdmin);
  }

  public getUsername() {
    var user = this.getUser();
    if (user == null) { return 'guest'; }
    else {
      return user.userName;
    }
  }

  public redirectToNewArticle() {

    location.replace("/articledetails?isEdit=true");
  }

  public redirectToUserDetail() {
    var user = this.getUser();
    if (user) {
      location.replace("/userdetails?id=" + user.id);
    }
  }
}
