import { Injectable } from '@angular/core';
import { Person } from './models/person';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Song } from './models/song';
import {HttpParams} from  "@angular/common/http";



const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json; charset=utf-8',
    'Authorization': 'my-auth-token',
  }) };

  let headers={
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
}

@Injectable({
  providedIn: 'root'
})

export class SongService {


  private basePath: string = "https://localhost:5001/api/";
  private songDocument: string ="song/";
  private songDocumentPath : string = this.basePath + this.songDocument;

  constructor(private http: HttpClient) { }

  GetSong(song : Song) {

    let params : HttpParams = new  HttpParams();
    params.set('title', song.Title);
    params.set('artist', song.Artist);

    return this.http.get(this.songDocumentPath + song.Title + "/" + song.Artist ).toPromise();
  }

  AddSongReference(song : Song){

    console.log("Song Title: " + song.Title);

    this.http.post(this.songDocumentPath, song, headers)
      .subscribe(resp => {
      console.log("response %o, ", resp);
    });
  }

  AddSongStorage(song : any){


  }


}
