import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-checout-address',
  templateUrl: './checout-address.component.html',
  styleUrls: ['./checout-address.component.scss']
})
export class ChecoutAddressComponent implements OnInit {
  @Input() checkoutForm: FormGroup;

  constructor() { }

  ngOnInit(): void {
  }

}
