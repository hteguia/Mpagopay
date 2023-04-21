if (typeof jQuery === "undefined") {
  throw new Error("jQuery plugins need to be before this file");
}

$(function () {
  ("use strict");

  // top sparklines
  var randomizeArray = function (arg) {
    var array = arg.slice();
    var currentIndex = array.length,
      temporaryValue,
      randomIndex;

    while (0 !== currentIndex) {
      randomIndex = Math.floor(Math.random() * currentIndex);
      currentIndex -= 1;

      temporaryValue = array[currentIndex];
      array[currentIndex] = array[randomIndex];
      array[randomIndex] = temporaryValue;
    }
    return array;
  };

  // data for the sparklines that appear below header area
  var sparklineData = [47, 45, 54, 38, 56, 24, 65, 31, 54, 38, 56];

  // topb big chart
  var spark1 = {
    chart: {
      type: "area",
      height: 60,
      sparkline: {
        enabled: true,
      },
    },
    stroke: {
      width: 1,
    },
    series: [
      {
        data: randomizeArray(sparklineData),
      },
    ],
    fill: {
      type: "gradient",
      gradient: {
        gradientToColors: ["var(--chart-color3)"],
        shadeIntensity: 2,
        opacityFrom: 0.7,
        opacityTo: 0.2,
        stops: [0, 100],
      },
    },
    colors: ["var(--chart-color3)"],
  };
  var spark1 = new ApexCharts(document.querySelector("#apexspark1"), spark1);
  spark1.render();

  var spark2 = {
    chart: {
      type: "area",
      height: 60,
      sparkline: {
        enabled: true,
      },
    },
    stroke: {
      width: 1,
    },
    series: [
      {
        data: randomizeArray(sparklineData),
      },
    ],
    fill: {
      type: "gradient",
      gradient: {
        gradientToColors: ["var(--chart-color5)"],
        shadeIntensity: 2,
        opacityFrom: 0.7,
        opacityTo: 0.2,
        stops: [0, 100],
      },
    },
    colors: ["var(--chart-color5)"],
  };
  var spark2 = new ApexCharts(document.querySelector("#apexspark2"), spark2);
  spark2.render();

  var spark3 = {
    chart: {
      type: "area",
      height: 60,
      sparkline: {
        enabled: true,
      },
    },
    stroke: {
      width: 1,
    },
    series: [
      {
        data: randomizeArray(sparklineData),
      },
    ],
    fill: {
      type: "gradient",
      gradient: {
        gradientToColors: ["var(--chart-color1)"],
        shadeIntensity: 2,
        opacityFrom: 0.7,
        opacityTo: 0.2,
        stops: [0, 100],
      },
    },
    colors: ["var(--chart-color1)"],
  };
  var spark3 = new ApexCharts(document.querySelector("#apexspark3"), spark3);
  spark3.render();

  var spark4 = {
    chart: {
      type: "area",
      height: 60,
      sparkline: {
        enabled: true,
      },
    },
    stroke: {
      width: 1,
    },
    series: [
      {
        data: randomizeArray(sparklineData),
      },
    ],
    fill: {
      type: "gradient",
      gradient: {
        gradientToColors: ["var(--chart-color2)"],
        shadeIntensity: 2,
        opacityFrom: 0.7,
        opacityTo: 0.2,
        stops: [0, 100],
      },
    },
    colors: ["var(--chart-color2)"],
  };
  var spark4 = new ApexCharts(document.querySelector("#apexspark4"), spark4);
  spark4.render();

  //   Word
  $(function () {
    "use strict";

    var randomizeArray = function (arg) {
      var array = arg.slice();
      var currentIndex = array.length,
        temporaryValue,
        randomIndex;

      while (0 !== currentIndex) {
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;

        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
      }
      return array;
    };

    // data for the sparklines that appear below header area
    var sparklineData = [47, 45, 54, 38, 56, 24, 65, 31, 54, 38, 56];

    // Worldwide Analytics
    $("#Top-Country").vectorMap({
      map: "world_mill",
      normalizeFunction: "polynomial",
      hoverOpacity: 0.2,
      hoverColor: false,
      backgroundColor: "var(--card-color)",
      regionStyle: {
        initial: {
          fill: "var(--color-400)",
          stroke: "var(--card-color)",
        },
      },
      markerStyle: {
        initial: {
          fill: "var(--primary-color)",
          stroke: "var(--card-color)",
        },
      },
      markers: [
        { latLng: [36.77, -119.41], name: "USA : 7.01%" },
        { latLng: [21.0, 78.0], name: "INDIA : 3.01%" },
        { latLng: [-33.0, 151.0], name: "Australia : 11k" },
        { latLng: [55.37, -3.41], name: "UK : 8.50%" },
        { latLng: [41.9, 12.45], name: "Vatican City : 1.2%" },
        { latLng: [1.3, 103.8], name: "Singapore : 2.8%" },
        { latLng: [26.02, 50.55], name: "Bahrain : 0.8%" },
      ],
      series: {
        regions: [
          {
            values: {
              US: "var(--chart-color2)",
              AU: "var(--chart-color3)",
              IN: "var(--chart-color4)",
              UK: "var(--chart-color5)",
            },
            attribute: "fill",
          },
        ],
      },
    });
  });

  // Product Report
  $(document).ready(function () {
    var options = {
      chart: {
        height: 300,
        type: "area",
        stacked: true,
        toolbar: {
          show: false,
        },
        events: {
          selection: function (chart, e) {
            console.log(new Date(e.xaxis.min));
          },
        },
      },

      colors: ["#ffd55d", "#ff7f81", "#e4bd51"],
      dataLabels: {
        enabled: false,
      },

      series: [
        {
          name: "Mobile",
          data: generateDayWiseTimeSeries(
            new Date("11 Feb 2017 GMT").getTime(),
            20,
            {
              min: 10,
              max: 60,
            }
          ),
        },
        {
          name: "Laptop",
          data: generateDayWiseTimeSeries(
            new Date("11 Feb 2017 GMT").getTime(),
            20,
            {
              min: 10,
              max: 20,
            }
          ),
        },
        {
          name: "Tablet",
          data: generateDayWiseTimeSeries(
            new Date("11 Feb 2017 GMT").getTime(),
            20,
            {
              min: 10,
              max: 15,
            }
          ),
        },
      ],

      fill: {
        type: "gradient",
        gradient: {
          opacityFrom: 0.6,
          opacityTo: 0.8,
        },
      },

      legend: {
        position: "top",
        horizontalAlign: "right",
        show: true,
      },
      xaxis: {
        type: "datetime",
      },
      grid: {
        yaxis: {
          lines: {
            show: false,
          },
        },
        padding: {
          top: 20,
          right: 0,
          bottom: 0,
          left: 0,
        },
      },
      stroke: {
        show: true,
        curve: "smooth",
        width: 2,
      },
    };

    var chart = new ApexCharts(
      document.querySelector("#apex-Product-Report"),
      options
    );
    chart.render();
    function generateDayWiseTimeSeries(baseval, count, yrange) {
      var i = 0;
      var series = [];
      while (i < count) {
        var x = baseval;
        var y =
          Math.floor(Math.random() * (yrange.max - yrange.min + 1)) +
          yrange.min;

        series.push([x, y]);
        baseval += 86400000;
        i++;
      }
      return series;
    }
  });

  // Total Revenue
  var apexwc17 = {
    series: [67],
    chart: {
      height: 200,
      type: "radialBar",
      offsetY: -10,
    },
    plotOptions: {
      radialBar: {
        startAngle: -135,
        endAngle: 135,
        dataLabels: {
          name: {
            fontSize: "14px",
            color: undefined,
          },
          value: {
            offsetY: 76,
            fontSize: "22px",
            color: undefined,
            formatter: function (val) {
              return val + "%";
            },
          },
        },
      },
    },
    fill: {
      type: "gradient",
      gradient: {
        gradientToColors: ["var(--chart-color5)", "var(--chart-color4)"],
        opacityFrom: 0.9,
        opacityTo: 0.3,
        stops: [0, 100],
      },
    },
    colors: ["var(--chart-color5)", "var(--chart-color4)"],
    stroke: {
      dashArray: 4,
    },
    labels: ["Total Revenue"],
  };
  new ApexCharts(
    document.querySelector("#apex-Total-Revenue"),
    apexwc17
  ).render();

  // Number of Cases
  $(document).ready(function () {
    var options = {
      chart: {
        height: 100,
        type: "bar",
        sparkline: {
          enabled: true,
        },
      },
      colors: ["var(--chart-color1)"],
      plotOptions: {
        bar: {
          horizontal: false,
        },
      },
      dataLabels: {
        enabled: false,
      },
      stroke: {
        show: false,
      },
      series: [
        {
          name: "CONFIRMED",
          data: [44, 55, 57, 56, 61, 58, 63, 60, 66,44, 55, 57, 56, 61, 58, 63, 60],
        },
      ],
      yaxis: {
        title: {
          text: "Satisfaction Rate",
        },
      },
      fill: {
        opacity: 1,
      },
      tooltip: {
        y: {
          formatter: function (val) {
            //return "$ " + val + " thousands"
          },
        },
      },
    };

    var chart = new ApexCharts(
      document.querySelector("#apex-Satisfaction-Rate"),
      options
    );
    chart.render();
  });
});
