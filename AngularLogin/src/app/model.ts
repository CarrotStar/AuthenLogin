export class LoginResult {
     result?:resultObj; 
}

export class resultObj {
   status?:boolean 
   userId?:number
   firstName:string = "";
   lastName:string = "";
   profileimg:string = "";
}