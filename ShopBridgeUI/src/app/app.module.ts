import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from "./app.component";
import { InventoryComponent } from "./inventory/inventory.component";
import { DetailsComponent } from "./inventory/details/details.component";
import { AppRoutingModule } from './app-routing.module';
import { AddItemComponent } from './inventory/add-item/add-item.component';

@NgModule({
  declarations: [
    AppComponent,
    InventoryComponent,
    AddItemComponent,
    DetailsComponent,
  ],
  imports: [
    BrowserModule, 
    FormsModule,
    HttpClientModule,
    AppRoutingModule,

    ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
