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

  onClickRegister() {
    this.router.navigateByUrl('/Register');
  }

  onSubmit() {

    this.loginService.GetUsers().then(result => {
    let validate : boolean =  this.loginService.AuthenticateUser(this.person, result);
 

    if(validate == true){
      this.isUserLoggedIn == true;

      this.router.navigateByUrl('/Home');
      
      alert('Welcome back, '+this.person.username+'!');

    }else {
      alert('Incorrect username or password.');
    }
    
    });

   }


  ngOnInit() {

  }

}