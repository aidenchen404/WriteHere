import { Component, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { HallOfFame, HallOfFamePack } from '../types';

@Component({
  selector: 'app-hallofame-component',
  templateUrl: './hallofame.component.html',
})
export class HallOFameComponent {
  private _baseUrl: string;
  private _http: HttpClient;

  public hallOfFamePack?: HallOfFamePack;
  public accordionExpand: boolean[];
  public accordionCount = 10;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;
    
  
    this.accordionExpand = new Array(this.accordionCount).fill(false);
    this.getHallOfFamePackByQuery();
  }

  public setAccordionSign(index:number)
  {
    this.accordionExpand[index] = !this.accordionExpand[index];
   }

  public getAccordionSign(index: number)
  {
    if (this.accordionExpand[index]) { return '-'; } else { return '+';}
  }

  public getHallOfFamePackByQuery() {
    var query = null;// new HallOfFameQuery();
    this._http.get<HallOfFamePack>(this._baseUrl + 'api/Article/GetHallOfFamePack/?queryString=' + encodeURIComponent(JSON.stringify(query)))
      .subscribe(result => {
        this.hallOfFamePack = result ;
      }, error => console.error(error));

  }


}
