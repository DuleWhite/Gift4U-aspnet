$(function() {
    // .slick
    var slick = $('.slick');
    if ($(window).width() < 900) {
        slick.not('.slick-initialized').slick({
            dots: false,
            slidesToShow: 1,
            slidesToScroll: 1
        });
        slick.width(260);
    } else {
        slick.width(900);
        slick.not('.slick-initialized').slick({
            dots: false,
            slidesToShow: 3,
            slidesToScroll: 1
        });
    }

    // .faq img
    var faq_img = $(".faq img");
    if ($(window).width() < 640) {
        faq_img.width($(window).width() - 40)
    } else { faq_img.width(640); }

    // .aboutus .cd-fixed-bg
    var fixed_bg = $('.aboutus .cd-fixed-bg');
    var fixed_bg_height = fixed_bg.height();
    $(window).on('scroll', function() {
        var scrollTop = Math.floor(($(window).scrollTop() - fixed_bg.offset().top) / 2);
        // console.log(scrollTop);
        fixed_bg.css('background-position', '50% ' + scrollTop + 'px');
    });

    // .aboutus .cd-fixed-bg .rect
    var rect = $(".aboutus .cd-fixed-bg .rect");
    if ($(window).width() < 940) {
        rect.width(0.53 * $(window).width())
    } else { rect.width(500); }
    rect.css("left", (($(window).width() - rect.width()) / 2) + "px")

    // 获取手指在轮播图元素上的一个滑动方向（左右）

    // 获取界面上轮播图容器
    var $carousels = $('.carousel');
    var startX, endX;
    // 在滑动的一定范围内，才切换图片
    var offset = 50;
    // 注册滑动事件
    $carousels.on('touchstart', function(e) {
        // 手指触摸开始时记录一下手指所在的坐标x
        startX = e.originalEvent.touches[0].clientX;

    });
    $carousels.on('touchmove', function(e) {
        // 目的是：记录手指离开屏幕一瞬间的位置 ，用move事件重复赋值
        endX = e.originalEvent.touches[0].clientX;
    });
    $carousels.on('touchend', function(e) {
        //console.log(endX);
        //结束触摸一瞬间记录手指最后所在坐标x的位置 endX
        //比较endX与startX的大小，并获取每次运动的距离，当距离大于一定值时认为是有方向的变化
        var distance = Math.abs(startX - endX);
        if (distance > offset) {
            //说明有方向的变化
            //根据获得的方向 判断是上一张还是下一张出现
            $(this).carousel(startX > endX ? 'next' : 'prev');
        }
    });


    // modal data declare
    var sizeList = $("#size-list");
    var productId,productName,productImages,productPrice,productColors,productSizes;
    var currentSelectedColor;

    function resetModal() {
        //Clear size options
        $("#size ul").empty();
        //Clear color options
        $("#color ul").empty();
        //Hide size panel
        $("#size").hide();
        //Hide color panel
        $("#color").hide();
        //Set Quantity is 1 as default
        $("#quantity-input").val("1");
        sizeListShow = false;
        $("#productImages").empty();
        $("#color-tip").hide();
        sizeList.hide();
        $("#selected-size span").html("");
        productColors=currentSelectedColor=productSizes=null;
    }

    $(".product-qwbtn").click(function () {
        resetModal();
        $.ajax({
            type: "POST",
            url:"/Products/ProductInfo",
            data:JSON.stringify({
                productId : ($($($($(this)).parent()[0]).parent()[0]).prev()[0].innerHTML).split("=")[1]
            }),
            dataType: "json",
            async:false,
            success:function(data){
                //Progress data
                productId = data.productId;
                productName = data.productName;
                productImages = data.productImages;
                productPrice = data.productPrice;
                productColors = data.productColors;
                productSizes = data.productSizes;

                //Set product id, product name, product price
                $("#productId").html(productId);
                $("#productName").html(productName);
                $("#productPrice").html("$" + productPrice.toFixed(2));

                //If product has color option then add colors
                if(productColors!=null && productColors.length>0){
                    var modalColor = $("#color");

                    //Set color panel visible
                    modalColor.show();

                    //Add colors dynamically
                    var ul = $("#color ul");
                    for(var i=0;i<productColors.length;i++){
                        $("<li><div class='color-label' style='background-color:"+productColors[i]+";' id='"+productColors[i]+"'></div></li>").appendTo(ul);
                    }

                    //Add click function for each colors, set selected when click and cancel others 'selected' class
                    $(".color-label").click(function() {
                        if (currentSelectedColor) {
                            $(currentSelectedColor).removeClass("selected");
                        }
                        $(this).addClass("selected");
                        currentSelectedColor = this;
                    });
                }

                //If product has size option then add sizes
                if(productSizes!=null && productSizes.length>0){
                    var size = $("#size");

                    //Set size panel visible
                    size.show();

                    //Add sizes dynamically
                    var ul = $("#size-list ul");
                    for(var i=0;i<productSizes.length;i++){
                        $("<li><span>"+productSizes[i]+"</span></li>").appendTo(ul);
                    }

                    //Add click function for each sizes, when select a size change current selected size
                    $("#size-list li").click(function() {
                        $("#selected-size span").html($(this).find("span").html());
                        sizeList.hide();
                    });

                    //Set default selected size as first size
                    $("#selected-size span").html(productSizes[0]);
                }

                //Add product images
                var oldPath,newPath;

                //Add images dynamically
                for(var i=0;i<productImages.length;i++){
                    if(i==0) $("<img class='guide-img selected' src='img/"+productImages[i]+"'>").appendTo($("#productImages"));
                    else $("<img class='guide-img' src='img/"+productImages[i]+"'>").appendTo($("#productImages"));
                }

                //Change default zoom-img image src and 'magiczoom-zoom' background image as first image
                oldPath="img/"+productImages[0];
                newPath=newPath = oldPath.replace(/-s/, "-l");
                $("#zoom-img").attr('src',oldPath);
                $("#magiczoom-zoom").css('background-image', 'url(' + newPath + ')');
                currentSelectedImg = $(".guide-img")[0];

                //Change zoom-img when a not active guide-img selected
                $(".guide-img").click(function() {
                    oldPath = this.src;
                    newPath = oldPath.replace(/-s/, "-l");
                    $("#zoom-img").attr('src', newPath);
                    $("#magiczoom-zoom").css('background-image', 'url(' + newPath + ')');
                    $(currentSelectedImg).removeClass("selected");
                    $(this).addClass("selected");
                    currentSelectedImg = this;
                });
            },
            error:function (jqXHR) {
                new Toast({context:$("body"),message:'Get Porudct Info Failed ('+jqXHR.status+')'}).show();
            }
        });
        var modal = artModal({
            element:document.getElementById("product-quick-view-modal"),
            close:function () {
                resetModal();
            }
        });
        modal.open();
    });

    //Add to cart button clicked
    $("#btn-add-to-cart").click(function() {
        if(productColors!=null){
            if (!currentSelectedColor) {
                new Toast({context:$("body"),message:'Select color first'}).show();
                return;
            }
        }
        var productId = $($($(this)).prev()[0]).html();
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
        console.log(quantity);
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
});
$(window).resize(function() {
    // .slick
    var slick = $('.slick');
    if ($(window).width() < 900) {
        slick.slick("unslick");
        slick.not('.slick-initialized').slick({
            dots: false,
            slidesToShow: 1,
            slidesToScroll: 1
        });
        slick.width(260);
    } else {
        slick.width(900);
        slick.slick("unslick");
        slick.not('.slick-initialized').slick({
            dots: false,
            slidesToShow: 3,
            slidesToScroll: 1
        });
    }

    // .faq img
    var faq_img = $(".faq img");
    if ($(window).width() < 640) {
        faq_img.width($(window).width() - 40)
    } else { faq_img.width(640); }

    // .aboutus .cd-fixed-bg .rect
    var rect = $(".aboutus .cd-fixed-bg .rect");
    if ($(window).width() < 940) {
        rect.width(0.53 * $(window).width())
    } else { rect.width(500); }
    rect.css("left", (($(window).width() - rect.width()) / 2) + "px");
});