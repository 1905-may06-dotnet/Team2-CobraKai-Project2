import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';

@Component({
  selector: 'app-playlist',
  templateUrl: './playlist.component.html',
  styleUrls: ['./playlist.component.css']
})
export class PlaylistComponent implements OnInit {

  constructor() { }

  ngOnInit() {

  }

  toggleAccordion() {

    console.log("Fred Brume");

    $('.accordion').next().toggle(function() {
      $(this).animate({
        height: 140
      }, 100).css('opacity', 1);
    });
  }

}
