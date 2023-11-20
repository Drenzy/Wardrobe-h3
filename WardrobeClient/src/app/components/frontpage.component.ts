import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Apparel } from '../models/apparel';
import { ApparelService } from '../services/apparel.service';
import { RouterModule } from '@angular/router';
import { Closet } from '../models/closet';
import { ClosetService } from '../services/closet.service';

@Component({
  selector: 'app-frontpage',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
  <h1>Wardrobe</h1>
    <div *ngFor="let apparel of apparels">
    <h3><a [routerLink]="['apparel', apparel.id]">{{ apparel.title }}</a></h3>    
      <p>{{apparel.description}}</p>
      <p>{{apparel.color}}</p>
    </div>
    <div *ngFor="let closet of closets">
      <h2> <a [routerLink]="['closet', closet.id]">{{ closet.name }}</a> </h2>
    </div>
  `,
  styles: [
  ]
})
export class FrontpageComponent implements OnInit {
  apparels: Apparel[] = [];

  closets: Closet[] = [];


  constructor(private apparelService: ApparelService, private closetService: ClosetService){}


  
  ngOnInit(): void {
    this.apparelService.getAll().subscribe(x => this.apparels = x);

    this.closetService.getAll().subscribe(x => this.closets = x);

  }
}
