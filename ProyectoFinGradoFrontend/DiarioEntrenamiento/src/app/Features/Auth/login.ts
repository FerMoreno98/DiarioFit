import { Component } from '@angular/core';
import { FormBuilder, FormsModule, Validators } from '@angular/forms';
import { Service } from './Service/service';
import { Router, RouterLink, RouterLinkActive } from "@angular/router";

@Component({
  selector: 'app-login',
  standalone:true,
  imports: [FormsModule, RouterLinkActive,RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
constructor(private servicioLogin:Service,private router:Router){}
email:string="";
contrasena:string="";
  remember = false;
  pwdVisible = false;
MensajeError:string | null="";
  


  togglePwd(){ this.pwdVisible = !this.pwdVisible; }
login(){
  this.servicioLogin.VerificarUsuario(this.email,this.contrasena).subscribe({
    next: (res)=>{
        localStorage.setItem('JWT',res.token);
        this.router.navigate(['/home']);
    },
    error:(e)=>{
      if (e.status === 401) {
        console.log(e)
        this.MensajeError=e.error.message;
      } else if (e.status === 500) {
        console.error('Error del servidor');
      } else {
        console.error('Error desconocido', e);
      }
    }
  })
}
  social(provider: 'google'|'github'){
    console.log('Social login:', provider);
  }
}
