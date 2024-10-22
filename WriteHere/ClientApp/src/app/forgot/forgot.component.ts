import { Component } from '@angular/core';

@Component({
  selector: 'app-forgot-component',
  templateUrl: './forgot.component.html',
//    styleUrls: ['.././site.component.css']
})
export class ForgotComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
