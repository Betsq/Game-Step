var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component } from "@angular/core";
import { DataService } from "../data.service";
let CatalogProductComponent = class CatalogProductComponent {
    constructor(http, spinner, _route, _router) {
        this.http = http;
        this.spinner = spinner;
        this._route = _route;
        this._router = _router;
        this.tags = [];
        this.gameMode = [];
        this.features = [];
        this.platform = [];
        this.url = "/api/catalog";
        this.countPg = 1;
        this.year = [
            1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997,
            1998, 1999, 2000, 2001, 2002, 2003, 2004, 2005,
            2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013,
            2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022
        ];
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
    setParam(ev, nameParam, param) {
        if (ev.target.checked === true) {
            console.log("1111");
            this.addParamInArray(nameParam, param);
            this.setParamInQuery();
        }
        else {
            this.removeParamInArray(nameParam, param);
            this.setParamInQuery();
        }
    }
    setParamInQuery() {
        let tg = this.tags;
        let gmMode = this.gameMode;
        let fts = this.features;
        let plt = this.platform;
        /*Setting to null so that extra ampersands from empty arrays
         are not displayed in the query string*/
        if (tg.length === 0)
            tg = null;
        if (gmMode.length === 0)
            gmMode = null;
        if (fts.length === 0)
            fts = null;
        if (plt.length === 0)
            plt = null;
        this._router.navigate([], {
            relativeTo: this._route,
            queryParams: {
                minPrice: this.minPrice,
                maxPrice: this.maxPrice,
                tags: tg,
                gameMode: gmMode,
                features: fts,
                platform: plt,
                publisher: this.publisher,
                minReleaseData: this.minReleaseData,
                maxReleaseData: this.maxReleaseData,
            },
            queryParamsHandling: 'merge',
            skipLocationChange: false
        });
    }
    ;
    addParamInArray(nameParam, param) {
        if (nameParam === 'minPrice') {
            this.minPrice = param;
        }
        else if (nameParam === 'maxPrice') {
            this.maxPrice = param;
        }
        else if (nameParam === 'tags') {
            this.tags.push(param);
        }
        else if (nameParam === 'gameMode') {
            this.gameMode.push(param);
        }
        else if (nameParam === 'features') {
            this.features.push(param);
        }
        else if (nameParam === 'platform') {
            this.platform.push(param);
        }
        else if (nameParam === 'publisher') {
            this.publisher = param;
        }
        else if (nameParam === 'minReleaseData') {
            this.minReleaseData = param;
        }
        else if (nameParam === 'maxReleaseData') {
            this.maxReleaseData = param;
        }
    }
    removeParamInArray(nameParam, param) {
        if (nameParam === 'minPrice') {
            this.minPrice = null;
        }
        else if (nameParam === 'maxPrice') {
            this.maxPrice = null;
        }
        else if (nameParam === 'publisher') {
            this.publisher = null;
        }
        else if (nameParam === 'minReleaseData') {
            this.minReleaseData = null;
        }
        else if (nameParam === 'maxReleaseData') {
            this.maxReleaseData = null;
        }
        else if (nameParam === 'tags') {
            const index = this.findIndex(this.tags, param);
            if (index !== -1) {
                this.tags.splice(index, 1);
            }
        }
        else if (nameParam === 'gameMode') {
            const index = this.findIndex(this.gameMode, param);
            if (index !== -1) {
                this.gameMode.splice(index, 1);
            }
        }
        else if (nameParam === 'features') {
            const index = this.findIndex(this.features, param);
            if (index !== -1) {
                this.features.splice(index, 1);
            }
        }
        else if (nameParam === 'platform') {
            const index = this.findIndex(this.platform, param);
            if (index !== -1) {
                this.platform.splice(index, 1);
            }
        }
    }
    findIndex(arr, name) {
        for (let i = 0; i < arr.length; i++) {
            if (arr[i] === name) {
                return i;
            }
        }
        return -1;
    }
    onScroll() {
        this.countPg++;
        this.getProducts(this.countPg)
            .subscribe((data) => this.products = this.products.concat(data));
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