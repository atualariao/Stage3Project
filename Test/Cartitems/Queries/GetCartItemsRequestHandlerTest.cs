using AutoMapper;
using Moq;
using S3E1.Contracts;
using S3E1.Handlers;
using S3E1.Profiles;
using S3E1.Queries;
using Shouldly;
using Test.Moq;
using Shouldly;
using S3E1.DTO;

namespace Test.Cartitems.Queries
{
    public class GetCartItemsRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICartItemRepository> _mockRepo;

        public GetCartItemsRequestHandlerTest()
        {
            _mockRepo = MockCartItemRepository.GetCartitems();
        }

        [Fact]
        public async Task GetCartitems()
        {
            var handler = new GetItemsHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetItemsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CartItems>>();

            result.Count.ShouldBe(2);
        }
    }
}
