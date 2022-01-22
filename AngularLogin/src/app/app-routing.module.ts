import { SignUpComponent } from './sign-up/sign-up.component';

import { SignInComponent } from './sign-in/sign-in.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// const signModule = () => import('./sign-in/sign-in.module').then(x => x.SignModule);

const routes: Routes = [ 
  {path: '', component: SignInComponent, pathMatch:'full'},
  {path: 'SignUp', component: SignUpComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
