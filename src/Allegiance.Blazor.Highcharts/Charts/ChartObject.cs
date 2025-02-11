﻿using Allegiance.Blazor.Highcharts.Core.Constants;
using Allegiance.Blazor.Highcharts.Core.Options;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Allegiance.Blazor.Highcharts.Core.Charts
{
    public partial class ChartObject : IHighchart
    {
        [JsonProperty("chart", NullValueHandling = NullValueHandling.Ignore)]
        public Chart Chart { get; set; }

        [JsonProperty("accessibility", NullValueHandling = NullValueHandling.Ignore)]
        public Accessibility Accessibility { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public Title Title { get; set; }

        [JsonProperty("subtitle", NullValueHandling = NullValueHandling.Ignore)]
        public Title Subtitle { get; set; }

        [JsonProperty("xAxis", NullValueHandling = NullValueHandling.Ignore)]
        public XAxis XAxis { get; set; }

        [JsonProperty("yAxis", NullValueHandling = NullValueHandling.Ignore)]
        public YAxis YAxis { get; set; }

        [JsonProperty("tooltip", NullValueHandling = NullValueHandling.Ignore)]
        public Tooltip Tooltip { get; set; }

        [JsonProperty("plotOptions", NullValueHandling = NullValueHandling.Ignore)]
        public PlotOptions PlotOptions { get;set; }

        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public List<SeriesElement> Series { get; set; }
        [JsonProperty("credits", NullValueHandling = NullValueHandling.Ignore)]
        public Credits Credits { get; set; }

        [JsonProperty("legend", NullValueHandling = NullValueHandling.Ignore)]
        public Legend Legend { get; set; }
        [JsonProperty("responsive", NullValueHandling = NullValueHandling.Ignore)]
        public Responsive Responsive { get; set; }
        [JsonProperty("exporting", NullValueHandling = NullValueHandling.Ignore)]
        public Exporting Exporting { get; set; }

        [JsonProperty("pane", NullValueHandling = NullValueHandling.Ignore)]
        public Pane Pane { get; set; }
        
        [JsonProperty("drilldown", NullValueHandling = NullValueHandling.Ignore)]
        public Drilldown Drilldown { get; set; }

        [JsonProperty("colors", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Colors { get; set; }

        public ChartObject()
        {
            Chart = new Chart();
            Accessibility = new Accessibility();
            Title = new Title();
            Subtitle = new Title();
            XAxis = new XAxis();
            YAxis = new YAxis();
            Tooltip = new Tooltip();
            PlotOptions = new PlotOptions();
            Series = null;
            Credits = new Credits();
            Legend = new Legend();
            Responsive = new Responsive();
            Exporting = new Exporting();
            Pane = new Pane();
            Drilldown = new Drilldown();
        }

        public string Generate()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true });
            return json.Replace("columnRange", "columnrange");
        }

        public async Task ChangeSeriesAsync(IJSRuntime jsRuntime, string containerId, List<object> newData)
        {
            await jsRuntime.InvokeVoidAsync("updateSeriesHighchartChart", containerId, newData);
        }
        public async Task DestroyAllChartsAsync(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("destroyCharts");
        }
        public async Task ChangeSeriesAtIndexAsync(IJSRuntime jsRuntime, string containerId, int index, List<object> newData)
        {
            await jsRuntime.InvokeVoidAsync("updateSeriesAtIndexHighchartChart", containerId, index, newData);
        }
        public async Task ChangeTitleAsync(IJSRuntime jsRuntime, string containerId, string titleText, string titleColor)
        {
            await jsRuntime.InvokeVoidAsync("updateTitleHighchartChart", containerId, titleText, titleColor);
        }
        public async Task ChangeXAxisPlotlineValAsync(IJSRuntime jsRuntime, string containerId, double plotlineVal, int plotlineIndex = 0)
        {
            await jsRuntime.InvokeVoidAsync("updateXAxisPlotLineValue", containerId, plotlineVal, plotlineIndex);
        }
        public async Task ChangeYAxisPlotlineValAsync(IJSRuntime jsRuntime, string containerId, double plotlineVal, int plotlineIndex = 0)
        {
            await jsRuntime.InvokeVoidAsync("updateYAxisPlotLineValue", containerId, plotlineVal, plotlineIndex);
        }
        public async Task ChangeXAxisCategoriesAsync(IJSRuntime jsRuntime, string containerId, List<double> newCategories)
        {
            await jsRuntime.InvokeVoidAsync("updateXAxisCategories", containerId, newCategories);
        }
        public void DisposeChart(IJSRuntime jsRuntime, string containerId)
        {
            // This is running synchronously, if not triggered it will get handled by JavaScript if the chart already exists.
            jsRuntime.InvokeVoidAsync("disposeHighchartChart", containerId);
        }
    }
}

