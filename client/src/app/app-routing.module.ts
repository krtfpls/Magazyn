import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { NotFoundComponent } from './errors/not-found/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error/server-error.component';
import { HomeComponent } from './home/home/home.component';
import { ProductsListComponent } from './products/products-list/products-list.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: '',
  runGuardsAndResolvers: 'always',
  canActivate: [AuthGuard],
  children:[
    {path: 'dashboard', component: DashboardComponent},
    {path: 'productsList', component: ProductsListComponent},
  ]
  },
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: HomeComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
