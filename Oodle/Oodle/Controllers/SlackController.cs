using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using Oodle.Models;
using Oodle.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Oodle.Controllers
{
    public class SlackController : Controller
    {
        //Slack token to allow creating and joining classes
        private string SlackToken = System.Web.Configuration.WebConfigurationManager.AppSettings["SlackToken"];
        private string SlackBot = System.Web.Configuration.WebConfigurationManager.AppSettings["SlackBot"];

        public Boolean HasToken()
        {
            return (!(SlackToken == null));
        }

        public Boolean HasBot()
        {
            return (!(SlackBot == null));
        }

        private string GetData(string method, string parameters)
        {
            string surl = "https://slack.com/api/" + method + "?token=" + SlackToken + parameters + "&pretty=1";
            WebRequest request = WebRequest.Create(surl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string slackData = reader.ReadToEnd();
            reader.Close();
            resp.Close();
            dataStream.Close();

            return slackData;
        }
        private string BotData(string method, string parameters)
        {
            string surl = "https://slack.com/api/ "+ method +"?token=" + SlackBot + parameters + "&pretty=1";
            //Send method request to Slack
            WebRequest request = WebRequest.Create(surl);
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            //Convert Slack method response to readable string
            string slackData = reader.ReadToEnd();
            Debug.WriteLine(slackData);
            //Close open requests
            reader.Close();
            resp.Close();
            dataStream.Close();

            return slackData;
        }

        /// <summary>
        /// Checks if user is on slack
        /// </summary>
        /// <param name="email">email of user to check</param>
        /// <returns>true if user is on slack, false if not</returns>
        public Boolean IsOnSlack(string email)
        {
            string slackData = GetData("users.lookupByEmail" , "&email=" + email);

            JObject userID = JObject.Parse(slackData);
            Boolean onSlack = Convert.ToBoolean(userID["ok"].ToString());
            Debug.WriteLine("User Email is On Slack: " + onSlack);
            return onSlack;
        }

        public string ValidateSlackName(string name)
        {
            string sName = name.ToLower();
            sName = Regex.Replace(sName, @"[\s]+", "-");
            sName = Regex.Replace(sName, @"[^a-z0-9-_]+", "_");
            if (sName.Length > 21)
            {
                sName = sName.Remove(21);
            }
            return sName;
        }

        public void SlackNotif(string notif, string sName)
        {
            string cID = GetChannelId(sName);
            notif = Regex.Replace(notif, @"[\s]+", "%20");

            string slackData = BotData("chat.postMessage" , "&channel=" + cID + "&as_user=true" + "&text=" + notif);

            JObject message = JObject.Parse(slackData);
            PinMessage(message["ts"].ToString(), message["channel"].ToString());
        }

        private void PinMessage(string time, string channel)
        {
            string slackData = BotData("pins.add" , "&channel=" + channel + "&timestamp=" + time);
        }

        /// <summary>
        /// Sends an http reqeust to slack api to create a private channel in the slack work space
        /// </summary>
        /// <param name="className">Name of class/channel</param>
        [Authorize]
        public string CreateChannel(string className)
        {
            //url to query Slack to create a private channel. Slack Token authorizes method and identifies slack workspace.
            //className is the name of the private channel
            string slackData = GetData("groups.create" , "&name=" + className);

            Debug.WriteLine(slackData);
            JObject channel = JObject.Parse(slackData);
            string name = "%";
            if (Convert.ToBoolean(channel["ok"].ToString()))
            {
                name = channel["group"]["name"].ToString();
                AddBot(name);
            }

            return name;

        }

        /// <summary>
        /// Sends an http reqeust to slack api to join a private channel in the slack work space
        /// </summary>
        /// <param name="email">email of user trying to join a slack private channel</param>
        /// <param name="className">name of private to join</param>
        [Authorize]
        public void JoinChannel(string email, string className)
        {
            String cid = GetChannelId(className);
            String uid = GetSlackUserId(email);
            string slackData = GetData("groups.invite" , "&channel=" + cid + "&user=" + uid);
        }

        private void AddBot(string className)
        {
            String cid = GetChannelId(className);
            string uid = GetSlackBotID();
            string slackData = GetData("groups.invite" , "&channel=" + cid + "&user=" + uid);
        }

        private string GetSlackBotID()
        {
            string slackData = GetData("users.list" , "");  

            string id = "";
            JObject users = JObject.Parse(slackData);
            IList<JToken> cData = users["members"].Children().ToList();
            foreach (JToken temp in cData)
            {
                SlackBot bTemp = temp.ToObject<SlackBot>();
                if (Convert.ToBoolean(bTemp.is_bot))
                {
                    id = bTemp.id;
                    Debug.WriteLine(id);
                }
            }
            return id;
        }

        /// <summary>
        /// Sends an http reqeust to slack api to get a user ID based on email
        /// </summary>
        /// <param name="email">email of user</param>
        /// <returns>the slack user ID</returns>
        [Authorize]
        private string GetSlackUserId(string email)
        {
            string slackData = GetData("users.lookupByEmail" , "&email=" + email);

            JObject userID = JObject.Parse(slackData);
            string id = "";
            id = userID["user"]["id"].ToString();
            return id;
        }

        /// <summary>
        /// Sends an http reqeust to slack api to get channel ID based on class name
        /// </summary>
        /// <param name="className">name of class/channel</param>
        /// <returns>ID of channel</returns>
        [Authorize]
        private string GetChannelId(string className)
        {

            string slackData = GetData("groups.list" , "");

            string id = "";
            JObject channels = JObject.Parse(slackData);
            IList<JToken> cData = channels["groups"].Children().ToList();
            foreach (JToken temp in cData)
            {
                ChannelID cTemp = temp.ToObject<ChannelID>();
                if (cTemp.name.Equals(className.ToLower()))
                {
                    id = cTemp.id;
                }
            }
            return id;
        }
    }
}