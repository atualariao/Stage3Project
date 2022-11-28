using S3E1.Entities;
using System.Text;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Security.Policy;
using S3E1.Data;
using Microsoft.Extensions.DependencyInjection;
using S3E1.Commands;
using Bogus;

namespace IntegrationTest.TestControllers
{
    public class TestCartitemController : IntegrationTestBaseClass
    {
        [Fact]
        public async Task Test_Cartitem_Controller()
        {

            //POST CART ITEM
            // Arrange
            string url = "api/cart-items";
            var item = new CartItemEntity
            {
                ItemID = Guid.NewGuid(),
                ItemName = "Item Name",
                ItemPrice = 55.5,
                ItemStatus = "Pending",
                OrderEntityOrderID = null
            };

            //Act
            var postResponse = await _httpClient.PostAsJsonAsync(url, item);

            //Assert
            postResponse.EnsureSuccessStatusCode();
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var newItem = await postResponse.Content.ReadFromJsonAsync<CartItemEntity>();
            newItem.ItemName.Should().Be(item.ItemName);
            newItem.ItemPrice.Should().Be(item.ItemPrice);
            newItem.ItemStatus.Should().Be(item.ItemStatus);

            //GET ALL ITEMS
            // Arrange
            var itemList = await _httpClient.GetFromJsonAsync<List<CartItemEntity>>(url);

            //Assert
            var newAddedItem = itemList.First(i => i.ItemID == newItem.ItemID);
            newAddedItem.Should().NotBeNull();
            newAddedItem.Should().BeEquivalentTo(newItem);
            newAddedItem.ItemName.Should().Be(newItem.ItemName);
            newAddedItem.ItemPrice.Should().Be(newItem.ItemPrice);
            newAddedItem.ItemStatus.Should().Be(newItem.ItemStatus);

            //GET CART ITEM BY ID
            // Arrange
            var id = newItem.ItemID;
            var urlWithId = url + "/" + id.ToString();

            // Act
            var fetchedItem = await _httpClient.GetFromJsonAsync<CartItemEntity>(urlWithId);

            // Assert
            newAddedItem.Should().BeEquivalentTo(fetchedItem);

            //UPDATE CART ITEM
            // Arrange
            var itemToUpdateRequest = new CartItemEntity
            {
                ItemID = fetchedItem.ItemID,
                ItemName = "Updated Item Name",
                ItemPrice = fetchedItem.ItemPrice,
                ItemStatus = fetchedItem.ItemStatus,
                OrderEntityOrderID = fetchedItem.OrderEntityOrderID,
            };

            // Act
            var itemUpdateResponse = await _httpClient.PutAsJsonAsync(url, itemToUpdateRequest);

            // Assert
            itemUpdateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            itemUpdateResponse.Content.Should().NotBeNull();
            var updatedItem = await itemUpdateResponse.Content.ReadFromJsonAsync<CartItemEntity>();
            updatedItem.Should().NotBeNull();
            updatedItem.ItemID.Should().Be(fetchedItem.ItemID);
            updatedItem.ItemName.Should().NotBe(fetchedItem.ItemName);
            updatedItem.ItemPrice.Should().Be(fetchedItem.ItemPrice);
            updatedItem.ItemStatus.Should().Be(fetchedItem.ItemStatus);
            updatedItem.OrderEntityOrderID.Should().Be(fetchedItem.OrderEntityOrderID);

            //DELETE CART ITEM BY ID
            // Act
            var deleteResponse = await _httpClient.DeleteAsync(urlWithId);

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deletedItem = await deleteResponse.Content.ReadFromJsonAsync<CartItemEntity>();
            deletedItem.Should().BeEquivalentTo(updatedItem);
        }

        [Fact]
        public async Task Test_User_Controller()
        {
            //POST CART ITEM
            // Arrange
            string url = "api/users";
            var user = new UserEntity
            {
                UserID = Guid.NewGuid(),
                Username = "Username"
            };

            //Act
            var postResponse = await _httpClient.PostAsJsonAsync(url, user);

            //Assert
            postResponse.EnsureSuccessStatusCode();
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var newUser = await postResponse.Content.ReadFromJsonAsync<UserEntity>();
            newUser.Username.Should().Be(user.Username);

            //GET CART ITEM BY ID
            // Arrange
            var id = newUser.UserID;
            var urlWithId = url + "/" + id.ToString();

            // Act
            var fetcheduser = await _httpClient.GetFromJsonAsync<UserEntity>(urlWithId);

            // Assert
            newUser.Should().BeEquivalentTo(fetcheduser);

        }

        [Fact]
        public async Task Test_OrdersAndCheckout_Controller()
        {
            //POST ORDER
            // Arrange
            string url = "api/checkout";
            string orderUrl = "api/orders";
            string userUrl = "api/users";
            string itemUrl = "api/cart-items";

            // UserEntity
            var user = new UserEntity
            {
                UserID = Guid.NewGuid(),
                Username = "Username"
            };

            // CartItem
            var cartItem = new CartItemEntity
            {
                ItemID = Guid.NewGuid(),
                ItemName = "Item 1",
                ItemPrice = 5.25,
                OrderEntityOrderID = null
            };

            // Act
            // User
            var getUserResponse = await _httpClient.PostAsJsonAsync(userUrl, user);
            var getUser = await getUserResponse.Content.ReadFromJsonAsync<UserEntity>();
            // CartItems
            var postItemList = await _httpClient.PostAsJsonAsync(itemUrl, cartItem);
            var getItemList = await _httpClient.GetFromJsonAsync<List<CartItemEntity>>(itemUrl);
            var getItem = getItemList.First(item => item.ItemStatus == "Pending");

            // Checkout
            var orderCheckout = new OrderEntity
            {
                OrderID = Guid.NewGuid(),
                UserOrderId = getUser.UserID,
                User = getUser,
                OrderTotalPrice = 5.5,
                OrderCreatedDate = DateTime.Now,
                CartItemEntity = new List<CartItemEntity>
                {
                    getItem
                }
            };

            // Act
            var postResponse = await _httpClient.PostAsJsonAsync(url, orderCheckout);

            // Assert
            postResponse.EnsureSuccessStatusCode();
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCartitemListFromOrderCheckout = await postResponse.Content.ReadFromJsonAsync<OrderEntity>();
            newCartitemListFromOrderCheckout.Should().NotBeNull();

            //GET ALL ORDERS
            // Act
            var orderResponse = await _httpClient.GetFromJsonAsync<List<OrderEntity>>(orderUrl);

            // Assert
            orderResponse.Should().BeOfType<List<OrderEntity>>();

            //GET ORDER BY ID
            // Arrange
            var orderId = newCartitemListFromOrderCheckout.OrderID;
            var urlWithOrderId = orderUrl + "/" + orderId.ToString();

            // Act
            var fetchedOrder = await _httpClient.GetFromJsonAsync<OrderEntity>(urlWithOrderId);

            // Assert
            fetchedOrder.Should().NotBeNull();
            fetchedOrder.Should().BeEquivalentTo(newCartitemListFromOrderCheckout);

            //UPDATE ORDER
            // Arrange
            var updateItemInOrder = new CartItemEntity
            {
                ItemID = getItem.ItemID,
                ItemName = "Updated Item 1",
                ItemPrice = 50.5,
                OrderEntityOrderID = getItem.OrderEntityOrderID
            };
            var postItemListFromOrder = await _httpClient.PutAsJsonAsync(itemUrl, updateItemInOrder);
            var getItemListFromOrder = await postItemListFromOrder.Content.ReadFromJsonAsync<CartItemEntity>();

            var updateOrder = new OrderEntity
            {
                OrderID = newCartitemListFromOrderCheckout.OrderID,
                UserOrderId = newCartitemListFromOrderCheckout.UserOrderId,
                User = newCartitemListFromOrderCheckout.User,
                OrderTotalPrice = newCartitemListFromOrderCheckout.OrderTotalPrice,
                OrderCreatedDate = newCartitemListFromOrderCheckout.OrderCreatedDate,
                CartItemEntity = new List<CartItemEntity>
                {
                    getItemListFromOrder
                }
            };

            // Act
            var orderUpdateResponse = await _httpClient.PutAsJsonAsync(orderUrl, updateOrder);

            // Assert
            orderUpdateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            orderUpdateResponse.Content.Should().NotBeNull();
            var updatedOrder = await orderUpdateResponse.Content.ReadFromJsonAsync<OrderEntity>();
            updatedOrder.Should().NotBeNull();
            updatedOrder.OrderID.Should().Be(fetchedOrder.OrderID);
            updatedOrder.OrderTotalPrice.Should().Be(fetchedOrder.OrderTotalPrice);
            updatedOrder.OrderCreatedDate.Should().Be(fetchedOrder.OrderCreatedDate);
            updatedOrder.UserOrderId.Should().Be(fetchedOrder.UserOrderId);
            updatedOrder.CartItemEntity.Should().NotBeEquivalentTo(fetchedOrder.CartItemEntity);

            //DELETE ORDER
            // Act
            var deleteOrderResponse = await _httpClient.DeleteAsync(urlWithOrderId);

            // Assert
            deleteOrderResponse.EnsureSuccessStatusCode();
            deleteOrderResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deletedItem = await deleteOrderResponse.Content.ReadFromJsonAsync<OrderEntity>();
            deletedItem.Should().BeEquivalentTo(newCartitemListFromOrderCheckout);
        }
    }
}