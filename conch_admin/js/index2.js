function see2(){ 			//商品显示方法
    $.get("http://47.93.220.57/groupbuying/api/GetRCC",function(rc){
	for(i=0;i<rc.length;i++){
		var txt = "<div class='card'><img class='product_picture' src=''/><div class='product_name'>"	+rc[i].rc_name+	"</div><br/><div class='product_intro'>"+rc[i].rc_phone+"</div><img class='delete' onclick='de()' src='img/_ionicons_svg_md-trash.svg'/><div class='product_price' onclick='gai()'>"	+rc[i].rc_address+	"</div>	<br/><div class='product_id' >"+re[i].re_nickame+"</div></div>"
		$(".card1").before(txt);								
		}
	});
}
window.onload=function(){
	
}