import { Component, OnInit, Input } from '@angular/core';
import { InventoryService } from '../../services/inventory.service';


@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  isDetailsPresent: boolean = false;
   item: any;
  
  constructor( private inventoryService: InventoryService) {}

ngOnInit() {  
    this.item = this.inventoryService.item;
    if(this.item != null){
      this.isDetailsPresent = true;
    }
  }

}
