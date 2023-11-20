import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Closet } from '../models/closet';
import { ClosetService } from '../services/closet.service';

@Component({
  selector: 'app-admin-closet',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h1>Admin Panel Closet</h1>
  Navn på skab <input [(ngModel)]="closet.name"><br>
  <button (click)="save()">Gem</button>
  <button (click)="cancel()">Anuller</button>

  <table>
    <tr>
      <th>Handling</th>
      <th>Id</th>
      <th>Navn</th>
    </tr>
    <tr *ngFor="let closet of closets">
      <th>
        <button (click)="edit(closet)">Ret</button>
        <button (click)="delete(closet)">Slet</button>
      </th>
      <th>{{closet.id}}</th>
      <th>{{closet.name}}</th>
    </tr>
  </table>
  `,
  styles: [
  ]
})
export class AdminClosetComponent implements OnInit {
  
  closets: Closet[] = [];
  closet: Closet = this.resetCloset();

  constructor(private closetService:ClosetService) {}

  resetCloset(): Closet {
    return {id: 0, name: ''};
  }

  ngOnInit(): void {
    this.closetService.getAll().subscribe(x => this.closets = x);
  }

  delete(closet:Closet): void{
    if(confirm('Er du sikker på at du vil slette ' + closet.name + '?')) {
      this.closetService.delete(closet.id).subscribe(() => {
        this.closets = this.closets.filter(x => x.id != closet.id);
      });
    }
  }

  save(): void{
    if (this.closet.id == 0) {
      this.closetService.create(this.closet).subscribe({
        next: (x) => {
          this.closets.push(x);
          this.closet = this.resetCloset();
        },
        error: (err) => {
          console.error(Object.values(err.error.errors).join(', '));
        }
      })
    } else {
      this.closetService.update(this.closet).subscribe({
        error: (err) => {
          console.error(Object.values(err.error.errors).join(', '));
        },
        complete: () => {
          this.closet = this.resetCloset();
          this.closetService.getAll().subscribe(x => this.closets = x);
        }
      })
    }
  }
  cancel(): void{
    this.closet = this.resetCloset();
  }

  edit(closet: Closet): void{
    Object.assign(this.closet, closet);
  }
}
