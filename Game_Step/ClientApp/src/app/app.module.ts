import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { AppComponent } from "./app.component";

import { HeaderComponent } from "./header/header.component";
import { HomeComponent } from  "./home/home.component"

@NgModule({
    imports: [BrowserModule, FormsModule],
    declarations: [AppComponent, HeaderComponent, HomeComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }