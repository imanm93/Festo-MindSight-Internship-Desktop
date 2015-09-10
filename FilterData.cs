using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportTemplates
{
    class FilterData
    {
        public String filterOne { get; set; }
        public String filterItem { get; set; }
        public String id { get; set; }

        public FilterData(String filterOne, String id, String filterItem)
        {
           this.filterOne = filterOne;
           this.id = id;
           this.filterItem = filterItem;
        }

        public bool Equals(FilterData obj)
        {
            return obj.filterOne == this.filterOne && obj.id == this.id && obj.filterItem == this.filterItem;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            FilterData fd = obj as FilterData;
            if (fd == null)
                return false;

            return this.Equals(fd);
        }

    }
}
