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

		string defaultIP = "iws03qa8";
		Guid testPerson = new Guid("3545EA74-700D-43F3-B78B-4562E818E46E");
		string TestPersonString = "3545EA74-700D-43F3-B78B-4562E818E46E";
		int docType = 1;

		public ISBTest()
		{
			testSvc = new ISBLibExtGCR(defaultIP);
		}

		public ISBTest(string ip)
		{
			if (ip.Length > 1)
			{
				testSvc = new ISBLibExtGCR(ip);
			}
			else
			{
				testSvc = new ISBLibExtGCR(defaultIP);
			}
		}

		[SetUp]
		public void Init()
		{
			// to debug from an NUnit invocation, uncomment the following line
			//System.Diagnostics.Debugger.Launch();
		}


		[Test, Description("public List<isblib.DMDocument> GetDocument(string personGUID, int docType)")]
		public void GetDocument()
		{
			GetDocument(TestPersonString, docType);
		}
		
		public void GetDocument(string personString, int docType)
		{
			Debug.WriteLine("\nTesting GetDocument(" + personString + ", " + docType + ")");

			List<ImageWare.ISBLibrary.DMDocument> docs = testSvc.GetDocument(personString, docType);

			Debug.WriteLine("docs.ToString() output:" + docs.ToString());

		}


		[Test, Description("public List<isblib.DMDocumentInfo> GetPersonDocuments(string personGUID)")]
		public void GetPersonDocuments()
		{
			GetPersonDocuments(TestPersonString);
		}

		public void GetPersonDocuments(string personString)
		{
			Debug.WriteLine("\nTesting GetPersonDocuments(" + personString + ")");

			List<ImageWare.ISBLibrary.DMDocumentInfo> personDocs = testSvc.GetPersonDocuments(personString);

			Debug.WriteLine("personDocs.ToString() output:" + personDocs.ToString());

		}

		[Test, Description("public isblib.IDMSPerson GetPerson(string personGUID)")]
		public void GetPerson()
		{
			GetPerson(TestPersonString);
		}

		public void GetPerson(string personString)
		{

			Debug.WriteLine("\n\n\n\nTesting GetPerson(" + personString + ")");

			ImageWare.ISBLibrary.IDMSPerson person = testSvc.GetPerson(personString);

			Debug.WriteLine("person.ToString() output:" + person.ToString());
			Debug.WriteLine("last name is '" + person.LastName + "'.");
			Debug.WriteLine("first name is '" + person.FirstName + "'.");
			Debug.WriteLine("DOB is '" + person.DOB + "'.");
			Debug.WriteLine("status is '" + person.Status + "'.");
			Assert.IsTrue(1 == 1);

		}

		public void AllTests(string personString)
		{
			GetPerson(personString);
			GetPersonDocuments(personString);
			GetDocument(personString, 1);
		}


	}
}
