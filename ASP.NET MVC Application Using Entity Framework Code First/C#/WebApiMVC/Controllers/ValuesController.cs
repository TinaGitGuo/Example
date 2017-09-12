using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiMVC.Controllers
{
//    public static void RegisterTypes(IUnityContainer container)
//    {
//        var storageAccountType = typeof(StorageAccount);
//        var retryPolicyFactoryType = typeof(IRetryPolicyFactory);

//        // Instance registration
//        StorageAccount account =
//          ApplicationConfiguration.GetStorageAccount("DataConnectionString");
//        container.RegisterInstance(account);

//        // Register factories
//        container
//          .RegisterInstance<IRetryPolicyFactory>(
//            new ConfiguredRetryPolicyFactory())
//          .RegisterType<ISurveyAnswerContainerFactory,
//            SurveyAnswerContainerFactory>(
//              new ContainerControlledLifetimeManager());

//        // Register table types
//        container
//          .RegisterType<IDataTable<SurveyRow>, DataTable<SurveyRow>>(
//            new InjectionConstructor(storageAccountType,
//              retryPolicyFactoryType, Constants.SurveysTableName))
//          ...

//        // Register message queue type, use typeof with open generics
//        container
//          .RegisterType(
//              typeof(IMessageQueue<>),
//              typeof(MessageQueue<>),
//              new InjectionConstructor(storageAccountType,
//                retryPolicyFactoryType, typeof(String)));
 
//    ...
 
//    // Register store types
//    container
//      .RegisterType<ISurveyStore, SurveyStore>()
//      .RegisterType<ITenantStore, TenantStore>()
//      .RegisterType<ISurveyAnswerStore, SurveyAnswerStore>(
//        new InjectionFactory((c, t, s) => new SurveyAnswerStore(
//          container.Resolve<ITenantStore>(),
//          container.Resolve<ISurveyAnswerContainerFactory>(),
//          container.Resolve<IMessageQueue<SurveyAnswerStoredMessage>>(
//            new ParameterOverride(
//              "queueName", Constants.StandardAnswerQueueName)),
//          container.Resolve<IMessageQueue<SurveyAnswerStoredMessage>>(
//            new ParameterOverride(
//              "queueName", Constants.PremiumAnswerQueueName)),
//          container.Resolve<IBlobContainer<List<String>>>())));
//    }
//}
[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
