import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./components/frontpage.component')
    .then(_ =>_.FrontpageComponent)
  },
  {
    path: 'admin/apparel',
    loadComponent: () => import('./components/admin-apparel.component')
    .then(_ =>_.AdminApparelComponent)
  },
  {
    path: 'apparel/:apparelId',
    loadComponent: () => import('./components/apparel-detail.component')
    .then(_ =>_.ApparelDetailComponent)
  },

  {
    path: 'admin/apparel-reactive',
loadComponent: () => import('./components/admin-apparel-reactiv-forms.component')
.then(_ => _.AdminApparelReactivFormsComponent)
  },
  {
    path: 'closet/:closetId',
    loadComponent: () => import('./components/closet-detail.component')
    .then(_ =>_.ClosetDetailComponent)
  },
  {
    path: 'admin/closet',
    loadComponent: () => import('./components/admin-closet.component')
    .then(_ =>_.AdminClosetComponent)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
