using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindMyLocation.Domain.Entities;
using FindMyLocation.Web.APIStructs;
using Microsoft.AspNetCore.Components;

namespace FindMyLocation.Web.Pages
{
    public partial class FindLocations
    {
        #region Injections
        [Inject]
        private IFourSqaureApi FourSquareApi { get; set; }
        [Inject] 
        public IFourSquareVenuApi FourSquareVenueApi { get; set; }
        #endregion
        #region models  
        public string locationSearch { get; set; }
        public string latSearch { get; set; }
        public string lonSearch { get; set; }
        public string countSearch { get; set; }

        public int index  = 0;
        
        public List<FourSqaureVenues> fourSquareVenues { get; set; } = new();
        public List<ModelFour> modelFours { get; set; } = new();
        public List<ImageModel> imageModels { get; set; } = new();
        public IEnumerable<FourSqaureVenues> fourSquareVenuesResult { get; set; }
        public IEnumerable<ImageModel> imageModelResult { get; set; }
        public IEnumerable<ModelFour> searchResult { get; set; }

        public FourSqaureVenues FourSquare=new();
        public ModelFour FourModel = new();
        bool btnVisble = true;
        
       
        #endregion
        public void counterInc()
        {
            index++;
        }
        public void counterClear()
        {
            index=0;
        }
        private async void searchResults()
        {
            await GetSearchResults();
        }
        //This method will go to the rest calls and return the results based of the scenario 
        private async Task GetSearchResults()
        {
            btnVisble = false;
            index = 0;
            StateHasChanged();
            FourModel = new();
            if (string.IsNullOrEmpty(countSearch))
                countSearch = "5";
            if (!string.IsNullOrEmpty(latSearch) && !string.IsNullOrEmpty(lonSearch) && !string.IsNullOrEmpty(locationSearch))
            {
                searchResult = await FourSquareApi.GetByAll(locationSearch, lonSearch, latSearch, countSearch);
            }
            else if (!string.IsNullOrEmpty(latSearch) && !string.IsNullOrEmpty(lonSearch))
            {
                searchResult = await FourSquareApi.GetByGeo(lonSearch, latSearch, countSearch);
            }
            else if (!string.IsNullOrEmpty(locationSearch))
            {
                searchResult = await FourSquareApi.GetByName(locationSearch, countSearch);
            }
            modelFours = searchResult.ToList();
            imageModels = new();
            //Find the images for each result
            foreach (var items in searchResult)
            {
                if (items.results != null)
                {
                    string fsq = "";
                    foreach (var item in items.results)
                    {
                        fsq = item.fsq_id;
                        imageModelResult = await FourSquareApi.GetImages(fsq);
                        foreach (var itemPic in imageModelResult)
                        {
                            if (!string.IsNullOrEmpty(itemPic.id))
                            {
                                var firstPic = imageModelResult.FirstOrDefault();
                                imageModels.Add(firstPic);
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            await SaveResults(modelFours);
            btnVisble = true;
            StateHasChanged();
        }
        //This method has been fixed to save the results
        //Updated 25/01/2022 8:08 am
        private async Task SaveResults(List<ModelFour> modelFours)
        {
            int index = 0;
            var results = await FourSquareVenueApi.GetAllResults();
            foreach (var item in modelFours)
            {                
                var t = results.ToList();
                var b = results.Where(a=>a.fsq_id== item.results[index].fsq_id);
                if(!b.Any())
                {
                    FourSqaureVenues itemToAdd = new FourSqaureVenues
                    {
                        fsq_id = item.results[index].fsq_id,
                        country = item.results[index].location.country,
                        address = item.results[index].location.address,
                        cross_street = item.results[index].location.cross_street,
                        postcode = item.results[index].location.postcode,
                        latitude = item.results[index].geocodes.main.latitude,
                        longitude = item.results[index].geocodes.main.longitude,
                        name = item.results[index].name,
                        region = item.results[index].location.region,
                        suffix = item.results[index].categories[0].icon.suffix,
                        prefix = item.results[index].categories[0].icon.prefix,
                        timezone = item.results[index].timezone
                    };

                    await FourSquareVenueApi.AddResult(itemToAdd);
                }
                index++;
            }
            
        }
    }
}
