//查看购物车遮罩
$('.opacity').click(function () {
    $('#buy_list').toggle();
    $(this).toggle();
    if ($('#share-tip').is(':visible')) {
        $('#share-tip').hide();
    }
});

//未提交时 查看购物车
function show_buy_list() {
    $('#buy_list').toggle();
    $('.opacity').toggle();
    var str = '';
    $('.pInfo').each(function () {
        if ($(this).find('i').text() != 0) {
            var x = '<tr><td>' + $(this).find('.x-title').text() + '</td><td>' + $(this).find('i').text() + '</td></tr>';
            str = str + x;
        }
    });
    $('#buy_list').find('table').html(str);
}

//提交购物车
function submit() {
    var result = [], p_single = 0;
    var order_number = group_number + '|' + localStorage.openid;
    var client_name = localStorage.client_name;
    //假单
    if ($('#false-from').is(':checked')) {
        order_number = order_number + '|' + new Date().getTime();
        p_single = 1;
        client_name = $('#false-name').val();
    }
    $('#none').show();
    $('.pInfo').each(function () {
        if ($(this).find('i').text() != 0) {
            var x = {product_id: $(this).attr('id'), product_number: $(this).find('i').text()};
            result.push(x)
        }
    });


    ajax({
        url: furl + '/SubShopList',
        type: 'POST',
        data: {
            value: result,
            open_id: localStorage.openid,
            group_number: group_number,//第几期
            order_number: order_number,//订单号
            p_single: p_single,//假单
            rc_id: localStorage.rc_id,
            client_name: client_name
        },
        success: function (res) {
            if (res == '0') {
                alert('提交失败');
                $('#none').hide();
            }
            if (res == '1') {
                alert('提交成功');
                if (rc == '1') {
                    location.reload();
                }

                $('.buying').hide();
                $('#none').hide();
                $('.right_box').empty();
                init();
            }
            if (res == '-1') {
                alert('未到开团时间');
                $('#none').hide();
            }
            if (res == '2') {
                alert('真单只能提交一次，请到订单列表重新下单');
                $('#none').hide();
            }
        },
    });
}

//删除
$('#delete-this').click(function () {
    var r = confirm("是否删除当前订单重新选择商品");
    if (r == true) {
        ajax({
            url: furl + '/Delete',
            type: 'get',
            data: {'ordernumber': order_number},
            success: function (res) {
                if (res == '1') {
                    if (rc == '1') {
                        location.reload();
                    }
                    $('#bought').hide();
                    init();
                    $('.buying').show();
                    $('#buy_list').hide();
                }
                else {
                    alert('删除失败')
                }
            }
        })
    }
    else {
    }
});

//分享
function wx_share(share_text) {
    wx.ready(function () {
        var shareData = {
            title: '我今天又在海螺王抢购了',
            desc: '我买了' + share_text,
            link: 'http://xmhome.xyz/groupbuying/api/s301',
            imgUrl: 'http://47.93.220.57/GROUP_BUY_PRODUCT/longxia1.jpg',
            success: function (res) {
                alert('已分享');
            },
            cancel: function (res) {
                alert('已取消');
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        };
        if (wx.onMenuShareAppMessage) { //微信文档中提到这两个接口即将弃用，故判断
            wx.onMenuShareAppMessage(shareData);//1.0 分享到朋友
            wx.onMenuShareTimeline(shareData);//1.0分享到朋友圈
        } else {
            wx.updateAppMessageShareData(shareData);//1.4 分享到朋友
            wx.updateTimelineShareData(shareData);//1.4分享到朋友圈
        }
    });
}

$('#share-this').click(function () {
    $('#share-tip').show();
    $('.opacity').css('background-color', '#f1f1f1').show();
});

//获得假单接口
function false_order(temp_ordernumber) {
    order_number = temp_ordernumber;
    $('.buying').hide();
    $('#bought_list table').empty();
    $('#bought').show();
    ajax({
        url: furl + '/GetShopList',
        type: 'get',
        data: {order_number: order_number},
        success: function (all) {
            var share_text = '';
            client_name = all[0].client_name;
            $('#bought #title').text(client_name + '的购物清单');
            for (var i in all) {
                share_text = share_text + all[i].product_name + ' ';
                var card1 = '<tr><td>' + all[i].product_name + '</td> <td>x' + all[i].product_number + '</td></tr>'
                $('#bought_list table').append(card1);
            }
            wx_share(share_text + '...');
        }
    })
}
