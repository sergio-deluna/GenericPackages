using GenericResult;
using GenericResult.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class ResultGenericTest
    {
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