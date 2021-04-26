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

    minPrice: number;
    maxPrice: number;
    tags: string[] = [];
    gameMode: string[] = [];
    features: string[] = [];
    platform: string[] = [];
    publisher: string;
    minReleaseData: number;
    maxReleaseData: number;

    private url = "/api/catalog";
    countPg = 1;

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

    setParam(ev, nameParam, param) {
        if (ev.target.checked === true) {
            this.addParamInArray(nameParam, param);

            this.setParamInQuery();
        } else {
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

        this._router.navigate([],
            {
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
    };

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
        } else if (nameParam === 'maxPrice') {
            this.maxPrice = null;
        } else if (nameParam === 'publisher') {
            this.publisher = null;
        } else if (nameParam === 'minReleaseData') {
            this.minReleaseData = null;
        } else if (nameParam === 'maxReleaseData') {
            this.maxReleaseData = null;
        }
        else if (nameParam === 'tags') {
            const index = this.findIndex(this.tags, param);

            if (index !== -1) {
                this.tags.splice(index, 1);
            }

        } else if (nameParam === 'gameMode') {
            const index = this.findIndex(this.gameMode, param);

            if (index !== -1) {
                this.gameMode.splice(index, 1);
            }

        } else if (nameParam === 'features') {
            const index = this.findIndex(this.features, param);

            if (index !== -1) {
                this.features.splice(index, 1);
            }

        } else if (nameParam === 'platform') {
            const index = this.findIndex(this.platform, param);

            if (index !== -1) {
                this.platform.splice(index, 1);
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
