import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Inventory } from "../inventory.model";
import { map } from "rxjs/operators";
import { environment } from "../../environments/environment";

@Injectable({ providedIn: "root" })
export class InventoryService {
  item: any;
  baseUrl = `${environment.url}`;

  constructor(private http: HttpClient) {}

  addItem(item: Inventory) {
    // Send Http request
    this.http
      .post<any>(`${this.baseUrl}/AddItem`, item)
      .subscribe((responseData) => {
        console.log(responseData);
      });
  }

  fetchItems() {
    return this.http.get(`${this.baseUrl}/GetAllItems`);
  }

  deleteItem(id) {
    return this.http.delete(`${this.baseUrl}/DeleteItem/${id}`);
  }

  deleteAllItems() {
    return this.http.delete(`${this.baseUrl}/DeleteAllItems`);
  }

  sendItem(item) {
    this.item = item;
  }
}
