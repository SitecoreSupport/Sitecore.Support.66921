﻿using Sitecore.Form.Core.Ascx.Controls;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Reflection;

namespace Sitecore.Support.Form.Web.UI.Controls
{
    public class SitecoreSimpleFormAscx : Sitecore.Form.Web.UI.Controls.SitecoreSimpleFormAscx
    {
        private void OnRefreshError(string message)
        {
            this.OnRefreshError(new string[]
			{
				message
			});
        }

        protected override void OnLoad(EventArgs e)
        {
            this.OnRefreshError(string.Empty);
            if (!this.Page.IsPostBack && !this.Page.IsCallback && !base.IsTresholdRedirect)
            {
                base.RenderedTime = DateTime.UtcNow;
            }
            this.Page.ClientScript.RegisterClientScriptInclude("jquery", "/sitecore%20modules/web/web%20forms%20for%20marketers/scripts/jquery.js");
            this.Page.ClientScript.RegisterClientScriptInclude("jquery-ui.min", "/sitecore%20modules/web/web%20forms%20for%20marketers/scripts/jquery-ui.min.js");
            this.Page.ClientScript.RegisterClientScriptInclude("jquery-ui-i18n", "/sitecore%20modules/web/web%20forms%20for%20marketers/scripts/jquery-ui-i18n.js");
            this.Page.ClientScript.RegisterClientScriptInclude("json2.min", "/sitecore%20modules/web/web%20forms%20for%20marketers/scripts/json2.min.js");
            this.Page.ClientScript.RegisterClientScriptInclude("head.load.min", "/sitecore%20modules/web/web%20forms%20for%20marketers/scripts/head.load.min.js");
            this.Page.ClientScript.RegisterClientScriptInclude("sc.webform", "/sitecore%20modules/web/web%20forms%20for%20marketers/scripts/sc.webform.js?v=17072012");
            if (base.IsAnalyticsEnabled && !base.FastPreview)
            {
                IAnalyticsTracker analyticsTracker = (IAnalyticsTracker)this.GetType().BaseType.BaseType.BaseType.BaseType.GetField("analyticsTracker", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).GetValue(this);
                analyticsTracker.GetType().GetProperty("BasePageTime", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).SetValue(analyticsTracker, base.RenderedTime);
                base.EventCounter.Value = ((int)analyticsTracker.GetType().GetProperty("EventCounter", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).GetValue(analyticsTracker) + 1).ToString();
            }
            this.OnAddInitOnClient();
            this.Page.PreRenderComplete += new EventHandler(base.OnPreRenderComplete);
        }
    }
}