using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace AirportIQ.Model.Models.Badging.Results
{
	[Serializable]
	public class Alias
	{
		[XmlAttribute]
		public string FirstName;

		[XmlAttribute]
		public string LastName;

		[XmlAttribute]
		public string MiddleName;

		[XmlAttribute]
		public string NamePrefixCode;

		[XmlAttribute]
		public string NameSuffixCode;

		[XmlAttribute]
		public int PersonAliasID;
	}

	[Serializable]
	public class Badge
	{
		[XmlAttribute]
		public int badgeId = -1;

		[XmlAttribute]
		public string BadgeIssuanceReasonCode;

		[XmlAttribute]
		public string ColorID;		

		[XmlAttribute]
		public Int64 IWS_BadgeId;

		[XmlAttribute]
		public string ProxNumber;

		public int[] Icons;
	}

	[Serializable]
	public class BadgingResults
	{
        public string PageMode;
		public Badge Badge;
		public BiographicModel BiographicModel;
		public int[] Categories;
		public int[] Locations;
		public int[] SpecialCategories;
		public int[] SpecialLocations;
		public Documentation[] Documentations;
		public FelonyAnswer[] FelonyAnswers;
		public Header Header;
		public StaDocs StaDocs;
		public PersonDivisionXrefModel PersonDivisionXrefModel; 
		public SpecialAccessCategory[] SpecialAccessCategories; //TODO: remove this
		public NoteRecord[] Notes;
        //public List<Dictionary<string, object>> Notes;

		[XmlAttribute]
		public int JobRoleID;

		public static BadgingResults FromJson(string json)
		{
			return JsonConvert.DeserializeObject<BadgingResults>(json);
		}

		public string Serialize()
		{
			return JsonConvert.SerializeObject(this);
		}
	}

	[Serializable]
	public class BiographicModel
	{
		[XmlAttribute]
		public string Address;

		public Alias[] Aliases;

		[XmlAttribute]
		public string ApartmentNumber;

		[XmlAttribute]
		public int C_DataChanges_RowID;

		[XmlAttribute]
		public string C_Legacy_DatabaseName;

		[XmlAttribute]
		public string C_Legacy_TableName;

		[XmlAttribute]
		public string City;

		[XmlAttribute]
		public string CountryCode;

		[XmlAttribute]
		public string CountryCode_Birth;

		[XmlAttribute]
		public string CountryCode_Citizenship;

		[XmlAttribute]
		public string CountrySubdivisionCode;

		[XmlAttribute]
		public string CountrySubdivisionCode_Birth;

		[XmlAttribute]
		public DateTime DateOfBirth;

		[XmlAttribute]
		public string EmailAddress_Alternate;

		[XmlAttribute]
		public string EmailAddress_Primary;

		[XmlAttribute]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		
		public string EmployeeID
		{
			get { return (iEmployeeID.HasValue) ? iEmployeeID.ToString() : null; }
			set { iEmployeeID = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?); }
		}

		[XmlIgnore]
		[JsonIgnore]
		public int? iEmployeeID { get; set; }

		[XmlAttribute]
		public string EyeColorCode;

		[XmlAttribute]
		public string FirstName;

		[XmlAttribute]
		public string FirstName_Sound;

		[XmlAttribute]
		public string FormCode;

		public object FormID;

		[XmlAttribute]
		public string HairColorCode;

		[XmlAttribute]
		public int HeightInInches;

		[XmlAttribute]
		public string HomePhoneNumber;

		[XmlAttribute]
		public bool IsSubmitted;

		[XmlAttribute]
		public string LastName;

		[XmlAttribute]
		public string LastName_Sound;

		[XmlAttribute]
		public string MiddleName;

		[XmlAttribute]
		public string MiddleName_Sound;

		[XmlAttribute]
		public string NamePrefixCode;

		[XmlAttribute]
		public string NameSuffixCode;

		[XmlAttribute]
		public int PersonID;

		[XmlAttribute]
		public string PIN;

		[XmlAttribute]
		public string PostalCode;

		[XmlAttribute]
		public string RaceCode;

		[XmlAttribute]
		public string SexCode;

		[XmlAttribute]
		public string SocialSecurityNumber;

		[XmlAttribute]
		public int WeightInPounds;

		[XmlAttribute]
		public string WorkPhoneNumber;

		[XmlAttribute]
		public string ApplicantTrackingIdentifier;

        [XmlIgnore]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Include)]
        public Nullable<DateTime> LastFingerprintDate { get; set; }

        // http://stackoverflow.com/questions/2074240/serializing-a-nullabledatetime-in-to-xml
        // jaredb 1/14/2016 - see here for reference: used so that a null datetime will not be serialized, but a non-null will; avoids the "1/1/1900" problem we had earlier,
        // but allows us to keep the date as an optional attribute.
        [XmlAttribute("LastFingerprintDate")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime LastFingerprintDateSerialized
        {
            get { return LastFingerprintDate.Value; }
            set { LastFingerprintDate = value; }
        }

        // and here we turn serialization of the value on/off per the value
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeLastFingerprintDateSerialized()
        {
            return LastFingerprintDate.HasValue;
        }
	}

	[Serializable]
	public class Documentation
	{
		[XmlAttribute]
		public string RequirementCode;

		[XmlAttribute]
		public string DropDownName;

		//JBienvenu 19133 2013-02-08 added
		[XmlAttribute]
		public int DocumentID;

		[XmlAttribute]
		public string DocumentDescription;

		[XmlAttribute]
		public string DocumentTypeNumber;

		[XmlAttribute]
		public string ExpirationDate;

		[XmlAttribute]
		public string IdentificationNumber;

		//JBienvenu 2013-01-09 added
		[XmlAttribute]
		public string IssuingAuthority_CountryCode;

		//JBienvenu 2013-01-09 added
		[XmlAttribute]
		public string IssuingAuthority_CountrySubdivisionCode;		

		[XmlAttribute]
		public string TypeDescription; //TODO == DocumentDescription?

		[XmlAttribute]
		public string LegalStatusCode;

		[XmlAttribute]
		public short LegalStatusPriority;

		//JBienvenu 19133 2013-02-08 END added

		//Rguidi 2013-12-07 added
		[XmlAttribute]
		public string IssuingAuthority_School;
	}

	[Serializable]
	public class FelonyAnswer
	{
		[XmlAttribute]
		public int FelonyQuestionID;

		[XmlAttribute]
		public bool Answer;

		[XmlAttribute]
		public string Question;
	}

	[Serializable]
	public class Header
	{
		[XmlAttribute]
		public string AgreementNumber;

		[XmlAttribute]
		public int CompanyID;

		[XmlAttribute]
		public int DivisionID;

		[XmlAttribute]
		public int PersonDivisionXrefID;

		[XmlAttribute]
		public string PersonID_IWS;

		[XmlAttribute]
		public string AppMode;
		
		[XmlAttribute]
		public DateTime ExpirationDate;

		[XmlAttribute]
		public int JobRoleId;

	}

	[Serializable]
	public class StaDocs
	{
		[XmlAttribute]
		public string EmployeeStatus;

		[XmlAttribute]
		public string FedLeoNumber;

		[XmlAttribute]
		public string CHRC;

		[XmlAttribute]
		public string CitizenShipStatus;

		[XmlElement]
		public USCitizen USCitizen;

		[XmlElement]
		public RegisteredAlien RegisteredAlien;

		[XmlElement]
		public Non_Immigrant Non_Immigrant;

	}

	[Serializable]
	public class USCitizen
	{
		[XmlAttribute]
		public int GovId;

		[XmlAttribute]
		public string GovDocumentDescription;

		[XmlAttribute]
		public string DocumentNumber;

        //[XmlAttribute]
        //public DateTime ExpirationDate;

        [XmlIgnore]
        public Nullable<DateTime> ExpirationDate { get; set; }

        // this is a shim property that we use to provide the serialization
        [XmlAttribute("ExpirationDate")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime ExpirationDateSerialized
        {
            get { return ExpirationDate.Value; }
            set { ExpirationDate = value; }
        }

        // and here we turn serialization of the value on/off per the value
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeExpirationDateSerialized()
        {
            return ExpirationDate.HasValue;
        }

		[XmlAttribute]
		public int ListB;

		[XmlAttribute]
		public string ListBDocumentDescription;

		[XmlAttribute]
		public string ListC;

		[XmlAttribute]
		public string ListCDocumentDescription;

		//RGuidi 8/8/2013
		[XmlAttribute]
		public int DocumentID;

		//RGuidi 8/8/2013
		[XmlAttribute]
		public int ListBDocumentID;

		//RGuidi 8/8/2013
		[XmlAttribute]
		public int ListCDocumentID;

		//RGuidi 8/9/2013
		[XmlAttribute]
		public string ListBExpirationDate;

		//RGuidi 8/9/2013
		[XmlAttribute]
		public string ListBIdentificationNumber;

		//RGuidi 8/9/2013
		[XmlAttribute]
		public string ListBIssuingAuthority_CountryCode;

		//RGuidi 8/9/2013
		[XmlAttribute]
		public string ListBIssuingAuthority_CountrySubdivisionCode;

		//RGuidi 8/9/2013
		[XmlAttribute]
		public string ListCExpirationDate;

		//RGuidi 8/9/2013
		[XmlAttribute]
		public string ListCIdentificationNumber;

		//RGuidi 8/9/2013
		[XmlAttribute]
		public string ListCIssuingAuthority_CountryCode;

		//RGuidi 8/9/2013
		[XmlAttribute]
		public string ListCIssuingAuthority_CountrySubdivisionCode;

		//Rguidi 2013-12-07 added
		[XmlAttribute]
		public string ListBIssuingAuthority_School;

	}

	[Serializable]
	public class RegisteredAlien
	{
		[XmlAttribute]
		public string AlienRegistrationNumber;

		[XmlAttribute]
		public int AlienType;

        //[XmlAttribute]
        //public DateTime ExpirationDate;

        [XmlIgnore]
        public Nullable<DateTime> ExpirationDate { get; set; }

        // this is a shim property that we use to provide the serialization
        [XmlAttribute("ExpirationDate")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime ExpirationDateSerialized
        {
            get { return ExpirationDate.Value; }
            set { ExpirationDate = value; }
        }

        // and here we turn serialization of the value on/off per the value
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeExpirationDateSerialized()
        {
            return ExpirationDate.HasValue;
        }

		//RGuidi 8/8/2013
		[XmlAttribute]
		public int DocumentID;

	}

	[Serializable]
	public class Non_Immigrant
	{
		[XmlAttribute]
		public string PassportNumber;

		[XmlAttribute]
		public string CountryIssued;

		[XmlAttribute]
		public DateTime PassportExpirationDate;

		[XmlAttribute]
		public string I94Number;

        //[XmlAttribute]
        //public DateTime I94ExpirationDate;

        [XmlIgnore]
        public Nullable<DateTime> I94ExpirationDate { get; set; }

        // this is a shim property that we use to provide the serialization
        [XmlAttribute("I94ExpirationDate")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime I94ExpirationDateSerialized
        {
            get { return I94ExpirationDate.Value; }
            set { I94ExpirationDate = value; }
        }

        // and here we turn serialization of the value on/off per the value
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeI94ExpirationDateSerialized()
        {
            return I94ExpirationDate.HasValue;
        }

		[XmlAttribute]
		public string I551Number;

		[XmlAttribute]
		public DateTime I551ExpirationDate;

		[XmlAttribute]
		public string VisaControlNumber;

		[XmlAttribute]
		public int VisaType;

		[XmlAttribute]
		public DateTime VisaExpirationDate;

		//RGuidi 8/8/2013
		[XmlAttribute]
		public int DocumentID;

		//RGuidi 8/8/2013
		[XmlAttribute]
		public int I94DocumentID;

		//RGuidi 8/8/2013
		[XmlAttribute]
		public int I55DocumentID;

		//RGuidi 8/8/2013
		[XmlAttribute]
		public int VisaDocumentID;

	}

	/// <summary>
	///
	/// </summary>
	/// <remarks>
	/// JBienvenu 19133 2013-02-01 new class
	/// </remarks>
	[Serializable]
	public class PersonDivisionXrefModel
	{
		[XmlAttribute]
		public int PersonDivisionXrefID;

		[XmlAttribute]
		public short STAEmployeeStatusID;

		[XmlAttribute]
		public string FederalIDLEONumber;

		[XmlAttribute]
		public bool IsSTACheckRequired;

        [XmlAttribute]
        public bool IsSignatureOnly;

		[XmlAttribute]
		public string ApplicantType; //rguidi 8/15/2013 TFS 22748

        [XmlAttribute]
        public bool SubmittedStateDoJRequest;
	}

	[Serializable]
	public class SpecialAccessCategory
	{
		[XmlAttribute]
		public int CategoryId;

		[XmlAttribute]
		public string Action;
	}

	[Serializable]
	public class NoteRecord
	{
		[XmlAttribute]
		public int NoteID;

		[XmlAttribute]
		public string FullName;

		[XmlAttribute]
		public DateTime WhenBecomesActive;

        [XmlAttribute]
        public DateTime WhenBegins;

		[XmlAttribute]
		public string NoteTypeCode;

		[XmlAttribute]
		public string NoteTypeDescription;

		[XmlAttribute]
		public bool IsHotNote;

		[XmlAttribute]
		public string Note;

        [XmlAttribute]
        public string NoteType;

        [XmlAttribute]
        public int StaffID;
	}	
}