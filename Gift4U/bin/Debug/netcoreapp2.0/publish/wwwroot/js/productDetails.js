$(function() {
    //Variables declare
    var currentSelectedColor = null;
    var currentSelectedImg;
    var sizeListShow;
    var sizeList = $("#size-list");

    //Add click function for each colors, set selected when click and cancel others 'selected' class
    $(".color-label").click(function() {
        if (currentSelectedColor) {
            $(currentSelectedColor).removeClass("selected");
        }
        $(this).addClass("selected");
        currentSelectedColor = this;
    });

    //Add click function for each sizes, when select a size change current selected size
    $("#size-list li").click(function() {
        $("#selected-size span").html($(this).find("span").html());
        sizeList.hide();
    });


    //Open magicZoom
    $("#zoom-img").magicZoom();


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

    //Open size list when clicked selected-size
    sizeList.hide();
    $("#selected-size").click(function() {
        if (sizeListShow) { sizeList.hide();sizeListShow=false; }
        else { sizeList.show(); sizeListShow = true; }
    });


    //Set Quantity is 1 as default
    $("#quantity-input").val("1");


    //Disable '-' at quantity input
    document.getElementById("quantity-input").addEventListener("keypress", function(event) {
        if (event.keyCode == 45) {
            event.preventDefault();
        }
    });

    //Search and set display which product parameter panel class is 'on'
    var product_parameters = $(".product-parameters li button");
    for (var i = 0; i < product_parameters.length; i++) {
        var h3title = $(product_parameters[i]).find("h3");
        if (h3title.attr("class") == "on") {
            $(product_parameters[i]).next().show();
        }
    }

    //Switch product parameter panel
    var currentOnProductParameters = product_parameters[0];
    product_parameters.click(function() {
        var h3title = $(this).find("h3");
        if (h3title.attr("class") == "off") {
            $(currentOnProductParameters).next().hide();
            $(currentOnProductParameters).find("h3").removeClass("on");
            $(currentOnProductParameters).find("h3").addClass("off");
            $(this).next().show();
            h3title.removeClass("off");
            h3title.addClass("on");
            currentOnProductParameters = this;
        }
    });

    var up_arrow = $("#up-arrow");
    var down_arrow = $("#down-arrow");
    up_arrow.click(function () {
        var quantity = Math.abs(parseInt($("#quantity-input").val()));
        if(!quantity){
            new Toast({context:$("body"),message:'Illegal quantity number'}).show();
            return;
        }
        if(quantity>=1) {
            $("#quantity-input").val(quantity + 1);
        }
    });
    down_arrow.click(function () {
        var quantity = Math.abs(parseInt($("#quantity-input").val()));
        if(!quantity){
            new Toast({context:$("body"),message:'Illegal quantity number'}).show();
            return;
        }
        if(quantity>1) {
            $("#quantity-input").val(quantity - 1);
        }
    })
});