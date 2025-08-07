using GenericResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Test;

[TestClass]
public class SimpleSerializationTest
{
    [TestMethod]
    public void XmlSerializationTest()
    {
        var ex = new InvalidOperationException("Error Test");
        var res = new Result().Error("mensaje de prueba", ex);

        XmlSerializer xsSubmit = new(typeof(Result));
        using var sww = new StringWriter();
        using XmlWriter writer = XmlWriter.Create(sww);
        xsSubmit.Serialize(writer, res);
        string xml = sww.ToString();

        Assert.IsNotNull(xml);
        Assert.Contains("success", xml.ToLower());
        Assert.Contains("message", xml.ToLower());
        Assert.DoesNotContain("diagnosticdata", xml.ToLower());
    }

    [TestMethod]
    public void JsonSerializationTest()
    {
        var ex = new InvalidOperationException("Error Test");
        var res = new Result().Error("mensaje de prueba", ex);

        string json = JsonSerializer.Serialize(res);

        Assert.IsNotNull(json);
        Assert.IsTrue(json.ToLower().Contains("success"));
        Assert.IsTrue(json.ToLower().Contains("message"));
        Assert.IsFalse(json.ToLower().Contains("diagnosticdata"));
    }
}