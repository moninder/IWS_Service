using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using ISBLibTest;
 
namespace ISBConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{

			ISBTest isbt;

			TextWriterTraceListener myWriter = new TextWriterTraceListener(System.Console.Out);
			Debug.Listeners.Add(myWriter);

			if (args.Length > 0)
			{
				isbt = new ISBTest(args[0]);
			}
			else
			{
				isbt = new ISBTest();
			}


			isbt.Init();

//			isbt.GetPerson();

//			isbt.GetPersonDocuments();

//			isbt.GetDocument();

			isbt.AllTests("01E51989-75CD-D64D-5FB0-A3F88C11C25E");
			isbt.AllTests("0384B853-3970-15B8-0835-431DD7ED95B0");
			isbt.AllTests("1314D7E6-7D61-A7FB-A5FF-5B9A7180C007");
			isbt.AllTests("1DDD9685-7DC6-0012-FE06-06146FCEE1A4");
			isbt.AllTests("25F41EC9-E152-7B02-5D64-8BEEFFA1BE50");
			isbt.AllTests("2DA776CB-6600-9FB8-7DBD-5405126B7834");
			isbt.AllTests("3F1F5C90-7F8C-BD0D-4583-A1F7F28BCD38");
			isbt.AllTests("4CCFC8B6-7F58-AD2E-0C08-955EE6E2502B");
			isbt.AllTests("7D62FF11-6047-B20D-3CB4-F3523864FA84");
			isbt.AllTests("7EC7B093-9103-311A-14E8-B308DF3FC15D");
			isbt.AllTests("89AF3F9B-CC81-8087-667E-9FA12C5C4B28");
			isbt.AllTests("A1B3FD9D-A975-C991-3BB8-9B6585F09036");
			isbt.AllTests("C35BC14B-64C8-5FDB-DD06-CFE1E2C25B46");
			isbt.AllTests("C7DB2583-91AC-95F9-0650-9CE372CACE0F");
			isbt.AllTests("C833777D-1BD0-5273-AAB3-990D99D89D8E");
			isbt.AllTests("CAC9D393-4868-1BE7-0AEE-BEC30A3C4072");
			isbt.AllTests("E3C9C937-1B40-DFAC-3FB4-2CB447DADB4B");
			isbt.AllTests("EC71EB56-F74F-CBCA-61CE-4E5579D54412");
			isbt.AllTests("FE9164C5-8CCC-3D07-6C81-1B69A3AB6539");
		}
	}
}
