using System;
using HtmlAgilityPack;
using System.IO;
using wSQL.Business.Repository;
using wSQL.Library;
using System.Collections;
using System.Linq;
using System.Net;
using wSQL.Data.Models;
using System.Collections.Generic;

namespace wSQL.Business.Services
{
   public class WebCore : WebCoreRepository
   {
      public event EventHandler<object> OnPrint;

      public string OpenPage(string url)
      {
         using (var web = new WebClient())
         {
            System.Diagnostics.Debug.WriteLine("OpenPage " + url);
            return web.DownloadString(url);
         }
      }

      public string OpenPage(string url, string loginPage, string userName, string password)
      {
         using (var web = new CookieAwareWebClient())
         {
            var response = web.DownloadString(loginPage);
            web.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            response = web.UploadString(loginPage,
              string.Format("UserName={0}&Password={1}&__RequestVerificationToken=", userName, password) + extractValidationToken(response));

            System.Diagnostics.Debug.WriteLine("OpenPage: " + url);
            response = web.DownloadString(url);
            System.Diagnostics.Debug.WriteLine("OpenPage Done");
            return response;
         }
      }

      public void Print(dynamic value)
      {
          if (OnPrint != null)
              OnPrint(this, value);
      }

      public dynamic Find(string value, string xpath)
      {
         HtmlDocument document = new HtmlDocument();
         document.Load(new StringReader(value));
         return document.DocumentNode.SelectNodes(xpath);
      }

      public dynamic ExtractText(string value, string xpath)
      {
         string html = "";

         HtmlDocument document = new HtmlDocument();
         document.Load(new StringReader(value));
         var nodes = document.DocumentNode.SelectNodes(xpath);
        
         if (nodes != null)
            foreach (var node in nodes)
               html += node.InnerText + System.Environment.NewLine;

         return html;
      }

      //

      private string extractValidationToken(string page)
      {
         var startIndex = page.IndexOf("__RequestVerificationToken");
         startIndex = page.IndexOf("value=", startIndex) + 7;
         var endIndex = page.IndexOf("\"", startIndex);
         return page.Substring(startIndex, endIndex - startIndex);
      }

      private IEnumerable<string> convertInputToString(dynamic value, dynamic itemSeparator, dynamic lineEnd)
      {
         string separator = ",";
         if (itemSeparator != null && itemSeparator is string)
            separator = (string)itemSeparator;

         string endLine = Environment.NewLine;
         if (lineEnd != null && lineEnd is string)
            endLine = lineEnd as string;
         
         string result = "";
         var list = value as IEnumerable;
         if (list != null)
         {
            bool skeep = false;
            foreach (var item in list)
            {
               if (!(item is string))
               {
                  var subList = item as IEnumerable;
                  if (subList != null)
                  {
                     skeep = true;
                     foreach (var subItem in convertInputToString(subList, separator, endLine))
                        yield return subItem;
                     //PrintList(subList, separator, endLine);
                  }
               }
            }

            if (!skeep)
            {
               result = string.Join(separator, list.OfType<string>().ToArray());
               result += endLine;
               yield return result;
            }
         }
      }

      public void PrintList(dynamic value, dynamic itemSeparator, dynamic lineEnd)
      {
         foreach (var line in convertInputToString(value, itemSeparator, lineEnd))
            Print(line);
         
         /*
         string separator = ",";
         if (itemSeparator != null && itemSeparator is string)
            separator = (string)itemSeparator;

         string endLine = lineEnd as string;
         if (endLine == null) endLine = Environment.NewLine;

         string result = "";
         var list = value as IEnumerable;
         if (list != null)
         {
            bool skeep = false;
            foreach (var item in list)
            {
               if (!(item is string))
               {
                  var subList = item as IEnumerable;
                  if (subList != null)
                  {
                     skeep = true;
                     PrintList(subList, separator, endLine);
                  }
               }
            }

            if (!skeep)
            {
               result = string.Join(separator, list.OfType<string>().ToArray());

               if (lineEnd != null && lineEnd is string)
                  result += (string)endLine;
            }
         }
         */
         //Print(result);
      }

      public string OpenFile(string fileName)
      {
         if (File.Exists(fileName))
         {
            return File.ReadAllText(fileName);
         }
         else
            throw new FileNotFoundException("Unable to find file: " + fileName);
      }

      public void WriteToFile(dynamic value, dynamic fileObject, dynamic itemSeparator, dynamic lineEnd)
      {
         if (fileObject != null && fileObject is FileObject)
         {
            //fileObject.FileMode
            var fileInfo = (FileObject)fileObject;
            string content = "";
            foreach (var item in convertInputToString(value, itemSeparator, lineEnd))
               content += item;

            try
            {
               switch (fileInfo.FileMode)
               {
                  case "a":
                     File.AppendAllText(fileInfo.FileName, content);
                     break;
                  default:
                     File.WriteAllText(fileInfo.FileName, content);
                     break;
               }
            }
            catch (Exception ex)
            {
               throw;
            }

         }
         else
            throw new Exception("WriteToFile missing file information");
      }
   }
}