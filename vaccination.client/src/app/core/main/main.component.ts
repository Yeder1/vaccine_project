import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  isOpenMenu: boolean = true;

  constructor() {}

  ngOnInit() {}

  openChanged($event: boolean) {
    this.isOpenMenu = $event;
  }
}
