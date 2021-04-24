var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component } from "@angular/core";
import { DataService } from "../data.service";
let CatalogProductComponent = class CatalogProductComponent {
    constructor(http, spinner) {
        this.http = http;
        this.spinner = spinner;
        this.notEmptyPost = true;
        this.notScrolly = true;
        this.url = "/api/catalog";
        this.countPg = 1;
    }
    ngOnInit() {
        this.loadProducts();
    }
    loadProducts() {
        this.getProducts(this.countPg)
            .subscribe((data) => this.products = data);
    }
    getProducts(countPag) {
        return this.http.get(this.url + "/" + countPag);
    }
    onScroll() {
    }
};
CatalogProductComponent = __decorate([
    Component({
        selector: "catalog-product",
        templateUrl: "./catalogProduct.component.html",
        styleUrls: ["./catalogProduct.component.css"],
        providers: [DataService]
    })
], CatalogProductComponent);
export { CatalogProductComponent };
//# sourceMappingURL=catalogProduct.component.js.map