import { Injectable } from '@angular/core';
import { Person } from './models/person';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';



const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': '*',
    'Authorization': 'my-auth-token',
  }) };

@Injectable({
  providedIn: 'root'
})

export class LoginService {

  private basePath: string = "https://localhost:5001/api/";
  private personDocument: string ="person";
  private personDocumentPath : string = this.basePath + this.personDocument;


constructor(private http: HttpClient, private router: Router) { }



AuthenticateUser(personAuth: Person, response: any) : boolean {

  let validate: boolean = false;

    for(var i in response){

      let person = Object.assign(new Person(), response [i])

      if(personAuth.username == person.username
        && personAuth.password == person.password){

          validate = true;

          break
      }
    }

    return validate;
}

RedirectToRegister() {
  this.router.navigateByUrl('/register');
}

GetUsers() {

  return this.http.get(this.personDocumentPath,httpOptions).toPromise();
}

}

