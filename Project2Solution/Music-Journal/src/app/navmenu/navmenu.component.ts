import { Component, OnInit } from '@angular/core';
import {navmenuItem} from '../home_menu';

@Component({
  selector: 'app-navmenu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css']
})
export class NavmenuComponent implements OnInit {

 menu: string[] = navmenuItem;

  constructor() { }

  ngOnInit() {
  }

}
