using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using ImageWare.ISBLibrary;

namespace ISBLibTest
{
	using NUnit.Framework;
	/******************
	 * 
	 *	NOTE:	to use NUnit, do the following:
	 *	
	 *				1 - Create a new NUnit project (within the NUnit app)  and use Project | Add VS Project to add your VS project with the test code you have added 
	 *				2 - from NUNit|Tools|Settings|Test Loader|Assembly Isolation
	 *						set 'Default Domain Usage' to "Use a single AppDomain for all tests'
	 *				3 - from Project | Edit
	 *						set 'Configuration File Name' to web.config (or whatever you are using)
	 * 
	 * ****************/
	[TestFixture]
	public class ISBTest
	{

		static ISBLibExtGCR testSvc;

		Guid testPerson = new Guid("6E6BCBA4-5F5C-FB94-6C53-550DB5A482D3");
		string TestPersonString = "6E6BCBA4-5F5C-FB94-6C53-550DB5A482D3";
		int docType = 0;


		[SetUp]
		public void Init()
		{
			// to debug from an NUnit invocation, uncomment the following line
			//System.Diagnostics.Debugger.Launch();
			testSvc = new ISBLibExtGCR();
		}


		[Test, Description("public List<isblib.DMDocument> GetDocument(string personGUID, int docType)")]
		public void GetDocument()
		{
			List<ImageWare.ISBLibrary.DMDocument> docs = testSvc.GetDocument(TestPersonString, docType);

			Assert.IsTrue(1 == 1);
			Assert.IsFalse(1 == 0);
		}

		[Test, Description("public List<isblib.DMDocumentInfo> GetPersonDocuments(string personGUID)")]
		public void GetPersonDocuments()
		{
			List<ImageWare.ISBLibrary.DMDocumentInfo> personDocs = testSvc.GetPersonDocuments(TestPersonString);

			Assert.IsTrue(1 == 1);
			Assert.IsFalse(1 == 0);
		}

		[Test, Description("public isblib.IDMSPerson GetPerson(string personGUID)")]
		public void GetPerson()
		{
			ImageWare.ISBLibrary.IDMSPerson person = testSvc.GetPerson(TestPersonString);

			Assert.IsTrue(1 == 1);
			Assert.IsFalse(1 == 0);
		}


	}
}
