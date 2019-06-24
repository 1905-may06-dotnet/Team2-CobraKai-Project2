import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../register.service';
import { Router } from '@angular/router';
import { Person } from '../models/person';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private registerservice: RegisterService, private router: Router) { }

  person: Person = {
    firstname: "",
    lastname: "",
    username: "",
    password: "",
    email: "",
  };

  onSubmit() : void {

    this.registerservice.GetUsers().then(result => {
      let validate : boolean = this.registerservice.EnsureNewUsername(this.person, result);

      if (validate == true) {
        alert('Username is taken. Please choose a different username.');
        //username is taken, please try again
      }
      else {
        //user will be registered
        this.registerservice.AddUser(this.person)
        .subscribe();
        //user will be navigated to the home page
        this.router.navigateByUrl('/Home');
      }
    });


  }

  onClickBack() {
    this.router.navigateByUrl('/login');
  }

  ngOnInit() {
  }

}
