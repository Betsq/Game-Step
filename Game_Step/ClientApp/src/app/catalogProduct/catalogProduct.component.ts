import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http"
import { Router, ActivatedRoute } from '@angular/router';

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

    //Set the value of the number to null so that we can pass it to the map 
    private minPrice: string[] = [];
    private maxPrice: string[] = [];
    private minReleaseData: string[] = [];
    private maxReleaseData: string[] = [];
    private tags: string[] = [];
    private gameMode: string[] = [];
    private features: string[] = [];
    private platform: string[] = [];
    private publisher: string[] = [];


    private url = "/api/catalog";
    countPg = 1;

    private paramArray = new Map([
        ["tags", this.tags],
        ["gameMode", this.gameMode],
        ["features", this.features],
        ["platform", this.platform],
    ]);

    year: number[] = [
        1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997,
        1998, 1999, 2000, 2001, 2002, 2003, 2004, 2005,
        2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013,
        2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022
    ];


    constructor(private http: HttpClient,
        private spinner: NgxSpinnerService,
        private _route: ActivatedRoute,
        private _router: Router) {
    }

    ngOnInit() {
        this.loadProducts();
        console.log(this._router.parseUrl(this._router.url));
    }

    loadProducts() {
        this.getProducts(this.countPg)
            .subscribe((data: Product[]) => this.products = data);
    }

    getProducts(countPag: number) {
        return this.http.get(this.url + "/" + countPag);
    }

    setParam(nameParam, param, ev) {
        if (ev.target.checked === true) {
            this.addParamInArray(nameParam, param);

            this.setParamInQuery();
        } else {
            this.removeParamInArray(nameParam, param);

            this.setParamInQuery();
        }
    }

    setParamSingle(nameParam, param) {
        this.addParamInArray(nameParam, param);

        this.setParamInQuery();
    }

    setParamInQuery() {
        let tg: string;
        let gmMode: string;
        let fts: string;
        let plt: string;
        if (this.tags.length !== 0)
            gmMode = this.formationParameters(this.tags);
        if (this.gameMode.length !== 0)
            tg = this.formationParameters(this.gameMode);
        if (this.features.length !== 0)
            fts = this.formationParameters(this.features);
        if (this.platform.length !== 0)
            plt = this.formationParameters(this.platform);

        this._router.navigate([],
            {
                relativeTo: this._route,
                queryParams: {
                    tags: tg,
                    gameMode: gmMode,
                    features: fts,
                    platform: plt,
                },
                queryParamsHandling: 'merge',
                skipLocationChange: false
            });
    };

    formationParameters(arr: string[]) {
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

    findIndex(arr: string[], name) {
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
            .subscribe((data: Product[]) => this.products = this.products.concat(data));
    }
}
