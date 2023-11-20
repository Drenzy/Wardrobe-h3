import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
  <h1> Min hjemmeside</h1>
  <nav>
    <a routerLink="/">Home</a> |
    <a routerLink="/admin/apparel">Apparel</a> |
    <a routerLink="/admin/apparel-reactive">Apparel Reactive</a> |
    <a routerLink="/admin/closet">Closet</a>
  </nav>
  <router-outlet></router-outlet>`,
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'WardrobeClient';
}
