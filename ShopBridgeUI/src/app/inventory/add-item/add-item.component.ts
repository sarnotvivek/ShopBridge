import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
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
  fileError = null;
  fileData: File = null;
  extension = null;
  base64Img = null;

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
    this.fileError = "";
    this.fileData=null;
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
      return;
    } else {
      if (this.fileData != null) {
        this.extension = this.fileData.type.split("/")[1];
        if (
          this.extension != "jpeg" &&
          this.extension != "png" &&
          this.extension !== "jpg"
        )  {
          this.fileError = "The file format is not supported!";
          return;
        }
      }
      this.isFetching = true;
      this.inventoryService.addItem(itemData, this.fileData).subscribe(() => {
        alert("Your item has been added successfully to the inventory!");
        this.getAllItems();
        this.onReset(form);
      });;
    
      
    }
  }
  getAllItems() {
    this.inventoryService.fetchItems().subscribe(
      (res: any) => {
        this.isFetching = false;
        this.loadedItems = res.resultObject != null ? res.resultObject : [];
        this.loadedItems.forEach(element => {
          element.imgUrl = element.imgUrl ==null || element.imgUrl ==""?null:("data:image/png;base64,"+element.imgUrl);
        });
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
      this.isFetching = true;
      this.inventoryService.deleteAllItems().subscribe(() => {
        alert("All the items are removed from the inventory!");
        this.getAllItems();
      });
    }
  }

  handleFileInput(fileInput) {
    this.fileData = fileInput.target.files[0];
    this.extension = this.fileData.type.split("/")[1];
    if (
      this.extension != "jpeg" &&
      this.extension != "png" &&
      this.extension !== "jpg"
    ) {
      this.fileError = "The file format is not supported!";
      return;
    } else {
      this.fileError = "";
    }
   
  }
  onDeleteItem(item) {
    var isDelete = confirm("Are you sure you want to delete this item?");
    if (isDelete == false) {
      return;
    } else {
      this.isFetching = true;
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
