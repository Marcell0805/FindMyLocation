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
        //The task that will search the results
        public async Task Get()
        {
            var searchText = locNameTxt.Text.ToString();
            string lon = longTxt.Text;
            string lat = latTxt.Text;
            string count = countTxt.Text;
            if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lon) || !string.IsNullOrEmpty(searchText))
            {
                MainScreenCode codeM = new MainScreenCode();
                resultTxtArea.Clear();
                
                count = string.IsNullOrEmpty(count) ? "5" : countTxt.Text;
                IEnumerable<ModelFour> result = null;

                if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lon) && !string.IsNullOrEmpty(searchText))
                {
                    result = await codeM.GetResults(searchText, lon, lat, count);
                }
                else if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lon))
                {
                    result = await codeM.GetByGeo(lon, lat, count);
                }
                else if (!string.IsNullOrEmpty(searchText))
                {
                    result = await codeM.GetByName(searchText, count);
                }
                string imageText = "";
                ////https://localhost:44356/api/FourSqaureApi/GetByAll?locationName=Germany&lat=52.3664&lon=9.7369&count=5
                //var r=await codeM.GetResults("Germany", "52.3664", "9.7369", count);
                //var r = await codeM.GetByName(searchText, count);

                bool imgFound = false;
                int index = 0;
                List<ImageModel> images = new();
                while (!imgFound)
                {
                    if(index==result.Count())
                    {
                        break;
                    }
                    foreach (var item in result)
                    {
                        var tt = await codeM.GetImages(item.results[index].fsq_id);
                        if (tt.Any())
                        {
                            foreach (var itemPic in tt)
                            {
                                string preSuf = itemPic.suffix;
                                if (!images.Contains(itemPic))
                                {
                                    images.Add(itemPic);
                                    imgFound = true;
                                    break;
                                }
                            }
                        }
                        index++;
                    }
                }
                int imgN = 0;
                foreach (var item in result)
                {
                    Random r = new Random();
                    int ranImg = r.Next(result.Count());
                    imageText = item.results[ranImg].name;
                    imgN = ranImg;
                    break;
                }
                if(imgFound)
                {
                    string UrlSS = "";
                    UrlSS = images[imgN].prefix + "original" + images[imgN].suffix;
                    pictureBox1.Load(UrlSS);
                    imgLblTxt.Text = imageText;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                int line = 0;
                //This part populkates the text area with the results
                foreach (var photo in result)
                {
                    string a = $"{line+1} - Category: {photo.results[line].categories[0].name} \n- Geo Code: {photo.results[line].geocodes.main.longitude} lon {photo.results[line].geocodes.main.latitude}\n " +
                        $"-Location: {photo.results[line].location.address} {photo.results[line].location.postcode} {photo.results[line].location.post_town} {photo.results[line].name}\n";
                    resultTxtArea.Text += a;
                    line++;

                }
            }
            else
            {
                MessageBox.Show("Please Enter Search Data", "Requires data");
            }
        }
    }
}
