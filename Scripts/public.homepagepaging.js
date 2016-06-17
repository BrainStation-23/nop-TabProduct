

var HomePagePaging = {
    loadWaiting: false,
    enumType: 0,
    productTypeDiv: {
        bestSeller: 1,
        homePageProduct: 2,
        newProduct: 3,
        properties: {
            1: { url: "/TypeProducts/HomepageBestSellersPaging", div: ".best-seller-product-paging-div", hidingDiv: ".best-seller-product-footer", page: 1, totalPage: 32000 },
            2: { url: "/TypeProducts/HomePageProductsPaging", div: ".home-page-product-paging-div", hidingDiv: ".home-page-product-footer", page: 1, totalPage: 32000 },
            3: { url: "/TypeProducts/NewProductsOnHomePagePaging", div: ".new-product-paging-div", hidingDiv: ".new-product-footer", page: 1, totalPage: 32000 }
        }
    },
    init: function () {
        HomePagePaging.enumType = this.productTypeDiv.homePageProduct;
        this.loadWaiting = false;
    },

    animation : function (){
        var $grid = $('.isotopeGrid').isotope({
            layoutMode: 'fitRows'
        });
        $grid.isotope('shuffle')
    },

    setLoadWaiting: function (display) {
        displayAjaxLoading(display);
        this.loadWaiting = display;
    },
    defineDiv: function (eType) {
        this.enumType = eType;
    },
    addProuduct: function () {
        this.setLoadWaiting(true);
        if (this.productTypeDiv.properties[HomePagePaging.enumType].page >= this.productTypeDiv.properties[HomePagePaging.enumType].totalPage) {
            alert("No Product available");
            this.setLoadWaiting(false);
            return;
        }
        console.log(this.productTypeDiv.properties[this.enumType].url + "," + this.productTypeDiv.properties[this.enumType].page);
        $.ajax({
            cache: false,
            url: this.productTypeDiv.properties[this.enumType].url,
            data: { pageIndex: this.productTypeDiv.properties[this.enumType].page },
            type: 'post',
            success: this.success_process,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
        //this.setLoadWaiting(false);
    },
    success_process: function (response) {
        HomePagePaging.productTypeDiv.properties[HomePagePaging.enumType].page++;
        $(HomePagePaging.productTypeDiv.properties[HomePagePaging.enumType].div).append(response.html);
        if (response.html === "") {
            $(HomePagePaging.productTypeDiv.properties[HomePagePaging.enumType].hidingDiv).hide();
        }
        HomePagePaging.productTypeDiv.properties[HomePagePaging.enumType].totalPage = response.pageCount;
        if (HomePagePaging.productTypeDiv.properties[HomePagePaging.enumType].page >= HomePagePaging.productTypeDiv.properties[HomePagePaging.enumType].totalPage) {
            $(HomePagePaging.productTypeDiv.properties[HomePagePaging.enumType].hidingDiv).hide();
        }
        HomePagePaging.animation();
        return false;
    },
    resetLoadWaiting: function () {
        HomePagePaging.setLoadWaiting(false);
    },

    ajaxFailure: function () {
        HomePagePaging.setLoadWaiting(false);
        alert('Failed to add the product. Please refresh the page and try one more time.');
    }
};