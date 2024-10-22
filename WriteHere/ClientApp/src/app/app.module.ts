//import { BrowserModule } from '@angular/platform-browser';
//import { NgModule } from '@angular/core';
//import { FormsModule } from '@angular/forms';
//import { HttpClientModule } from '@angular/common/http';
//import { RouterModule } from '@angular/router';

//import { AppComponent } from './app.component';
//import { NavMenuComponent } from './nav-menu/nav-menu.component';
//import { HomeComponent } from './home/home.component';
//import { CounterComponent } from './counter/counter.component';
//import { FetchDataComponent } from './fetch-data/fetch-data.component';

//@NgModule({
//  declarations: [
//    AppComponent,
//    NavMenuComponent,
//    HomeComponent,
//    CounterComponent,
//    FetchDataComponent
//  ],
//  imports: [
//    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
//    HttpClientModule,
//    FormsModule,
//    RouterModule.forRoot([
//      { path: '', component: HomeComponent, pathMatch: 'full' },
//      { path: 'counter', component: CounterComponent },
//      { path: 'fetch-data', component: FetchDataComponent },
//    ])
//  ],
//  providers: [],
//  bootstrap: [AppComponent]
//})
//export class AppModule { }
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'

@Injectable()
export class ConfigService {
  constructor(private http: HttpClient) { }
}

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ForgotComponent } from './forgot/forgot.component';
import { AboutUsComponent } from './aboutus/aboutus.component';
import { MemberdashboardComponent } from './memberdashboard/memberdashboard.component';
import { PublishedComponent } from './published/published.component';
import { RegisterComponent } from './register/register.component';
import { FileUploadComponent } from './fileupload/fileupload.component';
import { HallOFameComponent } from './hallofame/hallofame.component';
import { ArticleDetailsComponent } from './articledetails/articledetails.component';
import { UserListComponent } from './userlist/userlist.component';
import { UserDetailsComponent } from './userdetails/userdetails.component';

import { FetchDataComponent } from './fetch-data/fetch-data.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    MemberdashboardComponent,
    PublishedComponent,
    LoginComponent,
    ForgotComponent,
    AboutUsComponent,
    RegisterComponent,
    FileUploadComponent,
    ArticleDetailsComponent,
    HallOFameComponent,
    UserListComponent,
    UserDetailsComponent,

    FetchDataComponent,

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'forgot', component: ForgotComponent },
      { path: 'fileupload', component: FileUploadComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'aboutus', component: AboutUsComponent },
      { path: 'published', component: PublishedComponent },
      { path: 'memberdashboard', component: MemberdashboardComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'articledetails', component: ArticleDetailsComponent },
      { path: 'hallofame', component: HallOFameComponent },
      { path: 'userlist', component: UserListComponent },
      { path: 'userdetails', component: UserDetailsComponent },

      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
