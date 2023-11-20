import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Apparel } from '../models/apparel';
import { ApparelService } from '../services/apparel.service';

@Component({
  selector: 'app-admin-apparel-reactiv-forms',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-apparel-reactiv-forms.component.html',
  styles: [`
.formControl label{
display: inline-block;
width:85px;
text-align:right;
}
.formControl{
margin:7px 0;
}
input[type='text'], textarea{
width:200px;
margin:0 3px;
border: solid 2px #333;
}
input.ng-invalid.ng-touched, textarea.ng-invalid.ng-touched{
border: solid 2px #f00;
}
`]
})
export class AdminApparelReactivFormsComponent implements OnInit {
  apparels: Apparel[] = [];
  apparel: Apparel = this.resetApparel();
  apparelId: number = 0;
  message: string = '';
  form: FormGroup = this.resetForm();

  constructor(private apparelService: ApparelService) { }
  ngOnInit(): void {
    this.apparelService.getAll().subscribe(x => this.apparels = x);
  }

  resetApparel(): Apparel {
    return { id: 0, title: '', description: '', color: '', closetId: 0 };
  }

  resetForm(): FormGroup {
    return new FormGroup({
    title: new FormControl(null, Validators.required),
    description: new FormControl(null, Validators.required),
    color: new FormControl(null, Validators.required)
    });
  }

  cancel(): void {
    this.apparelId = 0;
    this.apparel = this.resetApparel();
    this.form = this.resetForm();
    }

  delete(apparel: Apparel): void {
      if (confirm('Er du sikker på du vil slette ' + apparel.title + '?')) {
      this.apparelService.delete(apparel.id).subscribe(() => {
      this.apparels = this.apparels.filter(x => x.id != apparel.id);
      });
    }
  }

  edit(apparel: Apparel): void {
    this.apparelId = apparel.id; // grab the Id for use when saving
    this.form.patchValue(apparel);
  }

  save(): void {
    this.message = '';
    if (this.form.valid && this.form.touched) {
    if (this.apparelId == 0) {
    this.apparelService.create(this.form.value).subscribe({
    next: (x) => {
    this.apparels.push(x);
    this.cancel();
    },
    error: (err) => {
    this.message = Object.values(err.error.errors).join(', ');
    }
    })
    } else {
    this.apparel = this.form.value;
    // attach the Id to apparel
    this.apparel.id = this.apparelId;
    this.apparelService.update(this.apparel).subscribe({
    error: (err) => {
    this.message = Object.values(err.error.errors).join(', ');
    },
    complete: () => {
    this.cancel();
    this.apparelService.getAll().subscribe(x => this.apparels = x);
    }
    })
    }
    }
  }

}
