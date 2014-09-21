using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace Service
{
    public class SSEService:ServiceStack.Service
    {
        public IServerEvents ServerEvents { get; set; }
        public IAppSettings AppSettings { get; set; }

        public object Any(EchoRequest request) {
            ServerEvents.NotifyChannel("default-channel", "cmd.echo", request.ToEcho);
            return new EchoResponse { Echoed = request.ToEcho };
        }
        public object Any(SSETestRemoteControlRequest request) {
            ServerEvents.NotifyChannel(request.Channel, request.Selector, request.Message);
            return null;
        }
        public object Any(SSETestRequest request) {            
            var response = new SSETestResponse { EchoMessage=request.Message};

            var sub = ServerEvents.GetSubscriptionInfo(request.From);
            if (sub == null)
                throw HttpError.NotFound("Subscription {0} does not exist".Fmt(request.From));

             
                // Notify everyone in the channel for public messages
                ServerEvents.NotifyChannel(request.Channel, request.Selector, request.Message);
            

            return response;
        }

    }
}
