import { NgModule } from '@angular/core';
import { RouterModule, Routes,Router } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { HomepageComponent } from './homepage/homepage.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { NewspageComponent } from './newspage/newspage.component';

const routes: Routes = [
  {path : 'login', component:LoginComponent},
  {path : 'register', component:RegistrationComponent},
  {path : '',component:HomepageComponent},
  {path : 'home',component:NewspageComponent},
  {path : 'wishlist',component:WishlistComponent},
  {path : 'signout',component:HomepageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
