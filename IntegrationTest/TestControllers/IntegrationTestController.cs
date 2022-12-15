using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using AutoMapper;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Configurations;
using S3E1.Enumerations;
using IntegrationTest.Data;
using IntegrationTest.Handlers;

namespace IntegrationTest.TestControllers
{
    public class TestCartitemController : IntegrationTestBaseClass
    {
        private readonly IMapper _mapper;
        public TestCartitemController()
        {
            MapperConfiguration mapConfig = new(c =>
            {
                c.AddProfile<AutoMapperInitializer>();
            });
            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task Test_Cartitem_Controller()
        {
            _httpClient.DefaultRequestHeaders.Remove(TestAuthHandler.userId);

            //POST CART ITEM
            // Arrange
            string url = "api/v1/cart-items";
            var item = new CartItemDTO
            {
                ItemName = "Item Name",
                ItemPrice = 55.5,
            };

            CartItem itemEntity = _mapper.Map<CartItem>(item);
            //Act
            var postResponse = await _httpClient.PostAsJsonAsync(url, itemEntity);

            //Assert
            postResponse.EnsureSuccessStatusCode();
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var newItem = await postResponse.Content.ReadFromJsonAsync<CartItemDTO>();
            newItem.ItemName.Should().Be(item.ItemName);
            newItem.ItemPrice.Should().Be(item.ItemPrice);

            //GET ALL ITEMS
            // Arrange
            var itemList = await _httpClient.GetFromJsonAsync<List<CartItem>>(url);

            //Assert
            var newAddedItem = itemList.First(i => i.ItemName == item.ItemName);
            newAddedItem.Should().NotBeNull();
            newAddedItem.Should().BeEquivalentTo(newItem);
            newAddedItem.ItemName.Should().Be(newItem.ItemName);
            newAddedItem.ItemPrice.Should().Be(newItem.ItemPrice);

            //GET CART ITEM BY ID
            // Arrange
            var id = newAddedItem.ItemID;
            var urlWithId = url + "/" + id.ToString();

            // Act
            var fetchedItem = await _httpClient.GetFromJsonAsync<CartItem>(urlWithId);

            // Assert
            newAddedItem.Should().BeEquivalentTo(fetchedItem);

            //UPDATE CART ITEM
            // Arrange
            var itemToUpdateRequest = new CartItemDTO
            {
                ItemID = fetchedItem.ItemID,
                ItemName = "Updated Item Name",
                ItemPrice = fetchedItem.ItemPrice
            };

            CartItem itemDTO = _mapper.Map<CartItem>(itemToUpdateRequest);
            // Act
            var itemUpdateResponse = await _httpClient.PutAsJsonAsync(url, itemDTO);

            // Assert
            itemUpdateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            itemUpdateResponse.Content.Should().NotBeNull();
            var updatedItem = await itemUpdateResponse.Content.ReadFromJsonAsync<CartItem>();
            updatedItem.Should().NotBeNull();
            updatedItem.ItemID.Should().Be(fetchedItem.ItemID);
            updatedItem.ItemName.Should().NotBe(fetchedItem.ItemName);
            updatedItem.ItemPrice.Should().Be(fetchedItem.ItemPrice);
            updatedItem.OrderStatus.Should().Be(fetchedItem.OrderStatus);
            updatedItem.OrderPrimaryID.Should().Be(fetchedItem.OrderPrimaryID);

            //DELETE CART ITEM BY ID
            // Act
            var deleteResponse = await _httpClient.DeleteAsync(urlWithId);

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deletedItem = await deleteResponse.Content.ReadFromJsonAsync<CartItem>();
            deletedItem.Should().BeEquivalentTo(updatedItem);
        }

        [Fact]
        public async Task Test_User_Controller()
        {
            //POST CART ITEM
            // Arrange
            string url = "api/v1/users";
            var user = new User
            {
                Username = "Username"
            };

            //Act
            var postResponse = await _httpClient.PostAsJsonAsync(url, user);

            //Assert
            postResponse.EnsureSuccessStatusCode();
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var newUser = await postResponse.Content.ReadFromJsonAsync<User>();
            newUser.Username.Should().Be(user.Username);

            //GET CART ITEM BY ID
            // Arrange
            var id = newUser.UserID;
            var urlWithId = url + "/" + id.ToString();

            // Act
            var fetcheduser = await _httpClient.GetFromJsonAsync<User>(urlWithId);

            // Assert
            newUser.Should().BeEquivalentTo(fetcheduser);

        }

        [Fact]
        public async Task Test_OrdersAndCheckout_Controller()
        {
            //POST ORDER
            // Arrange
            string url = "api/v1/checkout";
            string orderUrl = "api/v1/orders";
            string userUrl = "api/v1/users";
            string itemUrl = "api/v1/cart-items";

            // User
            var user = new User
            {
                Username = "Username"
            };

            // CartItem
            var cartItem = new CartItem
            {
                ItemName = "Item 1",
                ItemPrice = 5.25,
                OrderPrimaryID = null
            };

            // Act
            // User
            var getUserResponse = await _httpClient.PostAsJsonAsync(userUrl, user);
            var getUser = await getUserResponse.Content.ReadFromJsonAsync<User>();
            // CartItems
            var postItemList = await _httpClient.PostAsJsonAsync(itemUrl, cartItem);
            var getItemList = await _httpClient.GetFromJsonAsync<List<CartItem>>(itemUrl);
            var getItem = getItemList.First(item => item.OrderStatus == OrderStatus.Pending);

            // Checkout
            var orderCheckout = new Order
            {
                UserPrimaryID = getUser.UserID,
                User = getUser,
                OrderTotalPrice = 5.5,
                CartItemEntity = new List<CartItem>
                {
                    getItem
                }
            };

            // Act
            var postResponse = await _httpClient.PostAsJsonAsync(url, orderCheckout);

            // Assert
            postResponse.EnsureSuccessStatusCode();
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCartitemListFromOrderCheckout = await postResponse.Content.ReadFromJsonAsync<Order>();
            newCartitemListFromOrderCheckout.Should().NotBeNull();

            //GET ALL ORDERS
            // Act
            var orderResponse = await _httpClient.GetFromJsonAsync<List<Order>>(orderUrl);

            // Assert
            orderResponse.Should().BeOfType<List<Order>>();

            //GET ORDER BY ID
            // Arrange
            var PrimaryID = newCartitemListFromOrderCheckout.PrimaryID;
            var urlWithPrimaryID = orderUrl + "/" + PrimaryID.ToString();

            // Act
            var fetchedOrder = await _httpClient.GetFromJsonAsync<Order>(urlWithPrimaryID);

            // Assert
            fetchedOrder.Should().NotBeNull();
            fetchedOrder.Should().BeEquivalentTo(newCartitemListFromOrderCheckout);

            //UPDATE ORDER
            // Arrange
            var updateItemInOrder = new CartItem
            {
                ItemID = getItem.ItemID,
                ItemName = "Updated Item 1",
                ItemPrice = 50.5,
                OrderPrimaryID = getItem.OrderPrimaryID
            };
            var postItemListFromOrder = await _httpClient.PutAsJsonAsync(itemUrl, updateItemInOrder);
            var getItemListFromOrder = await postItemListFromOrder.Content.ReadFromJsonAsync<CartItem>();

            var updateOrder = new Order
            {
                PrimaryID = orderCheckout.PrimaryID,
                UserPrimaryID = orderCheckout.UserPrimaryID,
                User = orderCheckout.User,
                OrderTotalPrice = orderCheckout.OrderTotalPrice,
                OrderCreatedDate = orderCheckout.OrderCreatedDate,
                CartItemEntity = new List<CartItem> { updateItemInOrder }
            };

            // Act
            var orderUpdateResponse = await _httpClient.PutAsJsonAsync(orderUrl, updateOrder);

            // Assert
            orderUpdateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            orderUpdateResponse.Content.Should().NotBeNull();
            var updatedOrder = await orderUpdateResponse.Content.ReadFromJsonAsync<Order>();
            updatedOrder.Should().NotBeNull();
            updatedOrder.PrimaryID.Should().Be(fetchedOrder.PrimaryID);
            updatedOrder.OrderTotalPrice.Should().NotBe(fetchedOrder.OrderTotalPrice);
            updatedOrder.OrderCreatedDate.Should().Be(fetchedOrder.OrderCreatedDate);
            updatedOrder.CartItemEntity.Should().NotBeEquivalentTo(fetchedOrder.CartItemEntity);

            //DELETE ORDER
            // Act
            var deleteOrderResponse = await _httpClient.DeleteAsync(urlWithPrimaryID);

            // Assert
            deleteOrderResponse.EnsureSuccessStatusCode();
            deleteOrderResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var deletedItem = await deleteOrderResponse.Content.ReadFromJsonAsync<Order>();
            deletedItem.Should().BeEquivalentTo(updatedOrder);
        }
    }
}