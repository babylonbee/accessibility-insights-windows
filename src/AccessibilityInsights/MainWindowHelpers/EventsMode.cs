// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using AccessibilityInsights.Enums;
using AccessibilityInsights.Misc;
using AccessibilityInsights.Actions;
using AccessibilityInsights.Core.Bases;
using AccessibilityInsights.Desktop.UIAutomation;
using AccessibilityInsights.Desktop.UIAutomation.EventHandlers;
using AccessibilityInsights.DesktopUI.Enums;
using AccessibilityInsights.SharedUx.Dialogs;
using System.Collections.Generic;

namespace AccessibilityInsights
{
    /// <summary>
    /// this is partial class for MainWindow
    /// this portion is for Events Mode
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Start snapshot mode. 
        /// </summary>
        /// <param name="e">root element for listening events</param>
        private void StartEventsMode(A11yElement e)
        {
            if (e == null)
            {
                this.AllowFurtherAction = false;
                MessageDialog.Show(Properties.Resources.StartElementDetailViewNoElementIsSelectedMessage);
                this.AllowFurtherAction = true;
            }
            else if(e.IsSafeToRefresh() == false)
            {
                this.AllowFurtherAction = false;
                MessageDialog.Show(Properties.Resources.StartEventsModeElementNotAvailableMessage);
                this.AllowFurtherAction = true;
            }
            else
            {
                // we need to explicitly set the highlighter mode despite of the highlighter button status
                HighlightAction.GetDefaultInstance().HighlighterMode = HighlighterMode.Highlighter;

                this.ctrlEventMode.Clear();

                DisableElementSelector();

                this.ctrlCurMode.HideControl();
                this.ctrlCurMode = this.ctrlEventMode;
                this.ctrlEventMode.CurrentView = EventsView.Config;
                this.ctrlCurMode.ShowControl();

                var sa = SelectAction.GetDefaultInstance();

                // set the root element to listen to. 
#pragma warning disable CS4014
                // NOTE: We aren't awaiting this async call, so if you
                // touch it, consider if you need to add the await
                this.ctrlEventMode.SetElement(sa.GetSelectedElementContextId().Value);
#pragma warning restore CS4014
                this.CurrentPage = AppPage.Events;
                this.CurrentView = EventsView.Config;
                PageTracker.TrackPage(this.CurrentPage,this.CurrentView.ToString());
            }
        }

        /// <summary>
        /// Pause LiveMonitoring and get into Event mode for Loaded Events data
        /// </summary>
        /// <param name="path">path of event file. </param>
        private void StartEventsWithLoadedData(string path)
        {
            DisableElementSelector();

            List<EventMessage> el = ListenAction.LoadEventMessages(path);

            this.ctrlCurMode.HideControl();
            this.ctrlCurMode = this.ctrlEventMode;
            this.ctrlCurMode.ShowControl();

            this.ctrlEventMode.Clear();

            this.ctrlEventMode.LoadEventRecords(el);

            this.CurrentPage = AppPage.Events;
            this.CurrentView = EventsView.Load;

            PageTracker.TrackPage(this.CurrentPage, this.CurrentView.ToString());
        }
    }
}
