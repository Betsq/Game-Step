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
        this.platforms = [];
        this.url = "/api/catalog";
        this.countPg = 1;
        this.paramArray = new Map([
            ["tags", this.tags],
            ["gameMode", this.gameMode],
            ["features", this.features],
            ["platforms", this.platforms]
        ]);
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
    setParamInQuery() {
        const tg = this.formationParametersInStr(this.gameMode);
        const gmMode = this.formationParametersInStr(this.tags);
        const fts = this.formationParametersInStr(this.features);
        const plt = this.formationParametersInStr(this.platforms);
        this._router.navigate([], {
            relativeTo: this._route,
            queryParams: {
                tags: tg,
                gameMode: gmMode,
                features: fts,
                platforms: plt,
            },
            queryParamsHandling: 'merge',
            skipLocationChange: false
        });
    }
    ;
    formationParametersInStr(arr) {
        if (arr.length !== 0) {
            let queryString = "";
            //-1 for not adding the last item in the array
            for (let i = 0; i < arr.length - 1; i++) {
                queryString += arr[i] + ",";
            }
            //add the last item without comma
            queryString += arr[arr.length - 1];
            return queryString;
        }
    }
    addParamInArray(nameParam, param) {
        for (let [key, val] of this.paramArray) {
            if (key === nameParam)
                val.push(param);
        }
    }
    removeParamInArray(nameParam, param) {
        for (let [key, val] of this.paramArray) {
            if (key === nameParam) {
                const index = this.findIndex(val, param);
                if (index !== -1)
                    val.splice(index, 1);
            }
        }
    }
    findIndex(arr, name) {
        for (let i = 0; i < arr.length; i++) {
            if (arr[i] === name)
                return i;
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