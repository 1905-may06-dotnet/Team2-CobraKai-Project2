import { ViewChild, Component, OnInit, AfterViewInit, Pipe, PipeTransform } from '@angular/core';
import { SongComponent } from '../song/song.component';
import * as $ from 'jquery';
import { SongService } from '../song.service';
import { Song } from '../models/song';


@Pipe({name: 'keys'})

@Component({
  selector: 'app-playlist',
  templateUrl: './playlist.component.html',
  styleUrls: ['./playlist.component.css'],

})

export class PlaylistComponent implements OnInit, AfterViewInit {



  show : string = "";

  ngAfterViewInit(): void {

    console.log(SongComponent.name);
  }

   @ViewChild(SongComponent, {static: false}) child !: SongComponent;

  constructor(private songService: SongService) {

    this.loadSongs();
   }

   songs : Song[] = [];

  ngOnInit() {

  }

  loadSongs(){

    this.songService.GetSongs().then((result)=>{

      if(result != null){

          for(var i in result){

            let song = Object.assign(new Song(), result [i])

            console.log(song);
            this.songs.push(song);
          }
      }
    });
  }

  toggleAccordion() {

    $('.accordion').next().toggle(function() {
      $(this).animate({
        height: 140
      }, 100).css('opacity', 1);
    });
  }



}
