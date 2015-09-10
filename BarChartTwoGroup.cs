namespace ReportTemplates
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for BarChartTwoGroup.
    /// </summary>
    public partial class BarChartTwoGroup : Template
    {
        public BarChartTwoGroup(String reportname)
        {
            InitializeComponent();
            this.reportTitle.Value = reportname;
            this.reportTitle.StyleName = "Title";
            this.Name = reportname;
        }

        public void populateGroup1(Dictionary<String, String> items, String grouping_id)
        {
            if (items.Count > 0)
            {
                this.Groups[0].Groupings.Add(grouping_id);
                this.populatePanel(this.panelGroupName, items, "MajorGrouping");
                groupFooterSection2.PageBreak = PageBreak.After;
            }
            else
            {
                this.detail_panel.Location.Y.Subtract(panelGroupName.Height);
                this.groupHeaderSection2.Height.Subtract(panelGroupName.Height);
                this.groupHeaderSection2.Items.Remove(panelGroupName);
            }
        }

        public void populateGroup2(Dictionary<String, String> items, String grouping_id)
        {
            if (items.Count > 0)
            {
                this.Groups[1].Groupings.Add(grouping_id);
                this.populatePanel(this.second_header, items, "MinorGrouping");
                groupFooterSection1.PageBreak = PageBreak.After;
                this.Items.Remove(this.detailSection);
            }
            else
            {
                this.Items.Remove(this.groupHeaderSection1);
                this.Items.Remove(this.groupFooterSection1);
            }
        }

        /*
        * Populates the Bar Chart in the details panel
        * 
        * @return void
        * 
        */
        public void populateBarChart(Dictionary<String, String> section, ReportLibrary.RowData[] data, String category_group, String series_group, String graph_X, String filterOne, String filterTwo, String category_group_header, int flag)
        {
            if (section.Count > 0)
            {
                this.Groups[2].Groupings.Add(category_group_header);
                this.populateBarChart(this.graph_section, section, data, category_group, series_group, graph_X, filterOne, filterTwo, flag);
                //groupFooterSection.PageBreak = PageBreak.After;
                this.Items.Remove(this.detailSection);
            }
            else
            {
                this.Items.Remove(this.groupHeaderSection);
                this.Items.Remove(this.groupFooterSection);
            }
        }
    }
}