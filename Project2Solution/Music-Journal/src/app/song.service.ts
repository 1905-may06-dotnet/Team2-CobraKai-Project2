import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Song } from './models/song';
import {HttpParams} from  "@angular/common/http";


  let headers={
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'enctype':'multipart/form-data'
    })
}

@Injectable({
  providedIn: 'root'
})

export class SongService {


  private basePath: string = "http://musicjournalapi.azurewebsites.net/api/";
  private storageBasePath: string = "https://localhost:5001/api/";
  private storageDocument : string = "storage";
  private songDocument: string ="song/";
  private songDocumentPath : string = this.basePath + this.songDocument;
  private storageDocumentPath : string = this.storageBasePath + this.storageDocument;

  constructor(private http: HttpClient) { }

  GetSong(song : Song) {

    let params : HttpParams = new  HttpParams();
    params.set('title', song.Title);
    params.set('artist', song.Artist);

    return this.http.get(this.songDocumentPath + song.Title + "/" + song.Artist ).toPromise();
  }

  GetSongStreamLinks(songTitle : string){

    let params : HttpParams = new  HttpParams();
    params.set('title', songTitle);

    return this.http.get(this.storageDocumentPath + songTitle).toPromise();
  }

  AddSongReference(song : Song){

    console.log("Song Title: " + song.Title);

    this.http.post(this.songDocumentPath, song, headers)
      .subscribe(resp => {
     // console.log("response %o, ", resp);
    });
  }

  GetSongs() {
    return this.http.get(this.songDocumentPath).toPromise();
  }

  AddSongStorage(fileObject : FormData){

    this.http.post(this.storageDocumentPath, fileObject)
    .subscribe(resp => {
    console.log("response %o, ", resp);
  });
  }


}
