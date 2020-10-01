using System.Security.Claims;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TomLonghurst.AzureAD.TokenValidator.V1;
using TomLonghurst.AzureAD.TokenValidator.V2;

namespace TomLonghurst.AzureAD.TokenValidator.UnitTests
{
    [TestFixture]
    public class AADTokenValidatorTests
    {
        private Mock<IAADTokenV1Validator> _aadTokenV1Validator;
        private Mock<IAADTokenV2Validator> _aadTokenV2Validator;
        
        private AADTokenValidator _aadTokenValidator;

        [SetUp]
        public void Setup()
        {
            _aadTokenV1Validator = new Mock<IAADTokenV1Validator>();
            _aadTokenV2Validator = new Mock<IAADTokenV2Validator>();
            
            _aadTokenValidator = new AADTokenValidator(_aadTokenV1Validator.Object, _aadTokenV2Validator.Object);
        }

        [Test]
        public async Task Will_Accept_V1_AAD_Token()
        {
            _aadTokenV1Validator.Setup(x => x.ValidateTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(new ClaimsPrincipal());

            var result = await _aadTokenValidator.ValidateTokenAsync("");
            
            Assert.That(result, Is.Not.Null);
        }
        
        [Test]
        public async Task Will_Accept_V2_AAD_Token()
        {
            _aadTokenV2Validator.Setup(x => x.ValidateTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(new ClaimsPrincipal());

            var result = await _aadTokenValidator.ValidateTokenAsync("");
            
            Assert.That(result, Is.Not.Null);
        }
        
        [Test]
        public async Task Return_Null_If_Neither_V1_Or_V2_AAD_Tokens_Are_Valid()
        {
            _aadTokenV1Validator.Setup(x => x.ValidateTokenAsync(It.IsAny<string>()));
            _aadTokenV2Validator.Setup(x => x.ValidateTokenAsync(It.IsAny<string>()));

            var result = await _aadTokenValidator.ValidateTokenAsync("");
            
            Assert.That(result, Is.Null);
        }
    }
}