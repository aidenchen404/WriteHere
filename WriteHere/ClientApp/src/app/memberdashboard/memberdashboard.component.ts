import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  User, DashboardPack, ArticleRow,
  ArticleQuery, ArticleAssignment, CommonString
} from '../types';

@Component({
  selector: 'app-memberdashboard-component',
  templateUrl: './memberdashboard.component.html',
})


export class MemberdashboardComponent {
  private _baseUrl: string;
  private _http: HttpClient;
  public dashboardPack?: DashboardPack;
  public loginUser?: User|null;

  public accordionExpand: boolean[];
  public accordionCount = 10;

  public editors?: CommonString[];
  public tutors?: CommonString[];
  public drawers?: CommonString[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;

    this.loginUser = this.getUser();

    this.accordionExpand = new Array(this.accordionCount).fill(false);
    this.accordionExpand[2] = true;

    if (this.loginUser == null) {
      // navigation should not be here, because only loggin user can visit my dashboard.
      // this is for user visit by directly typing url.
      this.loginUser = new User();
      this.loginUser.isAdmin = false;
      this.loginUser.isReader = false;
      this.loginUser.isEditor = false;
      this.loginUser.isWriter = false;
      this.loginUser.isTutor = false;
      this.loginUser.isDrawer = false;
    }
    else {
      this.getDashBoardPackByLoginId();
      this.getEditors();
      this.getTutors();
      this.getDrawers();
    }
  }

  public getUser() {
    var str = localStorage.getItem('user');
    if (str) { return JSON.parse(str) as User; }
    return null;
  }

  public setAccordionSign(index: number) {
    this.accordionExpand[index] = !this.accordionExpand[index];
  }

  public getAccordionSign(index: number) {
    if (this.accordionExpand[index]) { return '-'; } else { return '+'; }
  }

  public getDashBoardPackByLoginId() {
    if (this.loginUser) {
      this._http.get<DashboardPack>(this._baseUrl + 'api/User/GetDashboardPack/?loginId=' + this.loginUser.id)
        .subscribe(result => {
          this.dashboardPack = result;
        }, error => console.error(error));
    }
  }

  public getEditors() {
    this._http.get<CommonString[]>(this._baseUrl + 'api/User/GetEditorList')
        .subscribe(result => {
          this.editors = result;
        }, error => console.error(error));
  }

  public getTutors() {
    this._http.get<CommonString[]>(this._baseUrl + 'api/User/GetTutorList')
        .subscribe(result => {
          this.tutors = result;
        }, error => console.error(error));
  }

  public getDrawers() {
    this._http.get<CommonString[]>(this._baseUrl + 'api/User/GetDrawerList')
        .subscribe(result => {
          this.drawers = result;
        }, error => console.error(error));
  }
  
  public getMyArticleRowsByAuthorId() {
    var user = this.getUser();
    if (user != null) {
      var query = new ArticleQuery();
      query.authorUserId = user.id;
      this.getMyArticleRowsByQuery(query);
    }
  }

  public getMyArticleRowsByEditorId() {
    var user = this.getUser();
    if (user != null) {
      var query = new ArticleQuery();
      query.editorUserId = user.id;
      this.getMyArticleRowsByQuery(query);
    }
  }
  public getMyArticleRowsByVoteupId() {
    var user = this.getUser();
    if (user != null) {
      var query = new ArticleQuery();
      query.votedUpByUserId = user.id;
      this.getMyArticleRowsByQuery(query);
    }
  }

  public getMyArticleRowsByCommentedId() {
    var user = this.getUser();
    if (user != null) {
      var query = new ArticleQuery();
      query.commentedByUserId = user.id;
      this.getMyArticleRowsByQuery(query);
    }
  }

  private getMyArticleRowsByQuery(query: ArticleQuery) {

    this._http.get<ArticleRow[]>(this._baseUrl + 'api/Article/GetArticleRow/?queryString='
      + encodeURIComponent(JSON.stringify(query)))
      .subscribe(result => {
        if (this.dashboardPack) {
          this.dashboardPack.writerArticles = result;
        }
      }, error => console.error(error));
  }

  public saveAssignment(articleAssignment: ArticleAssignment) {
    this._http.put<any>(this._baseUrl + 'api/Assignment/Update' , articleAssignment)
      .subscribe(result => {
        if (this.dashboardPack != null) {
          var newarr = this.ArrayRemove(this.dashboardPack.auditorAssignments, articleAssignment);
          this.dashboardPack.auditorAssignments = newarr;
        }
      }, error => console.error(error));
  }

  private ArrayRemove(arr:any, value:object) {
    return arr.filter(function (ele:object) {
      return ele != value;
    });
  }

  public composeArticle() {
    location.replace("/articledetails?isEditable=true")

  }

  public redirectToPublished() {
    location.replace("/published")
  }
  public redirectToArticleDetails() {
    location.replace("/articledetails")
  }
  public redirectToMyArticles() {
    location.replace("/myarticles")
  }
  public redirectToMyAssignments() {
    location.replace("/myassignments")
  }

}


