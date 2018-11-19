import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  constructor(private http: HttpClient) { }

  public Search(query: string): any {
    return this.http.get(`${environment.server.url}/api/search?query=${query}`); 
  }
}
