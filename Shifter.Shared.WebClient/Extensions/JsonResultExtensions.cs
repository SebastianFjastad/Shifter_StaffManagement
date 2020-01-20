using System;
using System.Web.Mvc;
using Framework.Notifications;

namespace Shifter.Shared.WebClient.Extensions
{
    public static class JsonResultExtensions
    {
        public static JsonResult Create(this JsonResult json, NotificationCollection notifications)
        {
            json.Data = new { HasErrors = notifications.HasErrors(), Notifications = notifications, Timestamp = DateTime.Now };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json; 
        }
        
        public static JsonResult Successful(this JsonResult json)
        {
            json.Data = new { HasErrors = false, Error = string.Empty, Timestamp = DateTime.Now };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public static JsonResult Successful(this JsonResult json, string message)
        {
            json.Data = new { HasErrors = false, Message = message, Timestamp = DateTime.Now };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public static JsonResult Successful(this JsonResult json, object data)
        {
            json.Data = new { HasErrors = false, Data = data, Timestamp = DateTime.Now };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public static JsonResult Error(this JsonResult json)
        {
            json.Data = new { HasErrors = true };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public static JsonResult Error(this JsonResult json, string message)
        {
            json.Data = new { HasErrors = true, Message = message };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public static JsonResult Error(this JsonResult json, NotificationCollection errors)
        {
            json.Data = new { HasErrors = true, Errors = errors };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public static JsonResult Warning(this JsonResult json, string message)
        {
            json.Data = new { HasWarnings = true, Warning = message };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public static JsonResult Message(this JsonResult json, object message)
        {
            json.Data = message;
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
      
    }
}