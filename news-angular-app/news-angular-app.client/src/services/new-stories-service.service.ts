import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Story {
  title: string;
  url: string;
}

@Injectable({
  providedIn: 'root'
})
export class NewStoriesServiceService {
  private apiUrl = 'https://localhost:44332/HackerNews/';

  constructor(private http: HttpClient) { }
  getNewestStoriesCount(): Observable<number[]> {
    return this.http.get<any[]>(this.apiUrl +'storycount');
  }
  getNewestStories(page: number, pageSize: number): Observable<any[]> {
    const params = new HttpParams()
      .set('page', page.toString()) 
      .set('pageSize', pageSize.toString());

    return this.http.get<any[]>(this.apiUrl + 'storydetails', { params });  
  }
}


