using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportLibrary
{
    public partial class BarChartCatalog : Report
    {
        string minor_group_header;
        string category_group_header;
        string category_group;
        string series_group;
        string graph_X;
        string filterOne;
        int flag;

        public BarChartCatalog(Reports.XmlRpc.XmlRpcRequest client, string url, bool showusername, string style, string minor_group_header, string category_group, string template_group)
            : base(client, url, showusername)
        {
            majorgroup.Add("Group Name", "gname");

            if (minor_group_header.Equals("Username") && category_group.Equals("Catalog") && template_group.Equals("Two Group"))
            {
                SetParams(0);
                minorgroup.Add("Full Name", "userdata");
                section.Add("Catalog Title", "ctitle");
                section.Add("Catalog Grade", "caverage");
                section.Add("Course Title", "title");
                section.Add("Course Grade", "average");
            }
            else if (minor_group_header.Equals("Catalog") && category_group.Equals("Username") && template_group.Equals("Two Group"))
            {
                SetParams(1);
                minorgroup.Add("Catalog Title", "ctitle");
                section.Add("Catalog Title", "ctitle");
                section.Add("Catalog Grade", "caverage");
                section.Add("Course Title", "title");
                section.Add("Course Grade", "average");
            }

        }

        public override Telerik.Reporting.Report getBarChartLayout(ReportLibrary.RowData[] data)
        {
            ReportTemplates.BarChartTwoGroup r = new ReportTemplates.BarChartTwoGroup("Bar Chart");
            r.populateGroup1(majorgroup, "gid");
            r.populateGroup2(minorgroup, minor_group_header);
            r.populateBarChart(section, data, category_group, series_group, graph_X, filterOne, "", category_group_header, flag);

            return r;
        }

        /*
         * Sets the following parameters for the Bar Chart
         * 
         * 1. SQL 
         * 2. Group Two Header
         * 3. Category Group
         * 4. Series Group
         * 5. Filter One
         * 6. X-Axis of Graph
         * 
         * @param int param
         * 
         * @return void
         * 
         */
        public void SetParams(int param)
        {
            switch (param)
            {
                case 0:
                    this.sql = "getCatalogParts";
                    this.minor_group_header = "uid";
                    this.category_group_header = "uid";
                    this.category_group = "uid";
                    this.series_group = "ctitle";
                    this.filterOne = "uid";
                    this.graph_X = "caverage";
                    this.flag = 4;
                    break;
                case 1:
                    this.sql = "getCatalogParts";
                    this.minor_group_header = "cid";
                    this.category_group_header = "cid";
                    this.category_group = "ctitle";
                    this.series_group = "userdata";
                    this.filterOne = "cid";
                    this.graph_X = "caverage";
                    this.flag = 5;
                    break;
            }
        }
    }
}
