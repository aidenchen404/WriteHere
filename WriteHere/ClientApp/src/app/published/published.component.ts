import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ArticleRow, ArticleQuery } from '../types';

@Component({
  selector: 'app-published-component',
  templateUrl: './published.component.html',
})
export class PublishedComponent {
  private _baseUrl: string;
  private _http: HttpClient;
  public articles?: ArticleRow[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;
    this.getPublishedArticleRows();
  }

  public getPublishedArticleRows() {
    var query = new ArticleQuery();
    query.statusName = 'Published';

    this._http.get<ArticleRow[]>(this._baseUrl + 'api/Article/GetArticleRows/?queryString='
      + encodeURIComponent(JSON.stringify(query)))
      .subscribe(result => {
        this.articles = result;
      }, error => console.error(error));

  }
}
