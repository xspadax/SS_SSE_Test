using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [Route("/echo/{ToEcho}")]
    public class EchoRequest : IReturn<EchoResponse>
    {
        public string ToEcho { get; set; }
    }
    public class EchoResponse:IHasResponseStatus{
        public string Echoed { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Route("/SSETest/{Channel}/chat")]
    public class SSETestRequest : IReturn<SSETestResponse>
    {
        public string From { get; set; }
        public string ToUserId { get; set; }
        public string Channel { get; set; }
        public string Message { get; set; }
        public string Selector { get; set; }
    }

    public class SSETestResponse : IHasResponseStatus{
        public string EchoMessage { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/SSETestRemote/{Channel}/{Selector}/{Message}")]
    [Route("/SSETestRemote/{Channel}/{Selector}/{Message}/{ToUserId}")]
    public class SSETestRemoteControlRequest : IReturnVoid
    {
        public string ToUserId { get; set; }
        public string Channel { get; set; }
        public string Message { get; set; }
        public string Selector { get; set; }
    }

   
}
