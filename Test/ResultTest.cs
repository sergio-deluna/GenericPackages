using GenericResult;
using GenericResult.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Test
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void ResultOk()
        {
            IResult res = new Result().Ok();

            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.DiagnosticData);
            Assert.IsFalse(res.DiagnosticData.Any());
            Assert.IsTrue(string.IsNullOrEmpty(res.Message));
        }

        [TestMethod]
        public void ResultOkWithMessage()
        {
            var msg = "Operation Successful";
            IResult res = new Result().Ok(msg);

            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.DiagnosticData);
            Assert.IsFalse(res.DiagnosticData.Any());
            Assert.IsTrue(res.Message == msg);
        }

        [TestMethod]
        public void ResultOkWithOptParams()
        {
            var msg = "Operation Successful";
            object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };

            IResult res = new Result().Ok(msg, optionalParams);

            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.DiagnosticData);
            Assert.IsTrue(res.DiagnosticData.Any());
            Assert.IsTrue(res.DiagnosticData.Length == optionalParams.Length);
            Assert.IsTrue(res.Message == msg);
        }

        [TestMethod]
        public void SimpleResultError()
        {
            object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };
            var msg = "Error test message";

            IResult res = new Result().Error(msg, optionalParams);

            Assert.IsFalse(res.Success);
            Assert.IsTrue(res.DiagnosticData.Length == optionalParams.Length);
            Assert.IsTrue(res.Message == msg);
        }

        [TestMethod]
        public void SimpleResultException()
        {
            object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };
            var msg = "Exception error test message";

            IResult res = new Result().Error(new InvalidOperationException(msg), optionalParams);

            Assert.IsFalse(res.Success);
            // + exception message
            Assert.IsTrue(res.DiagnosticData.Length == optionalParams.Length + 1);
            Assert.IsTrue(res.Message == string.Empty);
        }

        [TestMethod]
        public void SimpleResultFullError()
        {
            object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };
            var msgEx = "Exception error test message";
            var msg = "Error test message";
            IResult res = new Result().Error(msg, new InvalidOperationException(msgEx), optionalParams);

            Assert.IsFalse(res.Success);
            // + exception
            Assert.IsTrue(res.DiagnosticData.Length == optionalParams.Length + 1);
            Assert.IsTrue(res.Message == msg);
        }
    }
}