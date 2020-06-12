import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IOrder } from 'src/app/shared/models/order';

@Component({
  selector: 'app-checout-success',
  templateUrl: './checout-success.component.html',
  styleUrls: ['./checout-success.component.scss']
})
export class ChecoutSuccessComponent implements OnInit {
  order: IOrder;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation && navigation.extras && navigation.extras.state;
    if (state) {
      this.order = state as IOrder;
    }
  }

  ngOnInit(): void {
  }

}
