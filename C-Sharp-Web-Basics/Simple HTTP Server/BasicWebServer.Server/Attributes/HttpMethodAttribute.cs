using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAttribute:Attribute
    {
        public Method HttpMethod { get; }

        protected HttpMethodAttribute(Method httpMethod) 
            =>HttpMethod = httpMethod;
    }
}
