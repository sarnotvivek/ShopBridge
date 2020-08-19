import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public clickedEvent: Event;
 

  ngOnInit() {
   
  }
  childEventClicked(event: Event) {
    this.clickedEvent = event;
  }

  
  
}
