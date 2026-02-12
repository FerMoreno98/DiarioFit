import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Login } from "./Features/Auth/login";
import { Loginlayout } from "./layouts/loginlayout/loginlayout";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Login, Loginlayout],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('DiarioEntrenamiento');
}
