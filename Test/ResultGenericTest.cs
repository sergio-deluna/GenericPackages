using GenericResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Test
{
    [TestClass]
    public class ResultGenericTest
    {
        internal class mockClass
        {
            public Guid id { get; set; }
            public string text { get; set; }
        }

        [TestMethod]
        public void ResultOk()
        {
            var data = new mockClass
            {
                id = Guid.NewGuid(),
                text = "Test text"
            };

            var res = new Result<mockClass>().Ok(data);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == string.Empty);
            Assert.IsFalse(res.DiagnosticData.Any());

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.id, res.Object.id);
            Assert.AreEqual(data.text, res.Object.text);
        }

        [TestMethod]
        public void ResultOkWithMessage()
        {
            var msg = "Success";
            var data = new mockClass
            {
                id = Guid.NewGuid(),
                text = "Test text"
            };

            var res = new Result<mockClass>().Ok(msg, data);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == msg);
            Assert.IsFalse(res.DiagnosticData.Any());

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.id, res.Object.id);
            Assert.AreEqual(data.text, res.Object.text);
        }

        [TestMethod]
        public void ResultOkWithOptionalParams()
        {
            object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };

            var data = new mockClass
            {
                id = Guid.NewGuid(),
                text = "Test text"
            };

            var res = new Result<mockClass>().Ok(data, optionalParams);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == string.Empty);
            Assert.IsTrue(res.DiagnosticData.Length == optionalParams.Length);

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.id, res.Object.id);
            Assert.AreEqual(data.text, res.Object.text);
        }

        [TestMethod]
        public void ResultOkFull()
        {
            var msg = "Success";
            object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };

            var data = new mockClass
            {
                id = Guid.NewGuid(),
                text = "Test text"
            };

            var res = new Result<mockClass>().Ok(msg,data, optionalParams);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == msg);
            Assert.IsTrue(res.DiagnosticData.Length == optionalParams.Length);

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.id, res.Object.id);
            Assert.AreEqual(data.text, res.Object.text);
        }

        //[TestMethod]
        //public void SimpleResultDiagnosticDataGeneric3()
        //{
        //    object[] someData = new object[] { "uno", "dos", 3, 4.0, null };
        //    IResult<bool> res = new Result<bool>().Error("Mensaje de error de prueba", true, false, someData, "ultimo");

        //    Assert.IsFalse(res.Success);
        //    Assert.IsTrue(res.DiagnosticData.Length == 3);
        //    Assert.IsTrue(!string.IsNullOrEmpty(res.Message));
        //}

        //[TestMethod]
        //public void SimpleResultDiagnosticDataException()
        //{
        //    IResult<bool> res;
        //    string testMessage = "Test of failing";
        //    try
        //    {
        //        throw new InvalidOperationException(testMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        object[] someData = new object[] { "uno", "dos", 3, 4.0, null };
        //        res = new Result<bool>().Error("error", false, ex, someData);
        //    }
        //    Assert.IsFalse(res.Success);
        //    Assert.IsTrue(res.DiagnosticData.Length == 5); // includes the exception object
        //    Assert.IsTrue(!string.IsNullOrEmpty(res.Message));
        //    Assert.IsTrue(res.Message.Equals(testMessage));
        //}
    }
}