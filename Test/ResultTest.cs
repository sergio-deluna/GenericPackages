using GenericResult;
using GenericResult.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace Test
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void toStringTest()
        {
            object[] optionalParams = ["uno", "dos", 3, 4.0, null];
            var msgEx = "Exception error test message";
            var msg = "Error test message";
            IResult res = new Result().Ok(msg);
            string txt = res.ToString();
            Assert.IsNotNull(txt);
            Debug.WriteLine(txt);

            res = new Result().Error(msg, new InvalidOperationException(msgEx), optionalParams);
            txt = res.ToString();
            Assert.IsNotNull(txt);
            Debug.WriteLine(txt);
        }

        [TestMethod]
        public void ResultOk()
        {
            IResult res = new Result().Ok();

            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.DiagnosticData);
            Assert.AreEqual(0, res.DiagnosticData.Length);
            Assert.IsTrue(string.IsNullOrEmpty(res.Message));
        }

        [TestMethod]
        public void ResultOkWithMessage()
        {
            var msg = "Operation Successful";
            IResult res = new Result().Ok(msg);

            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.DiagnosticData);
            Assert.AreEqual(0, res.DiagnosticData.Length);
            Assert.AreEqual(msg, res.Message);
        }

        [TestMethod]
        public void ResultOkWithOptParams()
        {
            var msg = "Operation Successful";
            object[] optionalParams = ["uno", "dos", 3, 4.0, null];

            IResult res = new Result().Ok(msg, optionalParams);

            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.DiagnosticData);
            Assert.AreNotEqual(0, res.DiagnosticData.Length);
            Assert.AreEqual(optionalParams.Length, res.DiagnosticData.Length);
            Assert.AreEqual(msg, res.Message);
        }

        [TestMethod]
        public void SimpleResultError()
        {
            object[] optionalParams = ["uno", "dos", 3, 4.0, null];
            var msg = "Error test message";

            IResult res = new Result().Error(msg, optionalParams);

            Assert.IsFalse(res.Success);
            Assert.AreEqual(optionalParams.Length, res.DiagnosticData.Length);
            Assert.AreEqual(msg, res.Message);
        }

        [TestMethod]
        public void SimpleResultException()
        {
            object[] optionalParams = ["uno", "dos", 3, 4.0, null];
            var msg = "Exception error test message";

            IResult res = new Result().Error(new InvalidOperationException(msg), optionalParams);

            Assert.IsFalse(res.Success);
            // + exception message
            Assert.AreEqual(optionalParams.Length + 1, res.DiagnosticData.Length);
            Assert.AreEqual(string.Empty, res.Message);
        }

        [TestMethod]
        public void SimpleResultFullError()
        {
            object[] optionalParams = ["uno", "dos", 3, 4.0, null];
            var msgEx = "Exception error test message";
            var msg = "Error test message";
            IResult res = new Result().Error(msg, new InvalidOperationException(msgEx), optionalParams);

            Assert.IsFalse(res.Success);
            // + exception
            Assert.AreEqual(optionalParams.Length + 1, res.DiagnosticData.Length);
            Assert.AreEqual(msg, res.Message);
        }
    }
}