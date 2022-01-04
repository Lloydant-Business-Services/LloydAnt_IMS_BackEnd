using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Security;
using MailKit.Net.Smtp;
using MimeKit;

namespace LiteHR.Infrastructure
{
    public class Email
    {

        //public string FormatHtmlTemplate(MailGunModel mailGunModel, int MessageType)
        //{
        //    StringBuilder st = new StringBuilder();
        //    st.Append("<section style='width: 100%; background: whitesmoke; padding: 120px 10px;font-family: helvetica;'>");
        //    st.Append("<div style='width: 70%; background-color: #fff; padding: 0;margin: 0 auto;'>");
        //    st.Append("<header style='width: 100%; min-height:200px;text-align: center;padding-top: 50px;background: #007D53;COLOR:#FFF;'>");
        //    st.Append("<img src='http://97.74.6.243/tetfundgrant/images/tetfundlogo.png' alt='tetfund logo' style='width: auto; height: 50px;box-shadow: 2px 1px 15px #333;'>");
        //    st.Append("<h1 style='font-size: 40px;text-shadow:2px 3px #333;'>NNAMDI AZIKIWE UNIVERSITY</h1>");
        //    st.Append("</header>");
        //    st.Append("<section style='width: 100%; background: #fff;padding-bottom: 25px;'>");
        //    st.Append("<article style='padding: 5% 10%;font-size:20px;'>");
        //    if (MessageType == 1)
        //    {
        //        //optional: Sign-Up
        //        st.Append("<p style='word-break: break-all;text-align: center;font-style: normal;font-size:30px;'>");
        //        st.Append(mailGunModel.Body);
        //        st.Append("</p><br>");
        //        //optional: Sign-Up
        //    }
        //    else
        //    {
        //        //optional: Concept Note
        //        st.Append("Hi ");
        //        st.Append(mailGunModel.Name);
        //        st.Append(",<br>");
        //        st.Append("<p style='word-break: break-all;text-align: justify;font-style: normal;'>");
        //        st.Append(mailGunModel.Body);
        //        st.Append("</p><br>");
        //        //optional: Concept Note
        //    }

        //    if (!string.IsNullOrEmpty(mailGunModel.Link))
        //    {
        //        if (MessageType == 1)
        //        {
        //            //optional: Link
        //            st.Append("<div style='width: 100%;text-align: center;'>");
        //            st.Append("<a href='" + mailGunModel.Link + "'");
        //            st.Append("style='background: #13aa52;padding: 15px 20px;text-decoration:none;color:#fff;border-radius: 5px;box-shadow: 2px 1px 6px #333'>");
        //            st.Append("Verify Account");
        //            st.Append("</a>");
        //            st.Append("</div>");
        //            //optional: Link
        //        }
        //        else
        //        {
        //            //optional: Link
        //            st.Append("<div style='width: 100%;text-align: center;'>");
        //            st.Append("<a href='" + mailGunModel.Link + "'");
        //            st.Append("style='background: #13aa52;padding: 15px 20px;text-decoration:none;color:#fff;border-radius: 5px;box-shadow: 2px 1px 6px #333'>");
        //            st.Append("Visit Site");
        //            st.Append("</a>");
        //            st.Append("</div>");
        //            //optional: Link
        //        }
        //    }

        //    st.Append("</article>");
        //    st.Append("<footer style='width: 100%;'>");
        //    st.Append("<div style='width:80%;border-top: 2px solid #eee;margin: 0 auto;text-align: center;padding: 30px 15px;'>");
        //    st.Append("<a href='http://lloydant.com' style='font-size: 18px;text-decoration: none;color:#000;'>Powered by Lloydant</a>");
        //    st.Append("</div>");
        //    st.Append("</footer>");
        //    st.Append("</section>");
        //    st.Append("</div>");

        //    return st.ToString();
        //    //return string.Format(st.ToString(), mailGunModel.Name, mailGunModel.Body, mailGunModel.Link);
        //}

        //public void SendEMail(MailGunModel model, int MessageType)
        //{
        //    try
        //    {

        //        var encrypt = Encrypt(model.MessageTo);
        //        var verifyLink = "http://localhost/Home/VerifyAccount?encryptData=" + encrypt;


        //        MailGunModel mailGunModel = new MailGunModel();
        //        mailGunModel.Name = model.Name;
        //        mailGunModel.Subject = model.Subject;
        //        mailGunModel.Link = MessageType == 1 ? verifyLink : "http://localhost";
        //        mailGunModel.MessageTo = model.MessageTo ?? "support@lloydant.com";
        //        mailGunModel.Body = model.Body;

        //        var template=FormatHtmlTemplate(mailGunModel, MessageType);
        //        EmailSenderLogic<MailGunModel> receiptEmailSenderLogic = new EmailSenderLogic<MailGunModel>(template, mailGunModel);

        //        receiptEmailSenderLogic.Send(mailGunModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static string Encrypt(string encrypData)
        //{
        //    string data = "";
        //    try
        //    {
        //        string CharData = "";
        //        string ConChar = "";
        //        for (int i = 0; i < encrypData.Length; i++)
        //        {
        //            CharData = Convert.ToString(encrypData.Substring(i, 1));
        //            ConChar = char.ConvertFromUtf32(char.ConvertToUtf32(CharData, 0) + 115);
        //            data = data + ConChar;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        data = "1";
        //        return data;
        //    }
        //    return data;
        //}

        //public static string Decrypt(string encrypData)
        //{
        //    string data = "";
        //    try
        //    {
        //        string CharData = "";
        //        string ConChar = "";
        //        for (int i = 0; i < encrypData.Length; i++)
        //        {
        //            CharData = Convert.ToString(encrypData.Substring(i, 1));
        //            ConChar = char.ConvertFromUtf32(char.ConvertToUtf32(CharData, 0) - 115);
        //            data = data + ConChar;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        data = "1";
        //        return data;
        //    }
        //    return data;
        //}

    }
    public class MailGunModel
    {
        /// <summary>
        /// Readable name that tells which School or service is Sending the message E.g LLoydant or NAU
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// can be any email email addr as long as it ends with a valid domain name
        /// E.G VALID: admin@lloydant.com
        /// INVALID lloydant@admin.com
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string MessageFrom { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public string Link { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string MessageTo { get; set; }
        public string Name { get; set; }
    }
}
