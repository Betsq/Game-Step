import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from './product';

@Injectable()
export class DataService {
    private url = "/api/catalog";

    constructor(private http: HttpClient) {
    }

    getProducts() {
        return this.http.get(this.url);
    }
}