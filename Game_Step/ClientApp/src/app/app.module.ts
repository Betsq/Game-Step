import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { Routes, RouterModule } from "@angular/router";

import { FormsModule } from "@angular/forms";
import { AppComponent } from "./app.component";

import { HeaderComponent } from "./header/header.component";
import { HomeComponent } from "./home/home.component";
import { CatalogProductComponent } from "./catalogProduct/catalogProduct.component";

const appRoutes: Routes = [
/*{path: "", component: HomeComponent},*/
    {path: "CatalogProduct", component: CatalogProductComponent}
];

@NgModule({
    imports: [BrowserModule, FormsModule, RouterModule.forRoot(appRoutes)],
    declarations: [
        AppComponent,
        HeaderComponent,
        HomeComponent,
        CatalogProductComponent
        ],
    bootstrap: [AppComponent]
})
export class AppModule { }