using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportLibrary
{
    public partial class BarChartDetailCourse : Report
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

        public BarChartDetailCourse(Reports.XmlRpc.XmlRpcRequest client, string url, bool showusername, string style, string minor_group_header, string category_group, string template_group)
            : base(client, url, showusername)
        {
            majorgroup.Add("Group Name", "gname");
            minisection = new Dictionary<string, string>();

            if (minor_group_header.Equals("Username") && category_group.Equals("Course") && template_group.Equals("Three Group"))
            {
                SetParams(0);
                minorgroup.Add("Full Name", "userdata");
                section.Add("Course Title", "ctitle");
                minisection.Add("Course Title", "ctitle");
                minisection.Add("Current Grade", "caverage");
                minisection.Add("Lesson Title", "ltitle");
                minisection.Add("Lesson Grade", "laverage");
                minisection.Add("Status", "status");
            }
            else if (minor_group_header.Equals("Course") && category_group.Equals("Username") && template_group.Equals("Three Group"))
            {
                SetParams(1);
                minorgroup.Add("Course Title", "ctitle");
                section.Add("Lesson Title", "ltitle");
                minisection.Add("Full Name", "userdata");
                minisection.Add("Course Title", "ctitle");
                minisection.Add("Current Grade", "caverage");
                minisection.Add("Lesson Title", "ltitle");
                minisection.Add("Lesson Grade", "laverage"); 
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
                    this.sql = "getLessonAverages";
                    this.minor_group_header = "uid";
                    this.category_group_header = "cid";
                    this.category_group = "cid";
                    this.series_group = "ltitle";
                    this.filterOne = "uid";
                    this.filterTwo = "cid";
                    this.graph_X = "laverage";
                    this.flag = 0;
                    break;
                case 1:
                    this.sql = "getLessonAverages";
                    this.minor_group_header = "cid";
                    this.category_group_header = "ltitle";
                    this.category_group = "ltitle";
                    this.series_group = "userdata";
                    this.filterOne = "cid";
                    this.filterTwo = "ltitle";
                    this.graph_X = "laverage";
                    this.flag = 1;
                    break;
            }
        }

        public override ArrayList processData(ArrayList data)
        {
            ArrayList result = new ArrayList();
            result.Capacity = 400;

            foreach (Object oCourse in data)
            {
                Hashtable course = (Hashtable)oCourse;
                ArrayList grades = (ArrayList)course["grades"];

               foreach (Object l in grades)
                {
                    Hashtable lesson = (Hashtable)l;
                    ArrayList scogrades = (ArrayList)lesson["grades"];

                    foreach (Object s in scogrades)
                    {
                        Hashtable sco = (Hashtable)s;
                        Hashtable toadd = new Hashtable();
                        toadd.Add("cid", course["cid"]);
                        toadd.Add("ctitle", course["title"]);
                        toadd.Add("ltitle", lesson["title"]);
                        toadd.Add("caverage", course["average"]);
                        toadd.Add("laverage", lesson["average"]);
                        toadd.Add("status", lesson["status"]);
                        toadd.Add("lft", lesson["lft"]);
                        toadd.Add("title", sco["title"]);
                        toadd.Add("average", sco["grade"]);
                        toadd.Add("weight", sco["weight"]);
                        //toadd.Add("lweight", lesson["weight"]);
                        //toadd.Add("cweight", course["weight"]);
                        result.Add(toadd);
                    }
                }
            }

            return result;

        }
    }
}
