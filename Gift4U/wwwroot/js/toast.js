var Toast = function(config){
    this.context = config.context==null?$('body'):config.context;
    this.message = config.message;
    this.time = config.time==null?3000:config.time;
    this.left = config.left;
    this.top = config.top;
    this.init();
};
var msgEntity;
Toast.prototype = {
    //Create elements and set css
    init : function(){
        $("#toastMessage").remove();
        //Toast body
        var msgDIV = new Array();
        msgDIV.push('<div id="toastMessage">');
        msgDIV.push('<span>'+this.message+'</span>');
        msgDIV.push('</div>');
        msgEntity = $(msgDIV.join('')).appendTo(this.context);
        //Toast style
        var left = this.left == null ? this.context.width()/2-msgEntity.find('span').width()/2 : this.left;
        var top = this.top == null ? window.innerHeight/3+msgEntity.find('span').height()/2 : this.top;
        msgEntity.css({position:'fixed',top:top,'z-index':'9999',left:left,'background-color':'black',color:'white','font-size':'18px',padding:'10px',margin:'10px' });
        msgEntity.hide();
    },
    // fade in/out animation
    show :function(){
        msgEntity.fadeIn(this.time/2);
        msgEntity.fadeOut(this.time/2);
    }

};