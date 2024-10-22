import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import {
  User, 
  Article, ArticleComment, ArticleCommentQuery,
  ArticleVote, 
  CommonValue, StandardResponse
} from '../types';


@Component({
  selector: 'app-articledetails-component',
  templateUrl: './articledetails.component.html'
})


export class ArticleDetailsComponent{
  private _baseUrl: string;
  private _http: HttpClient;
  
  public article?: Article;
  public msg: string = '';
  public user: User |null;
  public genres: CommonValue[] | null;
 
  public isReader: boolean = true;
  public isEditor: boolean = false;
  public isWriter: boolean = false;
  public isAdmin: boolean = false;
  public isNewArticle: boolean = true;

  public accordionExpand: boolean[];
  public accordionCount = 10;
  public newComment: string = '';
  public editingComment: boolean = false;
  public requestDrawer: boolean = false;
  public requestTutor: boolean = false;

  public getUser() {
    var str = localStorage.getItem('user');
    if (str) {return JSON.parse(str) as User; }
     return null; 
  }

  public getGenres() {
    var str = localStorage.getItem('genres');
    if (str) {
      return JSON.parse(str) as CommonValue[];
    }
    return null;
  }

  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;
    this.accordionExpand = new Array(this.accordionCount).fill(false);
    this.accordionExpand[2] = true;

    const urlParams = new URLSearchParams(window.location.search);
    var articleid = urlParams.get('id');
    this.user = this.getUser();
    this.genres = this.getGenres();
   
    if (this.user != null) {
      // a logged-in user
      this.isAdmin = this.user.isAdmin;
      if (articleid == null) {
        // will create a new article
        this.article = new Article();
        this.article.title = 'NEW ARTICLE';
        this.isNewArticle = true;
        this.isEditor = false;
        this.isReader = false;
        this.isWriter = this.user.isWriter;
      }
      else {
        // will view/write an article
        this.article = new Article();
        this.article.title = 'loading ... ';
        this.isWriter = this.user.isWriter && (this.article.authorUserId == this.user.id);
        this.isNewArticle = false;
        this.getArticle(articleid);
      }
    } else {
      // a guest user will read an article
      if (articleid != null) {
        this.article = new Article();
        this.article.title = 'loading ... ';
        this.isWriter = false;
        this.isNewArticle = false;
        this.getArticle(articleid);
      }
    }
  }

  public setAccordionSign(index: number) {
    this.accordionExpand[index] = !this.accordionExpand[index];
  }

  public getAccordionSign(index:number) {
    if (this.accordionExpand[index]) { return '-'; }
    else { return '+'; }
  }

  public getArticle(id :string) {
    if (this.user) {
      this._http.get<Article>(this._baseUrl + 'Article/GetArticle?id=' + id
        + '&byId=' + this.user.id)
        .subscribe(result => {
          if (this.user) {
            this.article = result;
            this.isEditor = this.user.isEditor
              && this.article.editorUserId == this.user.id
              && this.article.authorUserId != this.user.id;
            this.isWriter = this.user.isWriter
              && this.article.authorUserId == this.user.id;
            this.isReader = this.user.isReader
              && this.article.authorUserId != this.user.id;
          }
        }, error => console.error(error));
    }
  }
  
  public saveArticle() {
    if (this.user && this.article) {
      this.article.authorUserId = this.user.id;
      this._http.post<Article>(this._baseUrl + 'api/Article/', this.article)
        .subscribe((res: Article) => {
          if (this.article) {
            this.article.id = res.id;
            this.msg = 'Saved at ' + new Date();
          }
        })
    }
  };

  public rejectArticle() {
    if (this.user && this.article) {
      this.article.authorUserId = this.user.id;
      this.saveArticle();
    }
  }

  public publishArticle() {
    // TODO
  }

  // still debug. not working
  public submitArticle() {
    if (this.article) {
      if (confirm("Once submitted, you cannot edit the article. \nAre you sure to submit the article?")) {
        alert('Submit  ' + this.article.id);
        this._http.get<StandardResponse>(this._baseUrl + 'api/ArticleStatusHistory/Submit?articleId=' + this.article.id)
          .subscribe((res: StandardResponse) => {
            if (res.success) {
              if (this.article) {
                this.article.articleStatus = 'Submitted';
                this.msg = 'Submitted at ' + new Date();
              }
            }
          })

        const body = {
          articleId: this.article.id,
          requestEditor: true,
          requestDrawer: this.requestDrawer,
          requestTutor: this.requestTutor
        }

        this._http.put(this._baseUrl + 'api/Assignment/Create', body)
          .subscribe(res => {
          }, error => console.error(error));
      }
    }
  }

  public deleteArticle() {
    if (this.user && this.article) {
      if (confirm("Once delete, you cannot undo the deletion. \nAre you sure to delete the article?")) {

        this.article.authorUserId = this.user.id;
        this._http.delete<StandardResponse>(this._baseUrl + 'api/Article/' + this.article.id)
          .subscribe((res) => {

            this.msg = 'Deleted at ' + new Date();
          })
      }
    }
  }

  public saveComment() {
    if (this.article && this.user) {
      var comment = new ArticleComment();
      comment.commentOwnerId = this.user.id;
      comment.comment = this.newComment;
      comment.articleId = this.article.id;
      this._http.post<ArticleComment>(this._baseUrl + 'api/ArticleComment/', comment)
        .subscribe((res) => {
          if (this.article && this.article.comments) {
            this.article.comments.unshift(res);
          }
        })
    }
  };

  public voteUp() {
    if (this.article) {
      this.putVoteToDb(1);
      if (this.article.viewerVote == 0) {
        this.article.upVote++;
        this.article.viewerVote = 1;
        return;
      }
      if (this.article.viewerVote == 1) {
        this.article.upVote--;
        this.article.viewerVote = 0;
        return;
      }
      if (this.article.viewerVote == -1) {
        this.article.upVote++;
        this.article.downVote--;
        this.article.viewerVote = 1;
        return;
      }
    }
  }

  public voteDown() {
    if (this.article) {
      this.putVoteToDb(-1);
      if (this.article.viewerVote == 0) {

        this.article.downVote++;
        this.article.viewerVote = -1;
        return;
      }
      if (this.article.viewerVote == 1) {
        this.article.downVote++;
        this.article.upVote--;
        this.article.viewerVote = -1;
        return;
      } if (this.article.viewerVote == -1) {
        this.article.downVote--;
        this.article.viewerVote = 0;
        return;
      }
    }
  }

  public putVoteToDb(vote: number) {
    if (this.article && this.user) {
      var articleVote = new ArticleVote();
      articleVote.userId = this.user.id;
      articleVote.vote = vote;
      articleVote.articleId = this.article.id;

      this._http.post(this._baseUrl + 'api/ArticleVote/', articleVote)
        .subscribe((res) => {
        })
    }
  };

  public getComments() {
    if (this.article) {
      var query = new ArticleCommentQuery();
      query.articleId = this.article.id;
      this._http.get<ArticleComment>(this._baseUrl + 'api/ArticleComment/GetCommentList?query='
        + encodeURIComponent(JSON.stringify(query)))
        .subscribe(result => {
          if (this.article) {
            this.article.comments = [];
            this.article.comments.fill(result);
          }
        }, error => console.error(error));
    }
  };


}
