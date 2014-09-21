using Funq;
using Service;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Mvc;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MvcApplication2.App_Start.AppHost), "Start")]

namespace MvcApplication2.App_Start
{
    public class AppHost : AppHostBase
    {
        public static void Start()
        {
            new AppHost().Init();
        }    

        public AppHost()  : base("SSETest", typeof(SSEService).Assembly)
        {
            //var liveSettings = "~/appsettings.txt".MapHostAbsolutePath();
            //AppSettings = File.Exists(liveSettings)
            //    ? (IAppSettings)new TextFileSettings(liveSettings)
            //    : new AppSettings();
        }

        public override void Configure(Container container)
        {
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
 
            JsConfig.EmitCamelCaseNames = true;


            Plugins.Add(new RequestLogsFeature());
            //Plugins.Add(new RazorFormat());
            Plugins.Add(new ServerEventsFeature());
            SetConfig(new HostConfig
            {
                HandlerFactoryPath = "api",
                DefaultContentType = MimeTypes.Json
            });
            this.CustomErrorHttpHandlers.Remove(HttpStatusCode.Forbidden);

            //Register all Authentication methods you want to enable for this web app.            
            //Plugins.Add(new AuthFeature(
            //    () => new AuthUserSession(),
            //    new IAuthProvider[] {
            //        new TwitterAuthProvider(AppSettings),   //Sign-in with Twitter
            //        new FacebookAuthProvider(AppSettings),  //Sign-in with Facebook
            //        new GithubAuthProvider(AppSettings),    //Sign-in with GitHub OAuth Provider
            //    }));

            //container.RegisterAutoWiredAs<MemoryChatHistory, IChatHistory>();

            //var redisHost = AppSettings.GetString("RedisHost");
            //if (redisHost != null)
            //{
            //    container.Register<IRedisClientsManager>(new PooledRedisClientManager(redisHost));

            //    container.Register<IServerEvents>(c =>
            //        new RedisServerEvents(c.Resolve<IRedisClientsManager>()));
            //    container.Resolve<IServerEvents>().Start();
            //}
        }
    
    
    }

}