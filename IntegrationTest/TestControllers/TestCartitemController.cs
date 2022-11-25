using Microsoft.AspNetCore.Mvc.Testing;
using S3E1.Commands;
using FluentAssertions;
using S3E1.Entities;
using System.Net;
using System.Net.Http.Json;
using S3E1.Data;

namespace IntegrationTest.TestControllers
{
    public class TestCartitemController : IntegrationTestBaseClass
    { 
        [Fact]
        public async Task Test_CartItemController()
        {
            // Arrange
            string url = "/api/cart-items";
            var input = new CartItemEntity
            {
                ItemID = Guid.NewGuid(),
                ItemName = "Item 1",
                ItemPrice = 5.5,
                ItemStatus = "Pending",
                OrderEntityOrderID = null
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync(url, input);

            //Assert
            response.Content.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newItem = await response.Content.ReadFromJsonAsync<CartItemEntity>();
            newItem.Should().NotBeNull();
            newItem.Should().BeOfType<CartItemEntity>();

            //-----GET ALL CART ITEMS-----
            // Act
            var items = await _httpClient.GetFromJsonAsync<List<CartItemEntity>>(url);

            //Assert
            var recentItem = items.FirstOrDefault(item => item.ItemID == newItem.ItemID);
            recentItem.Should().BeEquivalentTo(newItem);
            items.Should().HaveCount(1);

            //-----GET CART ITEM BY ID-----
            // Act
            var id = newItem.ItemID;
            var urlWithId = url + "/" + id.ToString();

            //Act
            var fetchedItem = await _httpClient.GetFromJsonAsync<CartItemEntity>(urlWithId);

            //Assert
            fetchedItem.Should().NotBeNull();
            newItem.Should().BeEquivalentTo(fetchedItem);


        }
    }
}