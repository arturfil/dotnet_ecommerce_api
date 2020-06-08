import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { BasketRoutingModule } from '../basket/basket-routing.module';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ChecoutAddressComponent } from './checout-address/checout-address.component';
import { ChecoutDeliveryComponent } from './checout-delivery/checout-delivery.component';
import { ChecoutReviewComponent } from './checout-review/checout-review.component';
import { ChecoutPaymentComponent } from './checout-payment/checout-payment.component';
import { ChecoutSuccessComponent } from './checout-success/checout-success.component';



@NgModule({
  declarations: [CheckoutComponent, ChecoutAddressComponent, ChecoutDeliveryComponent, ChecoutReviewComponent, ChecoutPaymentComponent, ChecoutSuccessComponent],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule
  ],
  exports: [RouterModule]
})
export class CheckoutModule { }
