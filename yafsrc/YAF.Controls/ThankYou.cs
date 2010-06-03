/* Yet Another Forum.NET
 * Copyright (C) 2006-2010 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
using System;
using System.Data;
using System.Text;
using System.Web.Security;
using AjaxPro;
using YAF.Classes;
using YAF.Classes.Core;
using YAF.Classes.Data;
using YAF.Classes.Utils;

namespace YAF.Controls
{
  /// <summary>
  /// Class for Thank you button
  /// </summary>
  public class ThankYou
  {
    /// <summary>
    /// Gets or sets Text.
    /// </summary>
    public string Text
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets Title.
    /// </summary>
    public string Title
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets MessageID.
    /// </summary>
    public int MessageID
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets ThanksInfo.
    /// </summary>
    public string ThanksInfo
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets Thanks.
    /// </summary>
    public string Thanks
    {
      get;
      set;
    }

    /// <summary>
    /// This method is called asynchronously when the user clicks "Thank" button.
    /// </summary>
    /// <param name="msgID">
    /// </param>
    /// <returns>
    /// </returns>
    [AjaxMethod]
    public ThankYou AddThanks(object msgID)
    {
      MessageID = msgID.ToType<int>();

      string username = DB.message_AddThanks(UserMembershipHelper.GetUserIDFromProviderUserKey(Membership.GetUser().ProviderUserKey), MessageID);

      // if the user is empty, return a null object...
      if (username.IsNotSet())
      {
        return null;
      }

      return CreateThankYou(username, "BUTTON_THANKSDELETE", "BUTTON_THANKSDELETE_TT");
    }

    /// <summary>
    /// This method is called asynchronously when the user clicks on "Remove Thank Note" button.
    /// </summary>
    /// <param name="msgID">
    /// </param>
    /// <returns>
    /// </returns>
    [AjaxMethod]
    public ThankYou RemoveThanks(object msgID)
    {
      MessageID = msgID.ToType<int>();
      string username = DB.message_RemoveThanks(UserMembershipHelper.GetUserIDFromProviderUserKey(Membership.GetUser().ProviderUserKey), MessageID);

      return CreateThankYou(username, "BUTTON_THANKS", "BUTTON_THANKS_TT");
    }

    /// <summary>
    /// Creates an instance of the thank you object from the current information.
    /// </summary>
    /// <param name="username">
    /// </param>
    /// <param name="textTag">
    /// </param>
    /// <param name="titleTag">
    /// </param>
    /// <returns>
    /// </returns>
    private ThankYou CreateThankYou(string username, string textTag, string titleTag)
    {
      // load the DB so YafContext can work...
      YafServices.InitializeDb.Run();

      // return thank you object...
      return new ThankYou
        {           
          MessageID = MessageID, 
          ThanksInfo = ThanksNumber(username), 
          Thanks = GetThanks(MessageID), 
          Text = YafContext.Current.Localization.GetText("BUTTON", textTag), 
          Title = YafContext.Current.Localization.GetText("BUTTON", titleTag)
        };
    }

    /// <summary>
    /// This method returns a string containing the HTML code for
    /// showing the the post footer. the HTML content is the name of users
    /// who thanked the post and the date they thanked.
    /// </summary>
    /// <param name="msgID">
    /// The msg ID.
    /// </param>
    /// <returns>
    /// The get thanks.
    /// </returns>
    public static string GetThanks(object msgID)
    {
      var filler = new StringBuilder();

      using (DataTable dt = DB.message_GetThanks(msgID.ToType<int>()))
      {
        foreach (DataRow dr in dt.Rows)
        {
          if (filler.Length > 0)
          {
            filler.Append(",&nbsp;");
          }

          // vzrus: quick fix for the incorrect link. URL rewriting don't work :(
          filler.AppendFormat(
              @"<a id=""{0}"" href=""{1}""><u>{2}</u></a>", dr["UserID"], YafBuildLink.GetLink(ForumPages.profile, "u={0}", dr["UserID"]).Replace("/yaf.controls.thankyou,yaf.controls.ashx", "/default.aspx"), dr["DisplayName"] != DBNull.Value ? YafContext.HttpContext.Server.HtmlEncode(dr["DisplayName"].ToString()) : YafContext.HttpContext.Server.HtmlEncode(dr["Name"].ToString()));

          if (YafContext.Current.BoardSettings.ShowThanksDate)
          {
            filler.AppendFormat(
              @" {0}", String.Format(YafContext.Current.Localization.GetText("DEFAULT", "ONDATE"), YafServices.DateTime.FormatDateShort(dr["ThanksDate"])));
          }
        }
      }

      return filler.ToString();
    }

    /// <summary>
    /// This method returns a string which shows how many times users have
    /// thanked the message with the provided messageID. Returns an empty string.
    /// </summary>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <returns>
    /// The thanks number.
    /// </returns>
    protected string ThanksNumber(string username)
    {
      int thanksNumber = DB.message_ThanksNumber(MessageID);

      switch (thanksNumber)
      {
        case 0:
          return String.Empty;
        case 1:
          return String.Format(YafContext.Current.Localization.GetText("POSTS", "THANKSINFOSINGLE"), username);
      }

      return String.Format(YafContext.Current.Localization.GetText("POSTS", "THANKSINFO"), thanksNumber, username);
    }
  }
}