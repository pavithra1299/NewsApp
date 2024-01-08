using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Wishlist.Models;
using Wishlist.Service;
using Wishlist.Repository;
using Wishlist.Exceptions;
using Xunit;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Test.InfraSetup;

namespace Test.Repository
{
   public class WishlistRepositoryTest : IClassFixture<WishlistDbFixture>
    {
        private readonly IWishlistRepository repository;

        public WishlistRepositoryTest(WishlistDbFixture _fixture)
        {
            repository = new WishlistRepository(_fixture.context);
        }

        [Fact]
        public void AddWishlistShouldReturnWishlist()
        {
            Wish item = new Wish { Title= "Drug to extend dogs' lives closer to FDA approval - Axios",Author= "Axios",Description = null,Url = "https://www.axios.com/2023/11/29/dogs-drug-extend-longevity",UrlToImage=null,UserName="Pavithra12" };

            var actual = repository.AddToWishlist(item);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Wish>(actual);
        }

        [Fact]
        public void DeleteWishlistShouldReturnTrue()
        {
            var UserName = "Pavithra12";
            var Author = "Axios";

            var actual = repository.DeleteFromWishlist(Author, UserName);

            Assert.True(actual);
        }

        [Fact]
        public void DeleteWishlistShouldReturnFalse()
        {
            var UserName = "Pavithra12";
            var Author = "Axios";

            var actual = repository.DeleteFromWishlist(Author, UserName);

            Assert.False(actual);
        }

       

        [Fact]
        public void GetAllWishlistShouldReturnList()
        {
            var UserName = "Pavithra12";

            var actual = repository.GetAllWishlists(UserName);

            Assert.IsAssignableFrom<List<Wish>>(actual);
            Assert.NotEmpty(actual);
        }

       

        [Fact]
        public void SearchWishlistShouldReturnNull()
        {
            var UserName = "John";
            var WishlistId = "6574c1d9f61e5957f3d83031";

            var actual = repository.SearchWishlist(WishlistId, UserName);

            Assert.Null(actual);
        }

        [Fact]
        public void GetAllWishlistShouldReturnEmptyList()
        {
            var UserName = "Steve";

            var actual = repository.GetAllWishlists(UserName);

            Assert.IsAssignableFrom<List<Wish>>(actual);
            Assert.Empty(actual);
        }

    }
}
