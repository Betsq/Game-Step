import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { Routes, RouterModule } from "@angular/router";

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from "@angular/forms";
import { AppComponent } from "./app.component";

import { HttpClientModule } from "@angular/common/http";

import { HeaderComponent } from "./header/header.component";
import { HomeComponent } from "./home/home.component";
import { CatalogProductComponent } from "./catalogProduct/catalogProduct.component";

import { InfiniteScrollModule } from "ngx-infinite-scroll";
import { NgxSpinnerModule } from "ngx-spinner";

const appRoutes: Routes = [
    {path: "CatalogProduct", component: CatalogProductComponent}
];

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule.forRoot(appRoutes),
        HttpClientModule,
        InfiniteScrollModule,
        NgxSpinnerModule,
        BrowserAnimationsModule
    ],
    declarations: [
        AppComponent,
        HeaderComponent,
        HomeComponent,
        CatalogProductComponent
        ],
    bootstrap: [AppComponent]
})
export class AppModule { }