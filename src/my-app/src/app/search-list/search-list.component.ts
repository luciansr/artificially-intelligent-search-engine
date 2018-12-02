import { Component, OnInit } from '@angular/core';
import { SearchService } from '../search.service';

@Component({
  selector: 'app-search-list',
  templateUrl: './search-list.component.html',
  styleUrls: ['./search-list.component.css']
})
export class SearchListComponent implements OnInit {
  searchItems: any;

  constructor(private searchService: SearchService) {
  }

  ngOnInit() {
    this.subscribeOnQueryResults();
  }

  private subscribeOnQueryResults() {
    this.searchService.queryResponseObservable().subscribe((data: any[]) => {
      if (data) {
        data.sort((a, b) => {
          return b.leadInQuery - a.leadInQuery;
        });
      }
      this.searchItems = data;
    })
  }

}
