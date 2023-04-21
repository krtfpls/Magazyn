import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateCategoryComponent } from './categories/create-category/create-category.component';
import { ListCategoryComponent } from './categories/list-category/list-category.component';
import { CreateCustomerComponent } from './customers/create-customer/create-customer.component';
import { ListCustomerComponent } from './customers/list-customer/list-customer.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DocumentCreateHeaderComponent } from './documents/document-create-header/document-create-header.component';
import { DocumentsCreateComponent } from './documents/documents-create/documents-create.component';
import { DocumentsDetailComponent } from './documents/documents-detail/documents-detail.component';
import { DocumentsListComponent } from './documents/documents-list/documents-list.component';
import { NotFoundComponent } from './errors/not-found/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error/server-error.component';
import { HomeComponent } from './home/home.component';
import { ProductCreateComponent } from './products/product-create/product-create.component';
import { ProductsDetailComponent } from './products/products-detail/products-detail.component';
import { ProductsListAllComponent } from './products/products-list-all/products-list-all.component';
import { ProductsListComponent } from './products/products-list/products-list.component';
import { EditProfileComponent } from './profile/edit-profile/edit-profile.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { VerifyEmailComponent } from './register/verify-email/verify-email.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: '',
  runGuardsAndResolvers: 'always',
  canActivate: [AuthGuard],
  children:[
    {path: 'dashboard', component: ProductsListComponent},
    {path: 'documentsList', component: DocumentsListComponent},
    {path: 'documentsCreate/:type', component: DocumentsCreateComponent},
    {path: 'documentsDetail/:id', component: DocumentsDetailComponent},
    {path: 'productsList', component: ProductsListComponent},
    {path: 'productsListAll', component: ProductsListAllComponent},
    {path: 'productsDetail/:id', component: ProductsDetailComponent},
    {path: 'productsCreate', component: ProductCreateComponent, canDeactivate: [PreventUnsavedChangesGuard]},
    {path: 'productsCreate/:id', component: ProductCreateComponent, canDeactivate: [PreventUnsavedChangesGuard]},
    {path: 'customersCreate', component: CreateCustomerComponent},
    {path: 'customerList', component: ListCustomerComponent},
    {path: 'categoryList', component: ListCategoryComponent},
    {path: 'categoryCreate', component: CreateCategoryComponent},
    {path: 'editProfile', component: EditProfileComponent}
  ]
  },
  {path: 'VerifyEmail', component: VerifyEmailComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: HomeComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
