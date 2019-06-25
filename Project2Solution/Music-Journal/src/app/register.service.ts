import { Injectable } from '@angular/core';
import { Person } from './models/person';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CATCH_ERROR_VAR } from '@angular/compiler/src/output/output_ast';



const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Authorization': 'my-auth-token',
  }) };

@Injectable({
  providedIn: 'root'
})

export class RegisterService {

  private basePath: string = "http://musicjournalapi.azurewebsites.net/api/";
  private personDocument: string ="person";
  private personDocumentPath : string = this.basePath + this.personDocument;


constructor(private http: HttpClient) { }

EnsureNewUsername(personAuth: Person, response: any) : boolean {
    let usertaken: boolean = false;

    for (var i in response) {

        let person = Object.assign(new Person(), response [i]) //assigns ith user for comparison

        if (personAuth.username == person.username) {
            console.log("username taken");

            usertaken = true;

            break
        }
    }

    return usertaken;
}

AddUser(person: Person) : Observable<Person> {
  return this.http.post<Person>(this.personDocumentPath, person, httpOptions)
    .pipe();
}

GetUsers() {
    return this.http.get(this.personDocumentPath,httpOptions).toPromise();
}

}

