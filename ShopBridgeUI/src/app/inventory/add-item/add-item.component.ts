import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Inventory } from "../../inventory.model";
import { InventoryService } from "src/app/services/inventory.service";

@Component({
  selector: "app-add-item",
  templateUrl: "./add-item.component.html",
  styleUrls: ["./add-item.component.css"],
})
export class AddItemComponent implements OnInit {
  loadedItems: Inventory[] = [];
  isFetching = false;
  error = null;

  constructor(
    private http: HttpClient,
    private inventoryService: InventoryService
  ) {}

  ngOnInit() {
    this.isFetching = true;
    this.getAllItems();
  }
  onReset(form) {
    form.resetForm();
  }
  onCreateItem(form) {
    // Send Http request
    var itemData = form.value;

    if (
      itemData == null ||
      itemData.price == null ||
      itemData.price == "" ||
      itemData.description == null ||
      itemData.description == "" ||
      itemData.name == "" ||
      itemData.name == null
    ) {
      alert("Please fill in the details!");
    } else {
      this.inventoryService.addItem(itemData);
      alert("Your item has been added successfully to the inventory!");
      this.getAllItems();
      form.resetForm();
    }
  }

  getAllItems() {
    this.inventoryService.fetchItems().subscribe(
      (res: any) => {
        this.isFetching = false;
        this.loadedItems =res.resultObject!=null? res.resultObject:[];
      },
      (error) => {
        this.error = error.message;
      }
    );
  }

  onDeleteAllItems() {
    var isDelete = confirm("Are you sure you want to delete all the items?");
    if (isDelete == false) {
      return;
    } else {
      this.inventoryService.deleteAllItems().subscribe(() => {
        alert("All the items are removed from the inventory!");
        this.getAllItems();
      });
    }
  }

  onDeleteItem(item) {
    var isDelete = confirm("Are you sure you want to delete this item?");
    if (isDelete == false) {
      return;
    } else {
      this.inventoryService.deleteItem(item.id).subscribe(() => {
        alert("Your item has been deleted successfully from the inventory!");
        this.getAllItems();
      });
    }
  }

  sendItem(item) {
    this.inventoryService.sendItem(item);
  }
}
