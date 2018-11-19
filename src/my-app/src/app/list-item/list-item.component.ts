import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-list-item',
  inputs:['item'],
  templateUrl: './list-item.component.html',
  styleUrls: ['./list-item.component.css']
})
export class ListItemComponent implements OnInit {
  item: any;

  constructor() { }

  ngOnInit() {
  }

}
