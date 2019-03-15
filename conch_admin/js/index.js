function see(){ 			//商品显示方法
    $.get("http://47.93.220.57/groupbuying/api/Get_Product_1",function(Product){
	for(i=0;i<Product.length;i++){
		var txt = "<div class='card'><img class='product_picture' src='"	+Product[i].product_picture_url+	"'/><div class='product_name'>"	+Product[i].product_name+	"</div><br/><div class='product_intro'>"+Product[i].product_intro+"</div><img class='delete' onclick='de()' src='img/_ionicons_svg_md-trash.svg'/><div class='product_price' onclick='gai()'>"	+Product[i].product_price+	"¥</div>	<br/><div class='product_id' >"+Product[i].product_id+"</div></div>"
		$(".card1").before(txt);								
		}
	});
}
window.onload=function(){
	document.getElementById("btn1").onclick=function(){displayDate1()};
	function displayDate1(){
		alert(1)
		window.open('on.html', 'newwindow', 'height=600, width=800, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no')
	}
				
	document.getElementById("btn2").onclick=function(){displayDate2()};
	function displayDate2(){
		var close=confirm("是否确认立即关团？");
		if (close){
			 $.get("http://47.93.220.57/groupbuying/api/CloseGroup",function(){
			    alert("团购已结束");
			 });
		}
	}
				
	document.getElementById("btn3").onclick=function(){displayDate3()};
	function displayDate3(){
		window.open('register.html', 'newwindow', 'height=600, width=800, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no')
	}
			
	document.getElementById("btn4").onclick=function(){displayDate4()};
	function displayDate4(){
		window.open('tzzc.html', 'newwindow', 'height=600, width=800, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no')
	}
	
	document.getElementById("btn5").onclick=function(){displayDate5()};
	function displayDate5(){
		
		window.open('http://47.93.220.57/groupbuying/api/GetExcel', 'newwindow', 'height=600, width=800, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no')
	}
	document.getElementById("btn6").onclick=function(){displayDate6()};
	function displayDate6(){
		window.open('index2.html', 'newwindow', 'height=1000, width=1500, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no')
	}
}


			
			
			function de(){
				var abi=0;
			  	 var off=confirm("确认删除此商品？");
			  			if(off){	
			  				$('#right').one('click', '.card', function () {
			  					var oDiv =$(this).find("div[class='product_id']").text();
			  					$(this).hide(function(){});
		  						$.get("http://47.93.220.57/groupbuying/api/Delete2",
							      {ids:oDiv},function(data){
							        if(data){alert("已删除");}
									else{alert("删除失败");}
									});
				  			});
			  			}
			}
			function gai(){
				$('#right').one('click', '.card', function () {
			  					var oDiv =$(this).find("div[class='product_id']").text();
			  					var uDiv = window.prompt("请输入改后的价格");

		  						$.get("http://47.93.220.57/groupbuying/api/AlterPrice",
							      {
									product_id:oDiv,
									price:uDiv
									},function(data){
							        if(data){alert("更改完毕");}
									else{alert("更改失败");}
									});
				  			});
			}
			$(document).ready(function(){

				see();//商品显示
				


			});