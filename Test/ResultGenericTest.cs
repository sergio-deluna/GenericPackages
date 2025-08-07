using GenericResult;
using GenericResult.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace Test
{
    [TestClass]
    public class ResultGenericTest
    {
        internal class MockClass
        {
            public Guid Id { get; set; }
            public string Text { get; set; }
        }

        [TestMethod]
        public void ToStringTest()
        {
            var data = new MockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            object[] optionalParams = ["uno", "dos", 3, 4.0, null];
            var msgEx = "Exception error test message";
            var msg = "Error test message";
            IResult<MockClass> res = new Result<MockClass>().Ok(msg, data);
            string txt = res.ToString();
            Assert.IsNotNull(txt);
            Debug.WriteLine(txt);

            res = new Result<MockClass>().Error(msg, data, new InvalidOperationException(msgEx), optionalParams);
            txt = res.ToString();
            Assert.IsNotNull(txt);
            Debug.WriteLine(txt);
        }

        [TestMethod]
        public void ResultOk()
        {
            var data = new MockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<MockClass>().Ok(data);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.AreEqual(string.Empty, res.Message);
            Assert.AreEqual(0, res.DiagnosticData.Length);

            Assert.IsInstanceOfType(res.Object, typeof(MockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void ResultOkWithMessage()
        {
            var msg = "Success";
            var data = new MockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<MockClass>().Ok(msg, data);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.AreEqual(msg, res.Message);
            Assert.AreEqual(0, res.DiagnosticData.Length);

            Assert.IsInstanceOfType(res.Object, typeof(MockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void ResultOkWithOptionalParams()
        {
            object[] optionalParams = ["uno", "dos", 3, 4.0, null];

            var data = new MockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<MockClass>().Ok(data, optionalParams);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.AreEqual(string.Empty, res.Message);
            Assert.AreEqual(optionalParams.Length, res.DiagnosticData.Length);

            Assert.IsInstanceOfType(res.Object, typeof(MockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void ResultOkFull()
        {
            var msg = "Success";
            object[] optionalParams = ["uno", "dos", 3, 4.0, null];

            var data = new MockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<MockClass>().Ok(msg, data, optionalParams);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.AreEqual(msg, res.Message);
            Assert.AreEqual(optionalParams.Length, res.DiagnosticData.Length);

            Assert.IsInstanceOfType(res.Object, typeof(MockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void GenericResultError()
        {
            var data = new MockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<MockClass>().Error(data);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.AreEqual(string.Empty, res.Message);
            Assert.AreEqual(0, res.DiagnosticData.Length);

            Assert.IsInstanceOfType(res.Object, typeof(MockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void GenericResultErrorWithMessage()
        {
            var msg = "Error";
            var data = new MockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<MockClass>().Error(msg, data);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.AreEqual(msg, res.Message);
            Assert.AreEqual(0, res.DiagnosticData.Length);

            Assert.IsInstanceOfType(res.Object, typeof(MockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void GenericResultErrorWithEx()
        {
            var ex = new InvalidOperationException("Error");

            var res = new Result<MockClass>().Error(ex);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNull(res.Object);
            Assert.AreEqual(string.Empty, res.Message);
            Assert.AreNotEqual(0, res.DiagnosticData.Length);
            Assert.AreEqual(1, res.DiagnosticData.Length);
            Assert.IsNull(res.Object);
        }

        [TestMethod]
        public void GenericResultErrorWithDataEx()
        {
            var ex = new InvalidOperationException("Error");
            var data = new MockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<MockClass>().Error(data, ex);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.AreEqual(string.Empty, res.Message);
            Assert.AreNotEqual(0, res.DiagnosticData.Length);
            Assert.AreEqual(1, res.DiagnosticData.Length);

            Assert.IsInstanceOfType(res.Object, typeof(MockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void GenericResultFullError()
        {
            var msg = "Error";
            var ex = new InvalidOperationException("Error");
            var data = new MockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };
            object[] optionalParams = ["uno", "dos", 3, 4.0, null];

            var res = new Result<MockClass>().Error(msg, data, ex, optionalParams);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.AreEqual(msg, res.Message);
            Assert.AreNotEqual(0, res.DiagnosticData.Length);
            Assert.AreEqual(optionalParams.Length + 1, res.DiagnosticData.Length);

            Assert.IsInstanceOfType(res.Object, typeof(MockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }
    }
}