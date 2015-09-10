using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportLibrary
{
    public partial class BarChartDetailCatalog : Report
    {
        Dictionary<String, String> minisection;
        string minor_group_header;
        string category_group_header;
        string category_group;
        string series_group;
        string graph_X;
        string filterOne;
        string filterTwo;
        int flag;

        public BarChartDetailCatalog(Reports.XmlRpc.XmlRpcRequest client, string url, bool showusername, string style, string minor_group_header, string category_group, string template_group)
            : base(client, url, showusername)
        {
            majorgroup.Add("Group Name", "gname");
            minisection = new Dictionary<string, string>();

            if (minor_group_header.Equals("Username") && category_group.Equals("Catalog") && template_group.Equals("Three Group"))
            {
                SetParams(0);
                minorgroup.Add("Full Name", "userdata");
                section.Add("Catalog Title", "ctitle");
                minisection.Add("Catalog Title", "ctitle");
                minisection.Add("Catalog Grade", "caverage");
                minisection.Add("Course Title", "title");
                minisection.Add("Course Grade", "average");
            }
            else if (minor_group_header.Equals("Catalog") && category_group.Equals("Username") && template_group.Equals("Three Group"))
            {
                SetParams(1);
                minorgroup.Add("Catalog Title", "ctitle");
                section.Add("Course Title", "title");
                minisection.Add("Catalog Title", "ctitle");
                minisection.Add("Catalog Grade", "caverage");
                minisection.Add("Course Title", "title");
                minisection.Add("Course Grade", "average");
            }
        }

        public override Telerik.Reporting.Report getBarChartLayout(ReportLibrary.RowData[] data)
        {
            ReportTemplates.BarChartThreeGroup r = new ReportTemplates.BarChartThreeGroup("Bar Chart");
            r.populateGroup1(majorgroup, "gid");
            r.populateGroup2(minorgroup, minor_group_header);
            r.populateGroup3(section, category_group_header);
            r.populateBarChart(minisection, data, category_group, series_group, graph_X, filterOne, filterTwo, category_group_header, flag);

            return r;
        }

        /*
         * Sets the following parameters for the Bar Chart
         * 
         * 1. SQL 
         * 2. Group Two Header
         * 3. Group Three Header
         * 4. Category Group
         * 5. Series Group
         * 6. Filter One
         * 7. Filter Two
         * 8. X-Axis of Graph
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
                    this.category_group_header = "cid";
                    this.category_group = "cid";
                    this.series_group = "title";
                    this.filterOne = "uid";
                    this.filterTwo = "cid";
                    this.graph_X = "average";
                    this.flag = 2;
                    break;
                case 1:
                    this.sql = "getCatalogParts";
                    this.minor_group_header = "cid";
                    this.category_group_header = "title";
                    this.category_group = "title";
                    this.series_group = "userdata";
                    this.filterOne = "cid";
                    this.filterTwo = "title";
                    this.graph_X = "average";
                    this.flag = 3;
                    break;
            }
        }

    }
}
