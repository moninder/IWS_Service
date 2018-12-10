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

		string defaultIP = "iws03qa8:8080";
		Guid testPerson = new Guid("3545EA74-700D-43F3-B78B-4562E818E46E");
		string TestPersonString = "3545EA74-700D-43F3-B78B-4562E818E46E";
		int docType = 1;

		public ISBTest()
		{
			testSvc = new ISBLibExtGCR(defaultIP);
			this.Init();
		}

		public ISBTest(string ip)
		{
			string ipAddress = defaultIP;

			if (ip.Length > 7)
			{
				ipAddress = ip;
			}

			Debug.WriteLine("\nUsing IP address " + ipAddress);
			testSvc = new ISBLibExtGCR(ipAddress);

			this.Init();
		}

		[SetUp]
		public void Init()
		{
			// to debug from an NUnit invocation, uncomment the following line
			//System.Diagnostics.Debugger.Launch();
		}


		[Test]
		public void GetDocument()
		{
			GetDocument(TestPersonString, docType);
		}
		
		public void GetDocument(string personString, int docType)
		{
			Debug.WriteLine("\nTesting GetDocument(" + personString + ", " + docType + ")");

			List<ImageWare.ISBLibrary.DMDocument> docs = testSvc.GetDocument(personString, docType);

			Debug.WriteLine(personString + " has " + docs.Count() + " documents of type " + docType + (docs.Count() > 0 ? ":" : "."));

			if (docs.Count() > 0)
			{
				ImageWare.ISBLibrary.DMDocumentInfo d;
				Debug.WriteLine("    name, type, version ");
				for (int x = 0; x < docs.Count(); x++)
				{
					d = docs[x].DocumentInfo;
					Debug.WriteLine("    " + d.Location + ", " + d.Type + ", " + d.Version + ".");
				}
			}
		}


		[Test]
		public void GetPersonDocuments()
		{
			GetPersonDocuments(TestPersonString);
		}

		public void GetPersonDocuments(string personString)
		{
			Debug.WriteLine("\nTesting GetPersonDocuments(" + personString + ")");

			List<ImageWare.ISBLibrary.DMDocumentInfo> personDocs = testSvc.GetPersonDocuments(personString);

			Debug.WriteLine(personString + " has " + personDocs.Count() + " documents.");

		}

		[Test]
		public void GetPerson()
		{
			GetPerson(TestPersonString);
		}

		public void GetPerson(string personString)
		{

			Debug.WriteLine("\n\n\n\nTesting GetPerson(" + personString + ")");
			ImageWare.ISBLibrary.IDMSPerson person = testSvc.GetPerson(personString);

			if (person == null)
			{
				Debug.WriteLine("GetPerson error");
				return;
			}
			Debug.WriteLine("person.ToString() output:" + person.ToString());
			Debug.WriteLine("last name is '" + person.LastName + "'.");
			Debug.WriteLine("first name is '" + person.FirstName + "'.");
			Debug.WriteLine("DOB is '" + person.DOB + "'.");
			Debug.WriteLine("status is '" + person.Status + "'.");
			Assert.IsTrue(1 == 1);

		}

		public void AllTests(string personString)
		{
			Debug.WriteLine("\n\n\n\nAllTests(" + personString + ")");

//			GetPerson(personString);
			GetPersonDocuments(personString);
			GetDocument(personString, 1);
		}

		[Test]
		public void AllTestsAllPeople_Battery()
		{

			AllTests("7D62FF11-6047-B20D-3CB4-F3523864FA84");
return;
			AllTests("01E51989-75CD-D64D-5FB0-A3F88C11C25E");
			AllTests("0384B853-3970-15B8-0835-431DD7ED95B0");
			AllTests("1314D7E6-7D61-A7FB-A5FF-5B9A7180C007");
			AllTests("1DDD9685-7DC6-0012-FE06-06146FCEE1A4");
			AllTests("25F41EC9-E152-7B02-5D64-8BEEFFA1BE50");
			AllTests("2DA776CB-6600-9FB8-7DBD-5405126B7834");
			AllTests("3F1F5C90-7F8C-BD0D-4583-A1F7F28BCD38");
			AllTests("4CCFC8B6-7F58-AD2E-0C08-955EE6E2502B");
			AllTests("7EC7B093-9103-311A-14E8-B308DF3FC15D");
			AllTests("89AF3F9B-CC81-8087-667E-9FA12C5C4B28");
			AllTests("A1B3FD9D-A975-C991-3BB8-9B6585F09036");
			AllTests("C35BC14B-64C8-5FDB-DD06-CFE1E2C25B46");
			AllTests("C7DB2583-91AC-95F9-0650-9CE372CACE0F");
			AllTests("C833777D-1BD0-5273-AAB3-990D99D89D8E");
			AllTests("CAC9D393-4868-1BE7-0AEE-BEC30A3C4072");
			AllTests("E3C9C937-1B40-DFAC-3FB4-2CB447DADB4B");
			AllTests("EC71EB56-F74F-CBCA-61CE-4E5579D54412");
			AllTests("FE9164C5-8CCC-3D07-6C81-1B69A3AB6539");

		}


	}
}
