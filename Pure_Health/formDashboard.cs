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
        private Timer realTimeUpdateTimer;
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
            UpdateLabels(); // Initial load
            updateTimer.Start(); // Start the update timer for pie chart and general updates

            // Set up the timer for Cartesian chart updates
            Timer chartUpdateTimer = new Timer();
            chartUpdateTimer.Interval = 5000; // Update every 5 seconds
            chartUpdateTimer.Tick += ChartUpdateTimer_Tick;
            chartUpdateTimer.Start();
            
            

            // Set up the Timer for periodic updates
            realTimeUpdateTimer = new Timer();
            realTimeUpdateTimer.Interval = 5000; // Refresh every 5 seconds
            realTimeUpdateTimer.Tick += RealTimeUpdateTimer_Tick;
            realTimeUpdateTimer.Start();

            // Initial data load
            UpdateChart();
            LoadDataAndUpdateChart();
        }
        private void UpdateLabels()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch total gross from dbo.Table_6
                    string grossQuery = "SELECT SUM(GROSS) AS GROSS FROM dbo.Table_6";
                    SqlCommand grossCommand = new SqlCommand(grossQuery, connection);
                    object grossResult = grossCommand.ExecuteScalar();
                    label6.Text = grossResult != DBNull.Value ? Convert.ToDecimal(grossResult).ToString("N2") : "0.00";

                    // Fetch total number of Patient Names from dbo.Table_1
                    string patientTable1Query = "SELECT COUNT([Patient Name]) AS [Patient Name] FROM dbo.Table_1";
                    SqlCommand patientTable1Command = new SqlCommand(patientTable1Query, connection);
                    object patientTable1Result = patientTable1Command.ExecuteScalar();
                    label7.Text = patientTable1Result != DBNull.Value ? Convert.ToInt32(patientTable1Result).ToString() : "0";

                    // Fetch total number of Patient Names from dbo.Table_4
                    string patientTable4Query = "SELECT COUNT([Patient Name]) AS [Patient Name] FROM dbo.Table_4";
                    SqlCommand patientTable4Command = new SqlCommand(patientTable4Query, connection);
                    object patientTable4Result = patientTable4Command.ExecuteScalar();
                    label8.Text = patientTable4Result != DBNull.Value ? Convert.ToInt32(patientTable4Result).ToString() : "0";

                    // Fetch total number of Employees from Employees Name column in dbo.Table_3
                    string employeeQuery = "SELECT COUNT([Employees Name]) AS TotalEmployees FROM dbo.Table_3";
                    SqlCommand employeeCommand = new SqlCommand(employeeQuery, connection);
                    object employeeResult = employeeCommand.ExecuteScalar();
                    label9.Text = employeeResult != DBNull.Value ? Convert.ToInt32(employeeResult).ToString() : "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating labels: {ex.Message}");
            }
        }


        private void RealTimeUpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateLabels(); // Refresh the labels periodically
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
            // Check if the cartesian chart has no series
            if (cartesianChart1.Series.Count == 0)
            {
                // Create a new LineSeries if none exists
                var lineSeries = new LineSeries
                {
                    Title = "Profit",
                    Values = new ChartValues<ObservablePoint>(),
                    LineSmoothness = 0, // Straight lines
                    PointGeometrySize = 10, // Point size
                    PointGeometry = DefaultGeometries.Circle // Circle points
                };

                cartesianChart1.Series.Add(lineSeries);

                // Configure the X-axis (date axis)
                cartesianChart1.AxisX.Clear();
                cartesianChart1.AxisX.Add(new Axis
                {
                    Title = "Date",
                    LabelFormatter = value => new DateTime((long)value).ToString("MM/dd/yyyy"),
                    MinValue = DateTime.Now.AddDays(-30).Ticks, // Example: last 30 days
                    MaxValue = DateTime.Now.Ticks              // Current date
                });

                // Configure the Y-axis (profit axis)
                cartesianChart1.AxisY.Clear();
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "GROSS",
                    LabelFormatter = value => value.ToString("C"), // Format as currency
                    MinValue = 0 // Ensure Y-axis starts at 0
                });
            }

            // Update the existing LineSeries with new data
            var lineSeriesExisting = cartesianChart1.Series[0] as LineSeries;

            if (lineSeriesExisting != null)
            {
                lineSeriesExisting.Values.Clear(); // Clear existing values

                foreach (DataRow row in data.Rows)
                {
                    // Check for valid data before adding to the chart
                    if (row["Date"] == DBNull.Value || row["GROSS"] == DBNull.Value)
                        continue;

                    DateTime date = Convert.ToDateTime(row["Date"]);
                    decimal profit = Convert.ToDecimal(row["GROSS"]);

                    // Add data to the series
                    lineSeriesExisting.Values.Add(new ObservablePoint(date.Ticks, (double)profit));
                }

                // Handle cases where no valid data was added
                if (lineSeriesExisting.Values.Count == 0)
                {
                    MessageBox.Show("No valid data available to display in the chart.");
                }
            }
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

        private void cartesianChart1_ChildChanged_1(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
    





