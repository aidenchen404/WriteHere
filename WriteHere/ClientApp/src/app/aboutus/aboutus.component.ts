import { Component } from '@angular/core';

@Component({
  selector: 'app-aboutus-component',
  templateUrl: './aboutus.component.html',
  //styleUrls: ['./site.css']
  //styleUrls: ['.././site.component.css']
})
export class AboutUsComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
