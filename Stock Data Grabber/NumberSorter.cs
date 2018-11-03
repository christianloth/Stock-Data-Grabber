using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Stock_Data_Grabber {
    class NumberSorter {
        /*static void Main(string[] args) {
            double[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9.1, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            int n = arr.Length;
            int x = 9;
            printKclosest(arr, x, 10, n);
            Console.ReadLine();
        }*/

        List<double> outputStrikes = new List<double>();
        private double stockPrice;
        private double[] strikeValsInput;

        public NumberSorter(double stockPrice, double[] strikeVals) {
            this.stockPrice = stockPrice;
            strikeValsInput = strikeVals;
            int n = Main.strikesCall.Count();
            printKclosest(strikeVals, stockPrice, 10, n);

            string s = "";
            foreach (double d in this.outputStrikes) {
                s += d + " ";
            }
            MessageBox.Show(s);

            for (int i = 0; i < strikeValsInput.Count(); i++) {
                if (!(outputStrikes.Contains(strikeVals[i]))) {
                    Main.lastTradeDateCall.RemoveAt(i);
                    Main.strikesCall.RemoveAt(i);
                    Main.lastPricesCall.RemoveAt(i);
                    Main.bidsCall.RemoveAt(i);
                    Main.asksCall.RemoveAt(i);
                }
            }
        }

        // This function prints k closest elements 
        // to x in arr[]. n is the number of 
        // elements in arr[]
        private void printKclosest(double[] arr, double x, int k, int n) {
            //change arr[] to HtmlNode
            //if the strikes value of html node meets all conditions, update all 'updated' variables to corrosponding values. Make sure that all of them have same index updated.

            // Find the crossover point
            int l = findCrossOver(arr, 0, n - 1, x);

            // Right index to search
            int r = l + 1;

            // To keep track of count of elements
            int count = 0;

            // If x is present in arr[], then reduce 
            // left index Assumption: all elements in
            // arr[] are distinct
            if (arr[l] == x)
                l--;

            // Compare elements on left and right of 
            // crossover point to find the k closest
            // elements
            while (l >= 0 && r < n && count < k) {
                if (x - arr[l] < arr[r] - x)
                    //Console.Write(arr[l--] + " ");
                    outputStrikes.Add(arr[l--]);
                else
                    //Console.Write(arr[r++] + " ");
                    outputStrikes.Add(arr[r++]);
                count++;
            }

            // If there are no more elements on right 
            // side, then print left elements
            while (count < k && l >= 0) {
                //Console.Write(arr[l--] + " ");
                outputStrikes.Add(arr[l--]);
                count++;
            }

            // If there are no more elements on left 
            // side, then print right elements
            while (count < k && r < n) {
                //Console.Write(arr[r++] + " ");
                outputStrikes.Add(arr[r++]);
                count++;
            }
        }
        private int findCrossOver(double[] arr, int low, int high, double x) {
            // Base cases
            // x is greater than all
            if (arr[high] <= x)
                return high;

            // x is smaller than all
            if (arr[low] > x)
                return low;

            // Find the middle point
            /* low + (high - low)/2 */
            int mid = (low + high) / 2;

            /* If x is same as middle element, then
            return mid */
            if (arr[mid] <= x && arr[mid + 1] > x)
                return mid;

            /* If x is greater than arr[mid], then 
            either arr[mid + 1] is ceiling of x or
            ceiling lies in arr[mid+1...high] */
            if (arr[mid] < x)
                return findCrossOver(arr, mid + 1,
                                          high, x);

            return findCrossOver(arr, low, mid - 1, x);
        }
    }
}
