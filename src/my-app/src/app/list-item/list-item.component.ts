import { Component, OnInit } from '@angular/core';
import { SearchService } from '../search.service';

interface Offer {
  id: number;
  title: string;
  width: number;
  height: number;
  weight: number;
}
interface NeuralItemResult {
  neuralOrder: number;
  leadInQuery: number;
  item: Offer;
}
@Component({
  selector: 'app-list-item',
  inputs:['item'],
  templateUrl: './list-item.component.html',
  styleUrls: ['./list-item.component.css']
})
export class ListItemComponent implements OnInit {
  item: NeuralItemResult;
  offer: Offer;

  constructor(private searchService: SearchService) { 
    
  }

  ngOnInit() {
    this.offer = this.item.item;
  }

  itemClicked() {
    this.searchService.itemClicked(this.item.item.id);
  }

}
