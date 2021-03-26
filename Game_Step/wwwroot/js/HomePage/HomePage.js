//Function smooth fade animation when switching directory
$(document).ready(function () {
    //Make the "Leaders of sales" catalog visible when the page is loaded
    $('.catalog-leaders-sells').fadeIn(1);

    $('.item-tabs').click(function () {
       //Change the style on the button in order to see in which directory we are
        $('li.item-tabs').attr('class', 'item-tabs');
        $(this).attr('class', 'item-tabs item-tabs-active');

        //Get the button attribute to know which directory to change to
        let getAtt = this.getAttribute('catalog');
        //Setting the animation time
        let setTimeAnim = 500;

        //Check which directory we switch to
        if (getAtt === 'catalog1') {

            //Hide the previous catalog with smooth animation
            $('.catalog-leaders-sells').fadeOut(setTimeAnim);
            $('.catalog-expected').fadeOut(setTimeAnim);

            /*Show the catalog that we need.
            "setTimeout" is set so that when the previous directory
            is hidden, another will appear smoothly*/
            setTimeout(function () {
                $('.catalog-novetly').fadeIn(setTimeAnim);
            }, setTimeAnim);

        }
        else if (getAtt === 'catalog2') {
            $('.catalog-novetly').fadeOut(setTimeAnim);
            $('.catalog-expected').fadeOut(setTimeAnim);

            setTimeout(function () {
                $('.catalog-leaders-sells').fadeIn(setTimeAnim);
            }, setTimeAnim);
        }
        else {
            $('.catalog-leaders-sells').fadeOut(setTimeAnim);
            $('.catalog-novetly').fadeOut(setTimeAnim);

            setTimeout(function () {
                $('.catalog-expected').fadeIn(setTimeAnim);
            }, setTimeAnim);

        }
    })
})