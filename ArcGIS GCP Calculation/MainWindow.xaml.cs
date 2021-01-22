using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArcGIS_GCP_Calculation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Filepaths for
        public string 
            TopLeftStart = string.Empty, 
            TopRightStart = string.Empty, 
            BottomLeftStart = string.Empty, 
            BottomRightStart = string.Empty;

        public int MapScopeX = 1100;
        public int MapScopeY = 1100;
        public readonly string FileNameExport = "ControlPoints_";

        private void Calculate(object sender, RoutedEventArgs e)
        {
            if(TopLeftStart != string.Empty &&
                TopRightStart != string.Empty &&
                BottomLeftStart != string.Empty &&
                BottomRightStart != string.Empty)
            {
                DoSomeCalculation();
            }
            else
            {
                 MessageBox.Show("Please add a file for each corner.");
            }
        }

        public void DoSomeCalculation()
        {
            MapObject topLeft = ExtractData(TopLeftStart),
                bottomLeft = ExtractData(BottomLeftStart),
                bottomRight = ExtractData(BottomRightStart),
                topRight = ExtractData(TopRightStart);

            int width = (int)Math.Abs((topRight.TopLeftX - topLeft.TopLeftX)) / MapScopeX ;
            int height = (int)Math.Abs((topLeft.TopLeftY - bottomLeft.TopLeftY)) / MapScopeY ;

            MapObject[,] DoomArray = new MapObject[width+1,height+1];
            DoomArray[0, 0] = bottomLeft;
            DoomArray[0, height] = topLeft;
            DoomArray[width, 0] = bottomRight;
            DoomArray[width, height] = topRight;


            for (int x = 0; x <= width; x++)
            {
                for(int y = 0; y<= height; y++)
                {
                    if(DoomArray[x, y] == null)
                    {
                    DoomArray[x, y] = DoomArray[0,0].ReturnCopyOfMe();
                    DoomArray[x, y].ApplyXModifier(MapScopeX * x);
                    DoomArray[x, y].ApplyYModifier(MapScopeY * y);
                    }
                }

            }






            //Dump in FIles
            int count = 1;
            Directory.CreateDirectory("Exports");
            var filepaths = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            for (int y = height; y >=0 ; y--)
            {
                for (int x = 0; x <= width; x++)
                {
                    if (DoomArray[x, y] !=null)
                    {
                        var filename = filepaths+"/Exports/" +FileNameExport + count.ToString("0000")+".txt";
                        File.WriteAllText(filename, DoomArray[x, y].DumpToFile());
                        count++;
                    }
                }

            }
        }

        public MapObject ExtractData(string filePath)
        {
            var ReturnObject = new MapObject();
            using (var reader = new StreamReader(filePath))
            {
                var coords = reader.ReadLine().Split('\t');
                ReturnObject.PxTopLeftX = double.Parse(coords[0], CultureInfo.InvariantCulture);
                ReturnObject.PxTopLeftY = double.Parse(coords[1], CultureInfo.InvariantCulture);
                ReturnObject.TopLeftX = double.Parse(coords[2], CultureInfo.InvariantCulture);
                ReturnObject.TopLeftY = double.Parse(coords[3], CultureInfo.InvariantCulture);

                coords = reader.ReadLine().Split('\t');
                ReturnObject.PxBottomLeftX = double.Parse(coords[0], CultureInfo.InvariantCulture);
                ReturnObject.PxBottomLeftY = double.Parse(coords[1], CultureInfo.InvariantCulture);
                ReturnObject.BottomLeftX = double.Parse(coords[2], CultureInfo.InvariantCulture);
                ReturnObject.BottomLeftY = double.Parse(coords[3], CultureInfo.InvariantCulture);

                coords = reader.ReadLine().Split('\t');
                ReturnObject.PxBottomRightX = double.Parse(coords[0], CultureInfo.InvariantCulture);
                ReturnObject.PxBottomRightY = double.Parse(coords[1], CultureInfo.InvariantCulture);
                ReturnObject.BottomRightX = double.Parse(coords[2], CultureInfo.InvariantCulture);
                ReturnObject.BottomRightY = double.Parse(coords[3], CultureInfo.InvariantCulture);

                coords = reader.ReadLine().Split('\t');
                ReturnObject.PxTopRightX = double.Parse(coords[0], CultureInfo.InvariantCulture);
                ReturnObject.PxTopRightY = double.Parse(coords[1], CultureInfo.InvariantCulture);
                ReturnObject.TopRightX = double.Parse(coords[2], CultureInfo.InvariantCulture);
                ReturnObject.TopRightY = double.Parse(coords[3], CultureInfo.InvariantCulture);

                coords = reader.ReadLine().Split('\t');
                ReturnObject.PxMiddleX = double.Parse(coords[0], CultureInfo.InvariantCulture);
                ReturnObject.PxMiddleY = double.Parse(coords[1], CultureInfo.InvariantCulture);
                ReturnObject.MiddleX = double.Parse(coords[2], CultureInfo.InvariantCulture);
                ReturnObject.MiddleY = double.Parse(coords[3], CultureInfo.InvariantCulture);


            }
            return ReturnObject;
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnDrag(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.
                Console.WriteLine(files[0].ToString());
                Console.WriteLine(((Rectangle)sender).Name);
                var s = ((Rectangle)sender);
                switch (s.Name)
                {
                    case "BottomLeft":
                        BottomLeftStart = files[0].ToString();
                        s.Fill = new SolidColorBrush(Color.FromRgb(127, 255, 0));
                        break;

                    case "BottomRight":
                        BottomRightStart = files[0].ToString();
                        s.Fill = new SolidColorBrush(Color.FromRgb(127, 255, 0));
                        break;

                    case "TopLeft":
                        TopLeftStart = files[0].ToString();
                        s.Fill = new SolidColorBrush(Color.FromRgb(127, 255, 0));
                        break;

                    case "TopRight":
                        TopRightStart = files[0].ToString();
                        s.Fill = new SolidColorBrush(Color.FromRgb(127, 255, 0));
                        break;
                }
            }
        }
    }
}
