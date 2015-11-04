using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DataReceiver
{
    public class DataController : ApiController
    {
        // GET api/data 
        public IEnumerable<string> Get()
        {
            Console.WriteLine("Get request received!");
            
            //return some dummy data
            return new string[] { "Test", "Working" };
        }

        // GET api/data/5 
        public string Get(int id)
        {
            Console.WriteLine("Get request received for ID {0}!", id);

            return "data" + id;
        }
        
        // POST api/data 
        
        public void Post(object value)
        {
            Console.WriteLine("Post request received, data: {0}", value);
        }
    }
} 