import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { RouterModule, Routes } from '@angular/router';
import { ChecoutSuccessComponent } from './checout-success/checout-success.component';

const routes: Routes = [
  {path: '', component: CheckoutComponent},
  {path: 'success', component: ChecoutSuccessComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CheckoutRoutingModule { }
