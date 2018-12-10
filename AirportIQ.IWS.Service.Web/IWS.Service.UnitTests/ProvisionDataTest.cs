using AirportIQ.IWS.Service.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace IWS.Service.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ProvisionDataTest and is intended
    ///to contain all ProvisionDataTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ProvisionDataTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for ProvisionData Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void ProvisionDataConstructorTest()
		{
			int card = 0; // TODO: Initialize to an appropriate value
			int door = 0; // TODO: Initialize to an appropriate value
			ProvisionData target = new ProvisionData(card, door);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for Card
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		[DeploymentItem("AirportIQ.IWS.Service.Web.dll")]
		public void CardTest()
		{
			PrivateObject param0 = null; // TODO: Initialize to an appropriate value
			ProvisionData_Accessor target = new ProvisionData_Accessor(param0); // TODO: Initialize to an appropriate value
			int expected = 0; // TODO: Initialize to an appropriate value
			int actual;
			target.Card = expected;
			actual = target.Card;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Door
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		[DeploymentItem("AirportIQ.IWS.Service.Web.dll")]
		public void DoorTest()
		{
			PrivateObject param0 = null; // TODO: Initialize to an appropriate value
			ProvisionData_Accessor target = new ProvisionData_Accessor(param0); // TODO: Initialize to an appropriate value
			int expected = 0; // TODO: Initialize to an appropriate value
			int actual;
			target.Door = expected;
			actual = target.Door;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
