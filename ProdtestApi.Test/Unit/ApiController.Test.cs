using ProdtestApi.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace ProdtestApi.Test.Unit;

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

        Assert.AreEqual(okObjectResult.StatusCode, 200);
        Assert.AreEqual(okObjectResult.Value, "{\"health\": \"ok\" }");
    }
}