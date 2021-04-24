import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http"

import { Product } from "../product"
import { DataService } from "../data.service";

import { NgxSpinnerService } from "ngx-spinner";

@
    Component({
        selector: "catalog-product",
        templateUrl: "./catalogProduct.component.html",
        styleUrls: ["./catalogProduct.component.css"],
        providers: [DataService]
    })

export class CatalogProductComponent implements OnInit {

    products: Product[];
    notEmptyPost = true;
    notScrolly = true;
    private url = "/api/catalog";

    countPg = 1;
   

    constructor(private http: HttpClient, private spinner: NgxSpinnerService) { }

    ngOnInit() {
        this.loadProducts();
    }

    loadProducts() {
        this.getProducts(this.countPg)
            .subscribe((data: Product[]) => this.products = data);
    }

    getProducts(countPag: number) {
        return this.http.get(this.url + "/" + countPag);
    }
    
    onScroll() {
        console.log("dd");
        this.countPg++;

        this.getProducts(this.countPg)
            .subscribe((data: Product[]) => this.products = this.products.concat(data) );
    }
}
