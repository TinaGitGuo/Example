using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiMVC.Method
{
    public  class DefaultClass1
    {
        public  void Main( )
        {
            Debug.WriteLine("-------Main thread starting-------");
            Task<int> task = GetStrLengthAsync();
            Debug.WriteLine("Main thread contiune ..");
            Debug.WriteLine("Task return :" + task.Result);
            Debug.WriteLine("-------Main thread end-------");
        }

         async Task<int> GetStrLengthAsync()
        {
            Debug.WriteLine("GetStrLengthAsync starting ");
            //此处返回的<string>中的字符串类型，而不是Task<string>
            string str = await GetString();
            Debug.WriteLine("GetStrLengthAsync end");
            return str.Length;
        }

         Task<string> GetString()
        {
            Debug.WriteLine("GetString starting");
            return Task<string>.Run(() =>
            {
                Thread.Sleep(2000);
                Debug.WriteLine("GetString ready");
                return "GetString value";
            });
        }
    }
  
}