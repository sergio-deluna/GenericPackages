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
            public Guid Id { get; set; }
            public string Text { get; set; }
        }

        [TestMethod]
        public void ResultOk()
        {
            var data = new mockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<mockClass>().Ok(data);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == string.Empty);
            Assert.IsFalse(res.DiagnosticData.Any());

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void ResultOkWithMessage()
        {
            var msg = "Success";
            var data = new mockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<mockClass>().Ok(msg, data);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == msg);
            Assert.IsFalse(res.DiagnosticData.Any());

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void ResultOkWithOptionalParams()
        {
            object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };

            var data = new mockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<mockClass>().Ok(data, optionalParams);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == string.Empty);
            Assert.IsTrue(res.DiagnosticData.Length == optionalParams.Length);

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void ResultOkFull()
        {
            var msg = "Success";
            object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };

            var data = new mockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<mockClass>().Ok(msg, data, optionalParams);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == msg);
            Assert.IsTrue(res.DiagnosticData.Length == optionalParams.Length);

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void GenericResultError()
        {
            var data = new mockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<mockClass>().Error(data);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == string.Empty);
            Assert.IsFalse(res.DiagnosticData.Any());

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void GenericResultErrorWithMessage()
        {
            var msg = "Error";
            var data = new mockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<mockClass>().Error(msg, data);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == msg);
            Assert.IsFalse(res.DiagnosticData.Any());

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void GenericResultErrorWithEx()
        {
            var ex = new InvalidOperationException("Error");

            var res = new Result<mockClass>().Error(ex);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNull(res.Object);
            Assert.IsTrue(res.Message == string.Empty);
            Assert.IsTrue(res.DiagnosticData.Any());
            Assert.IsTrue(res.DiagnosticData.Length == 1);
            Assert.IsNull(res.Object);
        }

        [TestMethod]
        public void GenericResultErrorWithDataEx()
        {
            var ex = new InvalidOperationException("Error");
            var data = new mockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };

            var res = new Result<mockClass>().Error(data, ex);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == string.Empty);
            Assert.IsTrue(res.DiagnosticData.Any());
            Assert.IsTrue(res.DiagnosticData.Length == 1);

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }

        [TestMethod]
        public void GenericResultFullError()
        {
            var msg = "Error";
            var ex = new InvalidOperationException("Error");
            var data = new mockClass
            {
                Id = Guid.NewGuid(),
                Text = "Test text"
            };
            object[] optionalParams = new object[] { "uno", "dos", 3, 4.0, null };

            var res = new Result<mockClass>().Error(msg, data, ex, optionalParams);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Object);
            Assert.IsTrue(res.Message == msg);
            Assert.IsTrue(res.DiagnosticData.Any());
            Assert.IsTrue(res.DiagnosticData.Length == optionalParams.Length + 1);

            Assert.IsInstanceOfType(res.Object, typeof(mockClass));
            Assert.AreEqual(data.Id, res.Object.Id);
            Assert.AreEqual(data.Text, res.Object.Text);
        }
    }
}