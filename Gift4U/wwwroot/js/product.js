$(function () {

    var productId,productName,productImages,productPrice,productColors,productSizes;
    var currentSelectedColor;

    $.ajax({
        type: "POST",
        url:"/Products/ProductInfo",
        data:JSON.stringify({
            productId:($($("#btn-add-to-cart")).prev()[0].innerHTML).split("=")[1]
        }),
        dataType: "json",
        async:false,
        success:function(data) {
            //Progress data
            productId = data.productId;
            productName = data.productName;
            productImages = data.productImages;
            productPrice = data.productPrice;
            productColors = data.productColors;
            productSizes = data.productSizes;
        },
        error:function (jqXHR) {
            new Toast({context:$("body"),message:'Get Product Info Failed(Servlet:'+jqXHR.status+')'}).show();
        }
    });

    $(".color-label").click(function() {
        currentSelectedColor = this;
    });

    //Add to cart button clicked
    $("#btn-add-to-cart").click(function() {
        if(productColors!=null){
            if (!currentSelectedColor) {
                new Toast({context:$("body"),message:'Select color first'}).show();
                return;
            }
        }
        var productId = $($($(this)).prev()[0]).html().split('=')[1];
        if(productColors!=null) {
            var color = ($($(".color-label.selected")[0]).attr("id")) ? ($($(".color-label.selected")[0]).attr("id")) : "";
        }
        if(productSizes!=null) {
            var size = $($("#selected-size span")[0]).html();
        }
        var quantity = Math.abs(parseInt($("#quantity-input").val()));
        if(!quantity){
            new Toast({context:$("body"),message:'Illegal quantity number'}).show();
            return;
        }
        $("#quantity-input").val(quantity);
        var $btn = $(this).button('loading');
        var failedToAddToCart = false;
        $.ajax({
            type: "POST",
            url:"/Products/AddToCart",
            data:JSON.stringify({
                productId : productId,
                color: color,
                size: size,
                quantity: quantity
            }),
            dataType: "text",
            async:false,
            success:function(data){
                if(data=="noLogin"){
                    new Toast({context:$("body"),message:'You have to Login/Sign up first'}).show();
                }
                else if(data=="true"){
                    new Toast({context:$("body"),message:'Add to cart successfully'}).show();
                }
                else{
                    new Toast({context:$("body"),message:'Failed to add to cart'}).show();
                    failedToAddToCart = true;
                }
            },
            error:function (jqXHR) {
                new Toast({context:$("body"),message:'Failed to add to cart (Servlet:'+jqXHR.status+')'}).show();
                failedToAddToCart = true;
            }
        });
        $btn.button('reset');
    });

    setTimeout(function(){
        var oldPath="/img/"+productImages[0];
        var newPath=newPath = oldPath.replace(/-s/, "-l");
        $("#magiczoom-zoom").css('background-image', 'url(' + newPath + ')');
    },200);
});