using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Definitions.Charts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Pure_Health
{
    public partial class formDashboard : Form
    {
        public formDashboard()
        {
            InitializeComponent();
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            // Initialize the timer
            updateTimer = new Timer();
            updateTimer.Interval = 5000; // Refresh every 5 seconds
            updateTimer.Tick += UpdateTimer_Tick;
        }
        private Timer updateTimer;
        private (int utz, int lab, int xray, int ecg, int echo) GetTestCountsFromDatabase()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = @"
                SELECT 
                    SUM(UTZ) AS UTZ,
                    SUM(LAB) AS LAB,
                    SUM(XRAY) AS XRAY,
                    SUM(ECG) AS ECG,
                    SUM(ECHO) AS ECHO
                FROM dbo.Table_6";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandTimeout = 60;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int utz = reader["UTZ"] != DBNull.Value ? Convert.ToInt32(reader["UTZ"]) : 0;
                                int lab = reader["LAB"] != DBNull.Value ? Convert.ToInt32(reader["LAB"]) : 0;
                                int xray = reader["XRAY"] != DBNull.Value ? Convert.ToInt32(reader["XRAY"]) : 0;
                                int ecg = reader["ECG"] != DBNull.Value ? Convert.ToInt32(reader["ECG"]) : 0;
                                int echo = reader["ECHO"] != DBNull.Value ? Convert.ToInt32(reader["ECHO"]) : 0;

                                return (utz, lab, xray, ecg, echo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching test counts: {ex.Message}");
            }

            return (0, 0, 0, 0, 0);
        }


        private void formDashboard_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            updateTimer.Start();

            // Perform an initial data load
            UpdateChart();
            // Get real test counts from the database
            var (utzCount, labCount, xrayCount, ecgCount, echoCount) = GetTestCountsFromDatabase();

            // Load the chart with the actual data
            LoadTestDistributionChart(utzCount, labCount, xrayCount, ecgCount, echoCount);
            LoadDataAndUpdateChart();

            cartesianChart1 = new LiveCharts.WinForms.CartesianChart();

            // Customize chart properties if needed (optional)
            // Set up the timer to update the chart every 10 seconds (10000 milliseconds)
            Timer chartUpdateTimer = new Timer();
            chartUpdateTimer.Interval = 10000; // 10 seconds
            chartUpdateTimer.Tick += ChartUpdateTimer_Tick;
            chartUpdateTimer.Start(); // Start the timer

        }


        private void ChartUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Load new data and update the chart automatically
            LoadDataAndUpdateChart();
        }


        private void LoadDataAndUpdateChart()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT Date, GROSS FROM dbo.Table_6"; // Your query to fetch Date and Profit

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Pass the loaded data to the chart update method
                UpdateChart1(dataTable);
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Refresh the chart data on every timer tick
            UpdateChart();
            LoadDataAndUpdateChart();

        }


        // In formDashboard, method to update the chart
        public void UpdateChart1(DataTable data)
        {
            // Clear existing series from the chart
            cartesianChart1.Series.Clear();

            // Create a new LineSeries for "Profit"
            var lineSeries = new LineSeries
            {
                Title = "Profit",
                Values = new ChartValues<decimal>(), // Store the profit values
                LineSmoothness = 0, // Straight line (no smoothing)
                PointGeometrySize = 10, // Size of points on the graph
                PointGeometry = DefaultGeometries.Circle // Shape of points on the graph
            };

            // Populate the series with data from the DataTable
            foreach (DataRow row in data.Rows)
            {
                DateTime date = Convert.ToDateTime(row["Date"]);
                decimal profit = Convert.ToDecimal(row["GROSS"]);

                // Add data points (X: Date, Y: Profit)
                lineSeries.Values.Add(profit);
            }

            // Add the series to the chart
            cartesianChart1.Series.Add(lineSeries);

            // Optional: Customize the chart's axes using automatic features
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Date",
                LabelFormatter = value => new DateTime((long)value).ToString("MM/dd/yyyy") // Format X-axis as Date
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "GROSS",
                LabelFormatter = value => value.ToString("C") // Format Y-axis as currency
            });
        }


        private void UpdateChart()
        {

            try
            {
                // Get the real data from the database
                var (utz, lab, xray, ecg, echo) = GetTestCountsFromDatabase();

                // Update the chart with the new data
                LoadTestDistributionChart(utz, lab, xray, ecg, echo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the chart: {ex.Message}");
            }
        }
        private void LoadTestDistributionChart(int utz, int lab, int xray, int ecg, int echo)
        {
            // Clear existing chart data
            pieChart1.Series.Clear();

            // Add new data to the pie chart
            pieChart1.Series = new LiveCharts.SeriesCollection
    {
        new LiveCharts.Wpf.PieSeries
        {
            Title = "Ultrasound",
            Values = new LiveCharts.ChartValues<int> { utz },
            DataLabels = true, // Enable data labels
            LabelPoint = chartPoint => $"{chartPoint.Y}", // Show only the value
        },
        new LiveCharts.Wpf.PieSeries
        {
            Title = "Lab",
            Values = new LiveCharts.ChartValues<int> { lab },
            DataLabels = true,
            LabelPoint = chartPoint => $"{chartPoint.Y}",
        },
        new LiveCharts.Wpf.PieSeries
        {
            Title = "X-Ray",
            Values = new LiveCharts.ChartValues<int> { xray },
            DataLabels = true,
            LabelPoint = chartPoint => $"{chartPoint.Y}",
        },
        new LiveCharts.Wpf.PieSeries
        {
            Title = "ECG",
            Values = new LiveCharts.ChartValues<int> { ecg },
            DataLabels = true,
            LabelPoint = chartPoint => $"{chartPoint.Y}",
        },
        new LiveCharts.Wpf.PieSeries
        {
            Title = "Echo",
            Values = new LiveCharts.ChartValues<int> { echo },
            DataLabels = true,
            LabelPoint = chartPoint => $"{chartPoint.Y}",
        }
    };

            // Optionally update chart labels and layout
            pieChart1.LegendLocation = LiveCharts.LegendLocation.Right; // Place legend on the right
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
       
        
    }
}
    





