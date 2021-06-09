using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Scrapper.ReviewTracking
{
	public interface IEmailSender
	{
		void Send(string emailBody, string emailSubject);
	}
}
