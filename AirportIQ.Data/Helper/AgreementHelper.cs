using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AirportIQ.Data.Helper
{
    //JBienvenu 2013-01-11 19304 new
    public class AgreementHelper
    {
        public class DateLimits
        {
            public static DateTime AbsoluteStartDate { get { return new DateTime(1900, 1, 1); } }
            public static DateTime AbsoluteEndDate { get { return new DateTime(9999, 12, 31); } }

            public DateTime EarliestStartDate;
            public DateTime LatestStartDate;
            public DateTime EarliestEndDate;
            public DateTime LatestEndDate;

            // default constructor
            public DateLimits()
            {
                this.EarliestStartDate = DateLimits.AbsoluteStartDate;
                this.EarliestEndDate = DateLimits.AbsoluteStartDate;
                this.LatestStartDate = DateLimits.AbsoluteEndDate;
                this.LatestEndDate = DateLimits.AbsoluteEndDate;
            }

            public DateLimits(DataRow dr)
            {
                this.EarliestStartDate = DateTime.Parse(dr["EarliestStartDate"].ToString());
                this.EarliestEndDate = DateTime.Parse(dr["EarliestEndDate"].ToString());
                this.LatestStartDate = DateTime.Parse(dr["LatestStartDate"].ToString());
                this.LatestEndDate = DateTime.Parse(dr["LatestEndDate"].ToString());
            }

            // clone constructor
            public DateLimits(DateLimits a)
            {
                this.EarliestStartDate = a.EarliestStartDate;
                this.EarliestEndDate = a.EarliestEndDate;
                this.LatestStartDate = a.LatestStartDate;
                this.LatestEndDate = a.LatestEndDate;
            }

            public virtual IDictionary<string,DateTime> ToDictionary()
            {
                IDictionary<string, DateTime> d = new Dictionary<string, DateTime>();
                d.Add("EarliestStartDate",this.EarliestStartDate);
                d.Add("EarliestEndDate",this.EarliestEndDate);
                d.Add("LatestStartDate",this.LatestStartDate);
                d.Add("LatestEndDate",this.LatestEndDate);
                return d;
            }
        }

        public class DateValidator : DateLimits
        {
            public DateTime StartDate;
            public DateTime EndDate;

            public bool HighlightStartDate { get; private set; }
            public bool HighlightEndDate { get; private set; }
            public List<Exception> Exceptions { get; private set; }

            public DateValidator() : base() { }

            public DateValidator(DateLimits a) : base(a) { }

            public void Validate()
            {
                this.Exceptions = new List<Exception>();

                if (this.EndDate < this.StartDate)
                {
                    this.Exceptions.Add(new StartDateBeforeEndDateException(this));
                    this.HighlightStartDate = true;
                    this.HighlightStartDate = true;
                }

                if (this.StartDate < this.EarliestStartDate)
                {
                    this.Exceptions.Add(new StartDateBeforeEarliestStartDateException(this));
                    this.HighlightStartDate = true;
                }
                if (this.StartDate > this.LatestStartDate)
                {
                    this.Exceptions.Add(new StartDateAfterLatestStartDateException(this));
                    this.HighlightStartDate = true;
                }
                if (this.EndDate < this.EarliestEndDate)
                {
                    this.Exceptions.Add(new EndDateBeforeEarliestEndDateException(this));
                    this.HighlightEndDate = true;
                }
                if (this.EndDate > this.LatestEndDate)
                {
                    this.Exceptions.Add(new EndDateAfterLatestEndDateException(this));
                    this.HighlightStartDate = true;
                }
            }

            public override IDictionary<string, DateTime> ToDictionary()
            {
                IDictionary<string, DateTime> d = base.ToDictionary();
                d.Add("StartDate", this.StartDate);
                d.Add("EndDate", this.EndDate);
                return d;
            }
        }

        public abstract class DateRangeValidationException : ArgumentOutOfRangeException
        {
            public DateRangeValidationException(DateValidator dv, string m0, DateTime d1, string m2, DateTime d3)
                : base(String.Format("{0} ({1:M/d/yy}) {2} ({3:M/d/yy}).", m0, d1, m2, d3))
            {
                foreach (KeyValuePair<string,DateTime> kvp in                 dv.ToDictionary())
                    this.Data[kvp.Key] = kvp.Value;
            }
        }

        public class StartDateBeforeEndDateException : DateRangeValidationException
        {
            public StartDateBeforeEndDateException(DateValidator dv)
                : base(dv, "Contract start date", dv.StartDate, "must be later than contract end date", dv.EndDate)
            {
            }
        }

        public class StartDateBeforeEarliestStartDateException : DateRangeValidationException
        {
            public StartDateBeforeEarliestStartDateException(DateValidator dv)
                : base(dv, "Contract start date", dv.StartDate, "must be later than earliest start date for this contract", dv.EarliestStartDate)
            {
            }
        }

        public class StartDateAfterLatestStartDateException : DateRangeValidationException
        {
            public StartDateAfterLatestStartDateException(DateValidator dv)
                : base(dv, "Contract start date", dv.StartDate, "must be earlier than latest start date for this contract", dv.LatestStartDate)
            {
            }
        }

        public class EndDateBeforeEarliestEndDateException : DateRangeValidationException
        {
            public EndDateBeforeEarliestEndDateException(DateValidator dv)
                : base(dv, "Contract end date", dv.EndDate, "must be later than earliest end date for this contract", dv.EarliestEndDate)
            {
            }
        }

        public class EndDateAfterLatestEndDateException : DateRangeValidationException
        {
            public EndDateAfterLatestEndDateException(DateValidator dv)
                : base(dv, "Contract end date", dv.EndDate, "must be earlier than latest end date for this contract", dv.LatestEndDate)
            {
            }
        }

    }
}
