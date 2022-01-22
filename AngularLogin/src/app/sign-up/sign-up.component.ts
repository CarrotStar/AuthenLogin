import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { LoginResult } from '../model';
import { User } from '../user';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent implements OnInit {
  txtUsername: string = '';
  UserId?: number;
  baseurl: string = '';
  txtname: string = '';
  txtfname: string = '';
  txtlname: string = '';
  txtpassword: string = '';
  txtCpassword: string = '';
  txtfile: string = '';
  txtfileSource: string = '';
  UserModel: User = new User();
  public IsLoginSuccess: boolean = false;
  submitted = false;
  Result: LoginResult = new LoginResult();
  imageSrc: string = '';
  selectfile: any = null;
  loading = false;
  myForm1 = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required]),
  });

  constructor(
    private httpClient: HttpClient,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.baseurl = 'https://localhost:5001/';
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  get f() {
    return this.myForm1.controls;
  }
  onFileChange(event: any) {
    const reader = new FileReader();
    this.selectfile = <File>event.target.files[0];
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.myForm1.patchValue({
          fileSource: reader.result,
        });
      };
    }
  }
  onSubmitform() {
    this.loading = true;
    var that = this;
    this.UserModel.UserId = this.UserId;
    this.UserModel.Username = this.txtname;
    this.UserModel.Password = this.txtpassword;
    this.UserModel.Firstname = this.txtfname;
    this.UserModel.Lastname = this.txtlname;
    this.UserModel.Profileimg = this.myForm1.value.fileSource;

    const df = new FormData();
    df.append('image', this.selectfile, this.selectfile.name);

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    let strjson = JSON.stringify(this.UserModel);
    this.httpClient
      .post<boolean>(this.baseurl + 'Users/insertUser', strjson, httpOptions)
      .toPromise()
      .then((res_bool) => {
        if (res_bool) {
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Your Profile has been saved',
            showConfirmButton: false,
            timer: 1500,
          }).then(function(){
            that.router.navigate(['']);

          });
        } else {
          Swal.fire({
            icon: 'error',
            title: "You Shouldn't Use the Same Password",
          });
          // that.IsLoginSuccess = false;
        }
      });
  }
  OnBtnBackClick(){
    this.router.navigate(['']);
  }
}
