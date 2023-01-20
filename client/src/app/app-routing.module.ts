import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DocumentsCreateComponent } from './documents/documents-create/documents-create.component';
import { DocumentsDetailComponent } from './documents/documents-detail/documents-detail.component';
import { DocumentsListComponent } from './documents/documents-list/documents-list.component';
import { NotFoundComponent } from './errors/not-found/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error/server-error.component';
import { HomeComponent } from './home/home.component';
import { ProductCreateComponent } from './products/product-create/product-create.component';
import { ProductsDetailComponent } from './products/products-detail/products-detail.component';
import { ProductsListComponent } from './products/products-list/products-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: '',
  runGuardsAndResolvers: 'always',
  canActivate: [AuthGuard],
  children:[
    {path: 'dashboard', component: DashboardComponent},
    {path: 'documentsList', component: DocumentsListComponent},
    {path: 'documentsCreate', component: DocumentsCreateComponent},
    {path: 'documentsDetail/:id', component: DocumentsDetailComponent},
    {path: 'productsList', component: ProductsListComponent},
    {path: 'productsDetail/:id', component: ProductsDetailComponent},
    {path: 'productsCreate', component: ProductCreateComponent, canDeactivate: [PreventUnsavedChangesGuard]},
    {path: 'productsCreate/:id', component: ProductCreateComponent, canDeactivate: [PreventUnsavedChangesGuard]}
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
