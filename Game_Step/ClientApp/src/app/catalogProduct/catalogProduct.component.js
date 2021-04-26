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
        console.log(this._router.parseUrl(this._router.url));
    }
    loadProducts() {
        this.getProducts(this.countPg)
            .subscribe((data) => this.products = data);
    }
    getProducts(countPag) {
        return this.http.get(this.url + "/" + countPag);
    }
    setParam(nameParam, param, ev) {
        if (ev.target.checked === true) {
            this.addParamInArray(nameParam, param);
            this.setParamInQuery();
        }
        else {
            this.removeParamInArray(nameParam, param);
            this.setParamInQuery();
        }
    }
    setParamSingle(nameParam, param) {
        this.addParamInArray(nameParam, param);
        this.setParamInQuery();
    }
    setParamInQuery() {
        let tg;
        let gmMode;
        let fts;
        let plt;
        if (this.tags.length !== 0)
            gmMode = this.formationParameters(this.tags);
        if (this.gameMode.length !== 0)
            tg = this.formationParameters(this.gameMode);
        if (this.features.length !== 0)
            fts = this.formationParameters(this.features);
        if (this.platform.length !== 0)
            plt = this.formationParameters(this.platform);
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
    formationParameters(arr) {
        let queryString = "";
        //-1 for not adding the last item in the array
        for (let i = 0; i < arr.length - 1; i++) {
            queryString += arr[i] + ",";
        }
        //add the last item without comma
        queryString += arr[arr.length - 1];
        return queryString;
    }
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
        else if (nameParam === 'platforms') {
            this.platform.push(param);
        }
        else if (nameParam === 'publisher') {
            this.publisher = param;
        }
        else if (nameParam === 'minReleaseData') {
            if (+param === this.year[0])
                this.minReleaseData = null;
            else
                this.minReleaseData = param;
        }
        else if (nameParam === 'maxReleaseData') {
            if (+param === this.year[this.year.length - 1])
                this.maxReleaseData = null;
            else
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
        else if (nameParam === 'platforms') {
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