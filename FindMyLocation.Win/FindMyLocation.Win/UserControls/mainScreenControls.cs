using FindMyLocation.Domain.Entities;
using FindMyLocation.Win.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindMyLocation.Win.UserControls
{
    public partial class mainScreenControls : UserControl
    {
        public mainScreenControls()
        {
            InitializeComponent();
        }

        private async void searchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                await Get();
            }
            catch (Exception aa)
            {

                throw;
            }
        }
        public async Task Get()
        {
            MainScreenCode codeM = new MainScreenCode();
            resultTxtArea.Clear();
            var searchText = locNameTxt.Text.ToString();
            string lon = longTxt.Text;
            string lat = latTxt.Text;
            string count = countTxt.Text;
            count = string.IsNullOrEmpty(count) ? "5" : countTxt.Text;

            string imageText = "";
            //https://localhost:44356/api/FourSqaureApi/GetByAll?locationName=Germany&lat=52.3664&lon=9.7369&count=5
            var r=await codeM.GetResults("Germany", "52.3664", "9.7369", count);
            //var r = await codeM.GetByName(searchText, count);

            bool imgFound = false;
            int index = 0;
            List<ImageModel> images = new();
            while (!imgFound)
            {
                foreach (var item in r)
                {
                    var tt = await codeM.GetImages(item.results[index].fsq_id);
                    if (tt.Any())
                    {
                        foreach (var itemPic in tt)
                        {
                            string preSuf =itemPic.suffix;
                            if (!images.Contains(itemPic))
                            {
                                images.Add(itemPic);
                                imgFound = true;
                                break;
                            }
                            
                        }
                        //break;
                    }
                    index++;
                }
            }

            foreach (var item in r)
            {
                imageText = item.results[0].name;
                break;
            }

            DataTable tbl = new DataTable();
            //tbl.
            tbl.Columns.Add("Name");
            tbl.Columns.Add("Latitude");
            tbl.Columns.Add("Longitude");
            tbl.Columns.Add("Address");
            tbl.Columns.Add("Locality");
            tbl.Columns.Add("Country");
            tbl.Columns.Add("Region");
            tbl.Columns.Add("TimeZone");
            tbl.Columns.Add("Image");
            foreach (var item in r)
            {
                
            }
            string UrlSS = "";
            UrlSS = images[0].prefix + "original" + images[0].suffix;
            pictureBox1.Load(UrlSS);
            imgLblTxt.Text = imageText;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }
    }
}
