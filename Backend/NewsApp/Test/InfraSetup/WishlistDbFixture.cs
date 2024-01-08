using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Wishlist.Models;

namespace Test.InfraSetup
{
    public class WishlistDbFixture : IDisposable
    {

        private readonly IConfigurationRoot configuration;
        public WishlistContext? context;
        public WishlistDbFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new WishlistContext(configuration);
            context.wishlists.DeleteMany(Builders<Wish>.Filter.Empty);
            context.wishlists.InsertMany(new List<Wish>
            {
                new Wish{Title="Apple to end Goldman Sachs credit card deal - The Hill",Author="Nick Robertson",Description="Apple will end its credit card program with Goldman Sachs in the next 12–15 months, multiple media outlets reported Tuesday, citing sources familiar with the deal. Apple launched the Goldman-backed card in 2019 to consumer fanfare alongside a flashy ad campai…",Url= "https://thehill.com/business/4332380-apple-to-end-goldman-sachs-credit-card-deal/",
            UrlToImage= "https://thehill.com/wp-content/uploads/sites/2/2023/11/654cd1b3f02584.68228409-e1701226014205.jpeg?w=1280", UserName="Pooja12" },
                new Wish{Author="Brian Evans",Title= "Stock futures inch higher on Tuesday night: Live updates - CNBC",Description= "The major averages notched modest gains during regular trading on Tuesday.",Url= "https://www.cnbc.com/2023/11/28/stock-market-today-live-updates.html",
            UrlToImage= "https://image.cnbcfm.com/api/v1/image/107259512-1687283761390-NYSE_Traders-OB-20230620-CC-PRESS-11.jpg?v=1687369270&w=1920&h=1080",UserName="Pavithra12" }
            });
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
