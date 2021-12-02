using GenericResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
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

        XmlSerializer xsSubmit = new XmlSerializer(typeof(Result));
        using var sww = new StringWriter();
        using XmlWriter writer = XmlWriter.Create(sww);
        xsSubmit.Serialize(writer, res);
        string xml = sww.ToString();

        Assert.IsNotNull(xml);
        Assert.IsTrue(xml.ToLower().Contains("success"));
        Assert.IsTrue(xml.ToLower().Contains("message"));
        Assert.IsFalse(xml.ToLower().Contains("diagnosticdata"));
    }

    [TestMethod]
    public void JsonSerializationTest()
    {
        var ex = new InvalidOperationException("Error Test");
        var res = new Result().Error("mensaje de prueba", ex);
        var jsonNetCore = System.Text.Json.JsonSerializer.Serialize<Result>((Result)res);
        var jsonNewtonSoft = Newtonsoft.Json.JsonConvert.SerializeObject(res);

        Assert.IsNotNull(jsonNewtonSoft);
        Assert.IsTrue(jsonNewtonSoft.ToLower().Contains("success"));
        Assert.IsTrue(jsonNewtonSoft.ToLower().Contains("message"));
        Assert.IsFalse(jsonNewtonSoft.ToLower().Contains("diagnosticdata"));

        Assert.IsNotNull(jsonNetCore);
        Assert.IsTrue(jsonNetCore.ToLower().Contains("success"));
        Assert.IsTrue(jsonNetCore.ToLower().Contains("message"));
        Assert.IsFalse(jsonNetCore.ToLower().Contains("diagnosticdata"));
    }
}