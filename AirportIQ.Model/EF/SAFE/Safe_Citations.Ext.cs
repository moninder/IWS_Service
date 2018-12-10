using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AirportIQ.Model.EF.SAFE
{
	public partial class Safe_Citations
	{
        public List<Documents> Documents { get; set; }

		public static Safe_Citations GetCitation(int citationID)
		{
			var context = new SafeEntities();

			var citations = context.Safe_Citations
				.Include("Safe_Hearings")
				.Include("Safe_Hearings.Safe_HearingPoliceOfficers")
				.Include("Safe_Hearings.Safe_HearingPoliceOfficers.LEO_OfficerBadges")
				.Include("Safe_Hearings.Safe_HearingPoliceOfficers.LEO_OfficerBadges.Person_FullName")
				.Include("Safe_Hearings.Safe_Officers")
				.Include("Safe_VehicleMakes")
				.Include("Safe_VehicleBodyTypes")
				.Include("Safe_CitationViolations")
				.Include("Safe_CitationViolations.Safe_Violations")
				.Include("Safe_CitationViolations.Safe_ViolationTypes")
				.Include("Safe_CitationPoliceOfficers")
				.Include("Safe_CitationPoliceOfficers.LEO_OfficerBadges")
				.Include("Safe_CitationPoliceOfficers.LEO_OfficerBadges.Person_FullName")
				.Include("Person_PersonDivisionXref")
				.Include("Person_PersonDivisionXref.Person_FullName")
				.Include("Person_PersonDivisionXref.Division_Divisions")
				.Include("Person_PersonDivisionXref.Division_Divisions.Company_Companies")
                //.Include("Documents")
				.Where(c => c.SafeCitationID == citationID);

            string identificationNumber = "CITATION:" + citationID.ToString();
            var documents = context.Documents.Where(d => d.IdentificationNumber == identificationNumber).ToList();

            var citation = citations.FirstOrDefault();

            citation.Documents = documents.ToList();

            return citation;
		}

		public static Safe_Citations NewCitation(string theBadgeNumber)
		{
			Safe_Citations result;
			using (var context = new SafeEntities())
			{
				result = new Safe_Citations();
				result.CitationNumber = "";
				
				// clean up badge number
				string[] separators = {" "};
				string[] badgeParts = theBadgeNumber.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				theBadgeNumber = badgeParts[0];

				var badge = context.Person_Badges.Where(b => b.BadgeNumber == theBadgeNumber).Single();
				var perXref = badge.PersonDivisionXrefID;
				var divXref = context.Person_PersonDivisionXref
					.Include("Person_FullName")
					.Include("Division_Divisions")
					.Include("Division_Divisions.Company_Companies")
					.Where(d => d.PersonDivisionXrefID == perXref).Single();

				result.Person_PersonDivisionXref = divXref;
				result.BadgeID = badge.BadgeID;
				result.PersonDivisionXrefID = divXref.PersonDivisionXrefID;
				//result.WhenCreated = DateTime.Now;
                //result.WhenDue = DateTime.Now.AddDays(15);
				result.Cancelled = "z";
				result.FollowUp = "z";

                result.Documents = new List<Documents>();
			}

			return result;
		}

		public string Save()
		{
			string result = "true";
			try
			{
				var safeCitationID = this.SafeCitationID;
				using (var context = new SafeEntities())
				{
					Safe_Citations safeCitation;
					if (this.SafeCitationID == 0)
					{
						safeCitation = new Safe_Citations();
					}
					else
					{
						safeCitation = (from c in context.Safe_Citations
														where c.SafeCitationID == safeCitationID
														select c).Single();
					}

					PropertyInfo[] props = safeCitation.GetType().GetProperties();

					foreach (PropertyInfo prop in props)
					{
						if (prop.GetGetMethod().IsVirtual)
						{
							//if (prop.PropertyType.Name.Contains("ICollection"))
							//{
							//  // get static save method
							//  MethodInfo saveMethod = prop.GetType().GetMethod("Save", BindingFlags.Static | BindingFlags.Public);
							//  // call save method and pass it the collection
							//  saveMethod.Invoke(null, new object[] {(object) prop.GetValue(this, null)});
							//}
							//else
							//{
							//  continue;
							//}
							continue;
						}

						var val = this.GetType().GetProperty(prop.Name).GetValue(this, null);
						prop.SetValue(safeCitation, val, null);
					}

					if (SafeCitationID == 0)
					{
						context.Safe_Citations.Add(safeCitation);
					}

					try
					{
						var saveFlag = true;

                        #region Validate
                        string errorMessage = string.Empty;

						var validationErrors = context.GetValidationErrors()
								.Where(vr => !vr.IsValid)
								.SelectMany(vr => vr.ValidationErrors).ToList();

                        // Check for duplicate citation number.
                        if (SafeCitationID == 0)
                        {
                            var citationNumberAlreadyExists = (from c in context.Safe_Citations
                                                               where c.CitationNumber == safeCitation.CitationNumber
                                            select c).Any();

                            if (citationNumberAlreadyExists)
                                validationErrors.Add(new DbValidationError("CitationNumber", "You entered a duplicate citation number. Either modify the citation number or edit the existing citation."));
                        }

						if (Safe_CitationViolations.Count() < 1)
							validationErrors.Add(new DbValidationError("Safe_CitationViolations", "At least one violation is required."));

                        if (Safe_CitationPoliceOfficers.Count() < 1 && safeCitation.Superintendent == "")
                            validationErrors.Add(new DbValidationError("Safe_CitationPoliceOfficers", "At least one officer or a superintendent is required."));

						foreach (var error in validationErrors)
						{
							saveFlag = false;
							errorMessage += error.ErrorMessage + "</br>";
                        }
                        #endregion

                        // Save Collections
						if (saveFlag)
						{
							context.SaveChanges();

                            if (this.WhenDue != null)
                            {
                                foreach (Safe_CitationViolations violation in Safe_CitationViolations)
                                {
                                    if (violation.Safe_ViolationTypes.ViolationTypeDescription == "Equipment")
                                    {
                                        violation.ViolationPoints = 0;
                                        violation.Safe_Violations.ViolationPoints = 0;
                                    }
                                }
                            }

							SAFE.Safe_CitationViolations.Save(Safe_CitationViolations, safeCitation.SafeCitationID);

                            SAFE.Safe_CitationPoliceOfficers.Save(Safe_CitationPoliceOfficers, safeCitation.SafeCitationID);
                            SAFE.Documents.Save(this.Documents, safeCitation.SafeCitationID);
							SAFE.Safe_Hearings.Save(Safe_Hearings, safeCitation.SafeCitationID);
							context.SaveChanges();

                            if (this.SafeCitationID == 0)
                                this.SafeCitationID = safeCitation.SafeCitationID;

							return "true";
						}
						else
							return errorMessage;
					}
					catch (Exception ex)
					{
						result = ((ex.InnerException).InnerException).Message;	
					}					
				}				

			}
			catch (Exception ex)
			{
				result = "ERROR:\n\r\n\r" + ex.Message + " INNER EXCEPTION: " + ((ex.InnerException).InnerException).Message;
			}

			return result;
		}

		public string ToJson()
		{
			var idtc = new IsoDateTimeConverter { DateTimeFormat = "MM/dd/yyyy" };
			return JsonConvert.SerializeObject(this, idtc);
		}
	}
}
