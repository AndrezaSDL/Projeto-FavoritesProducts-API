using Autofac.Extras.FakeItEasy;
using Bogus;
using FakeItEasy;
using FavoritesProductsAPI.Controllers;
using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using FavoritesProductsAPI.Services.Contracts;
using FavoritesProductsAPI.Unit.Tests.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace FavoritesProductsAPI.Unit.Test.Specs.Controllers
{
    public class FavoritesProductsControllerTest : BaseTests
    {
        protected IFavoriteProductService _service;
        protected FavoritesProductsController _controller;

        public FavoritesProductsControllerTest()
        {
            _service = AutoFake.Resolve<IFavoriteProductService>();
            _controller = AutoFake.Resolve<FavoritesProductsController>();
        }

        public class Get : FavoritesProductsControllerTest
        {
            [Fact]
            public void Get_WhenCalled_ReturnsOk()
            {
                var favoriteProductExpect = new FavoriteProductResponseDtoBuilder().Generate(3);

                A.CallTo(() => _service.Get(A<Parameters>.Ignored))
                    .Returns(favoriteProductExpect);

                var okResult = _controller.Get(new Parameters { Page = 1, Size = 2 }).Result;

                Assert.IsType<OkObjectResult>(okResult.Result);
            }

            [Fact]
            public void Get_WhenCalled_ReturnsNotFound()
            {
                A.CallTo(() => _service.Get(A<Parameters>.Ignored))
                   .Returns(Task.FromResult<IEnumerable<FavoriteProductResponseDto>>(null));

                var okResult = _controller.Get(new Parameters { Page = 1, Size = 2 }).Result;

                Assert.IsType<NotFoundResult>(okResult.Result);
            }
        }

        public class GetFavoriteProduct : FavoritesProductsControllerTest
        {
            [Fact]
            public void GetFavoriteProduct_WhenCalled_ReturnsOk()
            {
                var favoriteProductExpect = new FavoriteProductResponseDtoBuilder().Generate();

                A.CallTo(() => _service.GetByClientId(A<int>.Ignored))
                    .Returns(favoriteProductExpect);

                var okResult = _controller.GetFavoriteProduct(favoriteProductExpect.Client.Id).Result;

                Assert.IsType<OkObjectResult>(okResult.Result);
            }

            [Fact]
            public void GetFavoriteProduct_WhenCalled_ReturnsNotFound()
            {
                int clientIdExpect = Faker.Random.Int(1);

                A.CallTo(() => _service.GetByClientId(A<int>.Ignored))
                   .Returns(Task.FromResult<FavoriteProductResponseDto>(null));

                var okResult = _controller.GetFavoriteProduct(clientIdExpect).Result;

                Assert.IsType<NotFoundResult>(okResult.Result);
            }
        }

        public class Post : FavoritesProductsControllerTest
        {
            [Fact]
            public void Post_WhenCalled_ReturnsCreated()
            {
                var favoriteExpect = new FavoriteProductBuilder().Generate();

                A.CallTo(() => _service.GetFavoriteProduct(A<FavoriteProduct>.Ignored))
                    .Returns(null);

                var okResult = _controller.Post(favoriteExpect).Result;

                Assert.IsType<CreatedResult>(okResult.Result);
            }

            [Fact]
            public void Post_WhenCalled_ReturnsUnprocessableEntityObjectResult()
            {
                var param = new FavoriteProductBuilder().Generate();

                A.CallTo(() => _service.Save(A<FavoriteProduct>.Ignored))
                    .Returns(Task.FromResult<FavoriteProductResponseDto>(null));

                var okResult = _controller.Post(param).Result;

                Assert.IsType<UnprocessableEntityObjectResult>(okResult.Result);
            }

            [Fact]
            public void Post_InvalidObjectPassed_ReturnsBadRequest()
            {
                var param = new FavoriteProductBuilder().Generate();
                _controller.ModelState.AddModelError("Name", "Required");

                var actionResult = _controller.Post(param).Result as ActionResult<FavoriteProduct>;

                BadRequestObjectResult badResponse = actionResult.Result as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(badResponse);
            }
        }

        public class Delete : FavoritesProductsControllerTest
        {
            [Fact]
            public void Delete_NotExistingIdPassed_ReturnsNotFoundResponse()
            {
                int clientIdParam = Faker.Random.Int(1);
                var favoriteProductExpect = new FavoriteProductBuilder().Generate();

                A.CallTo(() => _service.Get(A<Parameters>.Ignored))
                    .Returns(Task.FromResult<IEnumerable<FavoriteProductResponseDto>>(null));

                var okResult = _controller.Delete(null).Result as NotFoundResult;

                Assert.IsType<NotFoundResult>(okResult);
            }

            [Fact]
            public void Delete_ExistingIdPassed_ReturnsNoContent()
            {
                var favoritesProducts = new FavoriteProductResponseDtoBuilder().Generate(3);
                var favoritesRequest = new FavoriteProduct() { 
                    ClientId = favoritesProducts.FirstOrDefault().Client.Id,
                    ProductId = favoritesProducts.FirstOrDefault()
                    .FavoritesProducts.FirstOrDefault().Id
                };

                A.CallTo(() => _service.Get(A<Parameters>.Ignored))
                   .Returns(Task.FromResult<IEnumerable<FavoriteProductResponseDto>>(favoritesProducts));

                NoContentResult noContent = _controller.Delete(favoritesRequest).Result as NoContentResult;

                Assert.IsType<NoContentResult>(noContent);
            }
        }
    }
}
