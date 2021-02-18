using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using Moq;
using SampleDataGenerator;
using Xunit;

namespace ExadelBonusPlus.Services.Tests
{
    public class RefreshTokenServiceTest
    {
        private Mock<ITokenRefreshRepository> _tokenRepoMock;
        private ITokenRefreshRepository _refreshTokenRepositry;
        private TokenRefreshService _tokenService;
        private List<TokenRefresh> _fakeTokens;
        private Random _random;

        public RefreshTokenServiceTest()
        {
            
        }
        [Fact]
        public async Task<TokenRefresh> TokenRefresh_GenerateRefreshToken_Return_TokenRefresh()
        {
            CreateDefaultTokenServiceInstance();
            var token = _fakeTokens[0];
            var newToken = await _tokenService.GenerateRefreshToken(token.CreatedByIp, token.CreatorId);
            Assert.NotNull(newToken);
            return newToken;
        }
        [Fact]
        public async Task<TokenRefresh> TokenRefresh_UpdateRefreshToken_Return_TokenRefresh()
        {
            CreateDefaultTokenServiceInstance();
            var token = _fakeTokens[0];
            var newToken = await _tokenService.UpdateRefreshToken(token.CreatedByIp, token);
            Assert.NotNull(newToken);
            return newToken;
        }

        [Fact]
        public async Task<IEnumerable<TokenRefresh>> String_GetRefreshTokenByIpAddress_Return_IEnumerableTokenRefresh()
        {
            CreateDefaultTokenServiceInstance();
            var token = _fakeTokens[0];
            var newTokens = await _tokenService.GetRefreshTokenByIpAddress(token.CreatedByIp);
            Assert.NotNull(newTokens);
            return newTokens;
        }

        [Fact]
        public async Task<IEnumerable<TokenRefresh>> TokenRefresh_GetRefreshTokenByToken_Return_IEnumerableTokenRefresh()
        {
            CreateDefaultTokenServiceInstance();
            var token = _fakeTokens[0];
            var newTokens = await _tokenService.GetRefreshTokenByToken(token.Token);
            Assert.NotNull(newTokens);
            return newTokens;
        }


        private void CreateDefaultTokenServiceInstance()
        {
            _random = new Random();
            var tokenGenerator = Generator
                    .For<TokenRefresh>()
                    .For(x => x.Id)
                    .ChooseFrom(Guid.NewGuid())
                    .For(x => x.CreatorId)
                    .ChooseFrom(Guid.NewGuid())
                    .For(x => x.Token)
                    .ChooseFrom(RandomString(20))
                    .For(x => x.CreatedDate)
                    .ChooseFrom(DateTime.Now);
            _fakeTokens = tokenGenerator.Generate(10).ToList();

            _tokenRepoMock = new Mock<ITokenRefreshRepository>();
            _tokenRepoMock.Setup(s => s.GetRefreshTokenByIpAddress(RandomString(10), default(CancellationToken))).ReturnsAsync(_fakeTokens);
            _tokenRepoMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_fakeTokens[0]);
            _tokenRepoMock.Setup(s => s.AddAsync(It.IsAny<TokenRefresh>(), default(CancellationToken))); 
            _tokenRepoMock.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TokenRefresh>(), default(CancellationToken)));
            _tokenRepoMock.Setup(s => s.RemoveAsync(It.IsAny<Guid>(),  default(CancellationToken)));
            _tokenRepoMock.Setup(s=>s.GetRefreshTokenByToken(RandomString(10), default(CancellationToken))).ReturnsAsync(_fakeTokens);
            _tokenRepoMock.Setup(s=>s.GetRefreshTokenByUserId(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_fakeTokens);



            _refreshTokenRepositry = _tokenRepoMock.Object;
            _tokenService = new TokenRefreshService(_refreshTokenRepositry);
        }
        public  string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
