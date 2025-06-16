using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;

namespace Rhyous.WebApiExtensions.Tests.Wrappers;

[TestClass]
public class UrlParametersTests
{
    private UrlParameters CreateUrlParameters(IQueryCollection collection)
    {
        return new UrlParameters { Collection = collection };
    }

    #region GetValues  
    [TestMethod]
    public void UrlParameters_GetValues_HasKey_ReturnsValues()
    {
        // Arrange  
        string key1 = "Key1";
        var keyValues1 = new StringValues(new[] { "Value1a", "Value1b" });
        var dict = new Dictionary<string, StringValues>
           {
               { key1, keyValues1 }
           };
        var queryCollection = new QueryCollection(dict);
        var urlParameters = CreateUrlParameters(queryCollection);

        // Act  
        var result = urlParameters.GetValues(key1);

        // Assert  
        CollectionAssert.AreEqual(keyValues1.ToArray(), result.ToArray());
    }

    [TestMethod]
    public void UrlParameters_GetValues_DoesNotHaveKey_ReturnsEmptyValues()
    {
        // Arrange  
        string key1 = "Key1";
        var keyValues1 = new StringValues(new[] { "Value1a", "Value1b" });
        var dict = new Dictionary<string, StringValues>
           {
               { key1, keyValues1 }
           };
        var queryCollection = new QueryCollection(dict);
        var urlParameters = CreateUrlParameters(queryCollection);

        // Act  
        var result = urlParameters.GetValues("OtherKey");

        // Assert  
        Assert.AreEqual(StringValues.Empty, result);
    }

    [TestMethod]
    public void UrlParameters_GetValues_QueryCollectionNull_ReturnsEmptyValues()
    {
        // Arrange  
        QueryCollection queryCollection = null;
        var urlParameters = CreateUrlParameters(queryCollection);

        // Act  
        var result = urlParameters.GetValues("SomeKey");

        // Assert  
        Assert.AreEqual(StringValues.Empty, result);
    }

    [TestMethod]
    public void UrlParameters_GetValues_QueryCollectionEmpty_ReturnsEmptyValues()
    {
        // Arrange  
        var queryCollection = new QueryCollection();
        var urlParameters = CreateUrlParameters(queryCollection);

        // Act  
        var result = urlParameters.GetValues("SomeKey");

        // Assert  
        Assert.AreEqual(StringValues.Empty, result);
    }
    #endregion
}
