import { ViewChild, Component, OnInit, AfterViewInit } from '@angular/core';
import { SongComponent } from '../song/song.component';
import * as $ from 'jquery';

@Component({
  selector: 'app-playlist',
  templateUrl: './playlist.component.html',
  styleUrls: ['./playlist.component.css'],

})

export class PlaylistComponent implements OnInit, AfterViewInit {

  show : boolean = false;

  ngAfterViewInit(): void {

    console.log(SongComponent.name);
  }

   @ViewChild(SongComponent, {static: false}) child !: SongComponent;

  constructor() { }

  ngOnInit() {

  }

  toggleAccordion() {

    $('.accordion').next().toggle(function() {
      $(this).animate({
        height: 140
      }, 100).css('opacity', 1);
    });
  }

}
