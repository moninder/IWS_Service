using AirportIQ.IWS.Service.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;

namespace IWS.Service.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for IIwsServiceTest and is intended
    ///to contain all IIwsServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class IIwsServiceTest
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


		internal virtual IIwsService CreateIIwsService()
		{
			// TODO: Instantiate an appropriate concrete class.
			IIwsService target = null;
			return target;
		}

		/// <summary>
		///A test for BiometricUpdate
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void BiometricUpdateTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			Guid personID = new Guid(); // TODO: Initialize to an appropriate value
			byte[] image = null; // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.BiometricUpdate(personID, image);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CardStatus
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void CardStatusTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			Guid personID = new Guid(); // TODO: Initialize to an appropriate value
			int cardID = 0; // TODO: Initialize to an appropriate value
			int badgeID = 0; // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.CardStatus(personID, cardID, badgeID);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CriminalHistoryCheckAccepted
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void CriminalHistoryCheckAcceptedTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			Guid personID = new Guid(); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.CriminalHistoryCheckAccepted(personID);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CriminalHistoryCheckSubmitted
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void CriminalHistoryCheckSubmittedTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			Guid personID = new Guid(); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.CriminalHistoryCheckSubmitted(personID);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for DeactivateBadge
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void DeactivateBadgeTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			Guid personID = new Guid(); // TODO: Initialize to an appropriate value
			int badgeID = 0; // TODO: Initialize to an appropriate value
			int cardID = 0; // TODO: Initialize to an appropriate value
			string reason = string.Empty; // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.DeactivateBadge(personID, badgeID, cardID, reason);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for EndBiometricCapture
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void EndBiometricCaptureTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			Guid personID = new Guid(); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.EndBiometricCapture(personID);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsAlive
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void IsAliveTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsAlive();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsDbConnected
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void IsDbConnectedTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsDbConnected();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ProvisionedByBadge
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void ProvisionedByBadgeTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			int badgeID = 0; // TODO: Initialize to an appropriate value
			List<ProvisionData> expected = null; // TODO: Initialize to an appropriate value
			List<ProvisionData> actual;
			actual = target.ProvisionedByBadge(badgeID);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ProvisionedByCard
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void ProvisionedByCardTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			int cardID = 0; // TODO: Initialize to an appropriate value
			List<ProvisionData> expected = null; // TODO: Initialize to an appropriate value
			List<ProvisionData> actual;
			actual = target.ProvisionedByCard(cardID);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ProvisionedByPerson
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void ProvisionedByPersonTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			Guid personID = new Guid(); // TODO: Initialize to an appropriate value
			List<ProvisionData> expected = null; // TODO: Initialize to an appropriate value
			List<ProvisionData> actual;
			actual = target.ProvisionedByPerson(personID);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for RetreiveFbiHistoryUrl
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void RetreiveFbiHistoryUrlTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			Guid personID = new Guid(); // TODO: Initialize to an appropriate value
			string expected = string.Empty; // TODO: Initialize to an appropriate value
			string actual;
			actual = target.RetreiveFbiHistoryUrl(personID);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for UpdatePersonStatus
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[AspNetDevelopmentServerHost("C:\\Users\\latchison\\code\\FAU\\Dev\\AirportIQ.IWS.Service.Web\\AirportIQ.IWS.Service.Web", "/")]
		[UrlToTest("http://localhost:61671/")]
		public void UpdatePersonStatusTest()
		{
			IIwsService target = CreateIIwsService(); // TODO: Initialize to an appropriate value
			Guid personID = new Guid(); // TODO: Initialize to an appropriate value
			int expected = 0; // TODO: Initialize to an appropriate value
			int actual;
			actual = target.UpdatePersonStatus(personID);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
