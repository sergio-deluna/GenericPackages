using GenericResult;
using GenericResult.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Test;

[TestClass]
public class LogTest
{
    private readonly ILogger<LogTest> _logger = LoggerFactory
            .Create(logging => logging.AddConsole().AddDebug())
            .CreateLogger<LogTest>();

    [TestMethod]
    public void SimpleLogTest()
    {
        IxResult res = new Result().Ok("TEST WITH LOGGING ENABLED").Log(this._logger);

        Assert.IsTrue(res.Success);
        Assert.IsNotNull(res.DiagnosticData);
        Assert.IsFalse(res.DiagnosticData.Any());
        Assert.IsFalse(string.IsNullOrEmpty(res.Message));
    }
}