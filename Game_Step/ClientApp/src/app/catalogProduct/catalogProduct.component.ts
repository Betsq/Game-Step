import { Component, OnInit } from "@angular/core";

import { Product } from "../product"
import { DataService } from "../data.service";

@
    Component({
        selector: "catalog-product",
        templateUrl: "./catalogProduct.component.html",
        styleUrls: ["./catalogProduct.component.css"],
        providers: [DataService]
    })

export class CatalogProductComponent implements OnInit{
    products: Product[];

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.loadProducts();    
    }
    
    loadProducts() {
        this.dataService.getProducts()
            .subscribe((data: Product[]) => this.products = data);
    }
}