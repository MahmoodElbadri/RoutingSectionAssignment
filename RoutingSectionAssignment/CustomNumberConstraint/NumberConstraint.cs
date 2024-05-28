
namespace RoutingSectionAssignment.CustomNumberConstraint
{
    public class NumberConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[routeKey] != null)
            {
                int number = Convert.ToInt32(values[routeKey]);
                if (number > 0 && number <= 100)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
