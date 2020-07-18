import { Component, OnInit } from '@angular/core';
import { ProductFormValues, IProduct } from 'src/app/shared/models/product';
import { IType } from 'src/app/shared/models/productType';
import { IBrand } from 'src/app/shared/models/brand';
import { AdminService } from '../admin.service';
import { ShopService } from 'src/app/shop/shop.service';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {
  product: IProduct;
  productFromValues: ProductFormValues;
  brands: IBrand[];
  types: IType[];

  constructor(
    private adminService: AdminService,
    private shopService: ShopService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.product = new ProductFormValues();
  }

  ngOnInit(): void {
    const brands = this.getBrands();
    const types = this.getTypes();

    forkJoin([types, brands]).subscribe(resutls => {
      this.types = resutls[0];
      this.brands = resutls[1];
    }, error => {
      console.log(error);
    }, () => {
      if (this.route.snapshot.url[0].path === 'edit') {
        this.loadProduct();
      }
    });
  }

  loadProduct() {
    this.shopService.getProduct(+this.route.snapshot.paramMap.get('id')).subscribe((response: any) => {
      const productBrandId = this.brands && this.brands.find(x => x.name === response.productBrand).id;
      const productTypeId = this.types && this.types.find(x => x.name === response.productType).id;
      this.productFromValues = {...response, productBrandId, productTypeId};
      this.product = response;
    });
  }

  getBrands() {
    return this.shopService.getBrands();
  }

  getTypes() {
    return this.shopService.getTypes();
  }

}
