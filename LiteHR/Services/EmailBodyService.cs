using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class EmailBodyService : IEmailBodyService
    {
		private readonly HRContext _context;

		public EmailBodyService(HRContext context)
		{
			_context = context;
		}
		public async Task<bool> SendEmailMessage(IEnumerable<EmailBody> emailBodys)
        {
			List<Person> personList = new List<Person>();
			int status = 0;
			try
			{
				if (emailBodys?.Count() > 0)
				{
					foreach(var item in emailBodys)
					{
						var person=_context.PERSON.SingleOrDefault(f => f.Id == item.Id);
						if (person?.Id > 0)
						{
							personList.Add(person);
						}
						
					}

				}
				status = await Send(personList, emailBodys.FirstOrDefault());
                if (status > 0)
                {
                    for(int i=0; i<personList.Count; i++)
                    {
                        var personId = personList[i].Id;
                       //var applicationForm= _context.APPLICATION.Where(m => m.PersonId == personId).FirstOrDefault();
                       //var applicationForm= _context.APPLICATION.Where(m => m.PersonId == personId).FirstOrDefault();
                        //if (applicationForm?.Id > 0)
                        //{
                        //    applicationForm.InterviewEmailSent = true;
                        //    _context.Update(applicationForm);
                        //    await _context.SaveChangesAsync();
                        //}
                    }
                }
			
			}
			catch (Exception ex)
			{

				throw ex;
			}
			return (status > 0) ? true : false;
		}



		public async Task<int> Send(List<Person> personList, EmailBody emailBody)
		{
			int count = 0;
			if (personList?.Count > 0)
			{

				foreach (var item in personList)
				{
					MailGunModel mailGunModel = new MailGunModel();
					mailGunModel.Body = emailBody.Message;
					mailGunModel.Subject = emailBody.Subject;
					mailGunModel.MessageTo = item.Email;
					mailGunModel.MessageFrom = "info@lloydant.com";
					mailGunModel.Name = item.Surname;
					mailGunModel.Link = "http://97.74.6.243/hrapp";
					mailGunModel.DisplayName = "NAU";
                    
                    SendEMail(mailGunModel, 1);

                    count += 1;
				}
			}
			return count;
		}
        public string FormatHtmlTemplate(MailGunModel mailGunModel, int MessageType)
        {
            StringBuilder st = new StringBuilder();
            st.Append("<section style='width: 100%; background: whitesmoke; padding: 120px 10px;font-family: helvetica;'>");
            st.Append("<div style='width: 70%; background-color: #fff; padding: 0;margin: 0 auto;'>");
            st.Append("<header style='width: 100%; min-height:200px;text-align: center;padding-top: 50px;background: #007D53;COLOR:#FFF;'>");
            st.Append("<img src='http://97.74.6.243/LiteHR/images/ziklogosm.png' alt=' logo' style='width: auto; height: 50px;box-shadow: 2px 1px 15px #333;'>");
            st.Append("<h1 style='font-size: 20px;text-shadow:2px 3px #333;'>NNAMDI AZIKIWE UNIVERSITY</h1>");
            st.Append("</header>");
            st.Append("<section style='width: 100%; background: #fff;padding-bottom: 25px;'>");
            st.Append("<article style='padding: 5% 10%;font-size:20px;'>");
            if (MessageType == 1)
            {
                //optional: Sign-Up
                st.Append("<p style='word-break: break-all;text-align: center;font-style: normal;font-size:30px;'>");
                st.Append(mailGunModel.Body);
                st.Append("</p><br>");
                //optional: Sign-Up
            }
            else
            {
                //optional: Concept Note
                st.Append("Hi ");
                st.Append(mailGunModel.Name);
                st.Append(",<br>");
                st.Append("<p style='word-break: break-all;text-align: justify;font-style: normal;'>");
                st.Append(mailGunModel.Body);
                st.Append("</p><br>");
                //optional: Concept Note
            }

            if (!string.IsNullOrEmpty(mailGunModel.Link))
            {
                if (MessageType == 1)
                {
                    //optional: Link
                    st.Append("<div style='width: 100%;text-align: center;'>");
                    st.Append("<a href='" + mailGunModel.Link + "'");
                    st.Append("style='background: #13aa52;padding: 15px 20px;text-decoration:none;color:#fff;border-radius: 5px;box-shadow: 2px 1px 6px #333'>");
                    st.Append("Verify Account");
                    st.Append("</a>");
                    st.Append("</div>");
                    //optional: Link
                }
                else
                {
                    //optional: Link
                    st.Append("<div style='width: 100%;text-align: center;'>");
                    st.Append("<a href='" + mailGunModel.Link + "'");
                    st.Append("style='background: #13aa52;padding: 15px 20px;text-decoration:none;color:#fff;border-radius: 5px;box-shadow: 2px 1px 6px #333'>");
                    st.Append("Visit Site");
                    st.Append("</a>");
                    st.Append("</div>");
                    //optional: Link
                }
            }

            st.Append("</article>");
            st.Append("<footer style='width: 100%;'>");
            st.Append("<div style='width:80%;border-top: 2px solid #eee;margin: 0 auto;text-align: center;padding: 30px 15px;'>");
            st.Append("<a href='http://lloydant.com' style='font-size: 18px;text-decoration: none;color:#000;'>Powered by Lloydant</a>");
            st.Append("</div>");
            st.Append("</footer>");
            st.Append("</section>");
            st.Append("</div>");

            return st.ToString();
            //return string.Format(st.ToString(), mailGunModel.Name, mailGunModel.Body, mailGunModel.Link);
        }

        public void SendEMail(MailGunModel model, int MessageType)
        {
            try
            {

                var encrypt = Encrypt(model.MessageTo);
                var verifyLink = "http://97.74.6.243/hrapp";


                MailGunModel mailGunModel = new MailGunModel();
                mailGunModel.Name = model.Name;
                mailGunModel.Subject = model.Subject;
                mailGunModel.Link = verifyLink;
                mailGunModel.MessageTo = model.MessageTo ?? "support@lloydant.com";
                mailGunModel.Body = model.Body;

                var template = FormatHtmlTemplate(mailGunModel, MessageType);
                EmailSenderLogic<MailGunModel> receiptEmailSenderLogic = new EmailSenderLogic<MailGunModel>(template, mailGunModel);

                receiptEmailSenderLogic.Send(mailGunModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Encrypt(string encrypData)
        {
            string data = "";
            try
            {
                string CharData = "";
                string ConChar = "";
                for (int i = 0; i < encrypData.Length; i++)
                {
                    CharData = Convert.ToString(encrypData.Substring(i, 1));
                    ConChar = char.ConvertFromUtf32(char.ConvertToUtf32(CharData, 0) + 115);
                    data = data + ConChar;
                }
            }
            catch (Exception ex)
            {
                data = "1";
                return data;
            }
            return data;
        }

        public static string Decrypt(string encrypData)
        {
            string data = "";
            try
            {
                string CharData = "";
                string ConChar = "";
                for (int i = 0; i < encrypData.Length; i++)
                {
                    CharData = Convert.ToString(encrypData.Substring(i, 1));
                    ConChar = char.ConvertFromUtf32(char.ConvertToUtf32(CharData, 0) - 115);
                    data = data + ConChar;
                }
            }
            catch (Exception ex)
            {
                data = "1";
                return data;
            }
            return data;
        }
    }
}
