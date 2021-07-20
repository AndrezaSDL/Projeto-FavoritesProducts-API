using Autofac.Extras.FakeItEasy;
using Bogus;
using FakeItEasy;
using FavoritesProductsAPI.Controllers;
using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using FavoritesProductsAPI.Services.Contracts;
using FavoritesProductsAPI.Unit.Test;
using FavoritesProductsAPI.Unit.Tests.Builder;
using FavoritesProductsAPI.Unit.Tests.Builder.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace FavoritesProductsAPI.Unit.Test.Specs.Controllers
{
    public class ClientsControllerTest : BaseTests
    {
        protected IClientService _service;
        protected ClientsController _controller;

        public ClientsControllerTest()
        {
            _service = AutoFake.Resolve<IClientService>();
            _controller = AutoFake.Resolve<ClientsController>();
        }

        public class Get : ClientsControllerTest
        {
            [Fact]
            public void Get_WhenCalled_ReturnsOk()
            {
                var clientExpect = new ClientBuilder().Generate();

                A.CallTo(() => _service.GetAll(A<Parameters>.Ignored))
                    .Returns(new List<Client> { clientExpect });

                var okResult = _controller.Get(new Parameters { Page=1, Size=2 }).Result;

                Assert.IsType<OkObjectResult>(okResult.Result);
            }

            [Fact]
            public void Get_WhenCalled_ReturnsNotFound()
            {
                A.CallTo(() => _service.GetAll(A<Parameters>.Ignored))
                    .Returns(Task.FromResult<IEnumerable<Client>>(null));

                var okResult = _controller.Get(new Parameters { Page = 1, Size = 2 }).Result;

                Assert.IsType<NotFoundResult>(okResult.Result);
            }

            [Fact]
            public void Get_ExistingClientsExpect_ReturnsItems()
            {
                var clientsExpect = new List<Client> { new ClientBuilder().Generate() };

                A.CallTo(() => _service.GetAll(A<Parameters>.Ignored))
                    .Returns(clientsExpect);

                var actionResult = _controller.Get(new Parameters { Page = 1, Size = 2 }).Result;
                OkObjectResult okResult = actionResult.Result as OkObjectResult;

                Assert.IsType<OkObjectResult>(okResult);
                Assert.Equal(clientsExpect, okResult.Value);
            }
        }

        public class GetClient : ClientsControllerTest
        {
            [Fact]
            public void GetClient_WhenCalled_ReturnsOk()
            {
                var clientExpect = new ClientBuilder().Generate();

                A.CallTo(() => _service.Get(A<Expression<Func<Client, bool>>>.Ignored))
                    .Returns(clientExpect);

                var okResult = _controller.GetClient(clientExpect.Id).Result;

                Assert.IsType<OkObjectResult>(okResult.Result);
            }

            [Fact]
            public void GetClient_WhenCalled_ReturnsNotFound()
            {
                int clientIdExpect = Faker.Random.Int(1);

                A.CallTo(() => _service.Get(A<Expression<Func<Client, bool>>>.Ignored))
                    .Returns(Task.FromResult<Client>(null));

                var okResult = _controller.GetClient(clientIdExpect).Result;

                Assert.IsType<NotFoundResult>(okResult.Result);
            }

            [Fact]
            public void GetClient_ExistingClientExpect_ReturnsItem()
            {
                var clientExpect = new ClientBuilder().Generate();

                A.CallTo(() => _service.Get(A<Expression<Func<Client, bool>>>.Ignored))
                    .Returns(clientExpect);

                var actionResult = _controller.GetClient(clientExpect.Id).Result as ActionResult<Client>;
                OkObjectResult okResult = actionResult.Result as OkObjectResult;

                Assert.IsType<OkObjectResult>(okResult);
                Assert.Equal(clientExpect, okResult.Value);
            }
        }

        public class Post : ClientsControllerTest
        {
            [Fact]
            public void Post_WhenCalled_ReturnsCreated()
            {
                var clientDtoParam = new ClientRequestDtoBuilder().Generate();

                A.CallTo(() => _service.Get(A<Expression<Func<Client, bool>>>.Ignored))
                  .Returns(Task.FromResult<Client>(null));

                var okResult = _controller.Post(clientDtoParam).Result;

                Assert.IsType<CreatedAtActionResult>(okResult.Result);
            }

            [Fact]
            public void Post_WhenCalled_ReturnsUnprocessableEntityObjectResult()
            {
                var clientDtoParam = new ClientRequestDtoBuilder().Generate();
                var clientReturn = new Client { Email = clientDtoParam.Email };

                A.CallTo(() => _service.Get(A<Expression<Func<Client, bool>>>.Ignored))
                    .Returns(Task.FromResult<Client>(clientReturn));

                var okResult = _controller.Post(clientDtoParam).Result;

                Assert.IsType<UnprocessableEntityObjectResult>(okResult.Result);
            }

            [Fact]
            public void Post_InvalidObjectPassed_ReturnsBadRequest()
            {
                var clientDtoParam = new ClientRequestDtoBuilder().Generate();
                _controller.ModelState.AddModelError("Name", "Required");

                var actionResult = _controller.Post(clientDtoParam).Result as ActionResult<Client>;

                BadRequestObjectResult badResponse = actionResult.Result as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(badResponse);
            }
        }

        public class Put : ClientsControllerTest
        {
            [Fact]
            public void Put_WhenCalled_ReturnsNotFound()
            {
                int clientIdParam = Faker.Random.Int(1);
                var clientDtoParam = new ClientRequestDtoBuilder().Generate();

                A.CallTo(() => _service.Get(A<Expression<Func<Client, bool>>>.Ignored))
                    .Returns(Task.FromResult<Client>(null));
             
                var okResult = _controller.Put(clientIdParam, clientDtoParam).Result;

                Assert.IsType<NotFoundResult>(okResult);
            }

            [Fact]
            public void Put_InvalidObjectPassed_ReturnsBadRequest()
            {
                int clientIdParam = Faker.Random.Int(1);
                var clientDtoParam = new ClientRequestDtoBuilder().Generate();
                _controller.ModelState.AddModelError("Name", "Required");

                var actionResult = _controller.Put(clientIdParam, clientDtoParam).Result as ActionResult;

                BadRequestObjectResult badResponse = actionResult as BadRequestObjectResult;

                Assert.IsType<BadRequestObjectResult>(badResponse);
            }
        }

        public class DeleteClient : ClientsControllerTest
        {
            [Fact]
            public void Delete_NotExistingIdPassed_ReturnsNotFoundResponse()
            {
                int clientIdParam = Faker.Random.Int(1);

                A.CallTo(() => _service.Get(A<Expression<Func<Client, bool>>>.Ignored))
                    .Returns(Task.FromResult<Client>(null));

                var okResult = _controller.Delete(clientIdParam).Result as ActionResult;

                NotFoundResult notfound = okResult as NotFoundResult;

                Assert.IsType<NotFoundResult>(notfound);
            }

            [Fact]
            public void Delete_ExistingIdPassed_ReturnsNoContent()
            {
                var client = new ClientBuilder().Generate();

                A.CallTo(() => _service.Get(A<Expression<Func<Client, bool>>>.Ignored))
                   .Returns(Task.FromResult<Client>(client));

                var okResult = _controller.Delete(client.Id).Result as ActionResult;

                NoContentResult badResponse = okResult as NoContentResult;

                Assert.IsType<NoContentResult>(badResponse);
            }
        }
    }
}
