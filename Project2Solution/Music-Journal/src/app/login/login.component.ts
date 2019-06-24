import { Component, OnInit, Input } from '@angular/core';
import { Person } from '../models/person';
import {NgForm} from '@angular/forms';
import { LoginService } from '../login.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginService : LoginService, private router: Router) { }

  person: Person = {

    firstname: "",
    lastname: "",
    username: "",
    password: "",
    email: "",

  };

  @Input()isUserLoggedIn : boolean = false;

  onSubmit() {

    alert('Test');

    this.loginService.GetUsers().then(result => {
    let validate : boolean =  this.loginService.AuthenticateUser(this.person, result);

 

    if(validate == true){
      this.isUserLoggedIn == true;

      this.router.navigateByUrl('/home');


    }else {

    }

    });

   }


  ngOnInit() {

  }

}