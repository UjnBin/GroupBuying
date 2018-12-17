//字符串占位
        String.prototype.format = function () {
            if (arguments.length == 0) return this;
            var param = arguments[0];
            var s = this;
            if (typeof(param) == 'object') {
                for (var key in param)
                    s = s.replace(new RegExp("\\{" + key + "\\}", "g"), param[key]);
                return s;
            } else {
                for (var i = 0; i < arguments.length; i++)
                    s = s.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i]);
                return s;
            }
        };

        //ajax
        function ajax(p) {
            var dataType = p.dataType || 'json';//接收数据类型
            var async = p.async || false;//异步请求
            var error = p.error || function (xhr, status, error) {
                alert('出现错误' + '\n' + JSON.stringify(xhr) + '\n' + status + '\n' + error);
            };
            $.ajax({
                url: p.url,
                type: p.type,
                data: p.data,
                success: p.success,
                dataType: dataType,
                async: async,
                error: error,
                xhrFields: {
                    withCredentials: true
                },
                crossDomain: true,
            });
        }

        // 获得url参数
        var Request = {};
        Request = GetRequest();

        function GetRequest() {
            var url = location.search;
            var theRequest = new Object();
            if (url.indexOf("?") != -1) {
                var str = url.substr(1);
                strs = str.split("&");
                for (var i = 0; i < strs.length; i++) {
                    theRequest[strs[i].split("=")[0]] = decodeURI(strs[i].split("=")[1]);
                }
            }
            return theRequest;
        }

