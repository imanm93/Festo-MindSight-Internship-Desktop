/*
         * populateBarChart
         * Populates bar chart in the appropriate panel with the correct axes.
         *  
         * @return void
         * 
         */
        virtual protected void populateBarChart(Telerik.Reporting.Panel panel, Dictionary<String, String> items, ReportLibrary.RowData[] data, String category_group, String series_group, String graph_X, String filterOne, String filterTwo, int flag) 
        {
            
            this.data = data;
            this.category_group = category_group;
            this.series_group = series_group;
            this.filterOne = filterOne;
            this.filterTwo = filterTwo;
            this.flag = flag;
            this.graph_X = graph_X;
            this.panel = panel;
            
            // Get Filters
            switch (flag)
            { 
                case 0:
                case 1:
                case 2:
                case 3:
                    filters = populateFilters(filterOne, filterTwo);
                    break;
                case 4:
                case 5:
                    filtersPrimary = populatePrimaryFilter(filterOne);
                    break;
            }

            // Create bar chart axes, groups
            barChart = new Telerik.Reporting.Graph();
            barChart.Name = "Graph";
            barChart.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20), Telerik.Reporting.Drawing.Unit.Cm(12));
            barChart.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0), Telerik.Reporting.Drawing.Unit.Cm(0.5));
            barChart.DataSource = data;
            
            category_Group_Graph = new Telerik.Reporting.GraphGroup();
            category_Group_Graph.Name = "Category Group" + primaryCounter;
            category_Group_Graph.Groupings.Add(new Telerik.Reporting.Grouping("=" + category_group));
            barChart.CategoryGroups.Add(category_Group_Graph);

            series_Group_Graph = new Telerik.Reporting.GraphGroup();
            series_Group_Graph.Name = "Series Group" + primaryCounter;
            series_Group_Graph.Groupings.Add(new Telerik.Reporting.Grouping("=" + series_group));
            barChart.SeriesGroups.Add(series_Group_Graph);

            barChart.Filters.Add("=" + graph_X, FilterOperator.NotEqual, "=-1");

            switch (flag)
            { 
                case 0:
                case 2:
                    barChart.Filters.Add("=" + filterOne, FilterOperator.Equal, "=" + filters[primaryCounter].filterOne.ToString());
                    barChart.Filters.Add("=" + category_group, FilterOperator.Equal, "=" + filters[primaryCounter].id.ToString());
                    break;
                case 1:
                case 3:
                    barChart.Filters.Add("=" + filterOne, FilterOperator.Equal, filters[primaryCounter].filterOne.ToString());
                    barChart.Filters.Add("=" + category_group, FilterOperator.Equal, filters[primaryCounter].filterItem.ToString());
                    break;
                case 4:
                case 5:
                    barChart.Filters.Add("=" + filterOne, FilterOperator.Equal, filtersPrimary[primaryCounter].ToString());
                    break;
            }

            primaryCounter++;

            // Add categorical graph axes
            var graphAxisCategoryScale = new Telerik.Reporting.GraphAxis();
            graphAxisCategoryScale.Name = "Category Scale" + primaryCounter;
            graphAxisCategoryScale.Scale = new Telerik.Reporting.CategoryScale();

            // Add numeric graph axes
            var graphAxisNumericScale = new Telerik.Reporting.GraphAxis();
            graphAxisNumericScale.Name = "Numerical Scale" + primaryCounter;

            var graphRange = new Telerik.Reporting.NumericalScale();
            graphRange.Maximum = 100;
            graphRange.Minimum = 0;

            graphAxisNumericScale.Scale = graphRange;

            // Assign Coordinate System and Axes of the Graph
            var cartesianBarChartCoordinateSystem = new Telerik.Reporting.CartesianCoordinateSystem();
            cartesianBarChartCoordinateSystem.Name = "barChartCoordinationSystem";
            cartesianBarChartCoordinateSystem.XAxis = graphAxisNumericScale;
            cartesianBarChartCoordinateSystem.YAxis = graphAxisCategoryScale;
            graphAxisCategoryScale.Style.Visible = false;
            // Add Coordinate System to the bar chart to be displayed
            barChart.CoordinateSystems.Add(cartesianBarChartCoordinateSystem);

            var barSeries = new Telerik.Reporting.BarSeries();
            barSeries.CategoryGroup = category_Group_Graph;
            barSeries.CoordinateSystem = cartesianBarChartCoordinateSystem;
            barSeries.LegendItem.Value = "=" + series_group;
            barSeries.SeriesGroup = series_Group_Graph;
            barSeries.X = "=" + graph_X;
            
            barChart.Series.Add(barSeries);

            panel.Items.Add(barChart);

            // Dummy textbox used to filter bar chart
            empty_textbox = new Telerik.Reporting.TextBox();
            empty_textbox.Name = "Course Title";
            empty_textbox.Value = "=" + items["Course Title"];

            empty_textbox.ItemDataBound += new System.EventHandler(dataFilter);
            panel.Items.Add(empty_textbox);

        }

        /*
         * populateFilters
         * Sets an array populated with the required filters.
         * 
         * @param string filterOne
         * @param string filterTwo
         * 
         * @return FilterData[]
         */
        private FilterData[] populateFilters(String filterOne, String filterTwo)
        {
            ArrayList primaryFilterList = populatePrimaryFilter(filterOne);
            FilterData[] tempfilters = new FilterData[data.Length];
            int counter = 0;

            foreach (String filter in primaryFilterList)
            {
                foreach (ReportLibrary.RowData row in data)
                {
                    switch (filterOne)
                    { 
                        case "uid":
                            switch (filterTwo)
                            { 
                                case "cid":
                                    switch (flag)
                                    { 
                                        case 0:
                                        case 2:
                                            if (row.uid.ToString().Equals(filter) && !tempfilters.Contains(new FilterData(filter, row.cid.ToString(), row.ctitle.ToString())))
                                            {
                                                FilterData item = new FilterData(filter, row.cid.ToString(), row.ctitle.ToString());
                                                tempfilters[counter] = item;
                                                counter++;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case "cid":
                            switch (filterTwo) 
                            {
                                case "ltitle":
                                    if (row.cid.ToString().Equals(filter) && !tempfilters.Contains(new FilterData(filter, row.cid.ToString(), row.ltitle.ToString())))
                                    {
                                        FilterData item = new FilterData(filter, row.cid.ToString(), row.ltitle.ToString());
                                        tempfilters[counter] = item;
                                        counter++;
                                    }
                                    break;
                                case "title":
                                    if (row.cid.ToString().Equals(filter) && !tempfilters.Contains(new FilterData(filter, row.cid.ToString(), row.title.ToString())))
                                    {
                                        FilterData item = new FilterData(filter, row.cid.ToString(), row.title.ToString());
                                        tempfilters[counter] = item;
                                        counter++;
                                    }
                                    break;
                            }
                            break;
                    }
                }
            }

            return tempfilters;

        }

        /*
         * 
         * Populates the primary filter
         * 
         * @param string filterOne
         * 
         * @return ArrayList
         * 
         */
        private ArrayList populatePrimaryFilter(String filterOne)
        {
            ArrayList tempPrimaryFilter = new ArrayList();

            foreach (ReportLibrary.RowData row in data)
            {
                switch (filterOne) 
                { 
                    case "uid":
                        if (tempPrimaryFilter.Contains(row.uid.ToString()))
                        {
                            // Do nothing as it already exists in the list
                        }
                        else
                        {
                            tempPrimaryFilter.Add(row.uid.ToString());
                        }       
                        break;
                    case "cid":
                        if (tempPrimaryFilter.Contains(row.cid.ToString()))
                        {
                            // Do nothing as it already exists in the list
                        }
                        else
                        {
                            tempPrimaryFilter.Add(row.cid.ToString());
                        }       
                        break;
                }
            }

            return tempPrimaryFilter;
        }
		
		
		/*
         * Changes filter applied to the graph to correspond to the current view.
         * 
         * @return void
         * 
         */
        private void graphFilter(int flag)
        {
            switch (flag)
            { 
                case 0:
                case 2:
                    // Remove previous filter
                    barChart.Filters.Remove(new Telerik.Reporting.Filter("=" + filterOne, FilterOperator.Equal, "="+filters[primaryCounter - 1].filterOne.ToString()));
                    barChart.Filters.Remove(new Telerik.Reporting.Filter("=" + category_group, FilterOperator.Equal, "="+filters[primaryCounter - 1].id.ToString()));    
                    // Add new filter
                    barChart.Filters.Add("=" + filterOne, FilterOperator.Equal, "="+filters[primaryCounter].filterOne.ToString());
                    barChart.Filters.Add("=" + category_group, FilterOperator.Equal, "="+filters[primaryCounter].id.ToString());
                    break;
                case 1:
                case 3:
                    // Remove previous filter
                    barChart.Filters.Remove(new Telerik.Reporting.Filter("=" + filterOne, FilterOperator.Equal, filters[primaryCounter-1].filterOne.ToString()));
                    barChart.Filters.Remove(new Telerik.Reporting.Filter("=" + category_group, FilterOperator.Equal, filters[primaryCounter-1].filterItem.ToString()));
                    // Add new filter
                    barChart.Filters.Add("=" + filterOne, FilterOperator.Equal, filters[primaryCounter].filterOne.ToString());
                    barChart.Filters.Add("=" + category_group, FilterOperator.Equal, filters[primaryCounter].filterItem.ToString());
                    break;
                case 4:
                case 5:
                    // Remove previous filter
                    barChart.Filters.Remove(new Telerik.Reporting.Filter("=" + filterOne, FilterOperator.Equal, filtersPrimary[primaryCounter - 1].ToString()));
                    // Add new filter
                    barChart.Filters.Add("=" + filterOne, FilterOperator.Equal, filtersPrimary[primaryCounter].ToString());
                    break;
            }
            
            primaryCounter++;
        }
		