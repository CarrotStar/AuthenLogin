import { SignInComponent } from './sign-in.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// import { LayoutComponent } from '';

const routes: Routes = [
    {
        // path: '',
        // children: [
        //     { path: 'Signin', component: SignInComponent },
        // ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AccountRoutingModule { }