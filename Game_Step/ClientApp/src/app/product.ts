export class Product {
    constructor(
        public id?: number,
        public image?: string,
        public name?: string,
        public keyActivate?: string,
        public genre?: string,
        public isDiscount?: boolean,
        public discount?: number,
        public priceDiscount?: number,
        public price?: number
    ) { }
}