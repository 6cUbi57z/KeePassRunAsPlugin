using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RunAsPlugin.Utils;

namespace RunAsPlugin.Test.UtilTests
{
    [TestClass]
    public class UIUtilTests
    {
        [TestMethod]
        public void GetControlByName_WhenParentFormNull_ThrowsError()
        {
            // Arrange
            const string EXPECTED_PARAMETER_NAME = "parentForm";
            const string EXPECTED_EXCEPTION_MESSAGE = "Value cannot be null.\r\nParameter name: parentForm";

            // Act
            try
            {
                UIUtils.GetControlByName<Control>(null, "FakeControl");

                Assert.Fail("No exception was thrown.");
            }
            catch (Exception ex)
            {
                // Asset
                Assert.IsTrue(ex is ArgumentNullException);
                Assert.AreEqual(EXPECTED_PARAMETER_NAME, ((ArgumentNullException)ex).ParamName);
                Assert.AreEqual(EXPECTED_EXCEPTION_MESSAGE, ex.Message);
            }
        }

        [TestMethod]
        public void GetControlByName_WhenControlNameNull_ThrowsError()
        {
            // Arrange
            const string EXPECTED_PARAMETER_NAME = "controlName";
            const string EXPECTED_EXCEPTION_MESSAGE = "Value cannot be null, empty or whitespace.\r\nParameter name: controlName";

            Mock<Form> mockForm = new Mock<Form>();

            // Act
            try
            {
                UIUtils.GetControlByName<Control>(mockForm.Object, null);

                Assert.Fail("No exception was thrown.");
            }
            catch (Exception ex)
            {
                // Asset
                Assert.IsTrue(ex is ArgumentNullException);
                Assert.AreEqual(EXPECTED_PARAMETER_NAME, ((ArgumentNullException)ex).ParamName);
                Assert.AreEqual(EXPECTED_EXCEPTION_MESSAGE, ex.Message);
            }
        }

        [TestMethod]
        public void GetControlByName_WhenControlNameEmpty_ThrowsError()
        {
            // Arrange
            const string EXPECTED_PARAMETER_NAME = "controlName";
            const string EXPECTED_EXCEPTION_MESSAGE = "Value cannot be null, empty or whitespace.\r\nParameter name: controlName";

            Mock<Form> mockForm = new Mock<Form>();

            // Act
            try
            {
                UIUtils.GetControlByName<Control>(mockForm.Object, String.Empty);

                Assert.Fail("No exception was thrown.");
            }
            catch (Exception ex)
            {
                // Asset
                Assert.IsTrue(ex is ArgumentException);
                Assert.AreEqual(EXPECTED_PARAMETER_NAME, ((ArgumentException)ex).ParamName);
                Assert.AreEqual(EXPECTED_EXCEPTION_MESSAGE, ex.Message);
            }
        }

        [TestMethod]
        public void GetControlByName_WhenControlNameWhitespace_ThrowsError()
        {
            // Arrange
            const string EXPECTED_PARAMETER_NAME = "controlName";
            const string EXPECTED_EXCEPTION_MESSAGE = "Value cannot be null, empty or whitespace.\r\nParameter name: controlName";

            Mock<Form> mockForm = new Mock<Form>();

            // Act
            try
            {
                UIUtils.GetControlByName<Control>(mockForm.Object, "   ");

                Assert.Fail("No exception was thrown.");
            }
            catch (Exception ex)
            {
                // Asset
                Assert.IsTrue(ex is ArgumentException);
                Assert.AreEqual(EXPECTED_PARAMETER_NAME, ((ArgumentException)ex).ParamName);
                Assert.AreEqual(EXPECTED_EXCEPTION_MESSAGE, ex.Message);
            }
        }

        // Might have to create some test forms for these because the find method is not virtual.
        // TODO: Test exception when no control found.
        // TODO: Test exception when multiple controls found.
        // TODO: Test exception when control is not of specified type.
        // TODO: Test success.
    }
}
