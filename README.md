

# RoutingSectionAssignment

This project demonstrates a simple ASP.NET Core application with custom routing constraints. It serves endpoints to retrieve information about countries based on country IDs.

## Prerequisites

- [.NET 8.0 SDK][https://dotnet.microsoft.com/en-us/download/dotnet/8.0]

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/MahmoodElbadri/RoutingSectionAssignment.git
   ```
2. Navigate to the project directory:
   ```bash
   cd routing-section-assignment
   ```
3. Build the project:
   ```bash
   dotnet build
   ```

## Running the Application

To run the application, use the following command:
```bash
dotnet run
```

The application will start and listen for HTTP requests.

## Endpoints

### Get All Countries

**URL:** `/countries`

**Method:** `GET`

**Response:**
- Status Code: `200 OK`
- Content Type: `application/json`
- Body: A JSON object containing all the countries.

Example:
```json
{
  "1": "United States",
  "2": "Canada",
  "3": "United Kingdom",
  "4": "India",
  "5": "Japan"
}
```

### Get Country by ID

**URL:** `/countries/{countryID:int:mini}`

**Method:** `GET`

**Constraints:** `countryID` should be an integer between 1 and 100 (inclusive).

**Response:**
- Status Code: `200 OK` (if country exists)
- Status Code: `404 Not Found` (if country does not exist)
- Content Type: `application/json`
- Body: A JSON string of the country name or `[No Country]` if the country does not exist.

Example:
```json
"United States"
```

**URL:** `/countries/{countryID}`

**Method:** `GET`

**Response:**
- Status Code: `400 Bad Request` (if `countryID` does not meet the constraints)
- Body: A plain text message indicating the valid range for `countryID`.

Example:
```
The CountryID should be between 1 and 100!
```

## Custom Number Constraint

This project includes a custom route constraint (`NumberConstraint`) to ensure that `countryID` is within the range of 1 to 100.

### CustomNumberConstraint/NumberConstraint.cs

```csharp
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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
```

### Adding the Custom Constraint

In the `Program.cs`, the custom constraint is registered:
```csharp
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("mini", typeof(NumberConstraint));
});
```

### Using the Custom Constraint

The custom constraint is applied in the route template for the `/countries/{countryID:int:mini}` endpoint.
