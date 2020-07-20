import { Component, OnInit, Input } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-edit-product-photos',
  templateUrl: './edit-product-photos.component.html',
  styleUrls: ['./edit-product-photos.component.scss']
})
export class EditProductPhotosComponent implements OnInit {
  @Input() product: IProduct;
  addPhotoMode = false

  constructor() { }

  ngOnInit(): void {
  }

  addPhotoModeToggle() {
    this.addPhotoMode != this.addPhotoMode;
  }

}
