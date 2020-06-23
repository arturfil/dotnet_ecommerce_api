import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from 'src/app/shared/models/basket';
import { BasketService } from 'src/app/basket/basket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-checout-review',
  templateUrl: './checout-review.component.html',
  styleUrls: ['./checout-review.component.scss']
})
export class ChecoutReviewComponent implements OnInit {
  basket$: Observable<IBasket>;

  constructor(private basketService: BasketService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  createPaymentIntent() {
    return this.basketService.createPaymentIntent().subscribe((respose: any) => {
      this.toastr.success('Pyament intent created');
    }, error => {
      this.toastr.error(error.message);
    });
  }

}
