import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { BasketRoutingModule } from '../basket/basket-routing.module';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [CheckoutComponent],
  imports: [
    CommonModule,
    CheckoutRoutingModule
  ],
  exports: [RouterModule]
})
export class CheckoutModule { }
