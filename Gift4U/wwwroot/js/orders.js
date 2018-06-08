$(function () {
    $(".btn-confirm").click(function () {
        var orderid = $($($($(this)).parent()[0]).children(":first")[0]).html().split(":")[1].trim();
        var success = false;
        $.ajax({
            type:"POST",
            url:"/Orders/ConfirmReceipt",
            data:JSON.stringify({
                orderid:orderid
            }),
            async:false,
            success:function (data) {
                if(data=="true") {
                    success = true;
                }
                else new Toast({context:$("body"),message:'Confirm Failed ('+data+')'}).show();
            },
            error:function (jqXHR) {
                new Toast({context:$("body"),message:'Confirm Failed ('+jqXHR.status+')'}).show();
            }
        });
        if(success){
            $(this).hide();
            $($($($($(this)).parent()[0]).children()[1]).children(":last")[0]).html("Status: Done");
        }

    });
    $("#btn-confirm-all").click(function () {
        $.ajax({
            type:"POST",
            url:"/Orders/ConfirmReceiptAll",
            async:false,
            success:function (data) {
                if(data=="true") {
                    $(".btn-confirm").hide();
                    var arr = $(".btn-confirm");
                    for(var i=0;i<arr.length;i++){
                        $($($($(arr[i]).parent()[0]).children()[1]).children(":last")[0]).html("Status: Done");
                    }
                }
                else new Toast({context:$("body"),message:'Confirm Failed ('+data+')'}).show();
            },
            error:function (jqXHR) {
                new Toast({context:$("body"),message:'Confirm Failed ('+jqXHR.status+')'}).show();
            }
        })
    })
});