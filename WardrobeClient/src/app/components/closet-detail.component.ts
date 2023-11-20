import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Closet } from '../models/closet';
import { ClosetService } from '../services/closet.service';

@Component({
  selector: 'app-closet-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <h1> Titel: {{closet.name | uppercase}}</h1>
    <a routerLink="/">Forside</a>
  `,
  styles: [
  ]
})
export class ClosetDetailComponent implements OnInit {
  closet: Closet = { id: 0, name: ''};
  constructor(private closetService: ClosetService, private route: ActivatedRoute) {}
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.closetService.findById(Number(params.get('closetId')))
      .subscribe(x => this.closet = x);
    })
  }
}
