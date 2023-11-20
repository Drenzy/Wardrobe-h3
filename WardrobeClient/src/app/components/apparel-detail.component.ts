import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Apparel } from '../models/apparel';
import { ApparelService } from '../services/apparel.service';
import { ActivatedRoute, RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-apparel-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <h1> Titel: {{apparel.title | uppercase}}</h1>
    <h2> Beskrivelse: {{apparel.description}}</h2>
    <h3> Farve: {{apparel.color}}</h3>
    <a routerLink="/">Forside</a>
  `,
  styles: [
  ]
})
export class ApparelDetailComponent implements OnInit {
  apparel: Apparel = { id: 0, title: '', description: '', color: '', closetId: 0};
  constructor(private apparelService: ApparelService, private route: ActivatedRoute) {}
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.apparelService.findById(Number(params.get('apparelId')))
      .subscribe(x => this.apparel = x);
    })
  }
}
