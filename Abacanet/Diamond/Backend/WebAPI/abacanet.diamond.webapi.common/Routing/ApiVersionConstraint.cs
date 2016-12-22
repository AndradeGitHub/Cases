using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace abacanet.diamond.webapi.common.Routing
{
    public class ApiVersionConstraint : IHttpRouteConstraint
    {
        public string AllowedVersion { get; private set; }

        public ApiVersionConstraint(string allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                return AllowedVersion.Equals(value.ToString().ToLowerInvariant());
            }
            return false;
        }
    }
}