using Microsoft.AspNetCore.Mvc;
using Order.API.Controllers;

namespace Order.API.Test.Unit;

[TestClass]
public class ApiControllerTest
{
    [TestMethod]
    public void GET_HealthEndpoint_Returns_HealthOk()
    {
        var controller = new ApiController();

        var result = controller.HealthCheck();

        var okObjectResult = result as OkObjectResult;

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        Assert.IsNotNull(okObjectResult);

        Assert.AreEqual(200, okObjectResult.StatusCode);
        Assert.AreEqual("{\"health\": \"ok\" }", okObjectResult.Value);
    }
}
