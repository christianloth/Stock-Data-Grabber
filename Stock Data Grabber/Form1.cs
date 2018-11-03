using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Stock_Data_Grabber {
    public partial class Main : Form {
        DataTable table;
        HtmlWeb web;

        public static List<HtmlNode> lastTradeDateCall;
        public static List<HtmlNode> strikesCall;
        public static List<HtmlNode> lastPricesCall;
        public static List<HtmlNode> bidsCall;
        public static List<HtmlNode> asksCall;

        public static List<HtmlNode> lastTradeDatePut;
        public static List<HtmlNode> strikesPut;
        public static List<HtmlNode> lastPricesPut;
        public static List<HtmlNode> bidsPut;
        public static List<HtmlNode> asksPut;

        private string[] stockCodes;

        public static int QUANTITY;

        public Main() {
            InitializeComponent();
            initTable();
        }
        private void Form1_Load(object sender, EventArgs e) {
            numStatments.Text = "10";
            export.Enabled = false;
        }
        private void initTable() {
            table = new DataTable("Stocks");
            table.Columns.Add("Call / Put", typeof(string));
            table.Columns.Add("Stock Symbol", typeof(string));
            table.Columns.Add("Date and Time", typeof(string));
            table.Columns.Add("Stock Price", typeof(double));
            table.Columns.Add("Last Trade Date", typeof(string));
            table.Columns.Add("Strike", typeof(double));
            table.Columns.Add("Last Price", typeof(double));
            table.Columns.Add("Bid", typeof(double));
            table.Columns.Add("Ask", typeof(double));
            dgv.DataSource = table;
        }

        private async void Refresh_Click(object sender, EventArgs e) {
            Int32.TryParse(numStatments.Text, out QUANTITY);
            // add FormatException
            if (QUANTITY <= 0) {
                MessageBox.Show("Please enter a positive number.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (int i = table.Rows.Count - 1; i >= 0; i--) {
                DataRow dr = table.Rows[i];
                dr.Delete();
                
            }

            lastTradeDateCall = new List<HtmlNode>();
            strikesCall = new List<HtmlNode>();
            lastPricesCall = new List<HtmlNode>();
            bidsCall = new List<HtmlNode>();
            asksCall = new List<HtmlNode>();

            lastTradeDatePut = new List<HtmlNode>();
            strikesPut = new List<HtmlNode>();
            lastPricesPut = new List<HtmlNode>();
            bidsPut = new List<HtmlNode>();
            asksPut = new List<HtmlNode>();

            try {
                stockCodes = File.ReadAllLines(@"stocks.txt");
            } catch (FileNotFoundException) {
                MessageBox.Show("Please ensure that your stocks.txt file is in your files directory.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ii = 0;
            foreach (string s in stockCodes) {
                try {
                    if (s == "" || s == null)
                        continue;
                    await Data(s);
                } catch (NullReferenceException) {
                    //MessageBox.Show("Please ensure that all of your stock codes in stocks.txt are correct.",
                    // "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ii++;
            }

            export.Enabled = true;
        }
        private async Task Data(String stockCode) {

            web = new HtmlWeb();

            string url = "https://finance.yahoo.com/quote/" + stockCode.ToUpper() + "/options?p=#4dasda" + stockCode.ToUpper();

            HtmlAgilityPack.HtmlDocument doc = null;
            try {
                doc = await Task.Factory.StartNew(() => web.Load(url));
            } catch (WebException) {
                MessageBox.Show("Please check your internet connection. You may not have connection or your IP may have made too many recent requests.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lastTradeDateCall.Clear();
            strikesCall.Clear();
            lastPricesCall.Clear();
            bidsCall.Clear();
            asksCall.Clear();

            lastTradeDatePut.Clear();
            strikesPut.Clear();
            lastPricesPut.Clear();
            bidsPut.Clear();
            asksPut.Clear();

            string dateTime = doc.DocumentNode.SelectSingleNode("//*[@id='quote-market-notice']/span").InnerHtml;
            double stockPrice = Convert.ToDouble(doc.DocumentNode.SelectSingleNode("//*[@id='quote-header-info']/div[3]/div[1]/div/span[1]").InnerHtml);

            double[] strikesCallsDouble = new double[QUANTITY];
            double[] strikesPutsDouble = new double[QUANTITY];


            // Get nodes of different columns
            for (int i = 0; i < QUANTITY; i++) {

                int c = 1 + i;
                lastTradeDateCall.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[1]/div[2]/div/table/tbody/tr[" + c + "]/td[2]"));
                strikesCall.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[1]/div[2]/div/table/tbody/tr[" + c + "]/td[3]/a"));
                lastPricesCall.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[1]/div[2]/div/table/tbody/tr[" + c + "]/td[4]"));
                bidsCall.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[1]/div[2]/div/table/tbody/tr[" + c + "]/td[5]"));
                asksCall.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[1]/div[2]/div/table/tbody/tr[" + c + "]/td[6]"));

                lastTradeDatePut.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[2]/div[2]/div/table/tbody/tr[" + c + "]/td[2]"));
                strikesPut.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[2]/div[2]/div/table/tbody/tr[" + c + "]/td[3]/a"));
                lastPricesPut.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[2]/div[2]/div/table/tbody/tr[" + c + "]/td[4]"));
                bidsPut.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[2]/div[2]/div/table/tbody/tr[" + c + "]/td[5]"));
                asksPut.Add(doc.DocumentNode.SelectSingleNode(
                "//*[@id='Col1-1-OptionContracts-Proxy']/section/section[2]/div[2]/div/table/tbody/tr[" + c + "]/td[6]"));
            }

            //for (int i = 0; i < QUANTITY; i++) {
            //    strikesCallsDouble[i] = Convert.ToDouble(strikesCall[i].InnerHtml);
            //}

            //for (int i = 0; i < QUANTITY; i++) {
            //    strikesPutsDouble[i] = Convert.ToDouble(strikesPut[i].InnerHtml);
            //}

            //Calls
            //NumberSorter ns = new NumberSorter(72, strikesCallsDouble);
            //return;

            table.Rows.Add();
            // output calls data to table
            for (int i = 0; i < QUANTITY; i++) {
                table.Rows.Add("Call", stockCode.ToUpper(), dateTime, stockPrice, lastTradeDateCall[i].InnerHtml, Convert.ToDouble(strikesCall[i].InnerHtml),
                    Convert.ToDouble(lastPricesCall[i].InnerHtml), Convert.ToDouble(bidsCall[i].InnerHtml), Convert.ToDouble(asksCall[i].InnerHtml));
            }

            table.Rows.Add();

            // output puts data to table
            for (int i = 0; i < QUANTITY; i++) {
                table.Rows.Add("Put", stockCode.ToUpper(), dateTime, stockPrice, lastTradeDateCall[i].InnerHtml, Convert.ToDouble(strikesPut[i].InnerHtml),
                    Convert.ToDouble(lastPricesPut[i].InnerHtml), Convert.ToDouble(bidsPut[i].InnerHtml), Convert.ToDouble(asksPut[i].InnerHtml));
            }

        }
        // export data to excel
        private void exportData() {
            var lines = new List<string>();

            string[] columnNames = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();

            var header = string.Join(",", columnNames);
            lines.Add(header);

            var valueLines = table.AsEnumerable().Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);

            File.WriteAllLines("Stock Data.csv", lines);
        }
        public static void RemoveAt<T>(ref T[] arr, int index) {
            for (int a = index; a < arr.Length - 1; a++) {
                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];
            }
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);
        }

        private void Export_Click(object sender, EventArgs e) {
            try {
                exportData();
                MessageBox.Show("File successfully exported as: Stock Data.csv");
            } catch {
                MessageBox.Show("File could not be exported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}