using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace AirportIQ.Domain.Helpers
{
	public class EmailHelper
	{
		#region Fields

		private MemoryStream stream;
		private string filename;
		private string mediaType;

		#endregion Fields

		#region Properties

		/// <summary>
		/// Gets the data stream for this attachment
		/// </summary>
		public Stream Data { get { return stream; } }

		/// <summary>
		/// Gets the original filename for this attachment
		/// </summary>
		public string Filename { get { return filename; } }

		/// <summary>
		/// Gets the attachment type: Bytes or String
		/// </summary>
		public string MediaType { get { return mediaType; } }

		/// <summary>
		/// Gets the file for this attachment (as a new attachment)
		/// </summary>
		public Attachment File { get { return new Attachment(Data, Filename, MediaType); } }

		#endregion Properties

		#region Constructors

		/// <summary>
		/// Construct a mail attachment form a byte array
		/// </summary>
		/// <param name="data">Bytes to attach as a file</param>
		/// <param name="filename">Logical filename for attachment</param>
		public EmailHelper(byte[] data, string filename)
		{
			this.stream = new MemoryStream(data);
			this.filename = filename;
			this.mediaType = MediaTypeNames.Application.Octet;
		}

		/// <summary>
		/// Construct a mail attachment from a string
		/// </summary>
		/// <param name="data">String to attach as a file</param>
		/// <param name="filename">Logical filename for attachment</param>
		public EmailHelper(string data, string filename)
		{
			this.stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(data));
			this.filename = filename;
			this.mediaType = MediaTypeNames.Text.Html;
		}

		#endregion Constructors

		public static void Email(string to,
													 string body,
													 string subject,
													 string fromAddress,
													 string fromDisplay,
													 params EmailHelper[] attachments)
		{
			string host = ConfigurationManager.AppSettings["SMTPHost"];
			try
			{
				MailMessage mail = new MailMessage() { Body = body, IsBodyHtml = true };
				mail.To.Add(new MailAddress(to));
				mail.From = new MailAddress(fromAddress, fromDisplay, Encoding.UTF8);
				mail.Subject = subject;
				mail.SubjectEncoding = Encoding.UTF8;
				mail.Priority = MailPriority.Normal;
				foreach (EmailHelper ma in attachments)
				{
					mail.Attachments.Add(ma.File);
				}
				using (SmtpClient smtp = new SmtpClient())
				{
					smtp.Host = host;
					smtp.Send(mail);
				}
			}
			catch (Exception ex)
			{
				// ErrorLog(sb.ToString(), ex.ToString(), ErrorLogCause.EmailSystem);
				string s = ex.Message; // FIX THIS ATCHISON - found during code review/build warning. What to do here?
			}
		}
	}
}