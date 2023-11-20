import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Apparel } from '../models/apparel';
import { FormsModule } from '@angular/forms';
import { ApparelService } from '../services/apparel.service';
import { Closet } from '../models/closet';
import { ClosetService } from '../services/closet.service';

@Component({
  selector: 'app-admin-apparel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
  <h1>Admin Panel</h1>
  Titel <input [(ngModel)]="apparel.title"><br>
  Beskrivelse <textarea [(ngModel)]="apparel.description"></textarea><br>
  Farve <input [(ngModel)]="apparel.color"><br>
  SkabsId <input [(ngModel)]="apparel.closetId"><br>
  <button (click)="save()">Gem</button>
  <button (click)="cancel()">Anuller</button>

  <table>
    <tr>
      <th>Handling</th>
      <th>Id</th>
      <th>Titel</th>
      <th>Beskrivelse</th>
      <th>Farve</th>
      <th>SkabsId</th>
    </tr>
    <tr *ngFor="let apparel of apparels">
      <th>
        <button (click)="edit(apparel)">Ret</button>
        <button (click)="delete(apparel)">Slet</button>
      </th>
      <th>{{apparel.id}}</th>
      <th>{{apparel.title}}</th>
      <th>{{apparel.description}}</th>
      <th>{{apparel.color}}</th>
      <th>{{apparel.closetId}}</th>
    </tr>
    <tr></tr>
  </table>
  `,
  styles: [
  ]
})
export class AdminApparelComponent implements OnInit {
  apparels: Apparel[] = [];
  apparel: Apparel = this.resetApparel();


  closet: Closet[] = [];
  closets: Closet = this.resetCloset();


  constructor(private apparelService: ApparelService, private closetService: ClosetService){}

  resetCloset(): Closet {
    return {id: 0, name: ''};
  }

  resetApparel(): Apparel {
    return {id: 0, title: '', description: '', color: '', closetId: 0};
  }

  ngOnInit(): void {
    this.apparelService.getAll().subscribe(x => this.apparels = x);
    this.closetService.getAll().subscribe(x => this.closet = x);
  }

  delete(apparel:Apparel): void{
    if(confirm('Er du sikker på at du vil slette ' + apparel.title + '?')) {
      this.apparelService.delete(apparel.id).subscribe(() => {
        this.apparels = this.apparels.filter(x => x.id != apparel.id);
      });
    }
  }

  save(): void{
    if (this.apparel.id == 0) {
      this.apparelService.create(this.apparel).subscribe({
        next: (x) => {
          this.apparels.push(x);
          this.apparel = this.resetApparel();
        },
        error: (err) => {
          console.error(Object.values(err.error.errors).join(', '));
        }
      })
    } else {
      this.apparelService.update(this.apparel).subscribe({
        error: (err) => {
          console.error(Object.values(err.error.errors).join(', '));
        },
        complete: () => {
          this.apparel = this.resetApparel();
          this.apparelService.getAll().subscribe(x => this.apparels = x);
        }
      })
    }
  }
  cancel(): void{
    this.apparel = this.resetApparel();
  }

  edit(apparel: Apparel): void{
    Object.assign(this.apparel, apparel);
  }

  
}
