import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {
  @Input() subtotal: number;
  @Input() shippingPrice: number;
  @Input() total: number;

  constructor() { }

  ngOnInit(): void {
  }

}
