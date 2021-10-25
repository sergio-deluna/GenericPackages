using GenericResult;
using GenericResult.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void SimpleResultOk()
        {
            IResult res = new Result().Ok();

            Assert.IsTrue(res.Success);
            Assert.IsNull(res.DiagnosticData);
            Assert.IsTrue(string.IsNullOrEmpty(res.Message));
        }

        [TestMethod]
        public void SimpleResultError()
        {
            IResult res = new Result().Error("Error test message");

            Assert.IsFalse(res.Success);
            Assert.IsTrue(res.DiagnosticData.Length == 0);
            Assert.IsTrue(!string.IsNullOrEmpty(res.Message));
        }

        [TestMethod]
        public void SimpleResultDiagnosticData()
        {
            IResult res = new Result().Error("", null);

            Assert.IsFalse(res.Success);
            Assert.IsTrue(res.DiagnosticData.Length == 0);
            Assert.IsTrue(string.IsNullOrEmpty(res.Message));
        }

        [TestMethod]
        public void SimpleResultDiagnosticDataGeneric()
        {
            IResult<bool> res = new Result<bool>().Error("", null);

            Assert.IsFalse(res.Success);
            Assert.IsTrue(res.DiagnosticData.Length == 0);
            Assert.IsTrue(string.IsNullOrEmpty(res.Message));
        }

        [TestMethod]
        public void SimpleResultDiagnosticDataGeneric2()
        {
            object[] someData = new object[] { true, false, 1, 3.3, "ultimo" };
            IResult<bool> res = new Result<bool>().Error("Mensaje de errorde prueba", someData);

            Assert.IsFalse(res.Success);
            Assert.IsTrue(res.DiagnosticData.Length == someData.Length);
            Assert.IsTrue(!string.IsNullOrEmpty(res.Message));
        }

        [TestMethod]
        public void SimpleResultDiagnosticDataGeneric3()
        {
            object[] someData = new object[] { "uno", "dos", 3, 4.0, null };
            IResult<bool> res = new Result<bool>().Error("Mensaje de errorde prueba", true, false, someData, "ultimo");

            Assert.IsFalse(res.Success);
            Assert.IsTrue(res.DiagnosticData.Length == 4);
            Assert.IsTrue(!string.IsNullOrEmpty(res.Message));
        }

        [TestMethod]
        public void SimpleResultDiagnosticDataException()
        {
            IResult<bool> res;
            string testMessage = "Test of failing";
            try
            {
                throw new InvalidOperationException(testMessage);
            }
            catch (System.Exception ex)
            {
                object[] someData = new object[] { "uno", "dos", 3, 4.0, null };
                res = new Result<bool>().Error(ex, true, false, someData, "ultimo");
            }
            Assert.IsFalse(res.Success);
            Assert.IsTrue(res.DiagnosticData.Length == 4);
            Assert.IsTrue(!string.IsNullOrEmpty(res.Message));
            Assert.IsTrue(res.Message.Equals(testMessage));
        }
    }
}