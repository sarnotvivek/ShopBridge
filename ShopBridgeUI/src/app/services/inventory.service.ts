import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Inventory } from "../inventory.model";
import { map } from "rxjs/operators";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class InventoryService {
  item: any;
  baseUrl = `${environment.url}`;

  constructor(private http: HttpClient) {}

  addItem(item, fileData?) {
    const jsonString = JSON.stringify(item);
    const formData: FormData = new FormData();
    if (fileData) {
      formData.append("image", fileData);
    }
    formData.append("jsonString", jsonString);
    // Send Http request
    return this.http.post<any>(`${this.baseUrl}/AddItem`, formData);
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
