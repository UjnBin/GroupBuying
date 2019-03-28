using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

/// <summary>
/// 获取签名
/// 2018.11.30
/// @author:hanrubing, liyongshu
/// </summary>

namespace group_buy.Controllers
{
    public class GetSignatureController : ApiController
    {
        // GET: api/GetSignature
        public JObject Get(string url)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "http://xmhome.xyz");//实现跨域
            string access_token = "";
            var request = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx4acf59cd381f3e28&secret=04c36604ecb6165c4133eaaf71f4835b");
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string json = responseString.ToString();
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            if (jo.Property("access_token") != null)
            {
                access_token = jo["access_token"].ToString();
            }
            if ( access_token != null && access_token != "")
            {
                string ticket = "";
                var request2 = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + access_token.Trim() +"&type=jsapi");
                var response2 = (HttpWebResponse)request2.GetResponse();
                var responseString2 = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                string json2 = responseString2.ToString();
                JObject jo2 = (JObject)JsonConvert.DeserializeObject(json2);
                if (jo2.Property("ticket") != null)
                {
                    ticket = jo2["ticket"].ToString();
                }
                if ( ticket != null && ticket != "")
                {
                    char[] S = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                    string noncestr = "";
                    Random rd = new Random();
                    for (int i = 0; i < 10; i++)
                    {
                        noncestr += S[rd.Next(26)];
                    }
                    DateTime today = DateTime.Now;
                    DateTime END = Convert.ToDateTime("2010-1-1 02:22:00");
                    TimeSpan span = (TimeSpan)(today - END);
                    long time = (long)span.TotalMilliseconds/100;
                    string timestamp = time.ToString();
                    timestamp = timestamp.Replace(" ", "");
                    string str = "jsapi_ticket=" + ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;
                    var request3 = (HttpWebRequest)WebRequest.Create("http://47.93.220.57/sha1.php?" + str);
                    var response3 = (HttpWebResponse)request3.GetResponse();
                    var responseString3 = new StreamReader(response3.GetResponseStream()).ReadToEnd();
                    string json4 = responseString3.ToString();
                    string json3 = "{" + "'timestamp':'" + timestamp + "','nonceStr':'" + noncestr + "','signature':'" + json4 + "'}";
                    return (JObject)JsonConvert.DeserializeObject(json3); 
                }
            }
            return (JObject)JsonConvert.DeserializeObject("");
        }

        public static string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString().ToUpper();
        }

        public string EncryptToSHA1(string str)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_in = Encoding.Default.GetBytes(str);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            sha1.Dispose();
            string result = BitConverter.ToString(bytes_out);
            result = result.Replace("-", "").ToLower();
            return result;
        }

        public string getsha1(string str)
        {
            byte[] temp1 = Encoding.UTF8.GetBytes(str);
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] temp2 = sha.ComputeHash(temp1);
            sha.Clear();            // 注意， 不能用这个           
            // string output = Convert.ToBase64String(temp2);// 不能直接转换成 base64string            
            var output = BitConverter.ToString(temp2);
            output = output.Replace("-", "");
            output = output.ToLower();
            return output;
        }

        public static string GetSha1(string str)

        {

            //建立SHA1对象

            SHA1 sha = new SHA1CryptoServiceProvider();

            //将mystr转换成byte[]

            ASCIIEncoding enc = new ASCIIEncoding();

            byte[] dataToHash = enc.GetBytes(str);

            //Hash运算

            byte[] dataHashed = sha.ComputeHash(dataToHash);

            //将运算结果转换成string

            string hash = BitConverter.ToString(dataHashed).Replace("-", "");

            return hash;

        }

        // GET: api/GetSignature/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetSignature
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetSignature/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetSignature/5
        public void Delete(int id)
        {
        }
    }
}
