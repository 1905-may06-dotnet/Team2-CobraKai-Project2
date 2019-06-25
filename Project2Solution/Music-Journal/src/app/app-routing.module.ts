import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { PlaylistComponent } from './playlist/playlist.component';
import { RegisterComponent } from './register/register.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { SignoutComponent } from './signout/signout.component';

const routes: Routes = [

  { path: '', redirectTo: '/Login', pathMatch: 'full',},
  { path: 'Home', component: HomeComponent },
  { path: 'Login', component: LoginComponent },
  { path: 'Playlist', component: PlaylistComponent },
  { path: 'Register', component: RegisterComponent},
  { path: 'About Us', component: AboutusComponent},
  { path: 'Sign Out', component: SignoutComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
