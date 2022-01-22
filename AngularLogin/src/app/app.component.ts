import { LoginResult } from './model';

import { User } from './user';
import { Component } from '@angular/core';
import { FormControl,Validators, FormBuilder,FormGroup } from '@angular/forms';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngularLogin';
  txtUsername:string = "";  
  UserId?:number;  
  baseurl:string = "";
  txtname:string = "";  
  txtfname:string = "";  
  txtlname:string = "";  
  txtpassword:string = "";
  txtfile:string = "";
  txtfileSource:string = "";
  UserModel: User = new User;
  public IsLoginSuccess:boolean = false;
  submitted = false;
  Result: LoginResult= new LoginResult; 
  imageSrc: string = "";
  selectfile: any  = null;
  myForm1 = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required])
  });

  
  constructor(private httpClient: HttpClient, 
    private route: ActivatedRoute,
    private router: Router ) {
    this.baseurl = "https://localhost:5001/";
    
  }
  get f(){
    return this.myForm1.controls;
  }
  onFileChange(event: any) {
    const reader = new FileReader();
    this.selectfile = <File>event.target.files[0];
    if(event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.myForm1.patchValue({
          fileSource: reader.result
        });
   
      };
    }
  }
  onSubmit(): void {
    
    var that = this;
    this.UserModel.Username = this.txtname
    this.UserModel.Password = this.txtpassword
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };


    let strjson = JSON.stringify(this.UserModel);

    this.httpClient.post<LoginResult> 
    (this.baseurl + "Users/Login",
        strjson, httpOptions)
      .toPromise().then(
        Result => {
          console.log(Result);
          
          if (Result?.result?.status) {
            // alert(Result.result.firstName)
            const Toast = Swal.mixin({
              toast: true,
              position: 'top-end',
              showConfirmButton: false,
              timer: 3000,
              timerProgressBar: true,
              didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer)
                toast.addEventListener('mouseleave', Swal.resumeTimer)
              }
            })
            
            Toast.fire({
              icon: 'success',
              title: 'Signed in successfully'
            })
            that.IsLoginSuccess = true;
            this.UserId = Result.result.userId
            this.txtfname =Result.result.firstName
            this.txtlname =Result.result.lastName
            this.imageSrc =Result.result.profileimg
            this.txtpassword = "";
          }
          else {
            Swal.fire({
              icon: 'error',
              title: 'Please try again',
              text: 'Username or password wrong!',
            })
            that.IsLoginSuccess = false;
          }

        }
      )
  }
  OnBtnSignupClick(){
    this.router.navigate(['/SignUp']);
  }
  onSubmitform(){
    var that = this;
    this.UserModel.UserId = this.UserId
    this.UserModel.Username = this.txtname
    this.UserModel.Password = this.txtpassword
    this.UserModel.Firstname = this.txtfname
    this.UserModel.Lastname = this.txtlname
    this.UserModel.Profileimg = this.myForm1.value.fileSource;
    
   const df = new FormData();
   df.append('image',this.selectfile,this.selectfile.name);
    
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    let strjson = JSON.stringify(this.UserModel);
    this.httpClient
      .post<boolean>(this.baseurl + "Users/UpdateUser",
        strjson, httpOptions)
      .toPromise().then(
        res_bool => {
          
          if (res_bool) {
            alert(res_bool)
            Swal.fire({
              position: 'center',
              icon: 'success',
              title: 'Your Profile has been saved',
              showConfirmButton: false,
              timer: 1500
            })          }
          else {
            Swal.fire({
              icon: 'error',
              title: "You Shouldn't Use the Same Password",
            })
                        // that.IsLoginSuccess = false;
          }

        }
      )
  }
  
}
