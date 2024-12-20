import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { BooksComponent } from './books/books.component';

const routes: Routes = [
  { path: 'products', component: ProductsComponent },
  { path: 'books', component: BooksComponent },
  { path: '', redirectTo : 'products', pathMatch: 'full' },
  { path: '**', redirectTo: '/products' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }