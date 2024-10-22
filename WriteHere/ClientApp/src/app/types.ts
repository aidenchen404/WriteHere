
import { Injectable } from '@angular/core';

Injectable()


// Dev Note: Variable names must match these in api side,
// excpet let the first letter in lower case


export class ArticleComment {
  id: string='';
  comment: string = '';
  commentOwnerId: string ='';
  commentOwnerPenName: string ='';
  articleId: string ='';
  articleTitle: string ='';
  showOwner: boolean=true;
  commentOwnerShowProfile: boolean=false;
  commentDate: string ='';
}

export class ArticleCommentQuery {
  commentOwnerId: string = '';
  articleId: string = '';
  pastMonths: number = 6;
}

export class ArticleVote {
  vote: number = 0;
  userId: string = '';
  articleId: string = '';
}

export class Article {
  comments?: ArticleComment[];
  id: string = '';
  title: string = '';
  subtitle: string = '';
  summary: string = '';
  content: string = '';
  editorReviewNote: string = '';
  genre: string = '';
  genreId: number = 0;   // a short

  authorUserId: string = '';  // a GUID
  authorIsPublicProfile: boolean = false;
  authorPenName: string = '';
  articleStatus: string = '';

  requestDrawer: boolean = false;
  bookCover: string = '';
  viewedCount: number = 0;
  upVote: number = 0;
  downVote: number = 0;
  commentCount: number = 0;

  viewerVote: number = 0;

  editorUserId: string = '';
  editorUserName: string = '';
  editorAssignedDate?: Date ;
  tutorUserId: string = '';
  tutorUserName: string = '';
  tutorAssignedDate?: Date;
  drawerUserId: string = '';
  drawerUserName: string = '';
  drawerAssignedDate?: Date;
}

export class ArticleRow {
  id: string ='';
  title: string ='';
  genre: string ='';

  authorPenName: string ='';
  authorIsPublicProfile : boolean=false;
  authorUserId: string ='';
  articleStatus: string ='';

  viewedCount: number=0;
  upVote: number = 0;
  downVote: number = 0;
  commentCount: number = 0;
}

export class ArticleQuery {
  genre: string = '';
  authorUserId: string = '';
  editorUserId: string = '';
  votedUpByUserId: string = '';
  commentedByUserId: string = '';
  isViewerAdmin: boolean = false;
  statusName: string = '';
}

export class ArticleSearch {
  field: string =''; // title, subtitle, summary, authorPenName, genre, editorUserName, viewedCount, upvote, downVote
  optor: string =''; //  Contain, greater than, equal, less than
  value: string ='';
}

export class ArticleOrderby {
  field: string = ''; // title, subtitle, summary, authorPenName, genre, editorUserName, viewedCount, upvote, downVote
  asc: boolean = true; // 1: asc, 0: desc
}

export class User {
  id: string ='';
  userName: string ='';
  penName: string ='';
  inactive: boolean = false;
  showProfile: boolean = false;
  showInHall: boolean = false;

  email: string ='';
  showEmail: boolean = false;
  loginPassword: string ='';
  firstName: string ='';
  lastName: string ='';
  showName: boolean = true;
  grade: number=0;
  showGrade: boolean = false;
  country: string ='';
  showCountry: boolean = true;
  state: string ='';
  showState: boolean = true;
  

  isAdmin: boolean = false;
  isReader: boolean = true;
  isWriter: boolean = false;
  writerAd: string = '';
  requestingWriter: boolean = false;
  requestWriterDeclined: boolean = false;
  isEditor: boolean = false;
  editorAd: string ='';
  requestingEditor: boolean = false;
  requestEditorDeclined: boolean = false;
  isAuditor: boolean = false;
  requestingAuditor: boolean = false;
  requestAuditorDeclined: boolean = false;
  isDrawer: boolean = false;
  drawerAd: string ='';
  requestingDrawer: boolean = false;
  requestDrawerDeclined: boolean = false;
  isTutor: boolean = false;
  tutorAd: string ='';
  requestingTutor: boolean = false;
  requestTutorDeclined: boolean = false;
}


export class StandardResponse {
  success: boolean=true;
  message: string ='';
}

export class ArticleStatusHistory {
  articleID: string ='';
  articleStatusID: string ='';
}

export class HallOfFame {
  articleId: string ='';
  authorUserId: string ='';
  articleTitle: string ='';
  personName: string ='';
  rankCount: number = 0;
  rowNumber: number = 0;
}

export class HallOfFamePack {
  mostVoteUp?: HallOfFame[];
  mostVoteDown?: HallOfFame[];
  mostCommented?: HallOfFame[];
  mostViewed?: HallOfFame[];
  mostPublished?: HallOfFame[];
  mostRejected?: HallOfFame[];
  mostUprising?: HallOfFame[];
}

export class ArticleAssignment {
  articleId: string = '';
  articleTitle: string = '';
  authorId: string = '';
  authorPenName: string = '';
  authorIsPublicProfile: boolean=false;
  assignedDate: string = '';
  assignPurpose: string = '';  // lookup
  assignPurposeCode: number = 0;
  assignedUserId: string = ''; // according to value in assignPurpose , display different
}

export class DashboardPack {
  writerArticles?: ArticleRow[]       // Writer Role
  readerLikedArticles?: ArticleRow[]  // Reader Role
  editorAssignments?: ArticleAssignment[]  // Editor Role
  tutorAssignments?: ArticleAssignment[]  // Tutor Role
  drawerAssignments?: ArticleAssignment[]  // Drawer Role
  auditorAssignments?: ArticleAssignment[]  // Auditor Role
  adminRoleRequests?: ArticleAssignment[]  // Admin Role
}

export interface LookupPack {
  genre: CommonValue[]
  assignPurpose: CommonValue[]
}

export interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
  value: number;
  text: string;
}

export interface CommonString {
  value: string;
  text: string;
}
export interface CommonValue {
  value: number;
  text: string;
}
