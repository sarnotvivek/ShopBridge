import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DetailsComponent } from './inventory/details/details.component';
import { AddItemComponent } from './inventory/add-item/add-item.component';


const routes: Routes = [
    { path: '', redirectTo: '/add-item', pathMatch: 'full' },
    {
      path: 'add-item',
      component: AddItemComponent,
     
    },
    { path: 'details', component: DetailsComponent },
  ];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
  })
export class AppRoutingModule {}